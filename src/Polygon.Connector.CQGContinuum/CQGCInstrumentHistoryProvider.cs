using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Провайдер истории по инструментам для CQG Continuum
    /// </summary>
    internal sealed class CQGCInstrumentHistoryProvider : IInstrumentHistoryProvider
    {
        private readonly CQGCAdapter adapter;
        private readonly CQGCInstrumentResolver instrumentResolver;

        private readonly ILockObject requestsLock = DeadlockMonitor.Cookie<CQGCInstrumentHistoryProvider>("requestsLock");
        private readonly Dictionary<uint, ITimeBarReportHandler> requests = new Dictionary<uint, ITimeBarReportHandler>();

        public CQGCInstrumentHistoryProvider(CQGCAdapter adapter, CQGCInstrumentResolver instrumentResolver)
        {
            this.adapter = adapter;
            this.instrumentResolver = instrumentResolver;

            adapter.TimeBarReportReceived += TimeBarReportReceived;
        }

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
                    return TimeSpan.FromDays(30);
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
            using (LogManager.Scope())
            {
                var message = await PrepareTimeBarRequestAsync(instrument, begin, end, span, TimeBarRequest.RequestType.GET);
                if (message == null)
                {
                    throw new ArgumentException($"Unable to resolve instrument {instrument}");
                }

                var request = new HistoryDataRequest(this, consumer, instrument, begin, end, span, message);
                using (requestsLock.Lock())
                {
                    requests[message.request_id] = request;
                }

                CQGCAdapter.Log.Debug().Print(
                    "Requesting history data block",
                    LogFields.RequestId(message.request_id),
                    LogFields.ContractId(message.time_bar_parameters.contract_id));
                adapter.SendMessage(message);

                // Поддержка отмены запроса
                cancellationToken.RegisterSafe(() => request.TrySetCanceled());

                var data = await request.Task;
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
                var message = await PrepareTimeBarRequestAsync(instrument, begin, DateTime.Now, span, TimeBarRequest.RequestType.SUBSCRIBE);
                if (message == null)
                {
                    throw new ArgumentException($"Unable to resolve instrument {instrument}");
                }

                message.time_bar_parameters.to_utc_time = 0;

                var request = new HistoryDataSubscription(
                    this, 
                    new HistoryData(instrument, begin, DateTime.Now, span),
                    consumer, 
                    message.time_bar_parameters.contract_id, 
                    message.request_id);

                using (requestsLock.Lock())
                {
                    requests[message.request_id] = request;
                }

                CQGCAdapter.Log.Debug().Print(
                    "Requesting history data stream", 
                    LogFields.RequestId(message.request_id), 
                    LogFields.ContractId(message.time_bar_parameters.contract_id));
                adapter.SendMessage(message);

                return request;
            }
        }

        internal void DropHistoryDataSubscription(uint requestId)
        {
            var message = new TimeBarRequest
            {
                request_id = requestId,
                request_type = (uint)TimeBarRequest.RequestType.DROP,
                time_bar_parameters = new TimeBarParameters { }
            };

            adapter.SendMessage(message);
        }

        private async Task<TimeBarRequest> PrepareTimeBarRequestAsync(
            Instrument instrument, 
            DateTime begin, 
            DateTime end,
            HistoryProviderSpan span, 
            TimeBarRequest.RequestType type)
        {
            using (LogManager.Scope())
            {
                var contractId = await instrumentResolver.GetContractIdAsync(instrument);
                if (contractId == uint.MaxValue)
                {
                    return await Task.FromResult<TimeBarRequest>(null);
                }

                TimeBarParameters.BarUnit barUnit;
                uint unitsNumber = 0;

                switch (span)
                {
                    case HistoryProviderSpan.Minute:
                        barUnit = TimeBarParameters.BarUnit.MIN;
                        unitsNumber = 1;
                        break;
                    case HistoryProviderSpan.Minute5:
                        barUnit = TimeBarParameters.BarUnit.MIN;
                        unitsNumber = 5;
                        break;
                    case HistoryProviderSpan.Minute10:
                        barUnit = TimeBarParameters.BarUnit.MIN;
                        unitsNumber = 10;
                        break;
                    case HistoryProviderSpan.Minute15:
                        barUnit = TimeBarParameters.BarUnit.MIN;
                        unitsNumber = 15;
                        break;
                    case HistoryProviderSpan.Minute30:
                        barUnit = TimeBarParameters.BarUnit.MIN;
                        unitsNumber = 30;
                        break;
                    case HistoryProviderSpan.Hour:
                        barUnit = TimeBarParameters.BarUnit.HOUR;
                        unitsNumber = 1;
                        break;
                    case HistoryProviderSpan.Hour4:
                        barUnit = TimeBarParameters.BarUnit.HOUR;
                        unitsNumber = 4;
                        break;
                    case HistoryProviderSpan.Day:
                        // Для DAY units_number не заполняется
                        barUnit = TimeBarParameters.BarUnit.DAY;
                        break;
                    case HistoryProviderSpan.Week:
                        // Для WEEK units_number не заполняется
                        barUnit = TimeBarParameters.BarUnit.WEEK;
                        break;
                    case HistoryProviderSpan.Month:
                        // Для MONTH units_number не заполняется
                        barUnit = TimeBarParameters.BarUnit.MONTH;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(span), span, $"Invalid time span - {span}");
                }

                var message = new TimeBarRequest
                {
                    request_id = adapter.GetNextRequestId(),
                    request_type = (uint)type,
                    time_bar_parameters = new TimeBarParameters
                    {
                        contract_id = contractId,
                        bar_unit = (uint)barUnit,
                        units_number = unitsNumber,
                        from_utc_time = adapter.ResolveDateTime(begin),
                        to_utc_time = adapter.ResolveDateTime(end)
                    }
                };

                return message;
            }
        }

        private void TimeBarReportReceived(AdapterEventArgs<TimeBarReport> args)
        {
            ITimeBarReportHandler request;
            using (requestsLock.Lock())
            {
                if (!requests.TryGetValue(args.Message.request_id, out request))
                {
                    return;
                }
            }

            args.MarkHandled();

            bool shouldRemoveHandler;

            request.Process(args.Message, out shouldRemoveHandler);
            if (shouldRemoveHandler)
            {
                using (requestsLock.Lock())
                {
                    requests.Remove(args.Message.request_id);
                }
            }
        }

        internal IEnumerable<HistoryDataPoint> PrepareDataPoints(uint contractId, TimeBarReport report)
        {
            if (report.time_bar == null)
            {
                yield break;
            }

            foreach (var bar in report.time_bar)
            {
                var dataPoint = new HistoryDataPoint(
                    adapter.ResolveDateTime(bar.bar_utc_time),
                    instrumentResolver.ConvertPriceBack(contractId, bar.high_price),
                    instrumentResolver.ConvertPriceBack(contractId, bar.low_price),
                    instrumentResolver.ConvertPriceBack(contractId, bar.open_price), 
                    instrumentResolver.ConvertPriceBack(contractId, bar.close_price), 
                    (int)bar.volume,
                    (int)bar.open_interest);
                yield return dataPoint;
            }
        }
    }
}

