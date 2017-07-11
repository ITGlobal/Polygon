using System;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLHeartbeat)]
    internal class QLHeartbeat : QLMessage
    {
        /// <summary>
        ///     Текущее серверное время и дата торгов
        /// </summary>
        public DateTime time { get; set; } = DateTime.MinValue;

        /// <summary>
        ///     Время начала основной сессии
        /// </summary>
        public TimeSpan startTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        ///     Время окончания основной сессии
        /// </summary>
        public TimeSpan endTime { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        ///     Время начала вечерней сессии
        /// </summary>
        public TimeSpan evnStartTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        ///     Время окончания вечерней сессии
        /// </summary>
        public TimeSpan evnEndTime { get; set; } = TimeSpan.FromDays(1);
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Time, time);
            fmt.AddField(LogFieldNames.StartTime, startTime);
            fmt.AddField(LogFieldNames.EndTime, endTime);
            fmt.AddField(LogFieldNames.EveningStartTime, evnStartTime);
            fmt.AddField(LogFieldNames.EveningEndTime, evnEndTime);
            return fmt.ToString();
        }
    }
}

