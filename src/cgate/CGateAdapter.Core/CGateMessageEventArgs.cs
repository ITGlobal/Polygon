using System;
using System.Diagnostics;
using CGateAdapter.Messages;
using JetBrains.Annotations;

namespace CGateAdapter
{
    /// <summary>
    ///     Параметры для события приема сообщения
    /// </summary>
    [PublicAPI]
    public sealed class CGateMessageEventArgs : EventArgs
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        [DebuggerStepThrough]
        public CGateMessageEventArgs(CGateMessage message)
        {
            Message = message;
        }

        /// <summary>
        ///     Сообщение
        /// </summary>
        [NotNull]
        public CGateMessage Message { get; }
    }
}