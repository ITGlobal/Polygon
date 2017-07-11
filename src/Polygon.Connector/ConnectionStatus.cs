using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     Состояние соединения
    /// </summary>
    [PublicAPI]
    public enum ConnectionStatus
    {
        /// <summary>
        /// Состояние не определено
        /// </summary>
        [EnumMemberName("UNDEF")]
        Undefined,

        /// <summary>
        /// Транспорт отключен
        /// </summary>
        [EnumMemberName("DISCONNECTED")]
        Disconnected,

        /// <summary>
        /// Транспорт подключается
        /// </summary>
        [EnumMemberName("CONNECTING")]
        Connecting,

        /// <summary>
        /// Транспорт подключен
        /// </summary>
        [EnumMemberName("CONNECTED")]
        Connected,

        /// <summary>
        /// Транспорт впал в состояние, из которого его нельзя выводить. Например, ограничение
        /// числа попыток логина у CQG
        /// </summary>
        [EnumMemberName("TERMINATED")]
        Terminated
    }
}

