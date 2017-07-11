using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    /// <summary>
    /// Сообщение в квик для подписки на обновление свечей
    /// </summary>
    [ObjectName(QLObjectNames.QLHistoryDataSubscription)]
    internal class QLHistoryDataSubscription : QLMessage
    {
        public enum SubscriptionAction
        {
            Subscribe,
            Unsubscribe
        };

        public override QLMessageType message_type
        {
            get { return QLMessageType.CandlesSubscription; }
        }

        public Guid id { get; set; }

        public string instrument { get; set; }

        public HistoryProviderSpan span { get; set; }

        public DateTime since { get; set; }

        public SubscriptionAction Action { get; set; }

        public QLHistoryDataSubscription(string code, DateTime since, HistoryProviderSpan span, SubscriptionAction action = SubscriptionAction.Subscribe)
        {
            instrument = code;
            this.since = new DateTime(since.Ticks, DateTimeKind.Local);
            this.span = span;
            id = Guid.NewGuid();
            this.Action = action;
        }

        #region IPrintable
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Id, id);
            fmt.AddField(LogFieldNames.Instrument, instrument);
            fmt.AddEnumField(LogFieldNames.Span, span);
            return fmt.ToString();
        } 
        #endregion
    }
}

