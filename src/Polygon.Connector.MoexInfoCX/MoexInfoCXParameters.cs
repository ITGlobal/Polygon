namespace Polygon.Connector.MoexInfoCX
{
    public sealed class MoexInfoCXParameters : IConnectorFactory
    {
        /// <summary>
        ///     Конвертер инструментов
        /// </summary>
        public InstrumentConverter<InfoCXInstrumentData> InstrumentConverter { get; }

        /// <summary>
        ///     Url
        /// </summary>
        public string BrokerUrl { get; set; }

        /// <summary>
        ///     Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Домен
        /// </summary>
        public string Domain { get; set; }

        public MoexInfoCXParameters(InstrumentConverter<InfoCXInstrumentData> instrumentConverter)
        {
            InstrumentConverter = instrumentConverter;
        }

        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector() => new MoexInfoCXConnector(this);

    }
}