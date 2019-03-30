using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using IBApi;
using ITGlobal.DeadlockDetection;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class IBHistoryDataSubscription : IHistoryDataSubscription
    {
        private const int FetchInterval = 60 * 1000; // 1min
        private static readonly ILog _Log = LogManager.GetLogger<IBHistoryDataSubscription>();

        private readonly IBAdapter adapter;
        private readonly IHistoryDataConsumer consumer;
        private readonly Contract contract;
        private readonly HistoryProviderSpan span;

        private readonly Dictionary<DateTime, HistoryDataPoint> points = new Dictionary<DateTime, HistoryDataPoint>();
        private readonly ILockObject syncRoot = DeadlockMonitor.Cookie<IBHistoryDataSubscription>();
        private readonly HistoryData historyData;

        private readonly InterlockedFlag terminated = new InterlockedFlag();

        public IBHistoryDataSubscription(
            IBAdapter adapter,
            IHistoryDataConsumer consumer,
            Instrument instrument,
            Contract contract,
            HistoryProviderSpan span
            )
        {
            this.adapter = adapter;
            this.consumer = consumer;
            this.contract = contract;
            this.span = span;

            historyData = new HistoryData(instrument, DateTime.Today, DateTime.Today, span);
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
            terminated.Set();
        }
        

        private async Task UpdateLoopAsync(DateTime begin)
        {
            try
            {
                while (true)
                {
                    // Ждем до следущего обновления
                    await Task.Delay(FetchInterval);

                    if (terminated.IsSet)
                    {
                        return;
                    }

                    // Загружаем блок исторических данных
                    var end = DateTime.Now;
                    // TODO handle OperationCanceledException
                    var fetchedPoints = await adapter.FetchHistoryDataBlock(consumer, contract, begin, end, span);

                    using (syncRoot.Lock())
                    {
                        // Объединяем с набором данных
                        int added, updated;
                        MergePoints(fetchedPoints, out added, out updated);

                        // Оповещаем потребителя
                        if (added > 0 || updated > 0)
                        {
                            begin = historyData.End;

                            if (added == 1 && updated == 0)
                            {
                                consumer.Update(historyData, HistoryDataUpdateType.OnePointAdded);
                            }
                            else if (added == 0 && updated == 1)
                            {
                                consumer.Update(historyData, HistoryDataUpdateType.OnePointUpdated);
                            }
                            else
                            {
                                consumer.Update(historyData, HistoryDataUpdateType.Batch);
                            }
                        }
                        else
                        {
                            begin = end;
                        }
                    }
                }
            }
            //catch (OperationCanceledException) { }
            catch (NoHistoryDataException)
            {
                _Log.Debug().Print($"No more historical data is available",
                    LogFields.Instrument(historyData.Instrument));
            }
            catch (Exception e)
            {
                HandleException(e, e.Message);
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

                historyData.Points.Clear();
                foreach (var p in points.OrderBy(_ => _.Key))
                {
                    historyData.Points.Add(p.Value);

                    if (p.Key > maxDate)
                    {
                        maxDate = p.Key;
                    }

                    if (p.Key < minDate)
                    {
                        minDate = p.Key;
                    }
                }

                historyData.Begin = minDate;
                historyData.End = maxDate;
            }
        }

        private void HandleException(Exception exception, string message)
        {
            consumer.Error(message);
            _Log.Error().Print(exception, $"History data subscription failed: {message.Preformatted()}");
            terminated.Set();
        }
    }
}

