using System;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    /// <summary>
    /// Запрос исторических данных
    /// </summary>
    [ObjectName(QLObjectNames.QLHistoryDataRequest)]
    internal class QLHistoryDataRequest : QLMessage
    {
        public override QLMessageType message_type
        {
            get { return QLMessageType.CandlesRequest; }
        }

        public Guid id { get; set; }

        public string instrument { get; set; }
        
        public HistoryProviderSpan span { get; set; }

        public QLHistoryDataRequest(string code, HistoryProviderSpan span)
        {
            instrument = code;
            this.span = span;
            id = Guid.NewGuid();
        }
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Id, id);
            fmt.AddField(LogFieldNames.Instrument, instrument);
            fmt.AddEnumField(LogFieldNames.Span, span);
            return fmt.ToString();
        }
    }
}

