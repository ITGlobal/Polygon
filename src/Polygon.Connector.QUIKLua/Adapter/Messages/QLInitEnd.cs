using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLInitEnd)]
    internal class QLInitEnd : QLMessage
    {
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            return fmt.ToString();
        }
    }
}

