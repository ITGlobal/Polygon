using System.Net;

namespace Polygon.Connector.IQFeed.Lookup
{
    internal sealed class LookupSocketWrapper : SocketWrapper
    {
        public const string RequestIdPrefix = "REQ-";

        public LookupSocketWrapper(IPAddress address, IQFeedParameters parameters)
            : base(address, SocketConnectionType.Lookup, parameters)
        { }

        public event ProcessMessageDelegate OnSecurityTypeMsg;
        public event ProcessMessageDelegate OnResultMsg;
        public event ProcessMessageDelegate OnResultEndMsg;

        public void RequestSymbolLookup(string code, int? type = null, string requestId = null)
        {
            SendCommand(LookupCommands.LookupSymbol(code, type, requestId));
        }

        protected override void OnConnected()
        {
            SendCommand(LookupCommands.RequestSecurityTypes());
        }

        protected override void ProcessMessage(string messageType, string message)
        {
            ProcessMessageDelegate processor;
            string requestId;

            if (messageType.StartsWith(RequestIdPrefix))
            {
                requestId = messageType;

                if (message.StartsWith(LookupMessages.ERROR))
                {
                    message = message.Substring(2);
                    processor = RaiseError;
                }
                else if (message.StartsWith(LookupMessages.ENDMSG))
                {
                    message = "";
                    processor = OnResultEndMsg;
                }
                else
                {
                    processor = OnResultMsg;
                }

            }
            else
            {
                requestId = "";
                message = messageType + "," + message;
                processor = OnSecurityTypeMsg;
            }

            RaiseEvent(processor, new IQMessageArgs(message, requestId));
        }
    }
}

