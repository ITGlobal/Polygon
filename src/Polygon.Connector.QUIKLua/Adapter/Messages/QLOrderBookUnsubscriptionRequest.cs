using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLOrderBookUnsubscriptionRequest)]
    internal class QLOrderBookUnsubscriptionRequest : QLMessage
    {
        public string instrument { get; set; }

        public QLOrderBookUnsubscriptionRequest(string code)
        {
            instrument = code;
        }

        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Instrument, instrument);
            return fmt.ToString();
        }
    }
}

