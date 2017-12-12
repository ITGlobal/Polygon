using System;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal class StompWireFormatException : Exception
    {
        public StompWireFormatException()
        {
        }

        public StompWireFormatException(string message)
            : base(message)
        {
        }

        public StompWireFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}