using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     Режим работы раутера
    /// </summary>
    [PublicAPI]
    public enum OrderRouterMode
    {
        /// <summary>
        ///     Только текущая сессия
        /// </summary>
        [EnumMemberName("THIS_SESSION")]
        ThisSessionOnly,

        /// <summary>
        ///     Текущая сессия + заявки с предыдущих сессий
        /// </summary>
        [EnumMemberName("RENEWABLE_SESSION")]
        ThisSessionRenewable,

        /// <summary>
        ///     Заявки от внешних источников
        /// </summary>
        [EnumMemberName("EXTERNAL_SESSION")]
        ExternalSessionsRenewable
    }
}

