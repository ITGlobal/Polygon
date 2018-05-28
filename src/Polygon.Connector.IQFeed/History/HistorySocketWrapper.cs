using System;
using System.Net;

namespace Polygon.Connector.IQFeed.History
{
    internal sealed class HistorySocketWrapper : SocketWrapper
    {
        public const string HistoryRequestIdPefix = "hi";

        public HistorySocketWrapper(IPAddress address, IQFeedParameters parameters)
            : base(address, SocketConnectionType.Lookup, parameters)
        { }

        public event ProcessMessageDelegate OnHistoryMsg;
        public event ProcessMessageDelegate OnHistoryEndMsg;
        public event ProcessMessageDelegate OnOtherMsg;

        public void RequestHistoryData(string code, DateTime begin, DateTime end, HistoryProviderSpan span, string requestId)
        {
           var interval = span.ToTimeSpan();

            // Приводим begin и end к нормальному виду: переводим в правильную зону и округляем до периода
            begin = IQFeedParser.ToIQFeedTime(begin);
            begin = IQFeedParser.RoundDateTime(begin, interval);
            end = IQFeedParser.ToIQFeedTime(end);
            end = IQFeedParser.RoundDateTime(end, interval) + interval;     

            string command;
            switch (span)
            {
                case HistoryProviderSpan.Minute:
                    command = HistoryMessages.HIT(code, 60, begin, end, requestId);
                    break;
                case HistoryProviderSpan.Minute5:
                    command = HistoryMessages.HIT(code, 5*60, begin, end, requestId);
                    break;
                case HistoryProviderSpan.Minute10:
                    command = HistoryMessages.HIT(code, 10 * 60, begin, end, requestId);
                    break;
                case HistoryProviderSpan.Minute15:
                    command = HistoryMessages.HIT(code, 15 * 60, begin, end, requestId);
                    break;
                case HistoryProviderSpan.Minute30:
                    command = HistoryMessages.HIT(code, 30 * 60, begin, end, requestId);
                    break;
                case HistoryProviderSpan.Hour:
                    command = HistoryMessages.HIT(code, 3600, begin, end, requestId);
                    break;
                case HistoryProviderSpan.Hour4:
                    command = HistoryMessages.HIT(code, 4*3600, begin, end, requestId);
                    break;

                case HistoryProviderSpan.Day:
                    command = HistoryMessages.HDT(code, begin, end, requestId);
                    break;

                case HistoryProviderSpan.Week:
                    command = HistoryMessages.HWX(code, begin, end, requestId);
                    break;

                case HistoryProviderSpan.Month:
                    command = HistoryMessages.HMX(code, begin, end, requestId); 
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(span), span, null);
            }
            
            SendCommand(command);
        }

        protected override void ProcessMessage(string messageType, string message)
        {
            ProcessMessageDelegate processor;
            string requestId;

            if (messageType.StartsWith(HistoryRequestIdPefix))
            {
                requestId = messageType;

                if (message.StartsWith(HistoryMessages.ERROR))
                {
                    message = message.Substring(2);
                    processor = RaiseError;
                }
                else if (message.StartsWith(HistoryMessages.ENDMSG))
                {
                    message = "";
                    processor = OnHistoryEndMsg;
                }
                else
                {
                    processor = OnHistoryMsg;
                }

            }
            else
            {
                requestId = "";
                switch (messageType)
                {
                    case HistoryMessages.E:
                        processor = RaiseError;
                        break;
                    default:
                        processor = OnOtherMsg;
                        break;
                }
            }

            RaiseEvent(processor, new IQMessageArgs(message, requestId));
        }
    }
}

