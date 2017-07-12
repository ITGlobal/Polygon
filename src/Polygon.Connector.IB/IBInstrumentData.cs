using JetBrains.Annotations;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Метаданные по инструменту от внешнего конвертера
    /// </summary>
    [PublicAPI]
    public sealed class IBInstrumentData : InstrumentData
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public IBInstrumentData()
        {
            InstrumentType = IBInstrumentType.Unknown;
            ExchangeCode = default(string);
        }

        /// <summary>
        ///     Тип инструмента
        /// </summary>
        public IBInstrumentType InstrumentType { get; set; }

        /// <summary>
        ///     Код биржи
        /// </summary>
        public string ExchangeCode { get; set; }
    }
}
