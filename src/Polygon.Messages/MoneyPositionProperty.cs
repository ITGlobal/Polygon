using System;

namespace Polygon.Messages
{
    /// <summary>
    ///     Параметр в <see cref="MoneyPosition"/>
    /// </summary>
    [Serializable]
    public sealed class MoneyPositionProperty
    {
        /// <summary>
        ///     Название параметра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Значение параметра
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        ///     Тип параметра
        /// </summary>
        public MoneyPositionPropertyType Type { get; set; }

        /// <summary>
        ///     Есть ли значение у параметра
        /// </summary>
        public bool HasValue => Value != null;

        /// <summary>
        ///     Преобразование в decimal
        /// </summary>
        public decimal? AsDecimal()
        {
            if (Type == MoneyPositionPropertyType.Decimal)
            {
                return (decimal?) Value;
            }
            return null;
        }

        /// <summary>
        ///     Преобразование в string
        /// </summary>
        public string AsString()
        {
            if (Type == MoneyPositionPropertyType.String)
            {
                return (string)Value;
            }
            return null;
        }

        /// <summary>
        ///     Преобразование из decimal
        /// </summary>
        public static implicit operator MoneyPositionProperty(decimal value)
        {
            return new MoneyPositionProperty { Value = (decimal?)value, Type = MoneyPositionPropertyType.Decimal };
        }

        /// <summary>
        ///     Преобразование из decimal?
        /// </summary>
        public static implicit operator MoneyPositionProperty(decimal? value)
        {
            return new MoneyPositionProperty { Value = value, Type = MoneyPositionPropertyType.Decimal };
        }

        /// <summary>
        ///     Преобразование из string
        /// </summary>
        public static implicit operator MoneyPositionProperty(string value)
        {
            return new MoneyPositionProperty { Value = value, Type = MoneyPositionPropertyType.String };
        }
    }
}

