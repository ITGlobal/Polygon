using System.Collections.Generic;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector.InteractiveBrokers;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Потокобезопасный контейнер номеров тикеров, связанных с тестом вендорских кодов
    /// </summary>
    internal sealed class PendingTestResultTickerContainer
    {
        private readonly ILockObject syncRoot = DeadlockMonitor.Cookie<PendingTestResultTickerContainer>();
        private readonly Dictionary<int, PendingTestResult> testResultByTicker = new Dictionary<int, PendingTestResult>();

        public void Store(int tickerId, PendingTestResult testResult)
        {
            using (syncRoot.Lock())
            {
                testResultByTicker[tickerId] = testResult;
            }
        }


        public bool TryGetPendingTestResult(int tickerId, out PendingTestResult testResult)
        {
            using (syncRoot.Lock())
            {
                return testResultByTicker.TryGetValue(tickerId, out testResult);
            }
        }

        public void RemoveTickerId(int tickerId)
        {
            using (syncRoot.Lock())
            {
                testResultByTicker.Remove(tickerId);
            }
        }
    }
}

