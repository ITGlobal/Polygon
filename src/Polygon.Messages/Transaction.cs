using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение-транзакция (new, kill, modify)
    /// </summary>
    [Serializable, DebuggerDisplay("{ToString()}"), PublicAPI]
    public abstract class Transaction : Message
    {
        #region Properties

        /// <summary>
        ///     Идентификатор транзакции.
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        ///     Номер счета заявки.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        ///     Код клиента.
        /// </summary>
        public string ClientCode { get; set; }

        /// <summary>
        ///     Информация об инструменте.
        /// </summary>
        public Instrument Instrument { get; set; }

        /// <summary>
        ///     Является ли транзакция "ручной", т.е. заданной пользователем. 
        ///     true - транзакция введена пользователем.
        ///     false - алго-транзакция.
        /// </summary>
        public bool IsManual { get; set; }

        // ReSharper disable once UnusedMember.Local используется в DebuggerDisplay
        private string DebugView => ToString();

        #endregion

        #region ToString convertion
        
        /// <summary>
        ///     Вывод на печать свойств из класса <see cref="Transaction"/>
        /// </summary>
        /// <param name="fmt"></param>
        protected void PrintCommonProperties(ObjectLogFormatter fmt)
        {
            fmt.AddField(LogFieldNames.TransactionId, TransactionId);
            fmt.AddField(LogFieldNames.Instrument, Instrument?.Code);
            fmt.AddField(LogFieldNames.Account, Account);
            fmt.AddField(LogFieldNames.ClientCode, ClientCode);
            fmt.AddField(LogFieldNames.IsManual, IsManual);
        }

        #endregion

        #region Visitor

        /// <summary>
        ///     Принять посетителя
        /// </summary>
        /// <param name="visitor">
        ///     Посетитель
        /// </param>
        public virtual void Accept(ITransactionVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        #endregion
    }
}

