using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Информация об инструменте.
    /// </summary>
    [Serializable, PublicAPI]
    public sealed class Instrument : IEquatable<Instrument>, IComparable<Instrument>, IComparable, IPrintable
    {
        #region Properties

        /// <summary>
        /// Код инструмента.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Стандартный вид кода инструмента
        /// </summary>
        [JsonIgnore]
        public string StandardView => Code;

        #endregion

        #region Operators

        /// <summary>
        ///     Оператор равенства.
        /// </summary>
        /// <param name = "instrument1">
        ///     Правый операнд.
        /// </param>
        /// <param name = "instrument2">
        ///     Левый операнд.
        /// </param>
        /// <returns>
        ///     true, если операнды равны.
        /// </returns>
        public static bool operator ==(Instrument instrument1, Instrument instrument2)
        {
            return ReferenceEquals(instrument1, instrument2) ||
                   !ReferenceEquals(instrument1, null) && instrument1.Equals(instrument2);
        }

        /// <summary>
        ///     Оператор неравенства.
        /// </summary>
        /// <param name = "instrument1">
        ///     Правый операнд.
        /// </param>
        /// <param name = "instrument2">
        ///     Левый операнд.
        /// </param>
        /// <returns>
        ///     true, если операнды не равны.
        /// </returns>
        public static bool operator !=(Instrument instrument1, Instrument instrument2)
        {
            return !(instrument1 == instrument2);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        ///     The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.
        /// </param>
        /// <exception cref="T:System.NullReferenceException">
        ///     The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return (obj is Instrument) && Equals((Instrument)obj);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Code?.GetHashCode() ?? 0;
        }

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option) => Code;

        /// <summary>
        ///     Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                return string.Empty;
            }

            return Code;
        }

        #endregion

        #region Implemented Interfaces

        #region IComparable<Instrument>

        /// <inheritdoc />
        public int CompareTo(Instrument other) => StringComparer.Ordinal.Compare(Code, other.Code);

        #endregion

        #region IComparable

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var other = obj as Instrument;
            if (obj == null)
            {
                throw new ArgumentException(
                    $"Expected an instance of {typeof(Instrument).FullName} but got an instance of {obj.GetType().FullName}",
                    nameof(obj));
            }

            return CompareTo(other);
        }

        #endregion

        #region IEquatable<Instrument>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals(Instrument other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(other.Code, Code, StringComparison.Ordinal);  
        }

        #endregion

        #endregion
    }
}

