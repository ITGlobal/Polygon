using System;
using System.Collections.Generic;
using System.Linq;
using Polygon.Diagnostics;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Connector;

namespace Polygon.Connector.CQGContinuum
{
    internal sealed class HistoryDataSubscription : IHistoryDataSubscription, ITimeBarReportHandler
    {
        private readonly CQGCInstrumentHistoryProvider provider;
        private readonly HistoryData data;
        private readonly IHistoryDataConsumer consumer;
        private readonly uint contractId;
        private readonly uint requestId;

        private int newPoints;
        private int updatedPoints;

        private readonly Dictionary<DateTime, HistoryDataPoint> points = new Dictionary<DateTime, HistoryDataPoint>();

        public HistoryDataSubscription(
            CQGCInstrumentHistoryProvider provider,
            HistoryData data,
            IHistoryDataConsumer consumer,
            uint contractId,
            uint requestId)
        {
            this.provider = provider;
            this.data = data;
            this.consumer = consumer;
            this.contractId = contractId;
            this.requestId = requestId;
        }

        public void Process(TimeBarReport report, out bool shouldRemoveHandler)
        {
            shouldRemoveHandler = false;

            // Проверяем статус отчета
            var status = (TimeBarReport.StatusCode)report.status_code;
            switch (status)
            {
                case TimeBarReport.StatusCode.SUCCESS:
                case TimeBarReport.StatusCode.SUBSCRIBED:
                case TimeBarReport.StatusCode.UPDATE:
                    break;
                case TimeBarReport.StatusCode.DISCONNECTED:
                case TimeBarReport.StatusCode.DROPPED:
                    shouldRemoveHandler = true;
                    return;
                default:
                    shouldRemoveHandler = true;
                    consumer.Error(report.text_message);
                    return;
            }
            
            // Складываем точки в словарь
            var dataPoints = provider.PrepareDataPoints(contractId, report)
                .Where(p => p.High != 0 && p.Low != 0 && p.Open != 0 && p.Close != 0);  // иногда нули приходят
            foreach (var point in dataPoints)
            {
                HistoryDataPoint p;
                if (!points.TryGetValue(point.Point, out p))
                {
                    points.Add(point.Point, point);
                    newPoints++;
                    continue;
                }

                if (p != point)
                {
                    // TODO это неоптимально, в data.Points уже есть точка для этой даты, надо ее обновить
                    points[point.Point] = point;
                    updatedPoints++;
                }
            }

            // Если хотя бы одна точка изменилась - перестраиваем набор данных
            if (newPoints >= 0 || updatedPoints > 0)
            {
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
            }

            // Если еще не все данные пришли - выходим
            if (!report.is_report_complete)
            {
                CQGCAdapter.Log.Debug().Print(
                    $"Got a {nameof(TimeBarReport)} but it's incomplete",
                    LogFields.Instrument(data.Instrument),
                    LogFields.Span(data.Span));
                return;
            }

            if (newPoints > 0 || updatedPoints > 0)
            {
                if (newPoints == 1 && updatedPoints == 0)
                {
                    // Пришла одна новая свечка 
                    consumer.Update(data, HistoryDataUpdateType.OnePointAdded);
                }
                else if (newPoints == 0 && updatedPoints == 1)
                {
                    // Одна свечка обновилась
                    consumer.Update(data, HistoryDataUpdateType.OnePointUpdated);
                }
                else
                {
                    // Свалилась пачка новых данных
                    consumer.Update(data, HistoryDataUpdateType.Batch);
                }

                newPoints = 0;
                updatedPoints = 0;
            }
        }

        public void Dispose()
        {
            provider.DropHistoryDataSubscription(requestId);
        }
    }
}

