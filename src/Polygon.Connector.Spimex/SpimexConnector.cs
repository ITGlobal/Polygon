using System;
using System.Threading;
using SpimexAdapter;
using SpimexAdapter.FTE;

namespace Polygon.Connector.Spimex
{
    /// <summary>
    ///     Коннектор для Spimex
    /// </summary>
    public sealed class SpimexConnector : IConnector, IConnectionStatusProvider, IInstrumentTickerLookup
    {
        #region Fields

        private readonly InfoCommClient infoClient;
        private readonly TransCommClient transClient;

        private readonly SpimexFeed feed;
        private readonly SpimexRouter router;


        #endregion

        /// <summary>
        ///     .ctor
        /// </summary>
        public SpimexConnector(CommClientSettings infoClientSettings, CommClientSettings transClientSettings)
        {
            infoClient = new InfoCommClient(infoClientSettings);
            transClient = new TransCommClient(transClientSettings);

            feed = new SpimexFeed(infoClient);
            router = new SpimexRouter(infoClient, transClient);

            infoClient.OnError += OnError;
            transClient.OnError += OnError;
        }

        private void OnError(int arg1, string arg2)
        {
            ChangeConnectionStatus(ConnectionStatus.Disconnected);
        }

        /// <inheritdoc />
        public string Name => "Spimex Connector";

        /// <inheritdoc />
        public IFeed Feed => feed;

        /// <inheritdoc />
        public IOrderRouter Router => router;

        /// <inheritdoc />
        public IInstrumentHistoryProvider HistoryProvider => null;

        /// <inheritdoc />
        public void Start()
        {
            ChangeConnectionStatus(ConnectionStatus.Connecting);

            try
            {
                infoClient.Connect();
                infoClient.Login().Wait();

                transClient.Connect();
                transClient.Login().Wait();


                SubscribeTables();

                feed.Start();
                router.Start();

                ChangeConnectionStatus(ConnectionStatus.Connected);
            }
            catch (FTEException e)
            {
                ChangeConnectionStatus(ConnectionStatus.Disconnected);
                // TODO log
            }
        }


        private void SubscribeTables()
        {
            infoClient.SubscribeOnce(
                Table.SECURITIES,
                Table.ACCOUNTS
                ).Wait();

            infoClient.Subscribe(Table.HOLDINGS).Wait();

            infoClient.Subscribe(
                Table.SECBOARDS,
                Table.TRADES,
                Table.ORDERS
                ).Wait();
        }

        /// <inheritdoc />
        public void Stop()
        {
            feed.Stop();
            router.Stop();

            infoClient?.Logoff();
            infoClient?.Disconnect();

            transClient?.Logoff();
            transClient?.Disconnect();

            ChangeConnectionStatus(ConnectionStatus.Disconnected);
        }

        /// <inheritdoc />
        public bool SupportsOrderModification(string account) => false;

        /// <inheritdoc />
        public void Dispose()
        {
            infoClient.OnError -= OnError;
            transClient.OnError -= OnError;

            Feed?.Dispose();
            Router?.Dispose();
        }

        #region IConnectionStatusProvider

        /// <inheritdoc />
        public ConnectionStatus ConnectionStatus => (ConnectionStatus)status;
        // Содержит значения типа ConnectionStatus
        private int status = (int)ConnectionStatus.Undefined;

        /// <inheritdoc />
        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        private void ChangeConnectionStatus(ConnectionStatus newStatus)
        {
            if (Interlocked.Exchange(ref status, (int)newStatus) != (int)newStatus)
            {
                ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(ConnectionStatus, Name));
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
