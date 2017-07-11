using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Стакан со сделками.
    /// </summary>
    [Serializable, ObjectName("ORDER_BOOK"), PublicAPI]
    public sealed class OrderBook : InstrumentMessage
    {
        #region .ctor

        /// <summary>
        ///     Создаёт новый экземпляр стакана.
        /// </summary>
        public OrderBook()
        {
            Items = new List<OrderBookItem>();
        }

        /// <summary>
        ///     Создаёт новый экземпляр стакана с заданым объёмом.
        /// </summary>
        public OrderBook(int capacity)
        {
            Items = new List<OrderBookItem>(capacity);
        }

        /// <summary>
        ///     Создаёт новый экземпляр стакана с заранее отсортированными строками.
        /// </summary>
        public OrderBook(IEnumerable<OrderBookItem> orderedItems)
        {
            Items = new List<OrderBookItem>(orderedItems);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Список сделок.
        /// </summary>
        public IList<OrderBookItem> Items { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///     A new object that is a copy of this instance.
        /// </returns>
        public OrderBook Clone()
        {
            var items = new OrderBookItem[Items.Count];
            for (var i = 0; i < Items.Count; i++)
            {
                items[i] = new OrderBookItem
                {
                    Operation = Items[i].Operation,
                    Price = Items[i].Price,
                    Quantity = Items[i].Quantity
                };
            }

            return new OrderBook { Instrument = Instrument, Items = items };
        }

        /// <summary>
        ///     Запускает посетителя.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя для обработки.
        /// </param>
        public override void Accept(IMessageVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Instrument, Instrument);
            fmt.AddListField(LogFieldNames.Items, Items);
            return fmt.ToString();
        }

        #endregion
    }
}

