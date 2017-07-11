using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Транзакция на снятие заявки
    /// </summary>
    [Serializable, ObjectName("KILL_ORDER_TRANSACTION"), PublicAPI]
    public sealed class KillOrderTransaction : Transaction
    {
        #region Properties
        
        /// <summary>
        ///     Идентификатор ордера, который присваивает ему биржа.
        /// </summary>
        public string OrderExchangeId { get; set; }

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
            return fmt.ToString();
        }
        
        #endregion
    }
}

