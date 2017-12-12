using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Primitives;

namespace Polygon.Connector.MoexInfoCX.Transport
{
    internal sealed class WebsocketTransportSettings
    {
        public int ReceiveBufferSize { get; set; } = 8 * 1024 /* 8Kb */;

        public Dictionary<string, StringValues> HttpHeaders { get; set; } = new Dictionary<string, StringValues>();

        public X509CertificateCollection SslCertificates { get; set; }
    }
}