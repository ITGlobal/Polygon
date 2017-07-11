using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Класс со структурой метаданных инструмента
    /// </summary>
    public class InstrumentData : IInstrumentData
    {
        /// <summary>
        ///     Вендорский код инструмента
        /// </summary>
        public virtual string Symbol { get; set; }

        /// <summary>
        ///     Инструмент
        /// </summary>
        public virtual Instrument TransportInstrument { get; set; }
    }
}
