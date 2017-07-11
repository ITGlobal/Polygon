using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     Тип подписки.
    /// </summary>
    [PublicAPI]
    public enum SubscriptionType : byte
    {
        /// <summary>
        ///     Значение по умолчанию
        /// </summary>
        [EnumMemberName("UNDEF")]
        Undefined = 0,

        /// <summary>
        ///     Параметры инструмента.
        /// </summary>
        [EnumMemberName("IP")]
        InstrumentParams,

        /// <summary>
        ///     Данные стакана.
        /// </summary>
        [EnumMemberName("ORDBOOK")]
        OrderBook,

        /// <summary>
        ///     Данные сделки.
        /// </summary>
        [EnumMemberName("TRADE")]
        Trade,

        /// <summary>
        ///     Своя сделка.
        /// </summary>
        [EnumMemberName("FILL")]
        Fill,

        /// <summary>
        ///     Информация о лимитах по деньгам по заданному счету.
        /// </summary>
        [EnumMemberName("MONEY")]
        MoneyPosition,

        /// <summary>
        ///     Информация о позиции по заданному инструменту для заданного счета.
        /// </summary>
        [EnumMemberName("POS")]
        Position
    }
}

