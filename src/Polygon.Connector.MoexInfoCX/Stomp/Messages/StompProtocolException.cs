using System;
using Polygon.Connector.MoexInfoCX.Common;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class StompProtocolException : StompException
    {
        public StompProtocolException()
        {
        }

        public StompProtocolException(string message)
            : base(message)
        {
        }

        public StompProtocolException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
