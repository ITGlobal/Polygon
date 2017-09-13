using System;
using System.Collections.Generic;
using ITGlobal.DeadlockDetection;
using Polygon.Messages;
using XidNet;

namespace Polygon.Connector.SpectraFix
{
    internal sealed class TransactionContainer
    {
        private readonly ILockObject _transactionIdsLock = DeadlockMonitor.Cookie<TransactionContainer>();

        private readonly Dictionary<string, Guid> _clientOrderIdToTransactionIdMap = new Dictionary<string, Guid>();
        private readonly Dictionary<Guid, string> _transactionIdToClientOrderIdMap = new Dictionary<Guid, string>();

        private readonly Dictionary<int, Guid> _seqNumberIdToTransactionIdMap = new Dictionary<int, Guid>();
        private readonly Dictionary<Guid, int> _transactionIdToSeqNumberMap = new Dictionary<Guid, int>();
        
        private readonly Action<Message> _sendMessage;

        public TransactionContainer(Action<Message> sendMessage)
        {
            _sendMessage = sendMessage;
        }

        public string Add(Transaction transaction)
        {
            using (_transactionIdsLock.Lock())
            {
                if (!_transactionIdToClientOrderIdMap.TryGetValue(transaction.TransactionId, out var clOrderId))
                {
                    clOrderId = Xid.NewXid().ToString();
                    _transactionIdToClientOrderIdMap[transaction.TransactionId] = clOrderId;
                    _clientOrderIdToTransactionIdMap[clOrderId] = transaction.TransactionId;
                }

                return clOrderId;
            }
        }

        public void RememberSeqNumber(int seqNumber, string clOrderId)
        {
            using (_transactionIdsLock.Lock())
            {
                if (_clientOrderIdToTransactionIdMap.TryGetValue(clOrderId, out var transactionId))
                {
                    _seqNumberIdToTransactionIdMap[seqNumber] = transactionId;
                    _transactionIdToSeqNumberMap[transactionId] = seqNumber;
                }
            }
        }

        public Guid? GetTransactionId(string clOrderId)
        {
            using (_transactionIdsLock.Lock())
            {
                if (!_clientOrderIdToTransactionIdMap.TryGetValue(clOrderId, out var transactionId))
                {
                    return null;
                }

                return transactionId;
            }
        }

        public void Accept(string clOrderId)
        {
            Guid transactionId;
            using (_transactionIdsLock.Lock())
            {
                if (!_clientOrderIdToTransactionIdMap.TryGetValue(clOrderId, out transactionId))
                {
                    return;
                }

                Forget(clOrderId);
            }

            _sendMessage(TransactionReply.Accepted(transactionId));
        }

        public void Reject(string clOrderId, string message)
        {
            Guid transactionId;
            using (_transactionIdsLock.Lock())
            {
                if (!_clientOrderIdToTransactionIdMap.TryGetValue(clOrderId, out transactionId))
                {
                    return;
                }

                Forget(transactionId);
            }

            _sendMessage(TransactionReply.Rejected(transactionId, message));
        }

        public void Reject(int seqNum, string message)
        {
            Guid transactionId;
            using (_transactionIdsLock.Lock())
            {
                if (!_seqNumberIdToTransactionIdMap.TryGetValue(seqNum, out transactionId))
                {
                    return;
                }

                Forget(transactionId);
            }

            _sendMessage(TransactionReply.Rejected(transactionId, message));
        }

        public void Forget(string clOrderId)
        {
            using (_transactionIdsLock.Lock())
            {
                if (!_clientOrderIdToTransactionIdMap.TryGetValue(clOrderId, out var transactionId))
                {
                    return;
                }

                Forget(transactionId);
            }
        }

        public void Forget(Guid? transactionId)
        {
            if (transactionId != null)
            {
                Forget(transactionId.Value);
            }
        }

        public void Forget(Guid transactionId)
        {
            using (_transactionIdsLock.Lock())
            {
                if (_transactionIdToClientOrderIdMap.TryGetValue(transactionId, out var clOrderId))
                {
                    _transactionIdToClientOrderIdMap.Remove(transactionId);
                    _clientOrderIdToTransactionIdMap.Remove(clOrderId);
                }

                if (_transactionIdToSeqNumberMap.TryGetValue(transactionId, out var seqNum))
                {
                    _transactionIdToSeqNumberMap.Remove(transactionId);
                    _seqNumberIdToTransactionIdMap.Remove(seqNum);
                }
            }
        }
    }
}