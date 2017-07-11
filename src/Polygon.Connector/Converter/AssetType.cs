using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     Вид актива (используется в коннекторе IB)
    /// </summary>
    public enum AssetType 
    {
        /// <summary>
        ///     Неопределенное значение
        /// </summary>
        [EnumMemberName("UNDEF")]
        Undefined,

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
        FX
    }
}
