using System;
using System.Collections.Generic;
using System.Linq;
using Polygon.Messages;
using SpimexAdapter.FTE;

namespace Polygon.Connector.Spimex
{
    internal class OrderBookBuilder
    {
        private const int Capacity = 10;

        private class ShortOrderInfo
        {
            public ShortOrderInfo(string code, decimal price, BuySell buySell)
            {
                Code = code;
                Price = price;
                BuySell = buySell;
            }

            public string Code { get; }
            public decimal Price { get; }
            public BuySell BuySell { get; }


            public long Quantity { get; set; }
        }

        private readonly Instrument instrument;

        private readonly Dictionary<string, ShortOrderInfo> mapOrders = new Dictionary<string, ShortOrderInfo>();

        private readonly IDictionary<decimal, List<ShortOrderInfo>> asks = new SortedList<decimal, List<ShortOrderInfo>>();
        private readonly IDictionary<decimal, List<ShortOrderInfo>> bids = new SortedList<decimal, List<ShortOrderInfo>>(ComparerInstance);

        public OrderBookBuilder(Instrument instrument)
        {
            this.instrument = instrument;
        }

        public bool ProcessOrder(InfoOrder order)
        {
            var code = order.code;
            var price = PriceHelper.ToPrice(order.price);

            var sorted = order.buy_sell == BuySell.BUY ? bids : asks;
            
            if (!mapOrders.TryGetValue(code, out var info))
            {
                //if (string.IsNullOrEmpty(order.security))
                //{
                //    //Logger.DebugFormat("Получено обновление заявки с пустым полем security. OrderId: {0}", order.code);
                //    return false;
                //}

                if (order.qtyLeft == 0 ||
                    order.status == OrderStatus.CANCELED ||
                    order.status == OrderStatus.MATCHED)
                    // TODO Проверить частичное
                    return false;

                info = new ShortOrderInfo(code, price, order.buy_sell);

                mapOrders[order.code] = info;

                List<ShortOrderInfo> list;
                if (!sorted.TryGetValue(price, out list))
                {
                    sorted[price] = list = new List<ShortOrderInfo>();
                }
                list.Add(info);
            }

            info.Quantity = order.qtyLeft;

            // Очищаем лишние заявки (в статусах Matched и Cancelled)
            if (order.qtyLeft == 0 ||
                order.status == OrderStatus.CANCELED ||
                order.status == OrderStatus.MATCHED)
            // TODO Проверить частичное
            {
                mapOrders.Remove(order.code);
                
                if (sorted.TryGetValue(price, out var list))
                {
                    list.Remove(info);
                    if (!list.Any())
                    {
                        sorted.Remove(price);
                    }
                }
            }

            return true;
        }

        public OrderBook BuildBook()
        {
            var asksArray = EnumerateBook(OrderOperation.Sell);
            var bidsArray = EnumerateBook(OrderOperation.Buy);
            var size = asksArray.Length + bidsArray.Length;

            var ob = new OrderBook(size)
            {
                Instrument = instrument,
            };

            var i = 0;
            for (; i < asksArray.Length; i++)
            {
                ob.Items.Add(asksArray[asksArray.Length - i - 1]);
            }

            for (; i < size; i++)
            {
                ob.Items.Add(bidsArray[i - asksArray.Length]);
            }

            //var prices = mapOrders.Values
            //    .Select(_ => _.Price)
            //    .Distinct()
            //    .OrderByDescending(_ => _)
            //    .ToArray();

            //var ob1 = new OrderBook
            //{
            //    Instrument = instrument,
            //    Items = (from p in prices
            //             let orders = mapOrders.Values.Where(o => o.Price == p)
            //             select new OrderBookItem(
            //                 orders.First().BuySell == BuySell.BUY ? OrderOperation.Buy : OrderOperation.Sell, 
            //                 p, 
            //                 orders.Sum(_ => _.Quantity))).ToList()
            //};

            return ob;
        }

        private OrderBookItem[] EnumerateBook(OrderOperation operation)
        {
            var sorted = operation == OrderOperation.Buy ? bids : asks;

            var size = Math.Min(Capacity, sorted.Count);
            var rArray = new OrderBookItem[size];

            var c = 0;
            foreach (var pair in sorted)
            {
                var price = pair.Key;
                var list = pair.Value;

                rArray[c] = new OrderBookItem(operation, price, list.Sum(_ => _.Quantity));

                if (++c >= Capacity)
                {
                    break;
                }
            }

            return rArray;
        }

        private static readonly IComparer<decimal> ComparerInstance = new InverceComparer<decimal>();
        private class InverceComparer<T> : IComparer<T>
            where T : IComparable<T>
        {
            public int Compare(T x, T y)
            {
                return -x.CompareTo(y);
            }
        }
    }
}
