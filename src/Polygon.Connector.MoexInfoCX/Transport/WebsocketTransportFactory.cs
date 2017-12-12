using System;

namespace Polygon.Connector.MoexInfoCX.Transport
{
    internal sealed class WebsocketTransportFactory : ITransportFactory
    {
        private readonly WebsocketTransportSettings _websocketSettings;

        public WebsocketTransportFactory(WebsocketTransportSettings websocketSettings)
        {
            _websocketSettings = websocketSettings;
        }

        public WebsocketTransportFactory()
            : this(new WebsocketTransportSettings())
        { }

        public ITransport Create(Uri brokerUrl, ITransportEventHandler handler)
        {
            return new WebsocketTransport(brokerUrl, handler, _websocketSettings);
        }
    }
}