using Polygon.Diagnostics;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLInitBegin)]
    internal class QLInitBegin : QLMessage
    {
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            return fmt.ToString();
        }
    }
}

