using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    /// <summary>
    /// Обновление свечей по подписке
    /// </summary>
    internal class QLHistoryDataUpdate : QLMessage
    {
        public override QLMessageType message_type
        {
            get { return QLMessageType.CandlesUpdate; }
        }

        public Guid id { get; set; }

        public string instrument { get; set; }

        public HistoryProviderSpan span { get; set; }

        public DateTime begin { get; set; }

        public DateTime end { get; set; }

        public List<QLCandle> candles { get; set; }

        public string update_type { get; set; }

        #region IPrintable
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Id, id);
            fmt.AddField(LogFieldNames.Instrument, instrument);
            fmt.AddEnumField(LogFieldNames.Span, span);
            fmt.AddListField(LogFieldNames.Candles, candles);
            return fmt.ToString();
        }
        #endregion
    }
}

