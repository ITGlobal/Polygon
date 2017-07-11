using System;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    /// <summary>
    /// LUA-вская таблица для представления даты и времени
    /// </summary>
    internal class QLDateTime : IEquatable<QLDateTime>, IPrintable
    {
        public int day { get; set; }
        public int week_day { get; set; }
        public int hour { get; set; }
        public int ms { get; set; }
        public int min { get; set; }
        public int month { get; set; }
        public int sec { get; set; }
        public int year { get; set; }
        
        public bool Equals(QLDateTime other)
        {
            return other.day == day &&
                   other.week_day == week_day &&
                   other.hour == hour &&
                   other.ms == ms &&
                   other.min == min &&
                   other.month == month &&
                   other.sec == sec &&
                   other.year == year;
        }

        public static bool operator ==(QLDateTime x, QLDateTime y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(QLDateTime x, QLDateTime y)
        {
            return !x.Equals(y);
        }
        
        public string Print(PrintOption option) => $"{year:D04}-{month:D02}-{day:D02}T{hour:D02}-{min:D02}-{sec:D02}.{ms:D02}";

        public override string ToString() => Print(PrintOption.Default);
    }
}

