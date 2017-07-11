using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Интерфейс для реализации структуры метаданных инструмента
    /// </summary>
    internal interface IInstrumentData
    {
        /// <summary>
        ///     Вендорский код инструмента
        /// </summary>
        string Symbol { get; set; }

        /// <summary>
        ///     Инструмент
        /// </summary>
        Instrument TransportInstrument { get; set; }
    }
}
