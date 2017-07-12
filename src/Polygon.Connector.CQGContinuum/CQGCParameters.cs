namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Настройки транспорта CQG Continuum
    /// </summary>
    public sealed class CQGCParameters : IConnectorFactory
    {
        /// <summary>
        ///     URL по умолчанию
        /// </summary>
        public const string DefaultUrl = "wss://api.cqg.com";

        /// <summary>
        ///     URL для демо доступа
        /// </summary>
        public const string DemoUrl = "wss://demoapi.cqg.com:443";

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CQGCParameters(InstrumentConverter<InstrumentData> instrumentConverter) 
        {
            ConnectionUrl = DefaultUrl;
            InstrumentConverter = instrumentConverter;
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
        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }
        
        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector() => new CQGCConnector(this);
    }
}

