using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Ответ на запрос об инициализаии клиента.
    /// </summary>
    [Serializable, ObjectName("INIT_RESPONSE"), PublicAPI]
    public sealed class InitResponseMessage : Message
    {
        /// <summary>
        ///     Список фидов в сервере шлюзов.
        /// </summary>
        [CanBeNull]
        public IDictionary<string, Instrument[]> Feeds { get; set; }

        /// <summary>
        ///     Список раутеров заявок в сервере шлюзов.
        /// </summary>
        [CanBeNull]
        public IDictionary<string, string[]> OrderRouters { get; set; }

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

        /// <inheritdoc />
        public override string ToString() => Print(PrintOption.Default);

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            if (Feeds != null)
            {
                fmt.AddListField(LogFieldNames.Feeds, from p in Feeds select p.Key);
            }

            if (OrderRouters != null)
            {
                fmt.AddListField(LogFieldNames.Routers, from p in OrderRouters select p.Key);
            }
            return fmt.ToString();
        }
    }
}

