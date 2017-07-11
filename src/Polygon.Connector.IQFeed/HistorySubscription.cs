using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.IQFeed
{
    internal sealed class HistorySubscription : IHistoryDataSubscription
    {
        private const int FetchInterval = 5 * 1000; // 5sec
        private static readonly ILog _Log = LogManager.GetLogger<HistorySubscription>();

        private readonly IQFeedGateway gateway;
        private readonly IHistoryDataConsumer consumer;
        private readonly string instrumentSymbol;
        private readonly HistoryProviderSpan span;

        private readonly Dictionary<DateTime, HistoryDataPoint> points = new Dictionary<DateTime, HistoryDataPoint>();
        private readonly ILockObject syncRoot = DeadlockMonitor.Cookie<HistorySubscription>();
        private readonly HistoryData data;
        
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;

        private bool isDisposed;

        public HistorySubscription(
            IQFeedGateway gateway,
            IHistoryDataConsumer consumer,
            Instrument instrument,
            string instrumentSymbol,
            HistoryProviderSpan span)
        {
            this.gateway = gateway;
            this.consumer = consumer;
            this.instrumentSymbol = instrumentSymbol;
            this.span = span;

            data = new HistoryData(instrument, DateTime.Today, DateTime.Today, span);

            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        public void StartFetch(DateTime begin)
        {
            try
            {
                Task.Factory.StartNew(() => UpdateLoopAsync(begin).Ignore());
            }
            catch (Exception e)
            {
                HandleException(e, $"Error in {nameof(StartFetch)} method");
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                cts.Cancel();
                cts.Dispose();
            }
        }

        private async Task UpdateLoopAsync(DateTime begin)
        {
            using (LogManager.Scope())
            {
                try
                {
                    while (true)
                    {
                        // Загружаем блок исторических данных
                        var end = DateTime.Now;
                        // TODO handle OperationCanceledException
                        var fetchedPoints = await gateway.FetchHistoryDataAsync(instrumentSymbol, begin, end, span, token);

                        // Обновляем точку старта на последнюю свечу, чтобы не грузить повторно
                        if (fetchedPoints.Any())
                        {
                            begin = fetchedPoints.Select(_ => _.Point).OrderByDescending(_ => _).First();
                        }

                        using (syncRoot.Lock())
                        {
                            // Объединяем с набором данных
                            int added, updated;
                            MergePoints(fetchedPoints, out added, out updated);

                            // Оповещаем потребителя
                            if (added > 0 || updated > 0)
                            {
                                begin = data.End;

                                if (added == 1 && updated == 0)
                                {
                                    consumer.Update(data, HistoryDataUpdateType.OnePointAdded);
                                }
                                else if (added == 0 && updated == 1)
                                {
                                    consumer.Update(data, HistoryDataUpdateType.OnePointUpdated);
                                }
                                else
                                {
                                    consumer.Update(data, HistoryDataUpdateType.Batch);
                                }
                            }
                            else
                            {
                                begin = end;
                            }
                        }

                        // Ждем до следущего обновления
                        await Task.Delay(FetchInterval, token);
                    }
                }
                catch (OperationCanceledException) { }
                catch (NoHistoryDataException)
                {
                    _Log.Debug().Print($"No more historical data is available", LogFields.Symbol(instrumentSymbol));
                }
                catch (Exception e)
                {
                    HandleException(e, e.Message);
                }
            }
        }

        private void MergePoints(IEnumerable<HistoryDataPoint> source, out int added, out int updated)
        {
            using (syncRoot.Lock())
            {
                added = 0;
                updated = 0;

                foreach (var point in source)
                {
                    HistoryDataPoint p;
                    if (!points.TryGetValue(point.Point, out p))
                    {
                        points.Add(point.Point, point);
                        added++;
                        continue;
                    }

                    if (p != point)
                    {
                        points[point.Point] = point;
                        updated++;
                    }
                }

                if (added > 0 || updated > 0)
                {
                    RebuildHistoryData();
                }
            }
        }

        private void RebuildHistoryData()
        {
            using (syncRoot.Lock())
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
        }

        private void HandleException(Exception exception, string message)
        {
            consumer.Error(message);
            _Log.Error().Print(exception, $"History data subscription failed: {message.Preformatted()}");
            Dispose();
        }
    }
}

