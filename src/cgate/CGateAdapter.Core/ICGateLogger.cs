using System;
using JetBrains.Annotations;

namespace CGateAdapter
{
    /// <summary>
    ///     Логгер
    /// </summary>
    [PublicAPI]
    public interface ICGateLogger
    {
        /// <summary>
        ///     Записать сообщение уровня Trace
        /// </summary>
        [StringFormatMethod("message")]
        void Trace(string message, params object[] args);

        /// <summary>
        ///     Записать сообщение уровня Debug
        /// </summary>
        [StringFormatMethod("message")]
        void Debug(string message, params object[] args);

        /// <summary>
        ///     Записать сообщение уровня Info
        /// </summary>
        [StringFormatMethod("message")]
        void Info(string message, params object[] args);

        /// <summary>
        ///     Записать сообщение уровня Warn
        /// </summary>
        [StringFormatMethod("message")]
        void Warn(string message, params object[] args);

        /// <summary>
        ///     Записать сообщение уровня Error
        /// </summary>
        [StringFormatMethod("message")]
        void Error(string message, params object[] args);

        /// <summary>
        ///     Записать сообщение уровня Error
        /// </summary>
        [StringFormatMethod("message")]
        void Error(Exception exception, string message, params object[] args);
    }
}