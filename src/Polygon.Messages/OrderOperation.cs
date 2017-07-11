using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Типы операций в заявках.
    /// </summary>
    [PublicAPI]
    public enum OrderOperation
    {
        /// <summary>
        ///     Операция не определена.
        /// </summary>
        [EnumMemberName("NOP")]
        NoOp = 0,

        /// <summary>
        ///     Операция покупки.
        /// </summary>
        [EnumMemberName("BUY")]
        Buy,

        /// <summary>
        ///     Операция продажи.
        /// </summary>
        [EnumMemberName("SELL")]
        Sell
    }
}

