using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Транзакция на постановку новой заявки
    /// </summary>
    [Serializable, ObjectName("NEW_ORDER_TRANSACTION"), PublicAPI]
    public sealed class NewOrderTransaction : Transaction
    {
        #region Properties

        /// <summary>
        ///     Комментарий заявки.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     Условие исполнения заявки.
        /// </summary>
        public OrderExecutionCondition ExecutionCondition { get; set; }

        /// <summary>
        ///     Признак заявки маркетмэйкера.
        /// </summary>
        public bool IsMarketMakerOrder { get; set; }

        /// <summary>
        ///     Операция заявки.
        /// </summary>
        public OrderOperation Operation { get; set; }

        /// <summary>
        ///     Цена заявки.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Количество контрактов/лотов/акций.
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        ///     Тип заявки (маркет/лимит).
        /// </summary>
        public OrderType Type { get; set; }
        
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
            fmt.AddField(LogFieldNames.Price, Price);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            fmt.AddEnumField(LogFieldNames.Operation, Operation);
            fmt.AddEnumField(LogFieldNames.Type, Type);
            fmt.AddEnumField(LogFieldNames.ExecutionCondition, ExecutionCondition);
            fmt.AddField(LogFieldNames.IsMarketMakerOrder, IsMarketMakerOrder);
            fmt.AddField(LogFieldNames.Comment, Comment);
            return fmt.ToString();
        }
        
        #endregion
    }
}

