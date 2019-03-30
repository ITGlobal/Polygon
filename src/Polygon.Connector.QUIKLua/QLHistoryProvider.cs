using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Connector.QUIKLua.Adapter;
using Polygon.Connector.QUIKLua.Adapter.Messages;
using Polygon.Messages;

namespace Polygon.Connector.QUIKLua
{
    // TODO Остаётся доделать:
    // 1. Не пересылать свечи по 10 раз
    // 2. Оптимизировать отправку чанков, отправлять их не по номеру, а по размеру, как посоветовал Станислав
    // 3. Обработка сообщения об отписке от данных на стороне LUA
    /// <summary>
    /// Провайдер исторических данных из квика
    /// </summary>
    internal sealed class QLHistoryProvider : IInstrumentHistoryProvider
    {
        #region Fields

        /// <summary>
        /// Адаптер к квику, для отправки и приёма сообщений
        /// </summary>
        private readonly QLAdapter adapter;

        /// <summary>
        /// Блокировщик
        /// </summary>
        private readonly ILockObject requestsLock = DeadlockMonitor.Cookie<QLHistoryProvider>("quikRequestsLock");

        /// <summary>
        /// Контейнер запросов свечей
        /// </summary>
        private readonly Dictionary<Guid, HistoryDataRequest> requests = new Dictionary<Guid, HistoryDataRequest>();

        /// <summary>
        /// Подписки на обновление свечей
        /// </summary>
        private readonly Dictionary<Guid, HistoryDataSubscription> subscriptions = new Dictionary<Guid, HistoryDataSubscription>();

        #endregion

        #region .ctor

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="adapter"></param>
        public QLHistoryProvider(QLAdapter adapter)
        {
            this.adapter = adapter;
            adapter.MessageReceived += AdapterOnMessageReceived;
        }

        #endregion

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
            switch (span)
            {
                case HistoryProviderSpan.Minute:
                case HistoryProviderSpan.Minute5:
                case HistoryProviderSpan.Minute10:
                case HistoryProviderSpan.Minute15:
                case HistoryProviderSpan.Minute30:
                case HistoryProviderSpan.Hour:
                case HistoryProviderSpan.Hour4:
                case HistoryProviderSpan.Day:
                case HistoryProviderSpan.Week:
                case HistoryProviderSpan.Month:
                    return TimeSpan.FromDays(365);
                default:
                    return null;
            }
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
            var symbol = await adapter.ResolveSymbolAsync(instrument);
            if (symbol == null)
            {
                consumer.Error($"Unable to resolve symbol for {instrument}");
                return;
            }

            QLAdapter.Log.Debug().Print($"Candles request: {symbol}, span {span}, from {begin} to {end}");
            var dataRequestMessage = new QLHistoryDataRequest(symbol, span);
            var request = new HistoryDataRequest(dataRequestMessage.id, instrument, begin, end, span);

            using (requestsLock.Lock())
            {
                requests[request.Id] = request;
            }

            // Поддержка отмены запроса
            cancellationToken.RegisterSafe(() => request.TrySetCanceled());

            adapter.SendMessage(dataRequestMessage);

            var data = await request.Task;

            QLAdapter.Log.Debug().Print("Push candles to consumer. ", LogFields.RequestId(request.Id));
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
            var symbol = await adapter.ResolveSymbolAsync(instrument);
            if (symbol == null)
            {
                consumer.Error($"Unable to resolve symbol for {instrument}");
                return new NullHistoryDataSubscription();
            }

            QLAdapter.Log.Debug().Print($"Candles subscription: {symbol}, span {span}, from {begin}");
            var subscriptionMessage = new QLHistoryDataSubscription(symbol, begin, span);
            var subscription =
                new HistoryDataSubscription(subscriptionMessage.id, instrument, begin, span, adapter, consumer);

            using (requestsLock.Lock())
            {
                subscriptions[subscription.Id] = subscription;
            }

            adapter.SendMessage(subscriptionMessage);

            return subscription;
        }

        private void AdapterOnMessageReceived(object sender, QLMessageEventArgs e)
        {
            switch (e.Message.message_type)
            {
                case QLMessageType.CandlesResponse:
                    var response = (QLHistoryDataResponse)e.Message;
                    QLAdapter.Log.Debug().Print($"CandlesResponse received, contains {response.candles?.Count} candles", LogFields.RequestId(response.id));

                    HistoryDataRequest request;
                    using (requestsLock.Lock())
                    {
                        if (!requests.TryGetValue(response.id, out request))
                        {
                            return;
                        }
                    }

                    request.ProcessResponse(response);

                    using (requestsLock.Lock())
                    {
                        requests.Remove(response.id);
                    }

                    break;

                case QLMessageType.CandlesUpdate:
                    var update = (QLHistoryDataUpdate)e.Message;
                    QLAdapter.Log.Trace().Print($"CandlesUpdate received: {update.candles}");

                    HistoryDataSubscription subscription = null;

                    using (requestsLock.Lock())
                    {
                        if (!subscriptions.TryGetValue(update.id, out subscription))
                        {
                            return;
                        }
                    }

                    subscription.ProcessUpdate(update);

                    break;
            }
        }
    }
}

