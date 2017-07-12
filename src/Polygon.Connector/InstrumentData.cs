using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Класс со структурой метаданных инструмента
    /// </summary>
    public class InstrumentData
    {
        /// <summary>
        ///     Вендорский код инструмента
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        ///     Инструмент
        /// </summary>
        public Instrument Instrument { get; set; }
    }
}
