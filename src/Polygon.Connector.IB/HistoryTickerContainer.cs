using System.Collections.Generic;
using ITGlobal.DeadlockDetection;
using JetBrains.Annotations;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class HistoryTickerContainer
    {
        private readonly ILockObject historyPointsLock = DeadlockMonitor.Cookie<HistoryTaskCompletionSource>("historyTickerContainer");
        private readonly Dictionary<int, HistoryTaskCompletionSource> tasks = new Dictionary<int, HistoryTaskCompletionSource>();
        
        public void Store(int tickerId, HistoryTaskCompletionSource tcs)
        {
            using (historyPointsLock.Lock())
            {
                tasks[tickerId] = tcs;
            }
        }

        public void AddPoint(int tickerId, HistoryDataPoint point) => GetHandler(tickerId, remove: false)?.AddPoint(point);
        public void Complete(int tickerId) => GetHandler(tickerId)?.Complete();
        public void Cancel(int tickerId) => GetHandler(tickerId)?.Cancel();
        public void NoMoreData(int tickerId) => GetHandler(tickerId)?.NoMoreData();
        public void PaceViolation(int tickerId) => GetHandler(tickerId)?.PaceViolation();
        public void TimeLengthExceedMax(int tickerId) => GetHandler(tickerId)?.TimeLengthExceedMax();
        public void Fail(int tickerId, string message) => GetHandler(tickerId)?.Fail(message);
        
        [CanBeNull]
        private HistoryTaskCompletionSource GetHandler(int tickerId, bool remove = true)
        {
            using (historyPointsLock.Lock())
            {
                HistoryTaskCompletionSource tcs;
                if (!tasks.TryGetValue(tickerId, out tcs))
                {
                    return null;
                }

                if (remove)
                {
                    tasks.Remove(tickerId);
                }

                return tcs;
            }
        }
    }
}

