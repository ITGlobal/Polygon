using System;
using Polygon.Connector.QUIKLua.Adapter;

namespace Polygon.Connector.QUIKLua
{
    internal sealed class QLConnector : IConnector, IConnectionStatusProvider, IInstrumentTickerLookup
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
        ///     Запуск транспорта
        /// </summary>
        public void Start()
        {
            ConnectionStatus = ConnectionStatus.Connecting;

            feed.Start();
            router.Start();
            adapter.Start();
            adapter.ConnectionStatusChanged += (sender, e) => ConnectionStatus = e.ConnectionStatus;
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
                handler(this, new ConnectionStatusEventArgs(ConnectionStatus, "QL Connector"));
            }
        }

        #endregion

        #region IInstrumentTickerLookup

        /// <summary>
        ///     Поиск тикеров по (частичному) коду
        /// </summary>
        public string[] LookupInstruments(string code, int maxResults = 10) => feed.LookupInstruments(code, maxResults);

        #endregion
    }
}

