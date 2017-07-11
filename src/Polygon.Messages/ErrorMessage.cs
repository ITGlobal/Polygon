using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение об ошибке.
    /// </summary>
    [Serializable, ObjectName("ERROR"), PublicAPI]
    public sealed class ErrorMessage : Message
    {
        #region Properties

        /// <summary>
        ///     Описание ошибки.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Стек вызовов во время возникновения ошибки.
        /// </summary>
        public string StackTrace { get; set; }

        #endregion

        #region Methods

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
            fmt.AddField(LogFieldNames.Message, Message);
            fmt.AddField(LogFieldNames.StackTrace, StackTrace);
            return fmt.ToString();
        }

        #endregion
    }
}

