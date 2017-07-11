using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;

namespace Polygon.Connector.IQFeed
{
    internal sealed class HistoryRequest : TaskCompletionSource<IList<HistoryDataPoint>>
    {
        private readonly ILockObject historyPointsLock = DeadlockMonitor.Cookie<HistoryRequest>("historyPointsLock");
        private readonly List<HistoryDataPoint> points = new List<HistoryDataPoint>();

        public HistoryRequest(HistoryProviderSpan span, DateTime begin, DateTime end)
        {
            Span = span;
            Begin = begin;
            End = end;
        }

        public HistoryProviderSpan Span { get; }
        public DateTime Begin { get; }
        public DateTime End { get; }

        public void AddPoint(HistoryDataPoint point)
        {
            using (historyPointsLock.Lock())
            {
                points.Add(point);
            }
        }

        public void Complete()
        {
            switch (Span)
            {
                case HistoryProviderSpan.Week:
                case HistoryProviderSpan.Month:
                    // Для 1W и 1M свечей в IQFeed-е нет ограничения по глубине, посему вводим ее искусственным путем
                    var minDate = points.Min(_ => _.Point);
                    if (minDate > Begin && minDate > End)
                    {
                        TrySetException(new NoHistoryDataException());
                        return;
                    }
                    break;
            }

            TrySetResult(points);
        }

        public void NoData() => TrySetException(new NoHistoryDataException());
        public void Fail(string message) => TrySetException(new IQFeedHistoryDataException(message));
    }
}

