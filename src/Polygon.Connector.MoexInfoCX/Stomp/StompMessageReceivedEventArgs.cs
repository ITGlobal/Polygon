using System;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal sealed class StompMessageReceivedEventArgs : EventArgs
    {
        public StompMessageReceivedEventArgs(IStompFrame frame)
        {
            Message = frame;
        }

        public IStompFrame Message { get; }
    }
}