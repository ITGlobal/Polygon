using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Connector;
using Polygon.Messages;
using Polygon.Connector.QUIKLua.Adapter;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua
{
    /// <summary>
    /// Хендлер подписки на исторические данные из квика
    /// </summary>
    internal sealed class HistoryDataSubscription : IHistoryDataSubscription
    {
        #region Fields

        private QLAdapter adapter;

        private readonly IHistoryDataConsumer consumer;

        #endregion

        #region Public properties

        public Guid Id { get; set; }

        public Instrument Instrument { get; set; }

        public DateTime Since { get; set; }

        public HistoryProviderSpan Span { get; set; } 

        #endregion

        public HistoryDataSubscription(Guid id, Instrument instrument, DateTime since, HistoryProviderSpan span, QLAdapter adapter, IHistoryDataConsumer consumer)
        {
            this.Id = id;
            this.Instrument = instrument;
            this.Span = span;
            this.Since = since;
            this.adapter = adapter;
            this.consumer = consumer;
        }

        #region IHistoryDataSubscription
        public void Dispose()
        {
            // отправляем сообщение на отписку от обновлений TODO обработать в LUA
            adapter.SendMessage(new QLHistoryDataSubscription(Instrument.Code, Since, Span, QLHistoryDataSubscription.SubscriptionAction.Unsubscribe));
        }
        #endregion

        #region Public methods
        
        public void ProcessUpdate(QLHistoryDataUpdate update)
        {
            var hd = new HistoryData(Instrument, update.begin, update.end, Span);
            foreach (var candle in update.candles)
                hd.Points.Add(new HistoryDataPoint(candle.Time, candle.h, candle.l, candle.o, candle.c, 0, 0));

            consumer.Update( hd, update.update_type == "added" ? HistoryDataUpdateType.OnePointAdded : update.update_type == "updated" ? HistoryDataUpdateType.OnePointUpdated : HistoryDataUpdateType.Batch);
        } 

        #endregion
    }
}

