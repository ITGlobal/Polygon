using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение о внешней заявке
    /// </summary>
    [Serializable, ObjectName("EXT_ORDER"), PublicAPI]
    public sealed class ExternalOrderMessage : Message
    {
        #region Properties

        /// <summary>
        ///     Заявка
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        ///     Инструмент заявки
        /// </summary>
        [JsonIgnore]
        public Instrument Instrument => Order?.Instrument;

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
            fmt.AddField(LogFieldNames.Order, Order?.OrderExchangeId);
            return fmt.ToString();
        }

        #endregion
    }
}