using System;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLOrderBookSubscriptionRequest)]
    internal class QLOrderBookSubscriptionRequest : QLMessage
    {
        public override QLMessageType message_type
        {
            get { return QLMessageType.OrderBookSubscriptionRequest; }
        }

        public Guid id { get; set; }

        public string instrument { get; set; }

        public QLOrderBookSubscriptionRequest(string code)
        {
            instrument = code;
            id = Guid.NewGuid();
        }

        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Instrument, instrument);
            fmt.AddField(LogFieldNames.Id, id);
            return fmt.ToString();
        }
    }
}

