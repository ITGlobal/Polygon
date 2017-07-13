using System;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Интерфейс типичного сервиса шлюза.
    /// </summary>
    [PublicAPI]
    public interface IGatewayService
    {
        /// <summary>
        ///     Транслировать ли ошибки в виде ErrorMessage
        /// </summary>
        bool SendErrorMessages { get; set; }

        /// <summary>
        ///     Вызывается при получении сообщения из фида.
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}

