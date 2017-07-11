using System;
using System.IO;
using CGateAdapter;
using Polygon.Diagnostics;

namespace Polygon.Connector.CGate
{
    /// <summary>
    /// Транспорт для шлюза cgate московской биржи
    /// </summary>
    internal sealed class CGateTransport : IConnector, IConnectionStatusProvider
    {
        private readonly CGateParameters settings;

        public const int DefaultPort = 4001;

        #region fields

        private static readonly ILog _Log = LogManager.GetLogger<CGateTransport>();

        //private readonly CGP2ClientAdapter cgAdapter;
        private readonly ICGAdapter cgAdapter;
        private readonly CGateFeed feed;
        private readonly CGateRouter router;
        private readonly CGateInstrumentResolver instrumentIsinResolver;
        private readonly CGateInstrumentParamsEmitter instrumentParamsEmitter;

        #endregion

        #region Private class

        /// <summary>
        /// Реализация ICGateLogger, обёртка над локальным логгером
        /// </summary>
        private class CGateLogger : ICGateLogger
        {
            public void Trace(string message, params object[] args)
            {
                _Log.Trace().PrintFormat(message, args);
            }

            public void Debug(string message, params object[] args)
            {
                _Log.Debug().PrintFormat(message, args);
            }

            public void Info(string message, params object[] args)
            {
                _Log.Info().PrintFormat(message, args);
            }

            public void Warn(string message, params object[] args)
            {
                _Log.Warn().PrintFormat(message, args);
            }

            public void Error(string message, params object[] args)
            {
                _Log.Error().PrintFormat(message, args);
            }

            public void Error(Exception exception, string message, params object[] args)
            {
                _Log.Error().PrintFormat(exception, message, args);
            }
        }

        #endregion

        #region ctor

        public CGateTransport(CGateParameters settings, string dataFolder)
        {
            this.settings = settings;
            var config = settings.ToCGAdapterConfiguration();
            
            if (!settings.IsTestConnection)
            {
                config.Key = settings.P2Key;
            }

            config.Logger = new CGateLogger();
            config.IniFolder = @"scheme\";
            cgAdapter = new CGP2ClientAdapter(config, Path.Combine(dataFolder, "cgate"), openOrderBooksStreams: settings.OrderBooksEnabled);
            cgAdapter.ConnectionStateChanged += CGAdapterConnectionStateChangedHandler;
            var instrumentConverter = settings.InstrumentConverter;
            instrumentIsinResolver = new CGateInstrumentResolver(instrumentConverter);
            instrumentParamsEmitter = new CGateInstrumentParamsEmitter(instrumentIsinResolver);

            feed = new CGateFeed(cgAdapter, instrumentIsinResolver, instrumentParamsEmitter);
            router = new CGateRouter(cgAdapter, instrumentIsinResolver, instrumentConverter, instrumentParamsEmitter);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Feed?.Dispose();
            Router?.Dispose();
            cgAdapter.Dispose();
        }

        #endregion

        #region ITransport

        public string Name => "CGate";

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
        public IInstrumentHistoryProvider HistoryProvider => null;

        public void Start()
        {
            try
            {
                connectionStatus = ConnectionStatus.Connecting;
                OnConnectionStatusChanged();
                cgAdapter.Start();
                connectionStatus = ConnectionStatus.Connected;
                OnConnectionStatusChanged();
                feed.Start();
                router.Start();
            }
            catch (Exception e)
            {
                _Log.Fatal().PrintFormat(e, "Failed to start CGAdapter: {0}", e.Message);
                connectionStatus = ConnectionStatus.Disconnected;
                OnConnectionStatusChanged();
            }
        }

        public void Stop()
        {
            try
            {
                feed.Stop();
                router.Stop();
                cgAdapter.Stop();
                connectionStatus = ConnectionStatus.Disconnected;
                OnConnectionStatusChanged();
            }
            catch (Exception e)
            {
                _Log.Fatal().PrintFormat(e, "Failed to stop CGAdapter: {0}", e.Message);
            }
        }

        /// <summary>
        ///     Поддерживается ли модификация заявок по счету <paramref name="account"/>
        /// </summary>
        bool IConnector.SupportsOrderModification(string account) => true;

        #endregion

        #region IConnectionStatusProvider

        private ConnectionStatus connectionStatus = ConnectionStatus.Undefined;

        public ConnectionStatus ConnectionStatus => connectionStatus;

        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        private void OnConnectionStatusChanged()
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new ConnectionStatusEventArgs(ConnectionStatus, Name));
            }
        }

        /// <summary>
        /// Обработчик события изменения состояния подключения, которое генерирует адаптер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CGAdapterConnectionStateChangedHandler(object sender, CGConnectionStateEventArgs e)
        {
            //switch (e.ConnectionState)
            //{
            //    case CGConnectionState.Connected:
            //        connectionStatus = ConnectionStatus.Connected;
            //        break;
            //    case CGConnectionState.Connecting:
            //        connectionStatus = ConnectionStatus.Connecting;
            //        break;
            //    case CGConnectionState.Disconnected:
            //        connectionStatus = ConnectionStatus.Disconnected;
            //        break;
            //    case CGConnectionState.Shutdown:
            //        connectionStatus = ConnectionStatus.Undefined;
            //        break;
            //}

            //OnConnectionStatusChanged();
        }

        #endregion
    }
}

