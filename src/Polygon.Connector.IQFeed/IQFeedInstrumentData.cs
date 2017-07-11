using JetBrains.Annotations;

namespace Polygon.Connector.IQFeed
{
    /// <summary>
    /// Метаданные по инструменту от внешнего конвертера инструментов
    /// </summary>
    [PublicAPI]
    public sealed class IQFeedInstrumentData : InstrumentData
    {
        /// <summary>
        ///     Тип ценной бумаги
        /// </summary>
        public SecurityType SecurityType { get; set; }

        /// <summary>
        ///     Тип инструмента
        /// </summary>
        public InstrumentType InstrumentType { get; set; }
    }
}