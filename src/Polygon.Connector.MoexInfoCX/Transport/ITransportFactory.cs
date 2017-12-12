using System;

namespace Polygon.Connector.MoexInfoCX.Transport
{
    internal interface ITransportFactory
    {
        ITransport Create(Uri brokerUrl, ITransportEventHandler handler);
    }
}