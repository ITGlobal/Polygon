using System;
using ITGlobal.DeadlockDetection;
using JetBrains.Annotations;
using Polygon.Diagnostics;
using Polygon.Messages;

namespace Polygon.Connector
{
    /// <summary>
    ///   Базовый класс типичного сервиса шлюза.
    /// </summary>
    [PublicAPI]
    public abstract class GatewayService : IGatewayService
    {
        /// <summary>
        /// Создаёт сервис шлюза.
        /// </summary>
        protected GatewayService()
        {
            Logger = LogManager.GetLogger(GetType());
            SyncRoot = DeadlockMonitor.Cookie(GetType());

            // Коллективный разум решил, что по умолчанию сообщения должны транслироваться
            SendErrorMessages = true;
        }

        /// <summary>
        ///   Логгер.
        /// </summary>
        protected ILog Logger { get; }

        /// <summary>
        ///     Объект для синхронизации
        /// </summary>
        protected ILockObject SyncRoot { get; }

        /// <summary>
        ///     Транслировать ли ошибки в виде ErrorMessage
        /// </summary>
        public bool SendErrorMessages { get; set; }

        /// <summary>
        ///   Вызывается при получении сообщения из фида.
        /// </summary>
        public virtual event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        ///   Запускает сервис.
        /// </summary>
        public abstract void Start();

        /// <summary>
        ///   Останавливает сервис.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        ///   Вызывает событие о получении сообщений.
        /// </summary>
        /// <param name = "message">
        ///   Сообщения для отправки.
        /// </param>
        protected void OnMessageReceived(Message message)
        {
            var handler = MessageReceived;
            if (handler != null && message != null)
            {
                handler(this, new MessageReceivedEventArgs(message));
            }
        }

        /// <summary>
        ///     Обработка ошибки (логгирование + проброс сообщения)
        /// </summary>
        protected virtual void HandleError(ILogMessage message)
        {
            Logger.Error().Print(message);

            if (SendErrorMessages)
            {
                OnMessageReceived(new ErrorMessage { Message = message.Print(PrintOption.Default) });
            }
        }

        /// <summary>
        ///     Обработка ошибки (логгирование + проброс сообщения)
        /// </summary>
        protected virtual void HandleError(string message)
        {
            Logger.Error().PrintFormat(message);

            if (SendErrorMessages)
            {
                OnMessageReceived(new ErrorMessage { Message = message });
            }
        }

        /// <summary>
        ///     Обработка ошибки (логгирование + проброс сообщения) с указанием исключения
        /// </summary>
        protected virtual void HandleExceptionError(Exception exception, ILogMessage message)
        {
            Logger.Error().Print(exception, message);
            if (SendErrorMessages)
            {
                OnMessageReceived(new ErrorMessage { Message = message.Print(PrintOption.Default) });
            }
        }

        /// <summary>
        ///     Обработка ошибки (логгирование + проброс сообщения) с указанием исключения
        /// </summary>
        protected virtual void HandleExceptionError(Exception exception, string message)
        {
            Logger.Error().PrintFormat(exception, message);
            if (SendErrorMessages)
            {
                OnMessageReceived(new ErrorMessage { Message = message });
            }
        }
    }
}

