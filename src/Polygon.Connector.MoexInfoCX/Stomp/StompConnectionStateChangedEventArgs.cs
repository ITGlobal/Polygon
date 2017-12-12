using System;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal sealed class StompConnectionStateChangedEventArgs : EventArgs
    {
        public StompConnectionStateChangedEventArgs(StompConnectionState state)
        {
            State = state;
        }

        public StompConnectionState State { get; }
    }
}
