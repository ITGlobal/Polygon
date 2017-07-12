using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Вид инструмента (используется в коннекторах IB и IQFeed)
    /// </summary>
    [PublicAPI]
    public enum IBInstrumentType
    {
        /// <summary>
        ///     Неопределенное значение
        /// </summary>
        [EnumMemberName("UNDEF")]
        Unknown,

        /// <summary>
        ///     Акция
        /// </summary>
        [EnumMemberName("EQ")]
        Equity,

        /// <summary>
        ///     Индекс
        /// </summary>
        [EnumMemberName("IDX")]
        Index,

        /// <summary>
        ///     Коммодити
        /// </summary>
        [EnumMemberName("CMDT")]
        Commodity,

        /// <summary>
        ///     Валюта
        /// </summary>
        [EnumMemberName("FX")]
        FX,

        /// <summary>
        ///     Фьючерс
        /// </summary>
        [EnumMemberName("FUTURE")]
        Future,

        /// <summary>
        ///     Опцион на актив
        /// </summary>
        [EnumMemberName("ASSET_OPTION")]
        AssetOption,

        /// <summary>
        ///     Опцион на фьючерс
        /// </summary>
        [EnumMemberName("FUTURE_OPTION")]
        FutureOption
    }
}