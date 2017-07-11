using System;
using System.Collections.Generic;
using Polygon.Diagnostics;


namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLHistoryDataResponse)]
    internal class QLHistoryDataResponse : QLMessage
    {
        public override QLMessageType message_type
        {
            get { return QLMessageType.CandlesResponse; }
        }

        public Guid id { get; set; }

        public string instrument { get; set; }

        /// <summary>
        /// Тип обновления, "added" или "updated"
        /// </summary>
        public string update_type { get; set; }

        public List<QLCandle> candles { get; set; }
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Id, id);
            fmt.AddField(LogFieldNames.Instrument, instrument);
            fmt.AddListField(LogFieldNames.Items, candles);
            return fmt.ToString();
        }
    }
}

