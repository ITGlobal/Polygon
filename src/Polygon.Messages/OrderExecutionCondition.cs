using JetBrains.Annotations;
using Polygon.Diagnostics;

namespace Polygon.Messages
{
    /// <summary>
    ///     Условие исполнения заявки
    /// </summary>
    [PublicAPI]
    public enum OrderExecutionCondition
    {
        /// <summary>
        ///     Неопределенное значение
        /// </summary>
        [EnumMemberName("UNDEF")]
        Undefined = 0,

        /// <summary>
        ///     Отправить заявку в очередь
        /// </summary>
        [EnumMemberName("PUT_IN_QUEUE")]
        PutInQueue,

        /// <summary>
        ///     Снять заявку, если она не исполнилась немедленно
        /// </summary>
        [EnumMemberName("FILL_OR_KILL")]
        FillOrKill,

        /// <summary>
        ///     KillBalance
        /// </summary>
        [EnumMemberName("KILL_BALANCE")]
        KillBalance
    }
}

