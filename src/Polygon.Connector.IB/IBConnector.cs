using System;
using ITGlobal.DeadlockDetection;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Транспорт для IB
    /// </summary>
    internal sealed class IBConnector : IConnector, IConnectionStatusProvider
    {
        private readonly ILockObject syncRoot = DeadlockMonitor.Cookie<IBConnector>();
        private readonly IBConnectorSettings settings;

        private bool isConnected;

        public IBConnector(IBConnectorSettings settings)
        {
            this.settings = settings;
            IBFeed = new IBFeed(this);
            IBOrderRouter = new IBOrderRouter(this, settings.SessionUid, settings.RouterMode);
            Adapter = new IBAdapter(this, settings.InstrumentConverter);
            ContractContainer = new ContractContainer(Adapter, settings.InstrumentConverter);

            ConnectionStatusProviders = new IConnectionStatusProvider[] {this};
        }

        /// <summary>
        ///     Название транспорта
        /// </summary>
        public string Name => "Interactive Brokers";

        /// <summary>
        ///     Фид транспорта
        /// </summary>
        public IFeed Feed => IBFeed;

        /// <summary>
        ///     Раутер транспорта
        /// </summary>
        public IOrderRouter Router => IBOrderRouter;

        /// <summary>
        ///     Подписчик на параметры инструментов
        /// </summary>
        public IInstrumentParamsSubscriber InstrumentParamsSubscriber => IBFeed;

        /// <summary>
        ///     Подписчик на стаканы по инструментам
        /// </summary>
        public IOrderBookSubscriber OrderBookSubscriber => IBFeed;

        /// <summary>
        ///     Поиск инструментов по коду
        /// </summary>
        public IInstrumentTickerLookup InstrumentTickerLookup => null;

        /// <summary>
        ///     Провайдер кодов инструментов для FORTS
        /// </summary>
        public IFortsDataProvider FortsDataProvider => null;

        /// <summary>
        ///     Провайдеры статусов соединений
        /// </summary>
        public IConnectionStatusProvider[] ConnectionStatusProviders { get; }

        /// <summary>
        ///     Провайдер исторических данных
        /// </summary>
        public IInstrumentHistoryProvider HistoryProvider => IBFeed;

        /// <summary>
        ///     Запуск транспорта
        /// </summary>
        public void Start()
        {
            using (syncRoot.Lock())
            {
                if (isConnected)
                {
                    return;
                }

                isConnected = Adapter.Connect(settings.Host, settings.Port, settings.ClientId);
            }
        }

        /// <summary>
        ///     Останов транспорта
        /// </summary>
        public void Stop()
        {
            using (syncRoot.Lock())
            {
                if (!isConnected)
                {
                    return;
                }

                Adapter.Disconnect();
                isConnected = false;
            }

        }

        /// <summary>
        ///     Поддерживается ли модификация заявок по счету <paramref name="account"/>
        /// </summary>
        bool IConnector.SupportsOrderModification(string account) => false;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        #region Implementation of IConnectionStatusProvider

        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        public string ConnectionName => "InteractiveBrokers";

        public ConnectionStatus ConnectionStatus => status;

        private ConnectionStatus status = ConnectionStatus.Undefined;

        internal void RaiseConnectionStatusChanged(ConnectionStatus status)
        {
            if (this.status == status)
                return;

            this.status = status;

            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new ConnectionStatusEventArgs(status, ConnectionName));
            }
        }

        #endregion

        #region Internals

        internal int ClientId => settings.ClientId;
        internal ContractContainer ContractContainer { get; }
        internal InstrumentParamsCache InstrumentParamsCache { get; } = new InstrumentParamsCache();
        internal IBFeed IBFeed { get; }
        internal IBOrderRouter IBOrderRouter { get; }
        internal IBAdapter Adapter { get; }

        #endregion
    }
}
