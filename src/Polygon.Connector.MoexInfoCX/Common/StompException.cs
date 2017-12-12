using System;

namespace Polygon.Connector.MoexInfoCX.Common
{
    public class StompException : Exception
    {
        public StompException()
        {
        }

        public StompException(string message)
            : base(message)
        {
        }

        public StompException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
