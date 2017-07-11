using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Котировка из стакана.
    /// </summary>
    [Serializable, ObjectName("ORDER_BOOK_ROW"), DebuggerDisplay("{ToString()}"), PublicAPI]
    public sealed class OrderBookItem : IPrintable
    {
        #region .ctor

        /// <summary>
        ///     Конструктор
        /// </summary>
        public OrderBookItem () { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public OrderBookItem(OrderOperation operation, decimal price, long quantity)
        {
            Operation = operation;
            Price = price;
            Quantity = quantity;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Операция котировки.
        /// </summary>
        public OrderOperation Operation { get; set; }

        /// <summary>
        ///     Цена котировки.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Объём котировки.
        /// </summary>
        public long Quantity { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string ToString() => Print(PrintOption.Default);

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddEnumField(LogFieldNames.Operation, Operation);
            fmt.AddField(LogFieldNames.Price, Price);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            return fmt.ToString();
        }

        #endregion
    }
}

