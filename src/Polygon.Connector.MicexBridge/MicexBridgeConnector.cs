using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.Connector.MicexBridge
{
    internal sealed class MicexBridgeConnector : IConnector, IConnectionStatusProvider
    {
        public MicexBridgeConnector(MicexBridgeConnectorSettings settings)
        {

        }

        #region IConnector

        public string Name => "Micex Bridge";

        public IFeed Feed { get; private set; }

        public IOrderRouter Router { get; private set; }

        public IInstrumentParamsSubscriber InstrumentParamsSubscriber { get; }

        public IOrderBookSubscriber OrderBookSubscriber { get; }

        public IInstrumentTickerLookup InstrumentTickerLookup { get; }

        public IFortsDataProvider FortsDataProvider { get; }

        public IInstrumentHistoryProvider HistoryProvider { get; }

        public IConnectionStatusProvider[] ConnectionStatusProviders { get; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public bool SupportsOrderModification(string account)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        { }

        #endregion

        #region IConnectionStatusProvider

        public string ConnectionName { get; }

        public ConnectionStatus ConnectionStatus { get; }

        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        #endregion
    }
}
