using System;
using JetBrains.Annotations;

namespace Polygon.Connector
{
    /// <summary>
    ///     Предоставляет информацию о состоянии соединения
    /// </summary>
    [PublicAPI]
    public interface IConnectionStatusProvider
    {
        /// <summary>
        ///     Текущее состояние соединения
        /// </summary>
        ConnectionStatus ConnectionStatus { get; }

        /// <summary>
        ///     Вызывается при изменении состояния соединения
        /// </summary>
        event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;
    }
}

