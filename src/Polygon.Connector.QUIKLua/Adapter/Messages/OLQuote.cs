using Polygon.Diagnostics;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.OLQuote)]
    internal class OLQuote : IPrintable
    {
        public decimal p { get; set; }
        public uint q { get; set; }

        public override string ToString() => Print(PrintOption.Default);
        
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Price, p);
            fmt.AddField(LogFieldNames.Quantity, q);
            return fmt.ToString();
        }
    }
}

