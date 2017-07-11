using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     Точка исторических данных
    /// </summary>
    [ObjectName("HISTORY_DATA_POINT"), DebuggerDisplay("{ToString()}"), PublicAPI]
    public sealed class HistoryDataPoint : IPrintable, IEquatable<HistoryDataPoint>
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public HistoryDataPoint(
            DateTime point,
            decimal high,
            decimal low,
            decimal open,
            decimal close,
            int periodVolume,
            int openInterest)
        {
            Point = point;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            PeriodVolume = periodVolume;
            OpenInterest = openInterest;
        }

        /// <summary>
        ///     Время
        /// </summary>
        public DateTime Point { get; }

        /// <summary>
        ///     Макс. значение за интервал
        /// </summary>
        public decimal High { get; }

        /// <summary>
        ///     Мин. значение за интервал
        /// </summary>
        public decimal Low { get; }

        /// <summary>
        ///     Значение открытия интервала
        /// </summary>
        public decimal Open { get; }

        /// <summary>
        ///     Значение закрытия интервала
        /// </summary>
        public decimal Close { get; }

        /// <summary>
        ///     Объем за период
        /// </summary>
        public int PeriodVolume { get; }

        /// <summary>
        ///     Открытый интерес
        /// </summary>
        public int OpenInterest { get; }

        /// <inheritdoc />
        public override string ToString() => Print(PrintOption.Default);

        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Time, Point);
            fmt.AddField(LogFieldNames.Low, Low);
            fmt.AddField(LogFieldNames.High, High);
            fmt.AddField(LogFieldNames.Open, Open);
            fmt.AddField(LogFieldNames.Close, Close);
            fmt.AddField(LogFieldNames.PeriodVolume, PeriodVolume);
            fmt.AddField(LogFieldNames.OpenInterest, OpenInterest);
            return fmt.ToString();
        }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(HistoryDataPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Point.Equals(other.Point) &&
                   High == other.High &&
                   Low == other.Low &&
                   Open == other.Open &&
                   Close == other.Close &&
                   PeriodVolume == other.PeriodVolume &&
                   OpenInterest == other.OpenInterest;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is HistoryDataPoint && Equals((HistoryDataPoint) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Point.GetHashCode();
                hashCode = (hashCode*397) ^ High.GetHashCode();
                hashCode = (hashCode*397) ^ Low.GetHashCode();
                hashCode = (hashCode*397) ^ Open.GetHashCode();
                hashCode = (hashCode*397) ^ Close.GetHashCode();
                hashCode = (hashCode*397) ^ PeriodVolume;
                hashCode = (hashCode*397) ^ OpenInterest;
                return hashCode;
            }
        }

        /// <summary>
        ///     Оператор равенства
        /// </summary>
        public static bool operator ==(HistoryDataPoint left, HistoryDataPoint right) => Equals(left, right);

        /// <summary>
        ///     Оператор неравенства
        /// </summary>
        public static bool operator !=(HistoryDataPoint left, HistoryDataPoint right) => !Equals(left, right);

        #endregion
    }
}

