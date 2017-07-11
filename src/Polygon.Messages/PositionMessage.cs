using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Класс содержит информацию о позиции по заданному инструменту на заданном счете.
    /// </summary>
    [Serializable, ObjectName("POSITION"), PublicAPI]
    public sealed class PositionMessage : AccountMessage
    {
        #region Properties

        /// <summary>
        ///     Инструмент, по которому эта позиция.
        /// </summary>
        public Instrument Instrument { get; set; }

        /// <summary>
        ///     Количество лотов в позиции.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     Цена позиции
        /// </summary>
        public decimal? Price { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Запускает посетителя.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя для обработки.
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
            fmt.AddField(LogFieldNames.Account, Account);
            fmt.AddField(LogFieldNames.ClientCode, ClientCode);
            fmt.AddField(LogFieldNames.Instrument, Instrument);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            fmt.AddField(LogFieldNames.Price, Price);
            return fmt.ToString();
        }

        #endregion
    }
}

