using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using IBApi;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class IBHistoricalDataRequest
    {
        private readonly IBAdapter adapter;
        private readonly Contract contract;      
        private readonly DateTime begin;
        private readonly DateTime end;
        private readonly HistoryProviderSpan span;
        private readonly string durationSuffix;
        private readonly string whatToShow;
        private readonly int useRth;
        private readonly int formatDate;
        private int duration;

        public IBHistoricalDataRequest(
            IBAdapter adapter,
            IHistoryDataConsumer consumer,
            Contract contract,
            DateTime begin,
            DateTime end,
            HistoryProviderSpan span,
            string whatToShow,
            int useRth,
            int formatDate)
        {
            this.adapter = adapter;
            Consumer = consumer;
            this.contract = contract;
            this.begin = begin;
            this.end = end;
            this.span = span;
            this.whatToShow = whatToShow;
            this.useRth = useRth;
            this.formatDate = formatDate;

            TimeSpan? minDuration, maxDuration;
            IBHistoryDataLimits.GetHistoryDataLimits(span, out minDuration, out maxDuration);

            var durationTimespan = end - begin;

            switch (span)
            {
                case HistoryProviderSpan.Minute:
                case HistoryProviderSpan.Minute5:
                case HistoryProviderSpan.Minute10:
                case HistoryProviderSpan.Minute15:
                case HistoryProviderSpan.Minute30:
                    if (durationTimespan >= maxDuration || durationTimespan > IBHistoryDataLimits.MaxBarsPerRequest)
                    {
                        duration = (int)Math.Ceiling(durationTimespan.TotalDays);
                        durationSuffix = " D";
                    }
                    else
                    {
                        duration = (int)Math.Ceiling(durationTimespan.TotalSeconds);
                        durationSuffix = " S";
                    }
                    break;
                case HistoryProviderSpan.Hour:
                case HistoryProviderSpan.Hour4:
                case HistoryProviderSpan.Day:
                case HistoryProviderSpan.Week:
                case HistoryProviderSpan.Month:
                    duration = (int)Math.Ceiling(durationTimespan.TotalDays);
                    durationSuffix = " D";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(span), span, null);
            }
        }

        public int ContractId => contract.ConId;
        public IHistoryDataConsumer Consumer { get; }

        public string Key => $"{contract.ConId}|{begin:s}|{end:s}|{span}|{whatToShow}|{useRth}|{formatDate}";

        public async Task<IList<HistoryDataPoint>> ExecuteAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var tickerId = adapter.NextTickerId();
            await adapter.HistoryDataLimits.WaitForPaceLimitAsync(this, cancellationToken);
            var request = new HistoryTaskCompletionSource(this, cancellationToken);

            adapter.HistoricalDepthTickers.Store(tickerId, request);
            ReqHistoricalData(tickerId);

            cancellationToken.RegisterSafe(() =>
            {
                adapter.Socket.cancelHistoricalData(tickerId);
                request.Cancel();
            });

            var points = await request.Task;
            return points;
        }

        public async Task ReissueAsync(HistoryTaskCompletionSource tcs, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tickerId = adapter.NextTickerId();
            await adapter.HistoryDataLimits.WaitForPaceLimitAsync(this, cancellationToken);
            adapter.HistoricalDepthTickers.Store(tickerId, tcs);
            ReqHistoricalData(tickerId);
        }

        public void ReduceTimeFrame()
        {
            var oldDuration = duration;
            var newDuration = (int)Math.Ceiling(duration / 2d);

            if (newDuration < 1)
            {
                newDuration = 1;
            }

            if (newDuration == oldDuration)
            {
                throw new Exception("Unable to reduce time frame below \"1 D\"");
            }

            duration = newDuration;
            IBAdapter.Log.Debug().Print($"Historical data request: duration reduced from {oldDuration} to {newDuration}");
        }

        private void ReqHistoricalData(int tickerId)
        {
            adapter.Socket.reqHistoricalData(
                tickerId,
                contract,
                end.ToString("yyyyMMdd HH:mm:ss"),
                duration + durationSuffix,
                GetBarSize(span),
                whatToShow,
                useRth,
                formatDate);
        }

        public bool Equals(IBHistoricalDataRequest other)
        {
            return contract == other.contract &&
                   begin == other.begin &&
                   end == other.end &&
                   span == other.span &&
                   string.Equals(whatToShow, other.whatToShow) &&
                   useRth == other.useRth &&
                   formatDate == other.formatDate;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is IBHistoricalDataRequest && Equals((IBHistoricalDataRequest)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = contract.GetHashCode();
                hashCode = (hashCode * 397) ^ begin.GetHashCode();
                hashCode = (hashCode * 397) ^ end.GetHashCode();
                hashCode = (hashCode * 397) ^ span.GetHashCode();
                hashCode = (hashCode * 397) ^ whatToShow.GetHashCode();
                hashCode = (hashCode * 397) ^ useRth;
                hashCode = (hashCode * 397) ^ formatDate;
                return hashCode;
            }
        }

        private static string GetBarSize(HistoryProviderSpan span)
        {
            string barSizeSetting;
            switch (span)
            {
                case HistoryProviderSpan.Minute:
                    barSizeSetting = "1 min";
                    break;
                case HistoryProviderSpan.Minute5:
                    barSizeSetting = "5 mins";
                    break;
                case HistoryProviderSpan.Minute10:
                    barSizeSetting = "10 mins";
                    break;
                case HistoryProviderSpan.Minute15:
                    barSizeSetting = "15 mins";
                    break;
                case HistoryProviderSpan.Minute30:
                    barSizeSetting = "30 mins";
                    break;
                case HistoryProviderSpan.Hour:
                    barSizeSetting = "1 hour";
                    break;
                case HistoryProviderSpan.Hour4:
                    barSizeSetting = "4 hours";
                    break;
                case HistoryProviderSpan.Day:
                    barSizeSetting = "1 day";
                    break;
                case HistoryProviderSpan.Week:
                    barSizeSetting = "1 week";
                    break;
                case HistoryProviderSpan.Month:
                    barSizeSetting = "1 month";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(span), span, null);
            }

            return barSizeSetting;
        }
    }
}

