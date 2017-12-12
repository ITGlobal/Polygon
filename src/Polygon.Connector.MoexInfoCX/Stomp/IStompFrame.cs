using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal interface IStompFrame
    {
        string Command { get; }
        IDictionary<string, StringValues> Headers { get; }
        string Body { get; }
    }
}