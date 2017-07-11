using System;
using CGateAdapter.Messages;
using ru.micexrts.cgate;

namespace CGateAdapter
{
    /// <summary>
    ///     Базовый класс для сущностей, создаваемых поверх подключения - потока данных и паблишера
    /// </summary>
    internal abstract class CGateSubconnection : IDisposable
    {
        protected bool _disposed;

        public CGateStreamType StreamType { get; set; }

        public string Name { get; set; }

        public string SchemeFileName { get; set; }

        protected string SchemeName { get; set; }
        
        /// <summary>
        /// Событие райзится в случае, когда возможно произошло изменение состояния потока
        /// </summary>
        public event Action<string, StreamRegime> StateMightBeenChanged;
        
        /// <summary>
        /// ONLINE, SNAPSHOT, 
        /// </summary>
        public StreamRegime Regime { get; set; }

        protected abstract string ConnectionString { get; }

        public abstract State State { get; }

        public abstract void SetConnection(Connection connection);

        public abstract void Open();

        public abstract void Close();

        public abstract void TryOpen();

        public abstract void Dispose();

        protected void RiseStateMightBeenChanged(string streamName, StreamRegime regime) 
            => StateMightBeenChanged?.Invoke(streamName, regime);
    }
}
