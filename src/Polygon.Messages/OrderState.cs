using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Состояние заявки.
    /// </summary>
    [PublicAPI]
    public enum OrderState
    {
        /// <summary>
        ///     Неопределенное значение
        /// </summary>
        [EnumMemberName("UNDEF")]
        Undefined = 0,

        /// <summary>
        ///     Новая заявка.
        /// </summary>
        [EnumMemberName("NEW")]
        New,

        /// <summary>
        ///     Заявка активна.
        /// </summary>
        [EnumMemberName("ACTIVE")]
        Active,

        /// <summary>
        ///     Ошибка в заявке.
        /// </summary>
        [EnumMemberName("ERROR")]
        Error,

        /// <summary>
        ///     Заявка частично исполнена.
        /// </summary>
        [EnumMemberName("PARTIALLY_FILLED")]
        PartiallyFilled,

        /// <summary>
        ///     Заявка исполнена.
        /// </summary>
        [EnumMemberName("FILLED")]
        Filled,

        /// <summary>
        ///     Заявка снята.
        /// </summary>
        [EnumMemberName("CANCELLED")]
        Cancelled
    }
}

