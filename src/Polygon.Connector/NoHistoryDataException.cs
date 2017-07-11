using System;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Бросается, если исторические данные за указанный период недоступны
    /// </summary>
    [Serializable]
    [PublicAPI]
    public class NoHistoryDataException : Exception
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public NoHistoryDataException() { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public NoHistoryDataException(string message)
            : base(message)
        { }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public NoHistoryDataException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}

