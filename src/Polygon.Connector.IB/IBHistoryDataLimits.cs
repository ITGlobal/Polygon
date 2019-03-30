using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;

namespace Polygon.Connector.InteractiveBrokers
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class IBHistoryDataLimits
    {
        public static readonly TimeSpan MaxBarsPerRequest = TimeSpan.FromSeconds(86400);

        public static TimeSpan? GetFetchBlockSize(HistoryProviderSpan span)
        {
            return null;
            // return MaxBarsPerRequest;
        }
        
        // Valid Duration and Bar Size Settings for Historical Data Requests
        // =================================================================

        private static readonly TimeSpan Duration_5m = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan Duration_10m = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan Duration_15m = TimeSpan.FromMinutes(15);
        private static readonly TimeSpan Duration_30m = TimeSpan.FromMinutes(30);
        private static readonly TimeSpan Duration_1h = TimeSpan.FromHours(1);
        private static readonly TimeSpan Duration_8h = TimeSpan.FromHours(8);
        private static readonly TimeSpan Duration_1d = TimeSpan.FromDays(1);
        private static readonly TimeSpan Duration_1w = TimeSpan.FromDays(7);
        private static readonly TimeSpan Duration_2w = TimeSpan.FromDays(14);
        private static readonly TimeSpan Duration_1M = TimeSpan.FromDays(30);
        private static readonly TimeSpan Duration_1Y = TimeSpan.FromDays(365);

        public static void GetHistoryDataLimits(HistoryProviderSpan span, out TimeSpan? min, out TimeSpan? max)
        {
            switch (span)
            {
                case HistoryProviderSpan.Minute:
                    // min: -
                    // max: 1d
                    min = null;
                    max = Duration_1d;
                    break;

                case HistoryProviderSpan.Minute5:
                    // min: 5m
                    // max: 1w
                    min = Duration_5m;
                    max = Duration_1w;
                    break;

                case HistoryProviderSpan.Minute10:
                    // min: 10m
                    // max: 1w
                    min = Duration_10m;
                    max = Duration_1w;
                    break;

                case HistoryProviderSpan.Minute15:
                    // min: 15m
                    // max: 2w
                    min = Duration_15m;
                    max = Duration_2w;
                    break;

                case HistoryProviderSpan.Minute30:
                    // min: 30m
                    // max: 1M
                    min = Duration_30m;
                    max = Duration_1M;
                    break;

                case HistoryProviderSpan.Hour:
                    // min: 1h
                    // max: 1M
                    min = Duration_1h;
                    max = Duration_1M;
                    break;

                case HistoryProviderSpan.Hour4:
                    // min: 8h
                    // max: 1M
                    min = Duration_8h;
                    max = Duration_1M;
                    break;

                case HistoryProviderSpan.Day:
                    // min: 1d
                    // max: 1Y
                    min = Duration_1d;
                    max = Duration_1Y;
                    break;

                case HistoryProviderSpan.Week:
                    // min: 1w
                    // max: 1Y
                    min = Duration_1w;
                    max = Duration_1Y;
                    break;

                case HistoryProviderSpan.Month:
                    // min: 1M
                    // max: 1Y
                    min = Duration_1M;
                    max = Duration_1Y;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        // Pacing Violations
        // =================
        //
        // All of the API technologies support historical data requests. 
        // However, requesting the same historical data in a short period of time can cause extra load on the backend and subsequently cause pacing violations. 
        // The error code and message that indicates a pacing violation is:
        //  * 162 - Historical Market Data Service error message: Historical data request pacing violation
        //
        // The following conditions can cause a pacing violation:
        //  * Making identical historical data requests within 15 seconds;
        //  * Making six or more historical data requests for the same Contract, Exchange and Tick Type within two seconds.
        //
        // Also, observe the following limitation when requesting historical data:
        //  * Do not make more than 60 historical data requests in any ten-minute period.
        //  * If the whatToShow parameter in reqHistoricalData() is set to BID_ASK, then this counts as two requests and we will call BID and ASK historical data separately.

        public async Task WaitForPaceLimitAsync(IBHistoricalDataRequest request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var waitTime = ComputeEffectiveWaitTime(DateTime.UtcNow, request);

            if (waitTime > 0)
            {
                const int LongEnough = 5 * 1000; // 5 sec

                if (waitTime >= LongEnough)
                {
                    IBAdapter.Log.Warn().PrintFormat("Will wait for history data for {0}s due to pace limits",
                        waitTime / 1000);
                }
                else
                {
                    IBAdapter.Log.Debug().PrintFormat("Pace limit wait time: {0}ms", waitTime);
                }

                await Task.Delay(waitTime, cancellationToken);
            }
        }

        private readonly ILockObject lastRequestTimesLock = DeadlockMonitor.Cookie<IBHistoryDataLimits>("lastRequestTimesLock");

        // Время последнего запроса по данному инструменту
        // Частота запросов по одному и тому же инструменту ограничена параметром MinRequestRate
        private readonly Dictionary<int, DateTime> lastRequestTimesByContract = new Dictionary<int, DateTime>();

        // Время последнего запроса с параметрами
        private readonly Dictionary<string, DateTime> lastRequestTimesByRequestParameters = new Dictionary<string, DateTime>();

        private readonly CircularBuffer<DateTime> allRequestTimes = new CircularBuffer<DateTime>(60);

        private int ComputeEffectiveWaitTime(DateTime now, IBHistoricalDataRequest request)
        {
            var key = request.Key;

            using (lastRequestTimesLock.Lock())
            {
                // Ограничение 1 - не более 2.5 запросов в секунду по одному и тому же инструменту
                var waitTimeByContract = GetWaitTimeByContract(now, request);

                // Ограничение 2 - идентичные запросы должны идти не чаще, чем 1 раз в 15 секунд
                var waitTimeByRequestParameters = GetWaitTimeByRequestParameters(now, key);

                // Ограничение 3 - не более 60 запросов за 10 минут
                var waitTimeByTotalRequestCount = GetWaitTimeByTotalRequestCount(now);

                // Берем наибольшее время ожидания
                var waitTime = Math.Max(waitTimeByContract, waitTimeByRequestParameters);
                waitTime = Math.Max(waitTime, waitTimeByTotalRequestCount);

                // Переводим в миллисекунды
                var waitTimeMs = (int) Math.Ceiling(waitTime*1000d);
                if (waitTimeMs <= 0)
                {
                    waitTimeMs = 0;
                }

                // Сразу же запоминаем время данного запроса с учетом ожидания
                // Это поможет в случае параллельных запросов
                var future = now.AddMilliseconds(waitTimeMs);

                lastRequestTimesByContract[request.ContractId] = future;
                lastRequestTimesByRequestParameters[key] = future;
                allRequestTimes.Push(future);

                return waitTimeMs;
            }
        }

        private double GetWaitTimeByContract(DateTime now, IBHistoricalDataRequest request)
        {
            // Не более 3 запросов за 2 секунды
            const double MinRequestRate = 2d/3d;

            DateTime lastRequestTime;
            if (!lastRequestTimesByContract.TryGetValue(request.ContractId, out lastRequestTime))
            {
                return 0d;
            }

            var delta = (now - lastRequestTime).TotalSeconds;
            delta = MinRequestRate - delta;

            if (delta <= 0)
            {
                return 0d;
            }

            return delta;
        }

        private double GetWaitTimeByRequestParameters(DateTime now, string key)
        {
            // Не более 1 запроса за 15 секунд
            const double MinRequestRate = 1d/15d;

            DateTime lastRequestTime;
            if (!lastRequestTimesByRequestParameters.TryGetValue(key, out lastRequestTime))
            {
                return 0d;
            }

            var delta = (now - lastRequestTime).TotalSeconds;
            delta = MinRequestRate - delta;

            if (delta <= 0)
            {
                return 0d;
            }

            return delta;
        }

        private double GetWaitTimeByTotalRequestCount(DateTime now)
        {
            const double timePeriod = 600; // 10min
            const double maxCount = 60;

            var count = allRequestTimes.Count(t => (now - t).TotalSeconds <= timePeriod);
            if (count < maxCount)
            {
                // За последние 10 минут было меньше 60 запросов, ожидание не требуется
                return 0d;
            }

            // За последние 10 минут было 60+ запросов, нужно ждать

            var time = allRequestTimes.Where(t => (now - t).TotalSeconds <= timePeriod).Min();
            var delta = (now - time).TotalSeconds;

            // Если мы подождем delta секунд, то лимит не превысим
            return delta;
        }

        /// <summary>
        ///     Кольцевой буфер FIFO фиксированного размера
        /// </summary>
        public sealed class CircularBuffer<T> : IEnumerable<T>
        {
            private readonly T[] buffer;
            private readonly int capacity;
            private int head;
            private int tail = -1;

            public CircularBuffer(int capacity)
            {
                if (capacity <= 0)
                    throw new ArgumentOutOfRangeException(nameof(capacity));

                this.capacity = capacity;
                buffer = new T[capacity];
            }

            public void Push(T value)
            {
                head++;
                if (head >= capacity)
                {
                    head = 0;
                }

                buffer[head] = value;

                if (tail == -1)
                {
                    tail = head;
                }

                if (head < tail)
                {
                    tail++;
                    if (tail >= capacity)
                    {
                        tail = 0;
                    }
                }
            }

            public bool TryPeek(out T value)
            {
                if (tail < 0)
                {
                    value = default(T);
                    return false;
                }

                value = buffer[tail];
                return true;
            }

            public IEnumerator<T> GetEnumerator()
            {
                if (tail < 0)
                {
                    yield break;
                }

                var i = tail;
                while (true)
                {
                    yield return buffer[i];

                    i++;
                    if (i >= capacity)
                    {
                        i = 0;
                    }

                    if (i == head)
                    {
                        break;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

