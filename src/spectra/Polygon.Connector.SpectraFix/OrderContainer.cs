using System;
using System.Collections.Generic;
using ITGlobal.DeadlockDetection;

namespace Polygon.Connector.SpectraFix
{
    internal sealed class OrderContainer
    {
        private struct OrderParams
        {
            public string Symbol;
            public decimal Qty;
            public char Side;
        }

        private readonly ILockObject _syncRoot = DeadlockMonitor.Cookie<OrderContainer>();

        private readonly Dictionary<Guid, OrderParams> _orderByTransactionIdMap = new Dictionary<Guid, OrderParams>();

        private readonly Dictionary<string, Guid> _orderIdToTransactionIdMap = new Dictionary<string, Guid>();
        private readonly Dictionary<Guid, string> _transactionIdToOrderIdMap = new Dictionary<Guid, string>();

        private readonly Dictionary<string, Guid> _clOrderIdToTransactionIdMap = new Dictionary<string, Guid>();
        private readonly Dictionary<Guid, string> _transactionIdToClOrderIdMap = new Dictionary<Guid, string>();

        public void SaveOrderParams(Guid transactionId, string clOrderId, string symbol, decimal qty, char side)
        {
            using (_syncRoot.Lock())
            {
                if (!_orderByTransactionIdMap.ContainsKey(transactionId))
                {
                    var orderParams = new OrderParams
                    {
                        Symbol = symbol,
                        Qty = qty,
                        Side = side
                    };
                    _orderByTransactionIdMap.Add(transactionId, orderParams);
                    _clOrderIdToTransactionIdMap.Add(clOrderId, transactionId);
                    _transactionIdToClOrderIdMap.Add(transactionId, clOrderId);
                }
            }
        }

        public void UpdateOrderParams(string orderExchangeId, string origClOrderId, string clOrderId, string symbol, decimal qty, char side)
        {
            using (_syncRoot.Lock())
            {
                if (_clOrderIdToTransactionIdMap.TryGetValue(origClOrderId, out var transactionId))
                {
                    ForgetOrder(transactionId);
                }
                else
                {
                    transactionId = Guid.NewGuid();
                }

                SaveOrderParams(transactionId, clOrderId, symbol, qty, side);
                SaveOrderExchangeId(transactionId, orderExchangeId);
            }
        }

        public Guid GetOrCreateOrderTransactionId(string clOrderId)
        {
            using (_syncRoot.Lock())
            {
                if (!_clOrderIdToTransactionIdMap.TryGetValue(clOrderId, out var transactionId))
                {
                    return Guid.Empty;
                }

                return transactionId;
            }
        }

        public void SaveOrderExchangeId(Guid transactionId, string orderExchangeId)
        {
            using (_syncRoot.Lock())
            {
                _orderIdToTransactionIdMap[orderExchangeId] = transactionId;
                _transactionIdToOrderIdMap[transactionId] = orderExchangeId;
            }
        }

        public bool GetOrderParams(string orderExchangeId, out Guid transactionId, out string symbol, out decimal qty, out char side)
        {
            transactionId = default(Guid);
            symbol = default(string);
            qty = default(decimal);
            side = default(char);

            using (_syncRoot.Lock())
            {
                if (!_orderIdToTransactionIdMap.TryGetValue(orderExchangeId, out transactionId))
                {
                    return false;
                }

                if (!_orderByTransactionIdMap.TryGetValue(transactionId, out var orderParams))
                {
                    return false;
                }

                symbol = orderParams.Symbol;
                qty = orderParams.Qty;
                side = orderParams.Side;

                return true;
            }
        }

        public void ForgetOrder(Guid transactionId)
        {
            using (_syncRoot.Lock())
            {
                if (_transactionIdToOrderIdMap.TryGetValue(transactionId, out var orderExchangeId))
                {
                    _transactionIdToOrderIdMap.Remove(transactionId);
                    _orderIdToTransactionIdMap.Remove(orderExchangeId);
                }

                if (_transactionIdToClOrderIdMap.TryGetValue(transactionId, out var clOrderId))
                {
                    _transactionIdToClOrderIdMap.Remove(transactionId);
                    _clOrderIdToTransactionIdMap.Remove(clOrderId);
                }

                _orderByTransactionIdMap.Remove(transactionId);
            }
        }
    }
}