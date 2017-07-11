using System;
using JetBrains.Annotations;

namespace Polygon.Messages
{
    /// <summary>
    ///     Сообщения по конкретному инструменту.
    /// </summary>
    [Serializable, PublicAPI]
    public abstract class InstrumentMessage : Message
    {
        /// <summary>
        ///     Информация об инструменте.
        /// </summary>
        public Instrument Instrument { get; set; }
    }
}

