using System;
using JetBrains.Annotations;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Бросается, если нет соединения с IB
    /// </summary>
    [Serializable, PublicAPI]
    public class IBNoConnectionException : Exception
    {
        /// <summary>
        ///     .ctor
        /// </summary>
        public IBNoConnectionException() { }

        /// <summary>
        ///     .ctor
        /// </summary>
        public IBNoConnectionException(string message)
            : base(message)
        { }

        /// <summary>
        ///     .ctor
        /// </summary>
        public IBNoConnectionException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}

