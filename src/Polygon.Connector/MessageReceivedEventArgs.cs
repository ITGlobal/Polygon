using System;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///     Аргументы события о получении сообщения.
    /// </summary>
    public sealed class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        ///     Создает аргументы события с указанным сообщением.
        /// </summary>
        /// <param name="message">
        ///     Полученное сообщение.
        /// </param>
        public MessageReceivedEventArgs(Message message)
        {
            Message = message;
        }

        /// <summary>
        ///     Сообщение, которое было получено.
        /// </summary>
        public Message Message { get; }
    }
}

