using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение о статусе заявки.
    /// </summary>
    [Serializable, ObjectName("OSCM"), PublicAPI]
    public sealed class OrderStateChangeMessage : Message
    {
        #region Properties

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
        /// <remarks>
        ///     Это поле имеет не бессмысленное значение только когда заявка частично или польностью исполняется.
        /// </remarks>
        public uint? FilledQuantity { get; set; }

        /// <summary>
        ///     Идентификатор ордера, который присваивает ему биржа.
        /// </summary>
        public string OrderExchangeId { get; set; }

        /// <summary>
        ///     Идентификатор транзакции.
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        ///     Текущий статус заявки.
        /// </summary>
        public OrderState? State { get; set; }

        /// <summary>
        /// Время изменения статуса заявки
        /// </summary>
        public DateTime ChangeTime { get; set; }

        #endregion

        #region .ctor

        /// <summary>
        ///     .ctor
        /// </summary>
        public OrderStateChangeMessage() { }

        /// <summary>
        ///     .ctor
        /// </summary>
        public OrderStateChangeMessage(Guid transactionId, string orderExchangeId, DateTime moment)
        {
            TransactionId = transactionId;
            OrderExchangeId = orderExchangeId;
            ChangeTime = moment;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Запускает посетителя.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя для обработки.
        /// </param>
        public override void Accept(IMessageVisitor visitor) => visitor.Visit(this);

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.TransactionId, TransactionId);
            fmt.AddFieldRequired(LogFieldNames.ExchangeOrderId, OrderExchangeId);
            fmt.AddField(LogFieldNames.Time, ChangeTime);
            fmt.AddEnumField(LogFieldNames.State, State);
            fmt.AddField(LogFieldNames.Price, Price);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            fmt.AddField(LogFieldNames.ActiveQuantity, ActiveQuantity);
            fmt.AddField(LogFieldNames.FilledQuantity, FilledQuantity);
            return fmt.ToString();
        }

        #endregion
    }
}

