using System;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Messages;
using Polygon.Connector;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Фид для IB
    /// </summary>
    internal sealed class IBFeed : Feed, IInstrumentParamsSubscriber, IOrderBookSubscriber, IInstrumentHistoryProvider
    {
        internal const int DefaultMarketDepth = 5;

        private readonly IBConnector connector;
        private int marketDepth = DefaultMarketDepth;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public IBFeed(IBConnector connector)
        {
            this.connector = connector;
        }

        /// <summary>
        ///     Глубина стаканов
        /// </summary>
        public int MarketDepth
        {
            get { return marketDepth; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                marketDepth = value;
            }
        }

        /// <summary>
        ///   Запускает сервис.
        /// </summary>
        public override void Start() { }

        /// <summary>
        ///   Останавливает сервис.
        /// </summary>
        public override void Stop() { }

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
        public TimeSpan? GetBestFetchBlockLength(Instrument instrument, HistoryProviderSpan span) => connector.Adapter.GetBestFetchBlockLength(instrument, span);

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
        public Task GetHistoryDataAsync(
            IHistoryDataConsumer consumer,
            Instrument instrument,
            DateTime begin,
            DateTime end,
            HistoryProviderSpan span,
            CancellationToken cancellationToken = new CancellationToken())
            => connector.Adapter.GetHistoryDataAsync(consumer, instrument, begin, end, span, cancellationToken);

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
        public Task<IHistoryDataSubscription> SubscribeToHistoryDataAsync(
            IHistoryDataConsumer consumer, Instrument instrument, DateTime begin, HistoryProviderSpan span)
            => connector.Adapter.SubscribeToHistoryDataAsync(consumer, instrument, begin, span);

        /// <summary>
        ///     Подписаться на инструмент.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public Task<SubscriptionResult> Subscribe(Instrument instrument) => connector.Adapter.Subscribe(instrument);

        /// <summary>
        ///     Отписаться от инструмента.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public void Unsubscribe(Instrument instrument) => connector.Adapter.Unsubscribe(instrument);

        /// <summary>
        ///     Подписаться на стакан по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public void SubscribeOrderBook(Instrument instrument) => connector.Adapter.SubscribeOrderBook(instrument);

        /// <summary>
        ///     Отписаться от стакана по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public void UnsubscribeOrderBook(Instrument instrument) => connector.Adapter.UnsubscribeOrderBook(instrument);

        /// <summary>
        ///     Выплюнуть из фида параметры инструмента
        /// </summary>
        /// <param name="instrumentParams">
        ///     Параметры инструмента
        /// </param>
        internal void Transmit(InstrumentParams instrumentParams) => OnMessageReceived(instrumentParams);

        /// <summary>
        ///     Выплюнуть из фида стакан
        /// </summary>
        /// <param name="orderBook">
        ///     Параметры инструмента
        /// </param>
        internal void Transmit(OrderBook orderBook) => OnMessageReceived(orderBook);
    }
}
