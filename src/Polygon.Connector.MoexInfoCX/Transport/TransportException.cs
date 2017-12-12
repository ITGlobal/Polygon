using System;
using Polygon.Connector.MoexInfoCX.Common;

namespace Polygon.Connector.MoexInfoCX.Transport
{
    internal class TransportException : StompException
    {
        public TransportException()
        {
        }

        public TransportException(string message)
            : base(message)
        {
        }

        public TransportException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
