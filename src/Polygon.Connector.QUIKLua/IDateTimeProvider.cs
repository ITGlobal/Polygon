using System;
using JetBrains.Annotations;

namespace Polygon.Connector.QUIKLua
{
    /// <summary>
    ///     Провайдер даты и времени
    /// </summary>
    [PublicAPI]
    public interface IDateTimeProvider
    {
        /// <summary>
        ///     Возвращает текущие дату и время
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        ///     Возвращает текущую дату
        /// </summary>
        DateTime Today { get; }
    }
}