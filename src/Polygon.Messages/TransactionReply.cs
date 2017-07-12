using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение-ответ на транзакцию
    /// </summary>
    [Serializable, ObjectName("REPLY"), PublicAPI]
    public sealed class TransactionReply : Message
    {
        /// <summary>
        ///     Идентификатор транзакции.
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        ///     Транзакция выполнена/зафэйлена.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Сообщение ответа на транзакцию.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Запуск посетителя для обработки сообщений.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя.
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
            fmt.AddField(LogFieldNames.TransactionId, TransactionId);
            fmt.AddField(LogFieldNames.Success, Success);
            fmt.AddField(LogFieldNames.Message, Message);
            return fmt.ToString();
        }

        /// <summary>
        ///     Создать ответ на транзакцию, обозначающий успешное ее исполнение
        /// </summary>
        public static TransactionReply Accepted(Guid transactionId, string message = null)
        {
            return new TransactionReply
            {
                TransactionId = transactionId,
                Success = true,
                Message = message
            };
        }

        /// <summary>
        ///     Создать ответ на транзакцию, обозначающий успешное ее исполнение
        /// </summary>
        public static TransactionReply Accepted(Transaction transaction, string message = null)
            => Accepted(transaction.TransactionId, message);

        /// <summary>
        ///     Создать ответ на транзакцию, обозначающий неуспешное ее исполнение
        /// </summary>
        public static TransactionReply Rejected(Guid transactionId, string message = null)
        {
            return new TransactionReply
            {
                TransactionId = transactionId,
                Success = false,
                Message = message
            };
        }

        /// <summary>
        ///     Создать ответ на транзакцию, обозначающий неуспешное ее исполнение
        /// </summary>}
        public static TransactionReply Rejected(Transaction transaction, string message = null) 
            => Rejected(transaction.TransactionId, message);
    }
}