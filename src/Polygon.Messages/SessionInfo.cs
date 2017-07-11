using System;
using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Информация о сессии
    /// </summary>
    [Serializable, ObjectName("SESSION_INFO"), PublicAPI]
    public sealed class SessionInfo : Message
    {
        #region Properties

        /// <summary>
        ///     Текущее серверное время и дата торгов
        /// </summary>
        public DateTime ServerTime { get; set; }

        /// <summary>
        ///     Идут ли сейчас торги
        /// </summary>
        public bool IsTrading { get; set; }

        /// <summary>
        ///     Время начала основной сессии
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        ///     Время окончания основной сессии
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        ///     Время начала вечерней сессии
        /// </summary>
        public TimeSpan EveningStartTime { get; set; }

        /// <summary>
        ///     Время окончания вечерней сессии
        /// </summary>
        public TimeSpan EveningEndTime { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Запуск посетителя для обработки сообщений.
        /// </summary>
        /// <param name="visitor">
        ///     Экземпляр посетителя.
        /// </param>
        public override void Accept(IMessageVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Time, ServerTime);
            fmt.AddField(LogFieldNames.IsTrading, IsTrading);
            fmt.AddField(LogFieldNames.StartTime, StartTime);
            fmt.AddField(LogFieldNames.EndTime, EndTime);
            fmt.AddField(LogFieldNames.EveningStartTime, EveningStartTime);
            fmt.AddField(LogFieldNames.EveningEndTime, EveningEndTime);
            return fmt.ToString();
        }

        #endregion
    }
}

