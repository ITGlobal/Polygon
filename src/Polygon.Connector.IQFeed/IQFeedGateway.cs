using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector.IQFeed.History;
using Polygon.Connector.IQFeed.Level1;
using Polygon.Connector.IQFeed.Lookup;
using Polygon.Messages;

namespace Polygon.Connector.IQFeed
{
    /// <summary>
    ///     Фид для IQFeed
    /// </summary>
    internal sealed class IQFeedGateway : Feed,
        IInstrumentParamsSubscriber,
        IOrderBookSubscriber,
        IConnectionStatusProvider,
        IInstrumentHistoryProvider,
        ISubscriptionTester<IQFeedInstrumentData>,
        IInstrumentConverterContext<IQFeedInstrumentData>
    {
        #region fields

        private const double UpdateIntervalMs = 100; /* 100ms */

        private static readonly string[] _FieldsToUpdate =
        {
            L1UpdateMsg.FIELD_SYMBOL,
            L1UpdateMsg.FIELD_BID,
            L1UpdateMsg.FIELD_ASK,
            L1UpdateMsg.FIELD_BID_SIZE,
            L1UpdateMsg.FIELD_ASK_SIZE,
            L1UpdateMsg.FIELD_LAST,
            L1UpdateMsg.FIELD_SETTLE,
            L1UpdateMsg.FIELD_EXTENDED_TRADE,
            L1UpdateMsg.FIELD_EXTENDED_TRADING_CHANGE,
            //"Volatility",
        };

        private readonly Level1SocketWrapper socketL1;
        private readonly HistorySocketWrapper historySocket;
        private readonly LookupSocketWrapper lookupSocket;

        private readonly L1FieldIndex fieldIndex = new L1FieldIndex();

        private readonly LookupSecurityTypeIndex securityTypeIndex = new LookupSecurityTypeIndex();
        private readonly TaskCompletionSource<bool> securityTypeIndexCompleted = new TaskCompletionSource<bool>();

        /// <summary>
        ///     Конвертер инструментов IQFeed
        /// </summary>
        private readonly InstrumentConverter<IQFeedInstrumentData> instrumentConverter;

        #endregion

        #region .ctor

        /// <summary>
        ///     Конструктор
        /// </summary>
        internal IQFeedGateway(IQFeedParameters parameters)
        {
            var ip = IQFeedParser.ParseIpAddressOrDns(parameters.IQConnectAddress, AddressFamily.InterNetwork);
            socketL1 = new Level1SocketWrapper(ip, parameters);

            socketL1.OnFundamentalMsg += L1OnFundamentalMsg;
            socketL1.OnSummaryMsg += L1OnUpdateMsg;
            socketL1.OnUpdateMsg += L1OnUpdateMsg;
            socketL1.OnSystemMsg += L1OnSystemMsg;
            //socketL1.OnTimestampMsg += socketLevelOne_OnTimestampMsg;
            //socketL1.OnRegionalMsg += LevelOne_OnRegionalMsg;
            //socketL1.OnNewsMsg += LevelOne_OnNewsMsg;
            socketL1.OnErrorMsg += L1OnErrorMsg;
            //socketL1.OnOtherMsg += LevelOne_OnOtherMsg;
            socketL1.OnSubscriptionErrorMsg += L1OnSubscriptionErrorMsg;

            historySocket = new HistorySocketWrapper(ip, parameters);
            historySocket.OnHistoryMsg += HistoryOnHistoryMsg;
            historySocket.OnHistoryEndMsg += HistoryOnHistoryEndMsg;
            historySocket.OnErrorMsg += HistoryOnErrorMsg;

            lookupSocket = new LookupSocketWrapper(ip, parameters);
            lookupSocket.OnSecurityTypeMsg += LookupOnSecurityTypeMsg;
            lookupSocket.OnResultMsg += LookupOnResultMsg;
            lookupSocket.OnErrorMsg += LookupOnErrorMsg;

            instrumentConverter = parameters.InstrumentConverter;
        }

        #endregion

        #region start/stop methods

        /// <summary>
        ///     Запускает сервис.
        /// </summary>
        public override void Start()
        {
            ChangeConnectionStatus(ConnectionStatus.Connecting);

            if (socketL1.Connect() && historySocket.Connect() && lookupSocket.Connect())
            {
                socketL1.SelectUpdateFields(_FieldsToUpdate);
                return;
            }

            ChangeConnectionStatus(ConnectionStatus.Disconnected);
        }

        /// <summary>
        ///     Останавливает сервис.
        /// </summary>
        public override void Stop()
        {
            socketL1.Disconnect();
            historySocket.Disconnect();
            lookupSocket.Disconnect();

            ChangeConnectionStatus(ConnectionStatus.Disconnected);
        }

        #endregion

        #region Implementation of IInstrumentsSubscriber

        private sealed class InstrumentSubscription : TaskCompletionSource<SubscriptionResult>
        {
            private readonly Action<InstrumentSubscription> onTimeout;

            private readonly ILockObject syncRoot = DeadlockMonitor.Cookie<InstrumentSubscription>();

            private DateTime lastUpdateTime = DateTime.MinValue;

            [SuppressMessage("ReSharper", "InconsistentNaming")]
            private const int UNKNOWN = 0;
            [SuppressMessage("ReSharper", "InconsistentNaming")]
            private const int SUBSCRIBED = 1;
            [SuppressMessage("ReSharper", "InconsistentNaming")]
            private const int NOT_SUBSCRIBED = -1;

            private const int SubscriptionTimeout = 20 * 1000;

            private int isConfirmed;

            public InstrumentSubscription(Instrument instrument, IQFeedInstrumentData data, Action<InstrumentSubscription> onTimeout)
            {
                Instrument = instrument;
                InstrumentParams = new InstrumentParams { Instrument = instrument };
                Code = data.Symbol;
                SecurityType = data.SecurityType;
                this.onTimeout = onTimeout;
            }

            public Instrument Instrument { get; }
            public InstrumentParams InstrumentParams { get; }
            public string Code { get; }
            public SecurityType SecurityType { get; }

            public void BeginTimeout()
            {
                System.Threading.Tasks.Task.Delay(SubscriptionTimeout).ContinueWith(_ =>
                {
                    if (Terminate())
                    {
                        onTimeout(this);
                    }
                });
            }

            public bool Update(ref L1FundamentalMsg msg)
            {
                Confirm();

                using (syncRoot.Lock())
                {
                    InstrumentParams.DecimalPlaces = msg.DecimalPlaces;
                    InstrumentParams.PriceStep = msg.PriceStep;
                    InstrumentParams.PriceStepValue = msg.PriceStepValue;

                    return true;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="msg"></param>
            /// <returns></returns>
            public bool Update(ref L1UpdateMsg msg)
            {
                Confirm();

                using (syncRoot.Lock())
                {
                    InstrumentParams.BestBidPrice = msg.BestBidPrice;
                    InstrumentParams.BestBidQuantity = msg.BestBidQuantity;
                    InstrumentParams.BestOfferPrice = msg.BestOfferPrice;
                    InstrumentParams.BestOfferQuantity = msg.BestOfferQuantity;
                    InstrumentParams.LastPrice = msg.LastPrice;
                    InstrumentParams.Settlement = msg.Settlement;
                    InstrumentParams.PreviousSettlement = msg.PreviousSettlement;

                    var date = DateTime.UtcNow;
                    if ((date - lastUpdateTime).TotalMilliseconds >= UpdateIntervalMs)
                    {
                        lastUpdateTime = date;
                        return true;
                    }

                    return false;
                }
            }

            public bool Terminate()
            {
                if (Interlocked.CompareExchange(ref isConfirmed, NOT_SUBSCRIBED, UNKNOWN) == UNKNOWN)
                {
                    TrySetCanceled();
                    return true;
                }

                return false;
            }

            private void Confirm()
            {
                if (Interlocked.CompareExchange(ref isConfirmed, SUBSCRIBED, UNKNOWN) == UNKNOWN)
                {
                    TrySetResult(SubscriptionResult.OK(Instrument));
                }
            }
        }

        private readonly ILockObject instrumentSubscriptionsLock = DeadlockMonitor.Cookie<IQFeedGateway>("instrumentSubscriptionsLock");
        private readonly Dictionary<Instrument, InstrumentSubscription> instrumentSubscriptionsByInstrument = new Dictionary<Instrument, InstrumentSubscription>();
        private readonly Dictionary<string, InstrumentSubscription> instrumentSubscriptionsByCode = new Dictionary<string, InstrumentSubscription>();

        /// <summary>
        ///     Подписаться на инструмент.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public async Task<SubscriptionResult> Subscribe(Instrument instrument)
        {
            var data = await instrumentConverter.ResolveInstrumentAsync(this, instrument);
            if (data == null)
            {
                return SubscriptionResult.Error(instrument, $"Unable to resolve symbol for {instrument}");
            }

            // Забираем подписку из коллекции или создаем новую
            InstrumentSubscription subscription;
            bool isNewSubscription;
            using (instrumentSubscriptionsLock.Lock())
            {
                if (instrumentSubscriptionsByInstrument.TryGetValue(instrument, out subscription))
                {
                    isNewSubscription = false;
                }
                else
                {
                    subscription = new InstrumentSubscription(instrument, data, OnInstrumentSubscriptionTimedOut);
                    instrumentSubscriptionsByInstrument.Add(instrument, subscription);

                    instrumentSubscriptionsByCode[subscription.Code] = subscription;
                    isNewSubscription = true;
                }
            }

            if (isNewSubscription)
            {
                // Если подписка уже существовала, то ничего не делаем
                // В противном случае требуется подписаться

                // Подписываемся
                socketL1.Subscribe(subscription.Code);
                subscription.BeginTimeout();
            }

            return await subscription.Task;
        }

        /// <summary>
        ///     Отписаться от инструмента.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public void Unsubscribe(Instrument instrument)
        {
            InstrumentSubscription subscription;
            using (instrumentSubscriptionsLock.Lock())
            {
                if (!instrumentSubscriptionsByInstrument.TryGetValue(instrument, out subscription))
                {
                    return;
                }

                instrumentSubscriptionsByCode.Remove(subscription.Code);
                instrumentSubscriptionsByInstrument.Remove(instrument);
            }

            socketL1.Unubscribe(subscription.Code);
            subscription.Terminate();
        }

        private void L1OnSubscriptionErrorMsg(IQMessageArgs args)
        {
            // args.RequestId

            /*
             * TODO
            InstrumentSubscription subscription;
            using (instrumentSubscriptionsLock.Lock())
            {
                if(!instrumentSubscriptionsByCode.TryGetValue(args.Message, out subscription))
                {
                    return;
                }

                if (instrumentSubscriptionsByCode.TryGetValue(args.RequestId, out subscription))
                {
                    instrumentSubscriptionsByCode.Remove(subscription.Code);
                    instrumentSubscriptionsByInstrument.Remove(instrument);
                }
            }

            subscription.TrySetResult(
                new SubscriptionResult(subscription.Instrument, false, string.Format("Can't subscribe \"{0}\"", args.Message))
                );*/
        }

        private void L1OnFundamentalMsg(IQMessageArgs args)
        {
            L1FundamentalMsg msg;
            L1FundamentalMsg.Parse(args, out msg);

            InstrumentSubscription subscription;
            using (instrumentSubscriptionsLock.Lock())
            {
                if (!instrumentSubscriptionsByCode.TryGetValue(msg.Symbol, out subscription))
                {
                    return;
                }
            }
            
            switch (subscription.SecurityType)
            {
                case SecurityType.FUTURE:
                case SecurityType.FOPTION:
                    break;
                default:
                    subscription.InstrumentParams.PriceStepValue = subscription.InstrumentParams.PriceStep;
                    break;
            }

            var shouldTransmit = subscription.Update(ref msg);
            if (shouldTransmit)
            {
                OnMessageReceived(subscription.InstrumentParams);
            }
        }

        private void L1OnUpdateMsg(IQMessageArgs args)
        {
            if (!L1UpdateMsg.TryParse(args, fieldIndex, out var msg))
            {
                return;
            }

            InstrumentSubscription subscription;
            using (instrumentSubscriptionsLock.Lock())
            {
                if (!instrumentSubscriptionsByCode.TryGetValue(msg.Symbol, out subscription))
                {
                    return;
                }
            }

            var shouldTransmit = subscription.Update(ref msg);
            if (shouldTransmit)
            {
                OnMessageReceived(subscription.InstrumentParams);
            }
        }

        private void OnInstrumentSubscriptionTimedOut(InstrumentSubscription subscription)
        {
            using (instrumentSubscriptionsLock.Lock())
            {
                instrumentSubscriptionsByCode.Remove(subscription.Code);
                instrumentSubscriptionsByInstrument.Remove(subscription.Instrument);
            }
        }

        #endregion

        #region IOrderBookSubscriber

        /// <summary>
        ///     Подписаться на стакан по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public void SubscribeOrderBook(Instrument instrument)
        {
            // TODO Not Supported Yet
        }

        /// <summary>
        ///     Отписаться от стакана по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public void UnsubscribeOrderBook(Instrument instrument)
        {
            // TODO Not Supported Yet
        }

        #endregion

        #region Implementation of IConnectionStatusProvider

        /// <summary>
        ///     Название соединения
        /// </summary>
        public string ConnectionName => "IQFeed";

        /// <summary>
        ///     Вызывается при изменении состояния соединения
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        private void RaiseConnectionStatusChanged()
        {
            ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(ConnectionStatus, ConnectionName));
        }

        /// <summary>
        ///     Текущее состояние соединения
        /// </summary>
        public ConnectionStatus ConnectionStatus => (ConnectionStatus)status;

        // Содержит значения типа ConnectionStatus
        private int status = (int)ConnectionStatus.Undefined;

        private void ChangeConnectionStatus(ConnectionStatus newStatus)
        {
            if (Interlocked.Exchange(ref status, (int)newStatus) != (int)newStatus)
            {
                RaiseConnectionStatusChanged();
            }
        }

        #endregion

        #region Implementation of IInstrumentHistoryProvider
        
        private readonly ILockObject historyRequestsLock = DeadlockMonitor.Cookie<IQFeedGateway>("historyRequestsLock");
        private readonly Dictionary<string, HistoryRequest> historyRequests = new Dictionary<string, HistoryRequest>();

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
        public TimeSpan? GetBestFetchBlockLength(Instrument instrument, HistoryProviderSpan span) => TimeSpan.FromDays(365);

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
            var instrumentData = await instrumentConverter.ResolveInstrumentAsync(this, instrument);
            if (instrumentData == null)
            {
                consumer.Error($"Unable to resolve symbol for {instrument}");
                return;
            }

            // Выгружаем список точек
            var points = await FetchHistoryDataAsync(instrumentData.Symbol, begin, end, span, cancellationToken);

            // Собираем результат
            var data = new HistoryData(instrument, begin, end, span);
            foreach (var p in points.OrderBy(_ => _.Point))
            {
                data.Points.Add(p);
            }

            // Передаем результат потребителю
            consumer.Update(data, HistoryDataUpdateType.Batch);
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
            var instrumentData = await instrumentConverter.ResolveInstrumentAsync(this, instrument);
            if (instrumentData == null)
            {
                consumer.Error($"Unable to resolve symbol for {instrument}");
                return new NullHistoryDataSubscription();
            }

            // Создаем подписку
            var subscription = new HistorySubscription(this, consumer, instrument, instrumentData.Symbol, span);
            subscription.StartFetch(begin);
            return subscription;
        }

        internal async Task<IList<HistoryDataPoint>> FetchHistoryDataAsync(
            string instrumentSymbol,
            DateTime begin,
            DateTime end,
            HistoryProviderSpan span,
            CancellationToken cancellationToken)
        {
            // Создаем операцию для ожидания
            var operation = new HistoryRequest(span, begin, end);
            var requestId = HistorySocketWrapper.HistoryRequestIdPefix + Guid.NewGuid().ToString("N");
            using (historyRequestsLock.Lock())
            {
                historyRequests[requestId] = operation;
            }

            // Подписываемся на исторические данные
            historySocket.RequestHistoryData(instrumentSymbol, begin, end, span, requestId);

            // Разрешаем отмену операции
            cancellationToken.RegisterSafe(() => operation.TrySetCanceled());

            // Дожидаемся результатов
            var points = await operation.Task;
            return points;
        }

        private void HistoryOnErrorMsg(IQMessageArgs args)
        {
            HistoryRequest request;
            using (historyRequestsLock.Lock())
            {
                if (!historyRequests.TryGetValue(args.RequestId, out request))
                {
                    return;
                }
                historyRequests.Remove(args.RequestId);
            }

            var message = args.Message.Trim(',', '\r', '\n');
            if (message == "!NO_DATA!")
            {
                request.NoData();
            }
            else
            {
                request.Fail(message);
            }
        }

        private void HistoryOnHistoryMsg(IQMessageArgs args)
        {
            HistoryRequest request;
            using (historyRequestsLock.Lock())
            {
                if (!historyRequests.TryGetValue(args.RequestId, out request))
                {
                    return;
                }
            }

            HistoryMsg mgs;
            HistoryMsg.Parse(args, request.Span, out mgs);

            request.AddPoint(new HistoryDataPoint(
                    mgs.Time,
                    mgs.High,
                    mgs.Low,
                    mgs.Open,
                    mgs.Close,
                    mgs.Volume,
                    mgs.OpenInterest));
        }

        private void HistoryOnHistoryEndMsg(IQMessageArgs args)
        {
            HistoryRequest request;
            using (historyRequestsLock.Lock())
            {
                if (!historyRequests.TryGetValue(args.RequestId, out request))
                {
                    return;
                }
                historyRequests.Remove(args.RequestId);
            }

            request.Complete();
        }

        #endregion

        #region Ticker subscription tests

        private sealed class SubscriptionTest : TaskCompletionSource<bool>
        {
            public SubscriptionTest(string code, SecurityType type)
            {
                Code = code;
                Type = type;
            }

            public string Code { get; }
            public SecurityType Type { get; }
            public string Message { get; set; }
        }

        private readonly ILockObject subscriptionTestsLock = DeadlockMonitor.Cookie<IQFeedGateway>("subscriptionTestsLock");
        private readonly Dictionary<string, SubscriptionTest> subscriptionTests = new Dictionary<string, SubscriptionTest>();

        /// <summary>
        ///     Проверить подписку по вендорскому коду
        /// </summary>
        public async Task<Tuple<bool, string>> TrySubscribeAsync(string code, SecurityType type)
        {
            await securityTypeIndexCompleted.Task;

            int typeId;
            if (!securityTypeIndex.TryGetValue(type, out typeId))
            {
                var message = LogMessage.Format($"Failed to find an identifier for {type}").ToString();
                Logger.Warn().Print(message);
                return Tuple.Create(false, message);
            }

            var operation = new SubscriptionTest(code, type);
            var requestId = LookupSocketWrapper.RequestIdPrefix + Guid.NewGuid().ToString("N");
            using (subscriptionTestsLock.Lock())
            {
                subscriptionTests[requestId] = operation;
            }

            lookupSocket.RequestSymbolLookup(code, typeId, requestId);

            var result = await operation.Task;
            return Tuple.Create(result, operation.Message);
        }

        private bool SubscriptionTestOnResultMsg(IQMessageArgs args)
        {
            SubscriptionTest operation;
            using (subscriptionTestsLock.Lock())
            {
                if (!subscriptionTests.TryGetValue(args.RequestId, out operation))
                {
                    return false;
                }

                subscriptionTests.Remove(args.RequestId);
            }

            var passed = args.Message.Split(',')[0] == operation.Code;
            operation.Message = args.Message;
            operation.TrySetResult(passed);
            Logger.Debug().PrintFormat("{0} {1} is found ({2})", operation.Type, operation.Code, args.Message);
            return true;
        }

        private bool SubscriptionTestOnErrorMsg(IQMessageArgs args)
        {
            SubscriptionTest operation;
            using (subscriptionTestsLock.Lock())
            {
                if (!subscriptionTests.TryGetValue(args.RequestId, out operation))
                {
                    return false;
                }

                subscriptionTests.Remove(args.RequestId);
            }

            operation.Message = args.Message;
            operation.TrySetResult(false);
            Logger.Warn().PrintFormat("Unable to find {0} {1} due to error {2}", operation.Type, operation.Code, args.Message);
            return true;
        }

        #endregion

        #region Symbol lookup

        private sealed class SymbolLookupRequest : TaskCompletionSource<string[]>
        {
            private readonly int? maxResults;
            private readonly List<string> codes = new List<string>();

            public SymbolLookupRequest(int? maxResults)
            {
                this.maxResults = maxResults;
            }

            public void Handle(IQMessageArgs args, out bool isCompleted)
            {
                var parts = args.Message.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                {
                    TrySetResult(codes.ToArray());
                    isCompleted = true;
                    return;
                }

                codes.Add(parts[0]);
                isCompleted = false;

                if (maxResults != null && codes.Count >= maxResults.Value)
                {
                    // Если установлен лимит на число результатов, то прерываемся преждевременно
                    // Все последующие сообщения будут проигнорированы.
                    // Увы, мощный API не позволяет задать макс. число результатов в запросе.
                    TrySetResult(codes.ToArray());
                    isCompleted = true;
                }
            }

            public void Fail(IQMessageArgs args)
            {
                TrySetCanceled();
            }
        }

        private readonly ILockObject symbolLookupRequestsLock = DeadlockMonitor.Cookie<IQFeedGateway>("symbolLookupRequestsLock");
        private readonly Dictionary<string, SymbolLookupRequest> symbolLookupRequests = new Dictionary<string, SymbolLookupRequest>();

        public async Task<string[]> LookupSymbols(string code, int? maxResults)
        {
            var operation = new SymbolLookupRequest(maxResults);
            var requestId = LookupSocketWrapper.RequestIdPrefix + Guid.NewGuid().ToString("N");
            using (symbolLookupRequestsLock.Lock())
            {
                symbolLookupRequests[requestId] = operation;
            }

            lookupSocket.RequestSymbolLookup(code, null, requestId);

            var result = await operation.Task;
            return result;
        }

        private bool SymbolLookupOnResultMsg(IQMessageArgs args)
        {
            SymbolLookupRequest operation;
            using (symbolLookupRequestsLock.Lock())
            {
                if (!symbolLookupRequests.TryGetValue(args.RequestId, out operation))
                {
                    return false;
                }
            }

            bool isCompleted;
            operation.Handle(args, out isCompleted);

            if (isCompleted)
            {
                using (symbolLookupRequestsLock.Lock())
                {
                    symbolLookupRequests.Remove(args.RequestId);
                }
            }

            return true;
        }

        private bool SymbolLookupOnErrorMsg(IQMessageArgs args)
        {
            SymbolLookupRequest operation;
            using (symbolLookupRequestsLock.Lock())
            {
                if (!symbolLookupRequests.TryGetValue(args.RequestId, out operation))
                {
                    return false;
                }

                symbolLookupRequests.Remove(args.RequestId);
            }

            operation.Fail(args);
            return true;
        }

        #endregion

        #region Private methods

        private void L1OnSystemMsg(IQMessageArgs args)
        {
            var systemMessage = args.Message;
            switch (systemMessage)
            {
                case L1SystemMsg.SERVER_CONNECTED:
                    ChangeConnectionStatus(ConnectionStatus.Connected);
                    break;

                case L1SystemMsg.SERVER_DISCONNECTED:
                    ChangeConnectionStatus(ConnectionStatus.Disconnected);
                    break;

                case L1SystemMsg.SYMBOL_LIMIT_REACHED:
                    OnMessageReceived(new ErrorMessage { Message = systemMessage });
                    break;

                default:
                    if (systemMessage.StartsWith(L1SystemMsg.CURRENT_UPDATE_FIELDNAMES))
                    {
                        fieldIndex.UpdateFromL1SystemMsg(args);
                        ChangeConnectionStatus(ConnectionStatus.Connected);
                    }
                    break;
            }
        }

        private void L1OnErrorMsg(IQMessageArgs args)
        {
            OnMessageReceived(new ErrorMessage { Message = args.Message });
            ChangeConnectionStatus(ConnectionStatus.Disconnected);
        }

        private void LookupOnSecurityTypeMsg(IQMessageArgs args)
        {
            var isCompleted = securityTypeIndex.UpdateFromSecurityTypeMsg(args);
            if (isCompleted)
            {
                securityTypeIndexCompleted.TrySetResult(true);
            }
        }

        private void LookupOnResultMsg(IQMessageArgs args)
        {
            if (SubscriptionTestOnResultMsg(args))
            {
                return;
            }

            if (SymbolLookupOnResultMsg(args))
            {
                return;
            }

            Logger.Trace().PrintFormat("Message LOOKUP.{0} wasn't handled. request_id={1}", args.Message, args.RequestId);
        }

        private void LookupOnErrorMsg(IQMessageArgs args)
        {
            if (SubscriptionTestOnErrorMsg(args))
            {
                return;
            }

            if (SymbolLookupOnErrorMsg(args))
            {
                return;
            }

            Logger.Trace().PrintFormat("Message LOOKUP.{0} wasn't handled. request_id={1}", args.Message, args.RequestId);
        }

        #endregion

        #region IInstrumentConverterContext

        ISubscriptionTester<IQFeedInstrumentData> IInstrumentConverterContext<IQFeedInstrumentData>.SubscriptionTester => this;

        #endregion

        #region Implementation of ISubscriptionTester

        /// <summary>
        ///     Проверить подписку
        /// </summary>
        async Task<SubscriptionTestResult> ISubscriptionTester<IQFeedInstrumentData>.TestSubscriptionAsync(IQFeedInstrumentData data)
        {
            await securityTypeIndexCompleted.Task;

            if (!securityTypeIndex.TryGetValue(data.SecurityType, out var typeId))
            {
                var message = LogMessage.Format($"Failed to find an identifier for {data.SecurityType}").ToString();
                Logger.Warn().Print(message);
                return SubscriptionTestResult.Failed(message);
            }

            var operation = new SubscriptionTest(data.Symbol, data.SecurityType);
            var requestId = LookupSocketWrapper.RequestIdPrefix + Guid.NewGuid().ToString("N");
            using (subscriptionTestsLock.Lock())
            {
                subscriptionTests[requestId] = operation;
            }

            lookupSocket.RequestSymbolLookup(data.Symbol, typeId, requestId);

            var result = await operation.Task;
            if (result)
            {
                return SubscriptionTestResult.Passed();
            }

            return SubscriptionTestResult.Failed();
        }

        #endregion      
    }
}

