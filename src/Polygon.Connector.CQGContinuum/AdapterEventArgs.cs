using System;

namespace Polygon.Connector.CQGContinuum
{
    internal sealed class AdapterEventArgs<T> : EventArgs
    {
        private readonly T message;
        
        public AdapterEventArgs(T message)
        {
            this.message = message;
        }

        public T Message { get { return message; } }
        public bool Handled { get; private set; }

        public void MarkHandled()
        {
            Handled = true;
        }
    }
}

