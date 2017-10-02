﻿using System;
using System.Linq;
using Polygon.Diagnostics;

namespace Polygon.Connector.IQFeed
{
    /// <summary>
    ///     Транспорт IQFeed
    /// </summary>
    internal sealed class IQFeedConnector : IConnector, IInstrumentTickerLookup
    {
        internal static readonly ILog _Log = LogManager.GetLogger<IQFeedConnector>();

        private readonly IQFeedConnectorSettings settings;
        private readonly IQFeedGateway feed;

        internal IQFeedConnector(IQFeedConnectorSettings settings)
        {
            this.settings = settings;
            feed = new IQFeedGateway(settings);
            ConnectionStatusProviders = new IConnectionStatusProvider[] { feed };
        }

        /// <summary>
        ///     Название транспорта
        /// </summary>
        public string Name => "IQFeed";

        /// <summary>
        ///     Фид транспорта
        /// </summary>
        public IFeed Feed => feed;

        /// <summary>
        ///     Раутер транспорта
        /// </summary>
        public IOrderRouter Router => null;

        /// <summary>
        ///     Провайдер исторических данных
        /// </summary>
        public IInstrumentHistoryProvider HistoryProvider => feed;

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
        public IInstrumentTickerLookup InstrumentTickerLookup => this;

        /// <summary>
        ///     Провайдер кодов инструментов для FORTS
        /// </summary>
        public IFortsDataProvider FortsDataProvider => null;

        /// <summary>
        ///     Провайдеры статусов соединений
        /// </summary>
        public IConnectionStatusProvider[] ConnectionStatusProviders { get; }

        /// <summary>
        ///     Запуск транспорта
        /// </summary>
        public void Start()
        {
            feed.Start();
        }

        /// <summary>
        ///     Останов транспорта
        /// </summary>
        public void Stop()
        {
            feed.Stop();
        }

        /// <summary>
        ///     Поддерживается ли модификация заявок по счету <paramref name="account"/>
        /// </summary>
        bool IConnector.SupportsOrderModification(string account) => false;

        public void Dispose()
        {
            feed.Dispose();
        }

        /// <summary>
        ///     Поиск тикеров по (частичному) коду
        /// </summary>
        string[] IInstrumentTickerLookup.LookupInstruments(string code, int maxResults)
        {
            try
            {
                var results = feed.LookupSymbols(code, maxResults).Result;
                if (results != null && results.Length > maxResults)
                {
                    results = results.Take(maxResults).ToArray();
                }

                return results;
            }
            catch (OperationCanceledException)
            {
                return new string[0];
            }
            catch (AggregateException e)
            {
                _Log.Error().PrintFormat(e.InnerException ?? e, "LookupInstruments({0}) failed", code);
                return new string[0];
            }
            catch (Exception e)
            {
                _Log.Error().PrintFormat(e, "LookupInstruments({0}) failed", code);
                return new string[0];
            }
        }
    }
}

