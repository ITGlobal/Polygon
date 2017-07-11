using System.Collections.Generic;
using Polygon.Messages;

namespace Polygon.Connector.CGate
{
    /// <summary>
    /// класс-помошник для сообщения TransactionReply
    /// в нём хранятся все статусы ордеров, которые нужно дождаться
    /// </summary>
    internal class TransactionReplyHelper
    {
        /// <summary>
        /// сообщение
        /// </summary>
        public TransactionReply Reply { get; private set; }

        /// <summary>
        /// статусы
        /// </summary>
        public List<PendingState> PendingStates { get; private set; }

        public bool ReadyToSend
        {
            get { return PendingStates.Count == 0; }
        }

        public TransactionReplyHelper(TransactionReply reply)
        {
            Reply = reply;
            PendingStates = new List<PendingState>();
        }

        /// <summary>
        /// добавление ожидания статуса
        /// </summary>
        /// <param name="orderExchangeId"></param>
        /// <param name="pendingForCancel"></param>
        public void AddPending(string orderExchangeId, bool pendingForCancel)
        {
            PendingStates.Add(new PendingState(orderExchangeId, pendingForCancel));
        }

        /// <summary>
        /// статус, который ждём
        /// </summary>
        public class PendingState
        {
            /// <summary>
            /// id ордера, по которому ждём статус
            /// </summary>
            public string OrderExchangeId { get; private set; }

            /// <summary>
            /// ждём статус Cancel или другой
            /// </summary>
            public bool PendingForCancel { get; private set; }

            public PendingState(string orderExchangeId, bool pendingForCancel)
            {
                OrderExchangeId = orderExchangeId;
                PendingForCancel = pendingForCancel;
            }
        }

    }
}

