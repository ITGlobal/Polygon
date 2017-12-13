using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.Connector.MoexInfoCX
{
    class MoexInfoCXConnector : IConnector, IInstrumentTickerLookup
    {
        private readonly MoexInfoCXParameters _parameters;
        private readonly MoexInfoCXFeed _feed;

        public MoexInfoCXConnector(MoexInfoCXParameters parameters)
        {
            _parameters = parameters;
            
            _feed = new MoexInfoCXFeed(parameters);
            ConnectionStatusProviders = new IConnectionStatusProvider[] { _feed };
        }

        #region IConnector

        /// <summary>
        ///     Название транспорта
        /// </summary>
        public string Name => "MoexInfoCX";

        /// <summary>
        ///     Фид транспорта
        /// </summary>
        public IFeed Feed => _feed;

        /// <summary>
        ///     Раутер транспорта
        /// </summary>
        public IOrderRouter Router => null;

        /// <summary>
        ///     Подписчик на параметры инструментов
        /// </summary>
        public IInstrumentParamsSubscriber InstrumentParamsSubscriber => _feed;

        /// <summary>
        ///     Подписчик на стаканы по инструментам
        /// </summary>
        public IOrderBookSubscriber OrderBookSubscriber => _feed;

        /// <summary>
        ///     Поиск инструментов по коду
        /// </summary>
        public IInstrumentTickerLookup InstrumentTickerLookup => this;

        /// <summary>
        ///     Провайдер кодов инструментов для FORTS
        /// </summary>
        public IFortsDataProvider FortsDataProvider => null;

        /// <summary>
        ///     Провайдер исторических данных
        /// </summary>
        public IInstrumentHistoryProvider HistoryProvider => null;

        /// <summary>
        ///     Провайдеры статусов соединений
        /// </summary>
        public IConnectionStatusProvider[] ConnectionStatusProviders { get; }

        public void Start()
        {
            _feed.Start();
        }

        public void Stop()
        {
            _feed.Stop();
        }

        public bool SupportsOrderModification(string account) => false;

        public void Dispose()
        {
            _feed.Dispose();
        }

        #endregion


        #region IInstrumentTickerLookup

        /// <summary>
        ///     Поиск тикеров по (частичному) коду
        /// </summary>
        public string[] LookupInstruments(string code, int maxResults = 10)
        {
            //TODO
            throw new NotImplementedException();
        }

        #endregion
    }
}
