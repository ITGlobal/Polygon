using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение с режимом торгов по инструменту
    /// </summary>
    [Serializable, ObjectName("MARKET_MODE"), PublicAPI]
    public sealed class MarketModeInfo : InstrumentMessage
    {
        /// <summary>
        ///     Режим торгов по инструменту
        /// </summary>
        public MarketMode Mode { get; set; }

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
            fmt.AddField(LogFieldNames.Instrument, Instrument);
            fmt.AddEnumField(LogFieldNames.Mode, Mode);
            return fmt.ToString();
        }
    }
}

