namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Метаданные по инструменту от внешнего конвертера
    /// </summary>
    public sealed class IBInstrumentData : InstrumentData
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public IBInstrumentData()
        {
            InstrumentType = InstrumentType.Unknown;
            AssetType = AssetType.Undefined;
            ExchangeCode = default(string);
        }

        /// <summary>
        ///     Тип инструмента
        /// </summary>
        public InstrumentType InstrumentType { get; set; }

        /// <summary>
        ///     Тип актива
        /// </summary>
        public AssetType AssetType { get; set; }

        /// <summary>
        ///     Код биржи
        /// </summary>
        public string ExchangeCode { get; set; }
    }
}
