using System;

namespace Polygon.Connector.InteractiveBrokers
{
    [Serializable]
    internal class TransactionRejectedException : Exception
    {
        public TransactionRejectedException()
        {
        }

        public TransactionRejectedException(string message) : base(message)
        {
        }

        public TransactionRejectedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

