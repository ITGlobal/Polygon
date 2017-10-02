using JetBrains.Annotations;

namespace Polygon.Connector.QUIKLua
{
    /// <summary>
    ///     Настройки адаптера QUIK LUA
    /// </summary>
    [PublicAPI]
    public sealed class QLConnectorSettings : IConnectorFactory
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public QLConnectorSettings(InstrumentConverter<InstrumentData> instrumentConverter)
        {
            InstrumentConverter = instrumentConverter;
        }

        /// <summary>
        ///     Провайдер даты и времени
        /// </summary>
        public IDateTimeProvider DateTimeProvider { get; set; } = new DefaultDateTimeProvider();

        /// <summary>
        ///     Конвертер инструментов QUICKLua
        /// </summary>
        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }

        /// <summary>
        ///     IP адрес, на котором открыт LUA сокет
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///     Порт
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Разрешить прием рыночных данных
        /// </summary>
        public bool ReceiveMarketdata { get; set; } = true;

        /// <summary>
        ///     Создать транспорт с текущими настройками
        /// </summary>
        /// <returns>
        ///     Транспорт
        /// </returns> 
        public IConnector CreateConnector() => new QLConnector(this, DateTimeProvider);
    }
}

