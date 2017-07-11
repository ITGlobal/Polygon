using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение, пересылаемое между сервером и клиентом.
    /// </summary>
    [Serializable, DebuggerDisplay("{ToString()}"), PublicAPI]
    public abstract class Message : IPrintable
    {
        /// <summary>
        ///     Запуск посетителя для обработки сообщений.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя.
        /// </param>
        public virtual void Accept(IMessageVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc />
        public override string ToString() => Print(PrintOption.Default);

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public abstract string Print(PrintOption option);
    }
}

