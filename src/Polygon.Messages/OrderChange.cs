using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Изменения в заявке
    /// </summary>
    [Serializable, ObjectName("ORDER_CHANGE"), DebuggerDisplay("{ToString()}"), PublicAPI]
    public struct OrderChange : IEquatable<OrderChange>, IPrintable
    {
        #region properties

        /// <summary>
        ///     Нет изменений
        /// </summary>
        public static readonly OrderChange NotChanged = new OrderChange();

        /// <summary>
        ///     Цена заявки
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        ///     Объем заявки
        /// </summary>
        public uint? Quantity { get; set; }

        /// <summary>
        ///     Оставшееся не исполненое количество.
        /// </summary>
        public uint? ActiveQuantity { get; set; }

        /// <summary>
        ///     Исполнившееся количество.
        /// </summary>
        public uint? FilledQuantity { get; set; }

        /// <summary>
        ///     Идентификатор ордера, который присваивает ему биржа.
        /// </summary>
        public string OrderExchangeId { get; set; }

        /// <summary>
        ///     Изменение статуса заявки.
        /// </summary>
        public OrderState? State { get; set; }
        
        #endregion
        
        #region methods

        /// <summary>
        ///     Скопировать изменения заявки в сообщение
        /// </summary>
        public void CopyTo(OrderStateChangeMessage message)
        {
            message.Price = Price;
            message.Quantity = Quantity;
            message.ActiveQuantity = ActiveQuantity;
            message.FilledQuantity = FilledQuantity;
            message.OrderExchangeId = OrderExchangeId;
            message.State = State;
        }

        /// <inheritdoc />
        public override string ToString() => Print(PrintOption.Default);

        /// <inheritdoc />
        public bool Equals(OrderChange other)
        {
            return
                Price == other.Price &&
                Quantity == other.Quantity &&
                ActiveQuantity == other.ActiveQuantity &&
                FilledQuantity == other.FilledQuantity &&
                string.Equals(OrderExchangeId, other.OrderExchangeId) &&
                State == other.State;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is OrderChange && Equals((OrderChange)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Price.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity.GetHashCode();
                hashCode = (hashCode * 397) ^ ActiveQuantity.GetHashCode();
                hashCode = (hashCode * 397) ^ FilledQuantity.GetHashCode();
                hashCode = (hashCode * 397) ^ (OrderExchangeId != null ? OrderExchangeId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ State.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.ExchangeOrderId, OrderExchangeId);
            fmt.AddField(LogFieldNames.Price, Price);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            fmt.AddField(LogFieldNames.ActiveQuantity, ActiveQuantity);
            fmt.AddField(LogFieldNames.FilledQuantity, FilledQuantity);
            fmt.AddEnumField(LogFieldNames.State, State);
            return fmt.ToString();
        }

        /// <summary>
        ///     Оператор равенства
        /// </summary>
        public static bool operator ==(OrderChange left, OrderChange right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Оператор неравенства
        /// </summary>
        public static bool operator !=(OrderChange left, OrderChange right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}

