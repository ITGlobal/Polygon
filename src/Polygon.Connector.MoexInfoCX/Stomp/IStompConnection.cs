using System;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal interface IStompConnection : IDisposable
    {
        StompConnectionState State { get; }
        event EventHandler<StompConnectionStateChangedEventArgs> StateChanged;
        event EventHandler<StompMessageReceivedEventArgs> MessageReceived;
        void Send(IStompFrame frame);
        void Connect();
        void Disconnect();
    }
}