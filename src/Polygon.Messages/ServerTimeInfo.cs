using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение с информацией о серверном времени.
    /// </summary>
    [Serializable, ObjectName("TIME"), PublicAPI]
    public sealed class ServerTimeInfo : Message
    {
        /// <summary>
        ///     Серверное время.
        /// </summary>
        public DateTime ServerTime { get; set; }

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
            fmt.AddField(LogFieldNames.Time, ServerTime);
            return fmt.ToString();
        }
    }
}

