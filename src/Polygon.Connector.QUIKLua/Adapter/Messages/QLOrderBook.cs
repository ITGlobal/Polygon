using System.Collections.Generic;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLOrderBook)]
    internal class QLOrderBook : QLMessage
    {
        public string instrument { get; set; }
        public List<OLQuote> offers { get; set; }
        public List<OLQuote> bids { get; set; }

        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Instrument, instrument);
            fmt.AddListField(LogFieldNames.Offers, offers);
            fmt.AddListField(LogFieldNames.Bids, bids);
            return fmt.ToString();
        }
    }
}

