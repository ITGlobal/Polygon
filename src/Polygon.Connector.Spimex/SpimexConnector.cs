using System;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Messages;
using SpimexAdapter;
using SpimexAdapter.FTE;

namespace Polygon.Connector.Spimex
{
    /// <summary>
    ///     Коннектор для Spimex
    /// </summary>
    internal sealed class SpimexConnector : IConnector, IConnectionStatusProvider, IInstrumentTickerLookup, IInstrumentConverterContext<SpimexInstrumentData>
    {
        #region Fields

        private readonly InfoCommClient infoClient;
        private readonly TransCommClient transClient;

        private readonly InstrumentConverter<SpimexInstrumentData> instrumentConverter;

        private readonly SpimexFeed feed;
        private readonly SpimexRouter router;
        
        #endregion

        /// <summary>
        ///     .ctor
        /// </summary>
        public SpimexConnector(
            CommClientSettings infoClientSettings, 
            CommClientSettings transClientSettings,
            InstrumentConverter<SpimexInstrumentData> instrumentConverter)
        {
            infoClient = new InfoCommClient(infoClientSettings);
            transClient = new TransCommClient(transClientSettings);
            this.instrumentConverter = instrumentConverter;

            feed = new SpimexFeed(this, infoClient);
            router = new SpimexRouter(this, infoClient, transClient);

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

        internal async Task<string> ResolveInstrumentAsync(Instrument instrument)
        {
            var data = await instrumentConverter.ResolveInstrumentAsync(this, instrument);
            return data?.Symbol;
        }

        internal async Task<SpimexInstrumentData> ResolveInstrumentDataAsync(Instrument instrument)
        {
            var data = await instrumentConverter.ResolveInstrumentAsync(this, instrument);
            return data;
        }

        internal async Task<Instrument> ResolveSymbolAsync(string symbol)
        {
            var instrument = await instrumentConverter.ResolveSymbolAsync(this, symbol);
            return instrument;
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

        #region IInstrumentConverterContext<SpimexInstrumentData>

        ISubscriptionTester<SpimexInstrumentData> IInstrumentConverterContext<SpimexInstrumentData>.SubscriptionTester => feed;

        #endregion
    }
}
