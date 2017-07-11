using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Режим торгов по инструменту
    /// </summary>
    [PublicAPI]
    public enum MarketMode
    {
        /// <summary>
        ///     Нет информации о режиме торгов
        /// </summary>
        [EnumMemberName("NONE")]
        None = 0,

        /// <summary>
        ///     Инструмент торгуется
        /// </summary>
        [EnumMemberName("OPEN")]
        Open = 1
    }
}

