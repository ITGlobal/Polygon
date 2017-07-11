using System;
using System.Collections.Generic;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class OrderBookBuilder
    {
        private readonly ILockObject syncRoot = DeadlockMonitor.Cookie<OrderBookBuilder>();

        private readonly Instrument instrument;
        private readonly List<OrderBookItem> bids = new List<OrderBookItem>();
        private readonly List<OrderBookItem> asks = new List<OrderBookItem>();
        private int marketDepth;

        public OrderBookBuilder(Instrument instrument)
        {
            this.instrument = instrument;
            MarketDepth = IBFeed.DefaultMarketDepth;
        }

        public int MarketDepth
        {
            get { using (syncRoot.Lock()) return marketDepth; }
            set { using (syncRoot.Lock()) marketDepth = value; }
        }

        public OrderBook Update(int position, int operation, int side, double price, int size)
        {
            using (syncRoot.Lock())
            {
                // Выбираем нужную сторону стакана
                List<OrderBookItem> targetList;
                switch (side)
                {
                    case 0:
                        targetList = asks;
                        break;
                    case 1:
                        targetList = bids;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(side));
                }

                // Дополняем стакан до нужной глубины
                var op = side == 0 ? OrderOperation.Sell : OrderOperation.Buy;
                while (targetList.Count <= position)
                {
                    targetList.Add(new OrderBookItem { Operation = op });
                }

                var item = targetList[position];
                switch (operation)
                {
                    case 0:
                        // Добавляем строку в стакан
                        item.Price = (decimal)price;
                        item.Quantity = size;
                        break;
                    case 1:
                        // Обновляем строку в стакане
                        item.Price = (decimal)price;
                        item.Quantity = size;
                        break;
                    case 2:
                        // Удаляем строку из стакана
                        item.Price = (decimal)price;
                        item.Quantity = 0;
                        break;
                }

                // Собираем стакан
                return BuildOrderBook();
            }
        }

        private OrderBook BuildOrderBook()
        {
            var orderBook = new OrderBook(MarketDepth) { Instrument = instrument };

            for (var index = asks.Count - 1; index >= 0; index--)
            {
                var item = asks[index];
                if (item.Quantity != 0)
                {
                    orderBook.Items.Add(item);
                }
            }

            for (var index = 0; index < bids.Count; index++)
            {
                var item = bids[index];
                if (item.Quantity != 0)
                {
                    orderBook.Items.Add(item);
                }
            }

            return orderBook;
        }
    }
}

