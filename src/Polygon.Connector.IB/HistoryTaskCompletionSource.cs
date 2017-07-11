using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class HistoryTaskCompletionSource : TaskCompletionSource<IList<HistoryDataPoint>>
    {
        private readonly IBHistoricalDataRequest request;

        private readonly ILockObject historyPointsLock = DeadlockMonitor.Cookie<HistoryTaskCompletionSource>("historyPointsLock");
        private readonly List<HistoryDataPoint> points = new List<HistoryDataPoint>();
        private readonly CancellationToken cancellationToken;

        public HistoryTaskCompletionSource(IBHistoricalDataRequest request, CancellationToken cancellationToken)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            cancellationToken.RegisterSafe(() => TrySetCanceled());
        }

        public void AddPoint(HistoryDataPoint point)
        {
            using (historyPointsLock.Lock())
            {
                points.Add(point);
            }
        }

        public void Complete() => this.TrySetResultBackground(points);

        public void Cancel() => this.TrySetCanceledBackground();

        public void NoMoreData() => this.TrySetExceptionBackground(new NoHistoryDataException());

        public void Fail(string message) => this.TrySetExceptionBackground(new Exception(message));

        public void PaceViolation()
        {
            // В случае проблем с частотой запросов ждем N секунд и передергиваем запрос

            request.Consumer.Warning("This operation might take a few minutes. This is a limitation of IB data source.");

            const int sleepTime = 60;
            IBAdapter.Log.Warn().Print($"Historical data request pace violation, request will be reissued in {sleepTime}s");
            System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(sleepTime), cancellationToken)
                .ContinueWith(async _ =>
                {
                    try
                    {
                        await request.ReissueAsync(this, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        IBAdapter.Log.Error().Print(e, "Unable to reissue historical data request");
                        TrySetException(e);
                    }
                }, cancellationToken);
        }

        public async void TimeLengthExceedMax()
        {
            // Если прилетело "Historical Market Data Service error message:Time length exceed max",
            // то уменьшаем размер таймфрема и передергиваем запрос

            using (LogManager.Scope())
            {
                try
                {
                    IBAdapter.Log.Debug().Print($"Historical data request: time length exceed max");
                    request.ReduceTimeFrame();
                    await request.ReissueAsync(this, cancellationToken);
                }
                catch (Exception e)
                {
                    IBAdapter.Log.Error().Print(e, "Unable to reissue historical data request");
                    TrySetException(e);
                }
            }
        }
    }
}

