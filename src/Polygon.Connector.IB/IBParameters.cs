using System;
using JetBrains.Annotations;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Настройки транспорта IB
    /// </summary>
    [PublicAPI]
    public sealed class IBParameters : IConnectorFactory
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public IBParameters(InstrumentConverter<IBInstrumentData> instrumentConverter)
        {
            Host = "127.0.0.1";
            Port = 7496;
            ClientId = 0;
            SessionUid = Guid.NewGuid().ToString("N").Substring(0, 5);
            RouterMode = OrderRouterMode.ExternalSessionsRenewable;
            InstrumentConverter = instrumentConverter;
        }

        /// <summary>
        ///     Конвертер инструментов IB
        /// </summary>
        public InstrumentConverter<IBInstrumentData> InstrumentConverter { get; }

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
