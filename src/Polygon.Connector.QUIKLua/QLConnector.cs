using System;
using Polygon.Connector.QUIKLua.Adapter;

namespace Polygon.Connector.QUIKLua
{
    internal sealed class QLConnector : IConnector, IConnectionStatusProvider
    {
        public const int DefaultPort = 1248;

        private readonly QLParameters settings;

        #region Fields

        private readonly QLAdapter adapter;
        private readonly QLFeed feed;
        private readonly QLRouter router;
        private readonly QLHistoryProvider historyProvider;

        #endregion

        #region .ctor

        public QLConnector(QLParameters settings, IDateTimeProvider dateTimeProvider)
        {
            this.settings = settings;
            adapter = new QLAdapter(settings.IpAddress, settings.Port, dateTimeProvider, settings.ReceiveMarketdata, settings.InstrumentConverter);
            feed = new QLFeed(adapter);
            router = new QLRouter(adapter);
            historyProvider = new QLHistoryProvider(adapter);

            ConnectionStatusProviders = new IConnectionStatusProvider[] {this};
        }

        #endregion

        #region IConnector

        /// <summary>
        ///     Название транспорта
        /// </summary>
        public string Name => "QUIK (Lua)";

        /// <summary>
        ///     Фид транспорта
        /// </summary>
        public IFeed Feed => feed;

        /// <summary>
        ///     Раутер транспорта
        /// </summary>
        public IOrderRouter Router => router;

        /// <summary>
        ///     Провайдер исторических данных
        /// </summary>
        public IInstrumentHistoryProvider HistoryProvider => historyProvider;

        /// <summary>
        ///     Подписчик на параметры инструментов
        /// </summary>
        public IInstrumentParamsSubscriber InstrumentParamsSubscriber => feed;

        /// <summary>
        ///     Подписчик на стаканы по инструментам
        /// </summary>
        public IOrderBookSubscriber OrderBookSubscriber => feed;

        /// <summary>
        ///     Поиск инструментов по коду
        /// </summary>
        public IInstrumentTickerLookup InstrumentTickerLookup => feed;

        /// <summary>
        ///     Провайдер кодов инструментов для FORTS
        /// </summary>
        public IFortsDataProvider FortsDataProvider => feed;

        /// <summary>
        ///     Провайдеры статусов соединений
        /// </summary>
        public IConnectionStatusProvider[] ConnectionStatusProviders { get; }

        /// <summary>
        ///     Запуск транспорта
        /// </summary>
        public void Start()
        {
            ConnectionStatus = ConnectionStatus.Connecting;

            feed.Start();
            router.Start();
            adapter.Start();
            adapter.ConnectionStatusChanged += (sender, e) => ConnectionStatus = adapter.ConnectionStatus;
        }

        /// <summary>
        ///     Останов транспорта
        /// </summary>
        public void Stop()
        {
            adapter.Stop();
            feed.Stop();
            router.Stop();
            ConnectionStatus = ConnectionStatus.Disconnected;
        }

        /// <summary>
        ///     Поддерживается ли модификация заявок по счету <paramref name="account"/>
        /// </summary>
        bool IConnector.SupportsOrderModification(string account) => true;

        /// <inheritdoc />
        public void Dispose() { }

        #endregion

        #region IConnectionStatusProvider

        public string ConnectionName => "QUIK";

        private ConnectionStatus connectionStatus = ConnectionStatus.Undefined;

        /// <summary>
        ///     Текущее состояние соединения
        /// </summary>
        public ConnectionStatus ConnectionStatus
        {
            get { return connectionStatus; }
            set
            {
                if (connectionStatus != value)
                {
                    connectionStatus = value;
                    OnConnectionStatusChanged();
                }
            }
        }

        /// <summary>
        ///     Вызывается при изменении состояния соединения
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        private void OnConnectionStatusChanged()
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new ConnectionStatusEventArgs(ConnectionStatus, ConnectionName));
            }
        }

        #endregion
    }
}

