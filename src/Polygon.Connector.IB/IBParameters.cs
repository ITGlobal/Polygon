using System;
using System.Xml.Linq;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Настройки транспорта IB
    /// </summary>
    [PublicAPI]
    public sealed class IBParameters : IConnectorFactory
    {
        internal sealed class IBInstrumentConverter : InstrumentConverter<IBInstrumentConverter, IBInstrumentData, IBAdapter> { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public IBParameters(IInstrumentConverter<IBInstrumentData> externalConverter)
        {
            Host = "127.0.0.1";
            Port = 7496;
            ClientId = 0;
            SessionUid = Guid.NewGuid().ToString("N").Substring(0, 5);
            RouterMode = OrderRouterMode.ExternalSessionsRenewable;
            InstrumentConverter = IBInstrumentConverter.Create(externalConverter);
        }

        /// <summary>
        ///     Конвертер инструментов IB
        /// </summary>
        internal IBInstrumentConverter InstrumentConverter;

        /// <summary>
        ///     Хост
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     Порт
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Номер клиент
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        ///     ID сессии
        /// </summary>
        public string SessionUid { get; set; }

        /// <summary>
        ///     Режим работы раутера
        /// </summary>
        public OrderRouterMode RouterMode { get; set; }

        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector() => new IBConnector(this);
    }
}
