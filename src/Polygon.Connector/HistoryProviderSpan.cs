using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     Размеры свечек
    /// </summary>
    [PublicAPI]
    public enum HistoryProviderSpan
    {
        /// <summary>
        ///     1 минута
        /// </summary>
        [EnumMemberName("1MIN")]
        Minute,

        /// <summary>
        ///     5 минут
        /// </summary>
        [EnumMemberName("5MIN")]
        Minute5,

        /// <summary>
        ///     10 минут
        /// </summary>
        [EnumMemberName("10MIN")]
        Minute10,

        /// <summary>
        ///     15 минут
        /// </summary>
        [EnumMemberName("15MIN")]
        Minute15,

        /// <summary>
        ///     30 минут
        /// </summary>
        [EnumMemberName("30MIN")]
        Minute30,

        /// <summary>
        ///     1 час
        /// </summary>
        [EnumMemberName("1H")]
        Hour,

        /// <summary>
        ///     4 часа
        /// </summary>
        [EnumMemberName("4H")]
        Hour4,

        /// <summary>
        ///     День
        /// </summary>
        [EnumMemberName("1DAY")]
        Day,

        /// <summary>
        ///     Неделя
        /// </summary>
        [EnumMemberName("1WEEK")]
        Week,

        /// <summary>
        ///     Месяц
        /// </summary>
        [EnumMemberName("1MONTH")]
        Month
    }
}

