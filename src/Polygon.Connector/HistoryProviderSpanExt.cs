using System;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Методы-расширения для <see cref="HistoryProviderSpan"/>
    /// </summary>
    [PublicAPI]
    public static class HistoryProviderSpanExt
    {
        /// <summary>
        ///     Конвертирует размер свечи во временной интервал
        /// </summary>
        public static TimeSpan ToTimeSpan(this HistoryProviderSpan span)
        {
            switch (span)
            {
                case HistoryProviderSpan.Month:
                    return TimeSpan.FromDays(28);
                case HistoryProviderSpan.Week:
                    return TimeSpan.FromDays(7);
                case HistoryProviderSpan.Day:
                    return TimeSpan.FromDays(1);
                case HistoryProviderSpan.Hour4:
                    return TimeSpan.FromHours(4);
                case HistoryProviderSpan.Hour:
                    return TimeSpan.FromHours(1);
                case HistoryProviderSpan.Minute30:
                    return TimeSpan.FromMinutes(30);
                case HistoryProviderSpan.Minute15:
                    return TimeSpan.FromMinutes(15);
                case HistoryProviderSpan.Minute10:
                    return TimeSpan.FromMinutes(10);
                case HistoryProviderSpan.Minute5:
                    return TimeSpan.FromMinutes(5);
                default:
                case HistoryProviderSpan.Minute:
                    return TimeSpan.FromMinutes(1);
            }
        }
    }
}

