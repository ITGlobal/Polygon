using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     Вид инструмента (используется в коннекторах IB и IQFeed)
    /// </summary>
    public enum InstrumentType
    {
        /// <summary>
        ///     Неопределенное значение
        /// </summary>
        [EnumMemberName("UNDEF")]
        Unknown,

        /// <summary>
        ///     Актив
        /// </summary>
        [EnumMemberName("ASSET")]
        Asset,

        /// <summary>
        ///     Фьючерс
        /// </summary>
        [EnumMemberName("FUTURE")]
        Future,

        /// <summary>
        ///     Серия опционов на актив
        /// </summary>
        [EnumMemberName("ASSET_OPTION_SERIES")]
        AssetOptionSeries,

        /// <summary>
        ///     Серия опционов на фьючерс
        /// </summary>
        [EnumMemberName("FUTURE_OPTION_SERIES")]
        FutureOptionSeries,

        /// <summary>
        ///     Опцион
        /// </summary>
        [EnumMemberName("OPTION")]
        Option
    }
}
