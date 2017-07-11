namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Настройки транспорта CQG Continuum
    /// </summary>
    public sealed class CQGCParameters : IConnectorFactory
    {
        internal sealed class CQGInstrumentConverter : InstrumentConverter<CQGInstrumentConverter, InstrumentData, CQGCAdapter> { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CQGCParameters(IInstrumentConverter<InstrumentData> externalConverter) 
        {
            ConnectionUrl = CQGCAdapter.DefaultUrl;
            InstrumentConverter = CQGInstrumentConverter.Create(externalConverter);
        }

        /// <summary>
        ///     Connection URL
        /// </summary>
        public string ConnectionUrl { get; set; }

        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        ///     CQGContinuum Instrument Converter
        /// </summary>
        internal CQGInstrumentConverter InstrumentConverter { get; }
        
        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector() => new CQGCConnector(this);
    }
}

