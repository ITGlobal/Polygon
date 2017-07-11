using JetBrains.Annotations;

namespace Polygon.Connector.IQFeed
{
    /// <summary>
    ///     Настройки адаптера IQFeed
    /// </summary>
    [PublicAPI]
    public sealed class IQFeedParameters : IConnectorFactory
    {
        #region .ctor

        /// <summary>
        ///     Конструктор
        /// </summary>
        public IQFeedParameters(IInstrumentConverter<IQFeedInstrumentData> externalConverter)
        {
            IQConnectAddress = "localhost";
            TreatCommodityAs = SecurityType.SPOT;
            InstrumentConverter = IQFeedInstrumentConverter.Create(externalConverter);
        }

        #endregion

        /// <summary>
        ///     Адрес для соединения
        /// </summary>
        public string IQConnectAddress { get; set; }

        /// <summary>
        ///     <see cref="SecurityType"/> для коммодити
        /// </summary>
        public SecurityType TreatCommodityAs { get; set; }

        #region InstrumentConverter

        /// <summary>
        ///     Конвертер инструментов IQFeed
        /// </summary>
        internal sealed class IQFeedInstrumentConverter
            : InstrumentConverter<IQFeedInstrumentConverter, IQFeedInstrumentData, IQFeedGateway>
        {
        }

        /// <summary>
        ///     Конвертер инструментов
        /// </summary>
        internal IQFeedInstrumentConverter InstrumentConverter { get; }

        #endregion

        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector() => new IQFeedConnector(this);
    }
}

