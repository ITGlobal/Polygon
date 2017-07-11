using System;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua.Adapter
{
    internal sealed class QLMessageEventArgs : EventArgs
    {
        public QLMessageEventArgs(QLMessage message)
        {
            Message = message;
        }

        public QLMessage Message { get; }
    }
}

