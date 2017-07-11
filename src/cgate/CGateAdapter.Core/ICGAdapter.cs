using System;
using CGateAdapter.Messages;
using JetBrains.Annotations;

namespace CGateAdapter
{
    /// <summary>
    ///     Адаптер для CGate
    /// </summary>
    [PublicAPI]
    public interface ICGAdapter : IDisposable
    {
        /// <summary>
        ///     Состояние соединения
        /// </summary>
        CGConnectionState ConnectionState { get; }

        /// <summary>
        ///     Запустить адаптер
        /// </summary>
        void Start();

        /// <summary>
        ///     Остановить адаптер
        /// </summary>
        void Stop();

        /// <summary>
        ///     Отправить сообщение
        /// </summary>
        void SendMessage([NotNull] CGateMessage message);

        /// <summary>
        ///     Событие изменения состояния соединения
        /// </summary>
        event EventHandler<CGConnectionStateEventArgs> ConnectionStateChanged;

        /// <summary>
        ///     Событие получения нового потокового сообщения рыночных данных
        /// </summary>
        event EventHandler<CGateMessageEventArgs> MarketdataMessageReceived;

        /// <summary>
        /// Событие получения нового потокового сообщения, связанного с исполнением заявок
        /// </summary>
        event EventHandler<CGateMessageEventArgs> ExecutionMessageReceived;
    }
}