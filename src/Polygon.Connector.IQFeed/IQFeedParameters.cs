﻿using JetBrains.Annotations;

namespace Polygon.Connector.IQFeed
{
    /// <summary>
    ///     Настройки адаптера IQFeed
    /// </summary>
    [PublicAPI]
    public sealed class IQFeedParameters : IConnectorFactory
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public IQFeedParameters(InstrumentConverter<IQFeedInstrumentData> instrumentConverter)
        {
            IQConnectAddress = "localhost";
            TreatCommodityAs = SecurityType.SPOT;
            InstrumentConverter = instrumentConverter;
        }

        /// <summary>
        ///     Адрес для соединения
        /// </summary>
        public string IQConnectAddress { get; set; }

        public string Level1Port { get; set; }

        public string LookupPort { get; set; }

        public string Level2Port { get; set; }

        public string AdminPort { get; set; }
        
        /// <summary>
        ///     <see cref="SecurityType"/> для коммодити
        /// </summary>
        public SecurityType TreatCommodityAs { get; set; }
        /// <summary>
        ///     Конвертер инструментов
        /// </summary>
        public InstrumentConverter<IQFeedInstrumentData> InstrumentConverter { get; }

        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector() => new IQFeedConnector(this);
    }
}