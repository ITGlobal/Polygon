using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using IBApi;
using ITGlobal.DeadlockDetection;
using IBOrder = IBApi.Order;
using OrderState = Polygon.Messages.OrderState;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Адаптер IB
    /// </summary>
    internal sealed partial class IBAdapter : EWrapperBase, 
        IInstrumentHistoryProvider, 
        ISubscriptionTester<IBInstrumentData>, 
        IInstrumentConverterContext<IBInstrumentData>
    {
        public const int TwsDefaultPort = 7496;
        public const int GatewayDefaultPort = 4001;

        internal static readonly ILog Log = LogManager.GetLogger<IBAdapter>();

        private readonly IBConnector connector;
        private readonly InstrumentConverter<IBInstrumentData> instrumentConverter;

        private readonly AutoResetEvent errorEvent = new AutoResetEvent(false);
        private readonly ManualResetEventSlim connectedEvent = new ManualResetEventSlim(false);
        private readonly ManualResetEventSlim disconnectedEvent = new ManualResetEventSlim(true);

        private readonly TickerContainer contractDetailsTickers = new TickerContainer();
        private readonly PendingResultTickerContainer marketDataTickers = new PendingResultTickerContainer();
        private readonly TickerContainer marketDepthTickers = new TickerContainer();

        private readonly PendingTestResultTickerContainer pendingTestResultTickerContainer = new PendingTestResultTickerContainer();

        private readonly ContractDetailsContainer contractDetailsContainer = new ContractDetailsContainer();

        private readonly ThreadSafeContainer<string, MoneyPosition> moneyPositions = new ThreadSafeContainer<string, MoneyPosition>();
        private readonly ThreadSafeContainer<Instrument, OrderBookBuilder> mktDepthBuilders = new ThreadSafeContainer<Instrument, OrderBookBuilder>(
            i => new OrderBookBuilder(i));

        private readonly ILockObject orderInfoContainerLock = DeadlockMonitor.Cookie<IBAdapter>("orderInfoContainerLock");
        private readonly OrderInfoContainer orderInfoContainer = new OrderInfoContainer();

        private int nextValidIdentifyer;

        public IBAdapter(IBConnector connector, InstrumentConverter<IBInstrumentData> instrumentConverter)
        {
            this.connector = connector;
            this.instrumentConverter = instrumentConverter;
            Socket = new LoggingEClientSocketFacade(new EClientSocket(this));
        }

        internal LoggingEClientSocketFacade Socket { get; }

        /// <summary>
        ///     Установить соединение с IB
        /// </summary>
        /// <param name="host">
        ///     Хост
        /// </param>
        /// <param name="port">
        ///     Порт
        /// </param>
        /// <param name="clientId">
        ///     Уникальный идентификатор программы-клиента
        /// </param>
        /// <returns>
        ///     true, если соединение было установлено, false - в противном случае
        /// </returns>
        public bool Connect(string host, int port, int clientId)
        {
            connector.RaiseConnectionStatusChanged(ConnectionStatus.Connecting);

            try
            {
                Socket.eConnect(host, port, clientId);

                const int connectTimeout = 15 * 1000;  // 15 sec
                var i = WaitHandle.WaitAny(new[] { errorEvent, connectedEvent.WaitHandle }, connectTimeout);

                switch (i)
                {
                    case WaitHandle.WaitTimeout:
                        // Вылетели по таймауту
                        throw new Exception("Timeout expired");

                    case 0:
                        // Соединение не было установлено, пришло событие error()
                        connector.RaiseConnectionStatusChanged(ConnectionStatus.Disconnected);
                        return false;

                    case 1:
                        // Соединение установлено

                        // Активируем нужный тип рыночных данных
                        //   1 означает реалтайм-поток данных
                        //   2 означает снапшот
                        const int marketDataType = 1; // 1 - реалтайм поток, 2 - снапшот
                        Socket.reqMarketDataType(marketDataType);

                        // Запрашиваем список счетов
                        const string accountCode = "";
                        Socket.reqAccountUpdates(true, accountCode);

                        // Запрашиваем позиции
                        Socket.reqPositions();

                        // Запрашиваем заявки
                        // NOTE используем reqAutoOpenOrders() вместо reqAllOpenOrders(), чтобы 
                        // приходили заявки, выставленные из терминала
                        Socket.reqAutoOpenOrders(true);

                        // Запрашиваем текущее время
                        Socket.reqCurrentTime();

                        connector.RaiseConnectionStatusChanged(ConnectionStatus.Connected);
                        return true;

                    default:
                        throw new InvalidOperationException($"Expected 0 or 1, but got {i}");
                }
            }
            catch (Exception exception)
            {
                Log.Error().Print(exception, "IB: Unable to connect");
                connector.RaiseConnectionStatusChanged(ConnectionStatus.Disconnected);
                return false;
            }
        }

        /// <summary>
        ///     Разорвать соединение
        /// </summary>
        public void Disconnect()
        {
            Socket.Close();
            disconnectedEvent.Wait();
            connector.RaiseConnectionStatusChanged(ConnectionStatus.Disconnected);
        }

        /// <summary>
        ///     Запросить детали контракта
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="contract">
        ///     Контракт-заглушка
        /// </param>
        /// <returns>
        ///     Номер тикера
        /// </returns>
        public int RequestContractDetails(Instrument instrument, Contract contract)
        {
            var id = NextTickerId();
            contractDetailsTickers.Store(id, instrument);
            contractDetailsContainer.Store(id, contract);

            Socket.reqContractDetails(id, contract);

            return id;
        }

        #region History data

        internal IBHistoryDataLimits HistoryDataLimits { get; } = new IBHistoryDataLimits();
        internal HistoryTickerContainer HistoricalDepthTickers { get; } = new HistoryTickerContainer();

        /// <summary>
        ///     Определить оптимальную длину блока для запроса исторических данных
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="span">
        ///     Интервал свечей для исторических данных
        /// </param>
        /// <returns>
        ///     Оптимальная длина блока либо null
        /// </returns>
        public TimeSpan? GetBestFetchBlockLength(Instrument instrument, HistoryProviderSpan span)
        {
            var size = IBHistoryDataLimits.GetFetchBlockSize(span);
            return size;
        }

        /// <summary>
        ///     Получить исторические данные
        /// </summary>
        /// <param name="consumer">
        ///     Потребитель исторических данных
        /// </param>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="begin">
        ///     Начало диапазона
        /// </param>
        /// <param name="end">
        ///     Конец диапазона
        /// </param>
        /// <param name="span">
        ///     Интервал свечей для исторических данных
        /// </param>
        /// <param name="cancellationToken">
        ///     Токен отмены
        /// </param>
        /// <returns>
        ///     Исторические данные
        /// </returns>
        /// <remarks>
        ///     Провайдер вправе переопределить параметры исторических графиков - диапазон, интервал,
        ///     если он не в состоянии предоставить запрошенные данные.
        /// </remarks>
        /// <exception cref="NoHistoryDataException">
        ///     Бросается, если исторические данные за указанный период недоступны
        /// </exception>
        public async Task GetHistoryDataAsync(
            IHistoryDataConsumer consumer,
            Instrument instrument,
            DateTime begin,
            DateTime end,
            HistoryProviderSpan span,
            CancellationToken cancellationToken = new CancellationToken())
        {
            using (LogManager.Scope())
            {
                // Получаем контракт по инструменту
                var contract = await connector.ContractContainer.GetContractAsync(instrument, cancellationToken: cancellationToken);
                if (contract == null)
                {
                    throw new InvalidOperationException($"Can't find instrument \"{instrument}\"");
                }

                // Загружаем блок исторических данных
                var points = await FetchHistoryDataBlock(consumer, contract, begin, end, span, cancellationToken);

                // Собираем объект HistoryData
                var minDate = DateTime.MaxValue;
                var maxDate = DateTime.MinValue;
                var data = new HistoryData(instrument, begin, end, span);
                foreach (var point in points.OrderBy(_ => _.Point))
                {
                    data.Points.Add(point);

                    if (minDate > point.Point)
                    {
                        minDate = point.Point;
                    }

                    if (maxDate < point.Point)
                    {
                        maxDate = point.Point;
                    }
                }

                data.Begin = minDate;
                data.End = maxDate;

                consumer.Update(data, HistoryDataUpdateType.Batch);
            }
        }

        /// <summary>
        ///     Подписаться на исторические данные
        /// </summary>
        /// <param name="consumer">
        ///     Потребитель исторических данных
        /// </param>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <param name="begin">
        ///     Начало диапазона
        /// </param>
        /// <param name="span">
        ///     Интервал свечей для исторических данных
        /// </param>
        /// <returns>
        ///     Подписка на исторические данные
        /// </returns>
        /// <remarks>
        ///     Провайдер вправе переопределить параметры исторических графиков - диапазон, интервал,
        ///     если он не в состоянии предоставить запрошенные данные.
        /// </remarks>
        public async Task<IHistoryDataSubscription> SubscribeToHistoryDataAsync(
            IHistoryDataConsumer consumer,
            Instrument instrument,
            DateTime begin,
            HistoryProviderSpan span)
        {
            using (LogManager.Scope())
            {
                // Получаем контракт по инструменту
                var contract = await connector.ContractContainer.GetContractAsync(instrument);
                if (contract == null)
                {
                    throw new InvalidOperationException($"Can't find instrument \"{instrument}\"");
                }

                // Создаем подписку
                var subscription = new IBHistoryDataSubscription(this, consumer, instrument, contract, span);
                subscription.StartFetch(begin);
                return subscription;
            }
        }

        internal async Task<IList<HistoryDataPoint>> FetchHistoryDataBlock(
            IHistoryDataConsumer consumer,
            Contract contract,
            DateTime begin,
            DateTime end,
            HistoryProviderSpan span,
            CancellationToken cancellationToken = new CancellationToken())
        {
            using (LogManager.Scope())
            {
                // Формируем параметры запроса
                ApplyHistoryDataLimits(span, ref begin, ref end);

                // Ждем, чтобы не превысить лимиты по частоте запросов
                var request = new IBHistoricalDataRequest(
                    this,
                    consumer,
                    contract: contract,
                    begin: begin,
                    end: end,
                    span: span,
                    whatToShow: "TRADES",
                    useRth: 1,
                    formatDate: 1
                    );

                // Отправляем запрос
                var points = await request.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                return points;
            }
        }
        private static void ApplyHistoryDataLimits(HistoryProviderSpan span, ref DateTime begin, ref DateTime end)
        {
            // SEE https://www.interactivebrokers.com/en/software/api/apiguide/tables/historical_data_limitations.htm
            // В IB есть ограничения по историческим данным. Если их превысить, то данные не придут.
            // Применяем эти ограничения, чтобы не было ошибок

            TimeSpan? min, max;
            IBHistoryDataLimits.GetHistoryDataLimits(span, out min, out max);
            ClampHistoryDataDuration(ref begin, ref end, min, max);
        }

        private static void ClampHistoryDataDuration(ref DateTime begin, ref DateTime end, TimeSpan? min, TimeSpan? max)
        {
            var duration = end - begin;

            if (min != null && duration < min)
            {
                end = begin + min.Value;
            }

            if (max != null && duration > max)
            {
                end = begin + max.Value;
            }
        }

        #endregion

        /// <summary>
        ///     Подписаться на рыночные данные
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        public async Task<SubscriptionResult> Subscribe(Instrument instrument)
        {
            using (LogManager.Scope())
            {
                // Получаем контракт по инструменту
                var contract = await connector.ContractContainer.GetContractAsync(instrument);

                if (contract == null)
                {
                    return new SubscriptionResult(instrument, false, $"Can't subscribe to \"{instrument}\"");
                }

                var tickerId = NextTickerId();

                var pendingTestResult = new PendingTestResult();
                // Сохраняем тикер
                marketDataTickers.Store(tickerId, instrument, pendingTestResult);

                // Подписываемся на рыночные данные
                const string genericTickList = "165"; // Miscellaneous Stats

                Socket.reqMktData(tickerId, contract, genericTickList, snapshot: false);

                var result = await pendingTestResult.WaitAsync();

                return new SubscriptionResult(instrument, result);
            }
        }

        /// <summary>
        ///     Отписаться от рыночных данных
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        public void Unsubscribe(Instrument instrument)
        {
            try
            {
                // Получаем тикер по инструменту
                int tickerId;
                if (!marketDataTickers.TryGetTickerId(instrument, out tickerId))
                {
                    return;
                }

                // Отменяем подписку
                Socket.cancelMktData(tickerId);

                // Удаляем тикер
                marketDataTickers.RemoveTickerId(tickerId);

            }
            catch (IBNoConnectionException e)
            {
                Log.Error().Print(e, $"Failed to unsubscribe from parameters on {instrument}");
            }
        }

        /// <summary>
        ///     Подписаться на стакан
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        public async void SubscribeOrderBook(Instrument instrument)
        {
            using (LogManager.Scope())
            {
                try
                {
                    // Получаем контракт по инструменту
                    var contract = await connector.ContractContainer.GetContractAsync(instrument);

                    if (contract == null)
                    {
                        return;
                    }

                    var depth = connector.IBFeed.MarketDepth;
                    var tickerId = NextTickerId();

                    // Сохраняем тикер
                    marketDepthTickers.Store(tickerId, instrument);

                    // Инициализируем построитель стакана
                    mktDepthBuilders.Get(instrument).MarketDepth = depth;

                    // Подписываемся на рыночные данные
                    Socket.reqMarketDepth(tickerId, contract, depth);

                    // Выброс псевдостакана (из лучших бида и аска)
                    var instrumentParams = connector.InstrumentParamsCache.GetInstrumentParams(instrument);
                    if (instrumentParams != null && instrumentParams.BestBidQuantity != 0 && instrumentParams.BestOfferQuantity != 0)
                    {
                        var orderBook = new OrderBook(2) { Instrument = instrument };

                        orderBook.Items.Add(new OrderBookItem
                        {
                            Operation = OrderOperation.Sell,
                            Price = instrumentParams.BestOfferPrice,
                            Quantity = instrumentParams.BestOfferQuantity
                        });

                        orderBook.Items.Add(new OrderBookItem
                        {
                            Operation = OrderOperation.Buy,
                            Price = instrumentParams.BestBidPrice,
                            Quantity = instrumentParams.BestBidQuantity
                        });

                        connector.IBFeed.Transmit(orderBook);
                    }
                }
                catch (IBNoConnectionException e)
                {
                    Log.Error().Print(e, $"Failed to subscribe to order book on {instrument}");
                }
            }
        }

        /// <summary>
        ///     Отписаться от стакана
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        public void UnsubscribeOrderBook(Instrument instrument)
        {
            try
            {
                // Получаем тикер по инструменту
                int tickerId;
                if (!marketDepthTickers.TryGetTickerId(instrument, out tickerId))
                {
                    return;
                }

                // Отменяем подписку
                Log.Debug().PrintFormat("cancelMktDepth {0}", tickerId);
                Socket.cancelMktDepth(tickerId);

                // Удаляем тикер
                marketDepthTickers.RemoveTickerId(tickerId);
            }
            catch (IBNoConnectionException e)
            {
                Log.Error().Print(e, $"Failed to unsubscribe from order book on {instrument}");
            }
        }

        /// <summary>
        ///     Выставить заявку
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция
        /// </param>
        /// <param name="order">
        ///     IB-заявка
        /// </param>
        public async Task PlaceOrderAsync(NewOrderTransaction transaction, IBOrder order)
        {
            using (LogManager.Scope())
            {
                var instrument = transaction.Instrument;

                var contract = await connector.ContractContainer.GetContractAsync(instrument, null);
                if (contract == null)
                {
                    throw new TransactionRejectedException($"Details of contract {instrument.Code} are not available");
                }

                using (orderInfoContainerLock.Lock())
                {
                    var tickerId = NextTickerId();
                    order.OrderId = tickerId;

                    // Сохраняем заявку по ее тикеру
                    var orderInfo = new OrderInfo(order, transaction.Instrument)
                    {
                        State = OrderState.New,
                        NewOrderTransactionId = transaction.TransactionId
                    };
                    orderInfoContainer.StoreByTickerId(tickerId, orderInfo, true);

                    Socket.placeOrder(tickerId, contract, order);
                }
            }
        }

        /// <summary>
        ///     Снять заявку
        /// </summary>
        /// <param name="permId">
        ///     Номер заявки
        /// </param>
        /// <param name="transaction">
        ///     Транзакция на снятие заявки
        /// </param>
        public void KillOrder(int permId, KillOrderTransaction transaction)
        {
            using (orderInfoContainerLock.Lock())
            {
                OrderInfo orderInfo;
                if (!orderInfoContainer.TryGetByPermId(permId, out orderInfo))
                {
                    throw new InvalidOperationException($"Unable to find order #{permId}");
                }

                orderInfo.KillOrderTransactionId = transaction.TransactionId;
                var orderId = orderInfo.OrderId;

                Socket.cancelOrder(orderId);
            }
        }

        /// <summary>
        ///     Проверить контракт
        /// </summary>
        /// <param name="contract">
        ///     Контракт
        /// </param>
        /// <returns>
        ///     Ожидающая операция
        /// </returns>
        public PendingTestResult TestContract(Contract contract)
        {
            var testResult = new PendingTestResult();
            var id = NextTickerId();
            pendingTestResultTickerContainer.Store(id, testResult);
            contractDetailsContainer.Store(id, contract);

            if (!Socket.reqContractDetails(id, contract))
            {
                testResult.Reject();
                pendingTestResultTickerContainer.RemoveTickerId(id);
                contractDetailsContainer.RemoveTickerId(id);
            }

            return testResult;
        }

        private static DateTime ParseExecutionTime(string executionTime)
        {
            // executionTime имеет ублюдский формат "YYYYMMdd  HH:mm:ss" (с ДВУМЯ пробелами в середине!)
            // Почему каждый уебан, разрабатывающий API, обязательно должен при этом разработать
            // формат для даты/времени, не совместимый вообще ни с чем?!

            var datetime = DateTime.ParseExact(executionTime, "yyyyMMdd  HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            return datetime;
        }

        internal int NextTickerId()
        {
            return Interlocked.Increment(ref nextValidIdentifyer);
        }

        #region IInstrumentConverterContext

        ISubscriptionTester<IBInstrumentData> IInstrumentConverterContext<IBInstrumentData>.SubscriptionTester => this;

        #endregion

        #region ISubscriptionTester implementation

        /// <summary>
        ///     Проверить подписку 
        /// </summary>
        async Task<SubscriptionTestResult> ISubscriptionTester<IBInstrumentData>.TestSubscriptionAsync(IBInstrumentData data)
        {
            if (data.Symbol == null)
            {
                return SubscriptionTestResult.Failed("No symbol has been specified");
            }
            
            // Мы ожидаем, что внешний конвертер сформирует код в формате LOCALSYMBOL

            Contract contract;
            switch (data.InstrumentType)
            {
                case IBInstrumentType.Equity:
                case IBInstrumentType.Index:
                case IBInstrumentType.Commodity:
                case IBInstrumentType.FX:
                    contract = ContractContainer.GetAssetContractStub(data);
                    break;
                case IBInstrumentType.Future:
                    contract = ContractContainer.GetFutureContractStub(data);
                    break;
                case IBInstrumentType.AssetOption:
                    contract = ContractContainer.GetAssetOptionContractStub(data);
                    break;
                case IBInstrumentType.FutureOption:
                    contract = ContractContainer.GetFutureOptionContractStub(data);
                    break;
                default:
                    return SubscriptionTestResult.Failed($"Bad instrument type: {data.InstrumentType}");
            }

            var testResult = TestContract(contract);
            var result = await testResult.WaitAsync();
            if (result)
            {
                return SubscriptionTestResult.Passed();
            }

            return SubscriptionTestResult.Failed();
        }
        
        #endregion
    }
}
