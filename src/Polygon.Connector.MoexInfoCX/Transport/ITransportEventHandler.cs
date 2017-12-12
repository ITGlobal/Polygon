using System;

namespace Polygon.Connector.MoexInfoCX.Transport
{
    internal interface ITransportEventHandler
    {
        void OnConnected();
        void OnDisconnected();
        void OnMessageReceived(string message);
        void OnDataReceived(string message);
        void OnError(string message, Exception exception);
    }
}