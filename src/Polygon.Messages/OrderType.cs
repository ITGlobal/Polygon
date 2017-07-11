using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Тип ордера (лимит/маркет).
    /// </summary>
    [PublicAPI]
    public enum OrderType
    {
        /// <summary>
        ///     Неопределенное значение
        /// </summary>
        [EnumMemberName("UNDEF")]
        Undefined = 0,

        /// <summary>
        ///     Лимитный ордер.
        /// </summary>
        [EnumMemberName("L")]
        Limit,

        /// <summary>
        ///     Рыночный ордер.
        /// </summary>
        [EnumMemberName("M")]
        Market
    }
}

