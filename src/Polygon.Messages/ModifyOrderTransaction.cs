using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Транзакция на модификацию заявки
    /// </summary>
    [Serializable, ObjectName("MODIFY_ORDER_TRANSACTION"), PublicAPI]
    public sealed class ModifyOrderTransaction : Transaction
    {
        #region Properties

        /// <summary>
        ///     Идентификатор ордера, который присваивает ему биржа.
        /// </summary>
        public string OrderExchangeId { get; set; }

        /// <summary>
        ///     Новый объем заявки
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        ///     Новая цена заявки
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Комментарий заявки.
        /// </summary>
        public string Comment { get; set; }

        #endregion

        #region Overrides of Transaction

        /// <summary>
        ///     Принять посетителя
        /// </summary>
        /// <param name="visitor">
        ///     Посетитель
        /// </param>
        public override void Accept(ITransactionVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            PrintCommonProperties(fmt);
            fmt.AddFieldRequired(LogFieldNames.ExchangeOrderId, OrderExchangeId);
            fmt.AddFieldRequired(LogFieldNames.Quantity, Quantity);
            fmt.AddFieldRequired(LogFieldNames.Price, Price);
            fmt.AddFieldRequired(LogFieldNames.Comment, Comment);
            return fmt.ToString();
        }
        
        #endregion
    }
}

