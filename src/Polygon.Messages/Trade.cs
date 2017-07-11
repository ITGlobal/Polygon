using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщение о сделке.
    /// </summary>
    [Serializable, ObjectName("TRADE"), PublicAPI]
    public sealed class Trade : InstrumentMessage
    {
        #region Properties

        /// <summary>
        ///     Биржевой идентификатор сделки.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Время сделки.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        ///     Операция в сделке.
        /// </summary>
        public OrderOperation Operation { get; set; }

        /// <summary>
        ///     Цена сделки.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///     Количество контрактов/лотов в сделке.
        /// </summary>
        public uint Quantity { get; set; }

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
            fmt.AddField(LogFieldNames.Id, Id);
            fmt.AddField(LogFieldNames.Instrument, Instrument);
            fmt.AddField(LogFieldNames.Time, DateTime);
            fmt.AddEnumField(LogFieldNames.Operation, Operation);
            fmt.AddField(LogFieldNames.Price, Price);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            return fmt.ToString();
        }

        #endregion
    }
}

