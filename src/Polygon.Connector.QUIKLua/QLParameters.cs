using Polygon.Connector.QUIKLua.Adapter;
using JetBrains.Annotations;

namespace Polygon.Connector.QUIKLua
{
    /// <summary>
    ///     Настройки адаптера QUIK LUA
    /// </summary>
    [PublicAPI]
    public sealed class QLParameters : IConnectorFactory
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public QLParameters(IDateTimeProvider dateTimeProvider, [CanBeNull]IInstrumentConverter<InstrumentData> externalConverter)
        {
            DateTimeProvider = dateTimeProvider;
            InstrumentConverter = QLInstrumentConverter.Create(externalConverter);
        }

        /// <summary>
        ///     Провайдер даты и времени
        /// </summary>
        public IDateTimeProvider DateTimeProvider { get; }

        /// <summary>
        ///     Конвертер инструментов QUICKLua
        /// </summary>
        internal QLInstrumentConverter InstrumentConverter { get; }

        internal sealed class QLInstrumentConverter
            : InstrumentConverter<QLInstrumentConverter, InstrumentData, QLAdapter>
        { }

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

