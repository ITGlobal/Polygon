using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using Polygon.Connector.QUIKLua.Adapter.Messages;
using Polygon.Connector.QUIKLua.Adapter.Messages.Transactions;

namespace Polygon.Connector.QUIKLua
{
    /// <summary>
    ///     Класс для хранения сущностей, используемых раутером
    /// </summary>
    internal sealed class Container
    {
        #region Fields

        private readonly ILog log = LogManager.GetLogger(typeof(Container));

        private readonly IRwLockObject locker = DeadlockMonitor.ReaderWriterLock(typeof(Container), "QLRouterContainer");
        private readonly IRwLockObject lockerReceivedFills = DeadlockMonitor.ReaderWriterLock(typeof(Container), "QLRouterContainer_lockerReceivedFills");

        private readonly List<long> currentSeccionTransactionIds = new List<long>();
        private readonly List<long> currentSeccionOrderIds = new List<long>();

        /// <summary>
        /// Список транзакций на постановку заявок, по которым пока не пришёл transactionReply
        /// </summary>
        private readonly List<long> newOrderTransactionsWithoutReplies = new List<long>();

        private readonly Dictionary<long, NewOrderTransaction> mapQuikTransIdOnNewOrderTransaction = new Dictionary<long, NewOrderTransaction>();
        private readonly Dictionary<long, KillOrderTransaction> mapQuikTransIdOnKillOrderTransaction = new Dictionary<long, KillOrderTransaction>();
        private readonly Dictionary<long, KillOrderTransaction> mapOrderIdIdOnKillOrderTransaction = new Dictionary<long, KillOrderTransaction>();
        private readonly Dictionary<long, ModifyOrderTransaction> mapQuikTransIdOnModifyOrderTransaction = new Dictionary<long, ModifyOrderTransaction>();
        private readonly Dictionary<long, ModifyOrderTransaction> mapOrderIdOnModifyOrderTransaction = new Dictionary<long, ModifyOrderTransaction>();
        private readonly Dictionary<long, List<QLOrderStateChange>> orderStateChanges = new Dictionary<long, List<QLOrderStateChange>>();
        private readonly Dictionary<long, QLFill> pendingFills = new Dictionary<long, QLFill>();
        private readonly Dictionary<long, long> mapQuikTransIdOnOrderExchangeId = new Dictionary<long, long>();
        private readonly Dictionary<long, Order> mapOrderIdOnOrder = new Dictionary<long, Order>();
        private readonly Dictionary<long, List<QLOrderStateChange>> mapOrderIdOnPendingOrderStateChange = new Dictionary<long, List<QLOrderStateChange>>();
        private readonly Dictionary<long, NewOrderTransaction> mapOrderIdOnNewOrderTransaction = new Dictionary<long, NewOrderTransaction>();
        private readonly List<long> receivedFills = new List<long>();
        private readonly List<QLTransactionReply> pendingTransactionReplies = new List<QLTransactionReply>();

        #endregion

        #region Public methods

        /// <summary>
        /// Добавить в очередь ответ на транзакцию
        /// </summary>
        /// <param name="tr"></param>
        public void PutPendingTransactionReply(QLTransactionReply tr)
        {
            using (locker.WriteLock())
            {
                pendingTransactionReplies.Add(tr);
            }
        }

        /// <summary>
        /// Сохранить сделку, требующую отложенной обработки
        /// </summary>
        /// <param name="message"></param>
        public void PutPendingFill(QLFill message)
        {
            log.Debug().Print("Postpone fill processing", LogFields.Message(message));
            using (locker.WriteLock())
            {
                if (pendingFills.ContainsKey(message.trade_num))
                {
                    log.Warn().Print($"Fill duplicate received", LogFields.Id(message.trade_num));
                    return;
                }

                pendingFills[message.trade_num] = message;
            }
        }

        /// <summary>
        /// Сохранить заявку по её биржевому номеру
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="order"></param>
        public void PutOrder(long orderNum, Order order)
        {
            using (locker.WriteLock())
            {
                mapOrderIdOnOrder[orderNum] = order;
            }
        }

        /// <summary>
        /// Сохранить идентификатор полученной сделки. Для дальнейшего анализа на получение дубликата сообщения
        /// </summary>
        /// <param name="id">Биржевой идентификатор сделки</param>
        /// <returns>true - если сделка уже есть в контейнере, иначе false</returns>
        public bool PutFillId(QLFill fill)
        {
            using (lockerReceivedFills.WriteLock())
            {
                if (receivedFills.Contains(fill.trade_num))
                {
                    log.Warn().Print($"Fill duplicate received", LogFields.Id(fill.trade_num));
                    return true;
                }

                receivedFills.Add(fill.trade_num);
            }

            return false;
        }

        /// <summary>
        ///     Возвращает фолс, если такой статус уже приходил 
        ///     (иногда квик присылает один и тот же QLOrderStateChange по нескольку раз, зафиксированы случаи по три присыла).
        /// 
        /// ВАЖНО: этот метод также расчитывает реально исполненное количество в текущем PartiallyFilled изменение
        /// </summary>
        public bool PutOrderStateChange(QLOrderStateChange osc)
        {
            List<QLOrderStateChange> changes;

            using (locker.WriteLock())
            {
                if (!orderStateChanges.TryGetValue(osc.order_num, out changes))
                {
                    orderStateChanges.Add(osc.order_num, (changes = new List<QLOrderStateChange>()));
                }

                if (changes.Contains(osc))
                {
                    return false;
                }

                var prevFilledQuantity = changes.Sum(_ => _.State == OrderState.PartiallyFilled ? _.filled : 0);
                var totalFilledQuantiry = osc.qty - osc.balance;
                osc.filled = totalFilledQuantiry - prevFilledQuantity;

                changes.Add(osc);

                if (currentSeccionTransactionIds.Contains(osc.trans_id))
                {
                    mapQuikTransIdOnOrderExchangeId[osc.trans_id] = osc.order_num;
                }
            }

            return true;
        }

        /// <summary>
        /// Сохраняет биржевой номер заявки в коллекции своих (отправленных в текущую сессию работы) заявок
        /// </summary>
        /// <param name="orderExchangeId"></param>
        public void PutOrderExchangeId(long orderExchangeId)
        {
            using (locker.ReadLock())
            {
                if (!currentSeccionOrderIds.Contains(orderExchangeId))
                {
                    currentSeccionOrderIds.Add(orderExchangeId);
                }
            }
        }

        /// <summary>
        /// Сохранить транзакцию на изменение заявки по её идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyOrderTransaction"></param>
        public void PutTransaction(long id, ModifyOrderTransaction modifyOrderTransaction)
        {
            using (locker.WriteLock())
            {
                currentSeccionTransactionIds.Add(id);
                mapQuikTransIdOnModifyOrderTransaction.Add(id, modifyOrderTransaction);
                long orderExchangeId = long.MinValue;
                if (long.TryParse(modifyOrderTransaction.OrderExchangeId, out orderExchangeId))
                {
                    mapQuikTransIdOnOrderExchangeId[id] = orderExchangeId;
                    mapOrderIdOnModifyOrderTransaction[orderExchangeId] = modifyOrderTransaction;
                }
                else
                {
                    log.Error().Print(
                        $"Can't save modify transaction. Unable to parse {LogFieldNames.ExchangeOrderId} from {modifyOrderTransaction.OrderExchangeId}"
                        );
                }
            }
        }

        /// <summary>
        /// Сохранить транзакцию на постановку заявки по её идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newOrderTransaction"></param>
        public void PutTransaction(long id, NewOrderTransaction newOrderTransaction)
        {
            using (locker.WriteLock())
            {
                currentSeccionTransactionIds.Add(id);
                newOrderTransactionsWithoutReplies.Add(id);
                mapQuikTransIdOnNewOrderTransaction.Add(id, newOrderTransaction);
            }
        }
        
        /// <summary>
        /// Сохранить транзакцию на снятие заявки по её идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="killOrderTransaction"></param>
        public void PutTransaction(long id, KillOrderTransaction killOrderTransaction)
        {
            using (locker.WriteLock())
            {
                long orderExchangeId = long.MinValue;
                if (long.TryParse(killOrderTransaction.OrderExchangeId, out orderExchangeId))
                {
                    currentSeccionTransactionIds.Add(id);
                    mapQuikTransIdOnKillOrderTransaction[id] = killOrderTransaction;
                    mapOrderIdIdOnKillOrderTransaction[orderExchangeId] = killOrderTransaction;
                    mapQuikTransIdOnOrderExchangeId[id] = orderExchangeId;
                }
                else
                {
                    log.Error().Print(
                        $"Can't save kill transaction. Unable to parse {LogFieldNames.ExchangeOrderId} from {killOrderTransaction.OrderExchangeId}"
                        );
                }
            }
        }

        /// <summary>
        /// Обработать ответ на постановку транзакции, возвращает список QLOrderStateChange, обработка которых была отложена из-за
        /// отсутсвия trans_id
        /// </summary>
        /// <param name="message"></param>
        /// <param name="orderExchangeId"></param>
        public List<QLOrderStateChange> PutTransactionReply(QLTransactionReply message, long orderExchangeId)
        {
            // необходимо отправить на обработку отложенные OrderStateChange сообщения
            List<QLOrderStateChange> oscmToFire;
            using (locker.WriteLock())
            {
                if (!newOrderTransactionsWithoutReplies.Contains(message.trans_id))
                {
                    return null;
                }

                var newOrderTransaction = mapQuikTransIdOnNewOrderTransaction[message.trans_id];
                newOrderTransactionsWithoutReplies.Remove(message.trans_id);

                mapOrderIdOnNewOrderTransaction[orderExchangeId] = newOrderTransaction;

                // сначала смотрим, есть ли отложенные oscm конкретно для этого номера заявки
                if (mapOrderIdOnPendingOrderStateChange.TryGetValue(orderExchangeId, out oscmToFire))
                {
                    log.Info().Print(
                        $"PutTransactionReply: to fire {oscmToFire.Count} pending OSCMs for {LogFields.ExchangeOrderId(orderExchangeId)} and {LogFields.TransactionId(message.trans_id)}"
                        );
                    mapOrderIdOnPendingOrderStateChange.Remove(orderExchangeId);

                    // проставляем trans_id в oscm, т.к. проблема заключается в том, что когда oscm приходит раньше transReply, то osc.trans_id == 0
                    foreach (var oscm in oscmToFire)
                    {
                        oscm.trans_id = message.trans_id;
                    }
                }

                // если транзакций без ответа не осталось, тогда нужно запроцессить все оставшиеся oscm, т.к. ответов на транзакции больше не последует
                if (newOrderTransactionsWithoutReplies.Count == 0 && mapOrderIdOnPendingOrderStateChange.Count!=0)
                {
                    log.Info().Print("PutTransactionReply: to fire all other pending OSCMs, since there is no more NewOrderTransaction without reply");
                    if(oscmToFire == null) oscmToFire = new List<QLOrderStateChange>();

                    foreach (var pendingOscms in mapOrderIdOnPendingOrderStateChange)
                    {
                        oscmToFire.AddRange(pendingOscms.Value);
                    }
                }
            }

            if (oscmToFire == null)
            {
                return null;
            }

            return oscmToFire;
        }

        /// <summary>
        /// Положить OSCM в отложенную обработку
        /// </summary>
        /// <param name="oscm"></param>
        public void PutPendingOrderStateChange(QLOrderStateChange oscm)
        {
            log.Info().Print("Postpone OSCM processing, there are transactions without replies.", LogFields.Message(oscm));
            List<QLOrderStateChange> oscms;

            using (locker.WriteLock())
            {
                if (!mapOrderIdOnPendingOrderStateChange.TryGetValue(oscm.order_num, out oscms))
                {
                    mapOrderIdOnPendingOrderStateChange[oscm.order_num] = oscms = new List<QLOrderStateChange>();
                }

                oscms.Add(oscm);
            }
        }

        /// <summary>
        /// Возвращает копию коллекции ожидающих ответов на транзакции и очищает её
        /// </summary>
        public List<QLTransactionReply> GetPendingTransactionReplies()
        {
            var rValue = new List<QLTransactionReply>();

            using (locker.WriteLock())
            {
                rValue.AddRange(pendingTransactionReplies);
                pendingTransactionReplies.Clear();
            }

            return rValue;
        }

        /// <summary>
        /// Возвращает копию коллекции ожидающих сделок
        /// </summary>
        public IEnumerable<QLFill> GetPendingFills()
        {
            var rValue = new List<QLFill>();

            using (locker.WriteLock())
            {
                rValue.AddRange(pendingFills.Values);
                pendingFills.Clear();
            }

            return rValue;
        }

        /// <summary>
        /// Возвращает последний полученный статус заявки, связанный с транзакцией с идентификатором transId
        /// </summary>
        /// <param name="reply">Квиковый идентификатор транзакции</param>
        /// <returns></returns>
        public QLOrderStateChange GetLastOrderStateChangeForTransactionId(QLTransactionReply reply)
        {
            QLOrderStateChange rValue;
            long orderId;

            using (locker.ReadLock())
            {
                if (!mapQuikTransIdOnOrderExchangeId.TryGetValue(reply.trans_id, out orderId))
                {
                    return null;
                }

                List<QLOrderStateChange> orderStates;
                if (!orderStateChanges.TryGetValue(orderId, out orderStates))
                {
                    return null;
                }

                rValue = orderStates.LastOrDefault();
            }

            return rValue;
        }

        /// <summary>
        /// Возвращает последний OSCM для указанного биржевого номера заявки
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public QLOrderStateChange GetLastOrderStateChangeForOrderId(long orderId)
        {
            QLOrderStateChange rValue;

            using (locker.ReadLock())
            {
                List<QLOrderStateChange> orderStates;
                if (!orderStateChanges.TryGetValue(orderId, out orderStates))
                {
                    return null;
                }

                rValue = orderStates.LastOrDefault();
            }

            return rValue;
        }

        /// <summary>
        /// Получить транзакцию на постоновку заявки по её идентификатору
        /// </summary>
        /// <param name="transId">Идентификатор транзакции</param>
        /// <param name="orderId">Биржевой идентификатор заявки</param>
        /// <returns></returns>
        public NewOrderTransaction GetNewOrderTransaction(long transId, long orderId)
        {
            using (locker.ReadLock())
            {
                NewOrderTransaction rValue = null;
                if (!mapQuikTransIdOnNewOrderTransaction.TryGetValue(transId, out rValue))
                    mapOrderIdOnNewOrderTransaction.TryGetValue(orderId, out rValue);
                return rValue;
            }
        }

        /// <summary>
        /// Получить транзакцию на постоновку заявки по её идентификатору
        /// </summary>
        /// <param name="transId">Идентификатор транзакции</param>
        /// <returns></returns>
        public NewOrderTransaction GetNewOrderTransaction(long transId)
        {
            using (locker.ReadLock())
            {
                NewOrderTransaction rValue = null;
                mapQuikTransIdOnNewOrderTransaction.TryGetValue(transId, out rValue);
                return rValue;
            }
        }

        /// <summary>
        /// Получить транзакцию на изменение заявки по её идентификатору
        /// </summary>
        /// <param name="transId"></param>
        /// <returns></returns>
        public ModifyOrderTransaction GetModifyOrderTransactionByTransId(long transId)
        {
            ModifyOrderTransaction rValue = null;
            using (locker.ReadLock())
            {
                mapQuikTransIdOnModifyOrderTransaction.TryGetValue(transId, out rValue);
            }
            return rValue;
        }

        /// <summary>
        /// Получить транзакцию на изменение заявки по номеру заявки
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ModifyOrderTransaction GetModifyOrderTransactionByOrderId(long orderId)
        {
            ModifyOrderTransaction rValue = null;
            using (locker.ReadLock())
            {
                mapOrderIdOnModifyOrderTransaction.TryGetValue(orderId, out rValue);
            }
            return rValue;
        }

        /// <summary>
        /// Получить транзакцию на снятие заявки по её идентификатору транзакции (trans Id)
        /// </summary>
        /// <param name="transId"></param>
        /// <returns></returns>
        public
        KillOrderTransaction GetKillOrderTransactionByTransId(long transId)
        {
            using (locker.ReadLock())
            {
                KillOrderTransaction rValue = null;
                mapQuikTransIdOnKillOrderTransaction.TryGetValue(transId, out rValue);
                return rValue;
            }
        }

        /// <summary>
        /// Получить транзакцию на снятие заявки по идентификатору заявки
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public KillOrderTransaction GetKillOrderTransactionByOrderId(long orderId)
        {
            KillOrderTransaction rValue = null;
            using (locker.ReadLock())
            {
                mapOrderIdIdOnKillOrderTransaction.TryGetValue(orderId, out rValue);
            }
            return rValue;
        }

        /// <summary>
        /// Получить заявку по её биржевому номеру
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public Order GetOrder(long orderNum)
        {
            using (locker.ReadLock())
            {
                Order order;
                mapOrderIdOnOrder.TryGetValue(orderNum, out order);
                return order;
            }
        }

        /// <summary>
        /// Возвращает true если заявка с биржевым номером orderExchangeId отправлялась в текущей сессии работы программы
        /// </summary>
        /// <param name="orderExchangeId"></param>
        /// <returns></returns>
        public bool IsCurrentSessionOrder(long orderExchangeId)
        {
            using (locker.ReadLock())
            {
                return currentSeccionOrderIds.Contains(orderExchangeId);
            }
        }

        /// <summary>
        /// Удалить отложенный ответ на транзакцию
        /// </summary>
        public void RemoveProcessedPendingReply(QLTransactionReply tr)
        {
            log.Debug().Print("Remove processed TR", LogFields.TransactionId(tr.trans_id));
            using (locker.WriteLock())
            {
                pendingTransactionReplies.Remove(tr);
            }
        }

        /// <summary>
        /// Проверяет есть ли транзакции на постановку заявки, на которые ещё не пришёл ответ
        /// </summary>
        /// <returns></returns>
        public bool HasUnrepliedTransactions()
        {
            using (locker.ReadLock())
            {
                if (newOrderTransactionsWithoutReplies.Any())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Возаращает true, если транзакция с идентификатором transactionId ещё не дождалась своего transactionReply
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public bool IsTransactionUnreplied(long transactionId)
        {
            using (locker.ReadLock())
            {
                return newOrderTransactionsWithoutReplies.Contains(transactionId);
            }
        }

        #endregion
    }
}

