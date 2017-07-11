using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Тип параметра в <see cref="MoneyPosition"/>
    /// </summary>
    [Serializable, PublicAPI]
    public enum MoneyPositionPropertyType
    {
        /// <summary>
        ///     Неопределенное значение
        /// </summary>
        [EnumMemberName("UNDEF")]
        Undefined,

        /// <summary>
        ///     Значение типа decimal?
        /// </summary>
        [EnumMemberName("DECIMAL")]
        Decimal,

        /// <summary>
        ///     Значение типа string
        /// </summary>
        [EnumMemberName("STRING")]
        String
    }
}

