using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.CQGContinuum
{
    internal sealed class HistoryDataRequest : TaskCompletionSource<HistoryData>, ITimeBarReportHandler
    {
        private readonly CQGCInstrumentHistoryProvider provider;
        private readonly IHistoryDataConsumer consumer;
        private readonly HistoryData data;
        private readonly TimeBarRequest message;

        private readonly Dictionary<DateTime, HistoryDataPoint> points = new Dictionary<DateTime, HistoryDataPoint>();

        public HistoryDataRequest(
            CQGCInstrumentHistoryProvider provider,
            IHistoryDataConsumer consumer,
            Instrument instrument,
            DateTime begin,
            DateTime end,
            HistoryProviderSpan span,
            TimeBarRequest message)
        {
            this.provider = provider;
            this.consumer = consumer;
            data = new HistoryData(instrument, begin, end, span);
            this.message = message;
        }

        public void Process(TimeBarReport report, out bool shouldRemoveHandler)
        {
            shouldRemoveHandler = true;

            // Проверяем статус отчета
            var status = (TimeBarReport.StatusCode)report.status_code;
            switch (status)
            {
                case TimeBarReport.StatusCode.SUCCESS:
                    break;
                case TimeBarReport.StatusCode.DISCONNECTED:
                    TrySetCanceled();
                    return;
                case TimeBarReport.StatusCode.OUTSIDE_ALLOWED_RANGE:
                    // Мы зашли слишком глубоко в прошлое
                    CQGCAdapter.Log.Debug().Print(
                        "Historical data depth limit has been reached", 
                        LogFields.Instrument(data.Instrument),
                        LogFields.Span(data.Span));
                    TrySetException(new NoHistoryDataException(nameof(TimeBarReport.StatusCode.OUTSIDE_ALLOWED_RANGE)));
                    return;
                default:
                    TrySetException(new Exception($"Request ended with {status:G} status code. {report.text_message}"));
                    return;
            }

            // Складываем точки данных в пакет
            var dataPoints = provider.PrepareDataPoints(message.time_bar_parameters.contract_id, report)
                .Where(p => p.High != 0 && p.Low != 0 && p.Open != 0 && p.Close != 0);  // иногда нули приходят
            foreach (var point in dataPoints)
            {
                HistoryDataPoint p;
                if (!points.TryGetValue(point.Point, out p))
                {
                    points.Add(point.Point, point);
                    continue;
                }

                if (p != point)
                {
                    points[point.Point] = point;
                }
            }
            
            // Если отчет не полон, то ожидаем прихода следующей пачки данных
            if (!report.is_report_complete)
            {
                CQGCAdapter.Log.Debug().Print(
                    $"Got a {nameof(TimeBarReport)} but it's incomplete",
                    LogFields.Instrument(data.Instrument),
                    LogFields.Span(data.Span));
                shouldRemoveHandler = false;
                consumer.Warning("This operation might take some time...");
                return;
            }

            // Если все данные пришли, то собираем ответ и выставляем его потребителю
            var minDate = DateTime.MaxValue;
            var maxDate = DateTime.MinValue;

            data.Points.Clear();
            foreach (var p in points.OrderBy(_ => _.Key))
            {
                data.Points.Add(p.Value);

                if (p.Key > maxDate)
                {
                    maxDate = p.Key;
                }

                if (p.Key < minDate)
                {
                    minDate = p.Key;
                }
            }

            data.Begin = minDate;
            data.End = maxDate;

            TrySetResult(data);
        }
    }
}

