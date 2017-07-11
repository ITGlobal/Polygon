using System;

namespace Polygon.Connector
{
    /// <summary>
    ///     Аргументы события об изменении состояния соединения
    /// </summary>
    public sealed class ConnectionStatusEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        ///     .ctor
        /// </summary>
        public ConnectionStatusEventArgs(ConnectionStatus connectionStatus, string connectionName)
        {
            ConnectionStatus = connectionStatus;
            ConnectionName = connectionName;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Название соединения
        /// </summary>
        public string ConnectionName { get; }

        /// <summary>
        ///     Новое состояние соединения
        /// </summary>
        public ConnectionStatus ConnectionStatus { get; }

        #endregion
    }
}

