using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace CGateAdapter
{
    /// <summary>
    ///     Параметры для события изменения состояния соединения
    /// </summary>
    [PublicAPI]
    public sealed class CGConnectionStateEventArgs : EventArgs
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        [DebuggerStepThrough]
        public CGConnectionStateEventArgs(CGConnectionState connectionState)
        {
            ConnectionState = connectionState;
        }

        /// <summary>
        ///     Состояние соединения
        /// </summary>
        public CGConnectionState ConnectionState { get; }
    }
}