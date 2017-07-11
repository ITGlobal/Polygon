using Polygon.Diagnostics;
using Polygon.Connector.InteractiveBrokers;
using IBApi;

namespace Polygon.Connector.InteractiveBrokers
{
    /// <summary>
    ///     Фасад над <see cref="EClientSocket"/>, логирующий запросы к сокету
    /// </summary>
    internal sealed class LoggingEClientSocketFacade
    {
        private static readonly ILog _Log = IBAdapter.Log;
        private readonly EClientSocket socket;

        public LoggingEClientSocketFacade(EClientSocket socket)
        {
            this.socket = socket;
        }

        public void eConnect(string host, int port, int clientId)
        {
            _Log.Debug().PrintFormat("> eConnect host={0}, port={1}, clientId={2}", host, port, clientId);
            socket.eConnect(host, port, clientId);
        }

        public void reqMarketDataType(int marketDataType)
        {
            _Log.Debug().PrintFormat("> reqMarketDataType marketDataType={0}", marketDataType);
            ThrowIfNotConnected();
            socket.reqMarketDataType(marketDataType);
        }

        public void reqAccountUpdates(bool subscribe, string acctCode)
        {
            _Log.Debug().PrintFormat("> reqAccountUpdates subscribe={0}, acctCode={1}", subscribe, acctCode);
            ThrowIfNotConnected();
            socket.reqAccountUpdates(subscribe, acctCode);
        }

        public void reqPositions()
        {
            _Log.Debug().PrintFormat("> reqPositions");
            ThrowIfNotConnected();
            socket.reqPositions();
        }

        public void reqAutoOpenOrders(bool autoBind)
        {
            _Log.Debug().PrintFormat("> reqAutoOpenOrders autoBind={0}", autoBind);
            ThrowIfNotConnected();
            socket.reqAutoOpenOrders(autoBind);
        }

        public void reqCurrentTime()
        {
            _Log.Debug().PrintFormat("> reqCurrentTime");
            ThrowIfNotConnected();
            socket.reqCurrentTime();
        }

        public void Close()
        {
            socket.Close();
        }

        public bool reqContractDetails(int reqId, Contract contract)
        {
            _Log.Trace().PrintFormat("> reqContractDetails reqId={0}, contract={1}", reqId, contract);
            ThrowIfNotConnected();
            return socket.reqContractDetails(reqId, contract);
        }
        
        public void reqHistoricalData(int tickerId, Contract contract, string endDateTime,
            string durationString, string barSizeSetting, string whatToShow, int useRTH, int formatDate)
        {
            _Log.Trace().PrintFormat(
                "> reqHistoricalData tickerId={0}, contract={1}, endDateTime={2}, durationString={3}, barSizeSetting={4}, whatToShow={5}, useRTH={6}, formatDate={7}",
                tickerId, 
                contract,
                endDateTime,
                durationString,
                barSizeSetting, 
                whatToShow, 
                useRTH, 
                formatDate);
            ThrowIfNotConnected();
            socket.reqHistoricalData(tickerId, contract, endDateTime, durationString, barSizeSetting, whatToShow, useRTH, formatDate);
        }

        public void reqMktData(int tickerId, Contract contract, string genericTickList, bool snapshot)
        {
            _Log.Trace().PrintFormat(
                "> reqMktData tickerId={0}, contract={1}, genericTickList={2}, snapshot={3}", 
                tickerId, contract, genericTickList, snapshot);
            ThrowIfNotConnected();
            socket.reqMktData(tickerId, contract, genericTickList, snapshot);
        }

        public void cancelMktData(int tickerId)
        {
            _Log.Trace().PrintFormat("> cancelMktData tickerId {0}", tickerId);
            ThrowIfNotConnected();
            socket.cancelMktData(tickerId);
        }

        public void cancelHistoricalData(int tickerId)
        {
            _Log.Trace().PrintFormat("> cancelHistoricalData tickerId {0}", tickerId);
            ThrowIfNotConnected();
            socket.cancelHistoricalData(tickerId);
        }

        public void reqMarketDepth(int tickerId, Contract contract, int numRows)
        {
            _Log.Trace().PrintFormat("> reqMarketDepth tickerId={0}, contract={1}, numRows={2}", tickerId, contract, numRows);
            ThrowIfNotConnected();
            socket.reqMarketDepth(tickerId, contract, numRows);
        }

        public void cancelMktDepth(int tickerId)
        {
            ThrowIfNotConnected();
            _Log.Trace().PrintFormat("> cancelMktDepth tickerId={0}", tickerId);
            socket.cancelMktDepth(tickerId);
        }

        public void placeOrder(int id, Contract contract, IBApi.Order order)
        {
            _Log.Debug().PrintFormat("> placeOrder id={0}, contract={1}, order={2}", id, contract, order);
            ThrowIfNotConnected();
            socket.placeOrder(id, contract, order);
        }

        public void cancelOrder(int orderId)
        {
            _Log.Debug().PrintFormat("> cancelOrder orderId={0}", orderId);
            ThrowIfNotConnected();
            socket.cancelOrder(orderId);
        }

        public void reqExecutions(int reqId, ExecutionFilter filter)
        {
            _Log.Debug().PrintFormat("> reqExecutions reqId={0}, filter={1}", reqId, filter);
            ThrowIfNotConnected();
            socket.reqExecutions(reqId, filter);
        }

        private void ThrowIfNotConnected()
        {
            if (!socket.IsConnected())
            {
                throw new IBNoConnectionException("IB Adapter is not connected");
            }
        }
    }
}

