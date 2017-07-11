using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Класс своей сделки. То есть не анонимной, а своей.
    /// </summary>
    [Serializable, ObjectName("FILL"), PublicAPI]
    public sealed class FillMessage : AccountMessage
    {
        #region Properties

        /// <summary>
        ///     Время сделки.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        ///     Инструмент сделки.
        /// </summary>
        public Instrument Instrument { get; set; }

        /// <summary>
        ///     Операция в сделке.
        /// </summary>
        public OrderOperation Operation { get; set; }

        /// <summary>
        ///     Цена сделки.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Количество контрактов/лотов в сделке.
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        ///     Биржевой номер сделки.
        /// </summary>
        public string ExchangeId { get; set; }

        /// <summary>
        ///     Биржевой номер заявки.
        /// </summary>
        public string ExchangeOrderId { get; set; }

        #endregion

        #region Public Methods
        
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
            fmt.AddField(LogFieldNames.Account, Account);
            fmt.AddField(LogFieldNames.ClientCode, ClientCode);
            fmt.AddField(LogFieldNames.Time, DateTime);
            fmt.AddField(LogFieldNames.Instrument, Instrument?.Code);
            fmt.AddEnumField(LogFieldNames.Operation, Operation);
            fmt.AddField(LogFieldNames.Price, Price);
            fmt.AddField(LogFieldNames.Quantity, Quantity);
            fmt.AddField(LogFieldNames.ExchangeId, ExchangeId);
            fmt.AddField(LogFieldNames.ExchangeOrderId, ExchangeOrderId);
            return fmt.ToString();
        }

        #endregion
    }
}

