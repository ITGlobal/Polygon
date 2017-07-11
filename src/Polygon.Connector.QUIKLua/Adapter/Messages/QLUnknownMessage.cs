using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLUnknownMessage)]
    internal sealed class QLUnknownMessage : QLMessage
    {
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            return fmt.ToString();
        }
    }
}

