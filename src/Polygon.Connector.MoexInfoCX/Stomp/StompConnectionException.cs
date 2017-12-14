using System;
using Polygon.Connector.MoexInfoCX.Common;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal class StompConnectionException : StompException
    {
        public StompConnectionException()
        {
        }

        public StompConnectionException(string message)
            : base(message)
        {
        }

        public StompConnectionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
