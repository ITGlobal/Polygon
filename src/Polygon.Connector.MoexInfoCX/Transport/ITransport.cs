using System;

namespace Polygon.Connector.MoexInfoCX.Transport
{
    internal interface ITransport : IDisposable
    {
        void Connect();
        void SendMessage(string message);
    }
}
