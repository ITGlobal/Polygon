using System.Net;

namespace Polygon.Connector.IQFeed.Level1
{
    internal sealed class Level1SocketWrapper : SocketWrapper
    {
        public Level1SocketWrapper(IPAddress address)
            : base(address, SocketConnectionType.Level1)
        { }

        public event ProcessMessageDelegate OnFundamentalMsg;
        public event ProcessMessageDelegate OnSummaryMsg;
        public event ProcessMessageDelegate OnSystemMsg;
        public event ProcessMessageDelegate OnOtherMsg;
        public event ProcessMessageDelegate OnTimestampMsg;
        public event ProcessMessageDelegate OnUpdateMsg;
        public event ProcessMessageDelegate OnRegionalMsg;
        public event ProcessMessageDelegate OnNewsMsg;
        public event ProcessMessageDelegate OnSubscriptionErrorMsg;

        public void Subscribe(string code)
        {
            SendCommand(L1Commands.GetSubscribeCommand(code));
        }

        public void Unubscribe(string code)
        {
            SendCommand(L1Commands.GetUnsubscribeCommand(code));
        }

        public void SelectUpdateFields(string[] codes)
        {
            SendCommand(L1Commands.GetSelectUpdateFieldsCommand(codes));
        }

        protected override void ProcessMessage(string messageType, string message)
        {
            ProcessMessageDelegate processor;

            switch (messageType)
            {
                case L1MessageTypes.Update:
                    processor = OnUpdateMsg;
                    break;
                case L1MessageTypes.Fundamental:
                    processor = OnFundamentalMsg;
                    break;
                case L1MessageTypes.Summary:
                    processor = OnSummaryMsg;
                    break;
                case L1MessageTypes.News:
                    processor = OnNewsMsg;
                    break;
                case L1MessageTypes.System:
                    processor = OnSystemMsg;
                    break;
                case L1MessageTypes.Regional:
                    processor = OnRegionalMsg;
                    break;
                case L1MessageTypes.Timestamp:
                    processor = OnTimestampMsg;
                    break;
                case L1MessageTypes.Error:
                    processor = RaiseError;
                    break;
                case L1MessageTypes.SubscriptionError:
                    processor = OnSubscriptionErrorMsg;
                    break;
                default:
                    processor = OnOtherMsg;
                    break;
            }

            RaiseEvent(processor, new IQMessageArgs(message));
        }
    }
}

