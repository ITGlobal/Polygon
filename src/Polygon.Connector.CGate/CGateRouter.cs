using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using CGateAdapter;
using CGateAdapter.Messages;
using CGateAdapter.Messages.FortsMessages;
using CGateAdapter.Messages.FutInfo;
using CGateAdapter.Messages.FutTrades;
using CGateAdapter.Messages.Part;
using CGateAdapter.Messages.Pos;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Messages;
using CgmSysEvents = CGateAdapter.Messages.OptInfo.CgmSysEvents;
using LogMessage = Polygon.Diagnostics.LogMessage;

namespace Polygon.Connector.CGate
{
    partial class CGateRouter : OrderRouter, ICGateMessageVisitor
    {
        #region Fields

        /// <summary>
        /// Объёкт синхронизации
        /// </summary>
        private readonly IRwLockObject locker = DeadlockMonitor.ReaderWriterLock<CGateRouter>("locker", LockRecursionPolicy.SupportsRecursion);

        /// <summary>
        /// Низкоуровневый адаптер cgate
        /// </summary>
        private readonly ICGAdapter cgAdapter;

        /// <summary>
        /// Код брокерской фирмы
        /// </summary>
        private string brokerCode;

        /// <summary>
        /// Инкрементируемый идентификатор транзакции
        /// </summary>
        private int newTransactionId;

        #region SessionInfo

        /// <summary>
        /// Нужно ли обрабатывать и транслировать информацию о сессии
        /// </summary>
        private readonly bool processSessionInfo = true;

        private readonly SessionInfo sessionInfo = new SessionInfo();

        [Flags]
        private enum SessionInfoStatus
        {
            None = 0,
            ServerTimeReceived = 1,
            SessionInfoReceived = 2
        }

        private SessionInfoStatus sessionInfoStatus = SessionInfoStatus.None;

        private const SessionInfoStatus SessionInfoReady = SessionInfoStatus.ServerTimeReceived | SessionInfoStatus.SessionInfoReceived;

        #endregion

        /// <summary>
        /// Очередь отложенных сообщений, которые не удалось обработать сразу
        /// </summary>
        private readonly ConcurrentQueue<CGateMessage> pendingIsinResolutionMessages = new ConcurrentQueue<CGateMessage>();

        /// <summary>
        /// Задача процессинга отложенных сообщений
        /// </summary>
        private Task pendingMessagesProcessingTask;

        /// <summary>
        /// Событие, поднимаемое, когда необходимо обработать очередь отложенных сообщений
        /// </summary>
        private readonly AutoResetEvent processPendingMessagesEvent = new AutoResetEvent(false);

        /// <summary>
        /// Используется для остановки фоновых задач
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Позволяет получить short_isin по isin_id, обартно и т.д.
        /// </summary>
        private readonly CGateInstrumentResolver instrumentIsinResolver;

        /// <summary>
        ///     Конвертер инструментов CGate
        /// </summary>
        private readonly InstrumentConverter<InstrumentData> instrumentConverter;

        /// <summary>
        /// Отдельный класс, обрабатывающий обновления параметров инструментов
        /// </summary>
        private readonly CGateInstrumentParamsEmitter instrumentParamsEmitter;

        /// <summary>
        /// Контэйнеры, хранящие промежуточные объекты и методы доступа к ним
        /// </summary>
        private readonly OrderRouterContainer container = new OrderRouterContainer();

        #endregion

        #region .ctor

        public CGateRouter(
            ICGAdapter adapter,
            CGateInstrumentResolver instrumentIsinResolver,
            CGateInstrumentParamsEmitter instrumentParamsEmitter)
            : base(true, null, null, OrderRouterMode.ExternalSessionsRenewable)
        {
            AvailableAccounts = new List<string>();
            cgAdapter = adapter;
            this.instrumentIsinResolver = instrumentIsinResolver;
            this.instrumentParamsEmitter = instrumentParamsEmitter;
            instrumentIsinResolver.OnNewIsinResolved += CGateIsinResolverOnNewIsinResolved;
            cgAdapter.ExecutionMessageReceived += CGateAdapterStreamMessageHandler;
            // TODO отправку сообщений подписчикам тоже нужно вынести в отдельную задачу
        }

        #endregion

        #region IOrderRouter

        public override void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            pendingMessagesProcessingTask = Task.Factory.StartNew(ProcessPendingMessages);
        }

        public override void Stop()
        {
            cancellationTokenSource?.Cancel();
            processPendingMessagesEvent.Set();
            pendingMessagesProcessingTask?.Wait();
        }

        protected override void SendTransactionImp(Transaction transaction)
        {
            // TODO ITransactionVisitor
            var newOrder = transaction as NewOrderTransaction;
            if (newOrder != null)
            {
                SendNewOrderTransaction(newOrder);
                return;
            }

            var killOrder = transaction as KillOrderTransaction;
            if (killOrder != null)
            {
                SendKillOrderTransaction(killOrder);
                return;
            }
        }

        #endregion

        #region Private methods

        #region SendKillOrderTransaction

        /// <summary>
        /// Отправка транзакции на снятие заявки
        /// </summary>
        private void SendKillOrderTransaction(KillOrderTransaction killOrder)
        {
            var data = instrumentIsinResolver.GetCGateInstrumentData(killOrder.Instrument);
            if (data == null)
            {
                OnMessageReceived(new TransactionReply
                {
                    Success = false,
                    Message = $"Unable to find instrument {killOrder.Instrument.Code}",
                    TransactionId = killOrder.TransactionId
                });
                return;
            }

            var iType = instrumentIsinResolver.GetInstrumentType(data.Symbol);

            using (locker.WriteLock())
            {
                if (iType == InstrumentType.Futures)
                {
                    var killOrderTransaction = CreateFutDelOrder(killOrder);
                    Logger.Debug().PrintFormat("Sending {0}", killOrderTransaction);
                    container.AddTransaction(killOrderTransaction.UserId, killOrder);
                    cgAdapter.SendMessage(killOrderTransaction);
                }
                else if (iType == InstrumentType.Option)
                {
                    var killOrderTransaction = CreateOptDelOrder(killOrder);
                    Logger.Debug().PrintFormat("Sending {0}", killOrderTransaction);
                    container.AddTransaction(killOrderTransaction.UserId, killOrder);
                    cgAdapter.SendMessage(killOrderTransaction);
                }
            }
        }

        private CgmFutDelOrder CreateFutDelOrder(KillOrderTransaction killOrder)
        {
            return new CgmFutDelOrder
            {
                OrderId = long.Parse(killOrder.OrderExchangeId),
                BrokerCode = brokerCode,
                UserId = (uint)IncrementOrderTransId()
            };
        }

        private CgmOptDelOrder CreateOptDelOrder(KillOrderTransaction killOrder)
        {
            return new CgmOptDelOrder
            {
                OrderId = long.Parse(killOrder.OrderExchangeId),
                BrokerCode = brokerCode,
                UserId = (uint)IncrementOrderTransId()
            };
        }

        /// <summary>
        /// Обработка ответа на отправку транзакции на снятие заявки
        /// </summary>
        private void HandleDelOrderReply(CGateDelOrderReply reply)
        {
            Transaction transaction = null;
            var extId = reply.UserId;
            var errCode = reply.Code;

            // сообщения, которые нужно будет выкинуть в конце после обработки, когда выйдем из под лока
            var messagesToRiseInEvent = new List<Message>();

            using (locker.WriteLock())
            {
                try
                {
                    if (!container.RemoveTransaction(extId, out transaction))
                    {
                        HandleError(LogMessage.Make(
                            $"Error while sending transaction: transaction with {LogFields.ExchangeId(extId)} not found",
                            LogFields.ErrorCode(reply.Code),
                            LogFields.Message(reply.Message))
                            ); // ok
                        return;
                    }

                    if (errCode == CGateReturnCodes.Success)
                    {
                        var killOrderTransaction = (KillOrderTransaction)transaction;
                        var id = killOrderTransaction.OrderExchangeId;

                        if (Logger.IsDebugEnabled)
                        {
                            Logger.Debug().Print(
                                $"Order {LogFields.TransactionId(killOrderTransaction.TransactionId)} succesfully cancelled",
                                LogFields.ExchangeOrderId(id),
                                LogFields.Message(reply.Message)
                                );
                        }

                        var replyMessage = new TransactionReply
                        {
                            Message = $"Order {killOrderTransaction.TransactionId} succesfully cancelled",
                            Success = true,
                            TransactionId = killOrderTransaction.TransactionId
                        };


                        Order killedOrder;

                        if (!container.TryGetOrder(id, out killedOrder))
                        {
                            Logger.Error().Print($"Can't find order {LogFields.ExchangeOrderId(id)}, but successful {nameof(TransactionReply)} will be sent");
                            return;
                        }

                        if (killedOrder.State != OrderState.Cancelled)
                        {
                            // если выполняется условие в if, то это значит, что заявка снялась полность, со всем объёмом и 
                            // мы можем сразу отправить OrderStateChange со статусом cancell
                            if (killedOrder.ActiveQuantity == (uint)reply.Amount)
                            {
                                killedOrder.State = OrderState.Cancelled;

                                // создаём OrderStateChange из ответа на постановку транзакции на снятие заявки, т.к. 
                                var oscm = new OrderStateChangeMessage
                                {
                                    FilledQuantity = 0,
                                    ActiveQuantity = (uint)reply.Amount,
                                    OrderExchangeId = id,
                                    State = OrderState.Cancelled,
                                    TransactionId = transaction.TransactionId
                                };
                                messagesToRiseInEvent.Add(oscm);
                                messagesToRiseInEvent.AddRange(ProcessOrderStateChange(oscm));
                                messagesToRiseInEvent.Add(replyMessage);
                            }

                            // иначе нам сначало нужно дождаться сообщения о снятии заявки из order_log-а и только потом отправить TransactionReply
                            else
                            {
                                // хэлпер для TransactionReply на снятие
                                var replyHelper = new TransactionReplyHelper(replyMessage);
                                // хотим дождаться Cancel для этой заявки: killOrderTransaction.OrderExchangeId
                                replyHelper.AddPending(killOrderTransaction.OrderExchangeId, true);
                                // добавляем ожидание статуса в словарь
                                container.AddPendingReply(killOrderTransaction.OrderExchangeId, replyHelper);
                            }
                        }
                        // Ордер уже имеет статус Cancelled, в этом случае нужно просто отправить TransactionReply 
                        else
                        {
                            Logger.Debug().PrintFormat("Raise {0} for {1}", reply, killedOrder);
                            messagesToRiseInEvent.Add(replyMessage);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(reply.Message) &&
                            (
                            reply.Message.Contains("Не найдена заявка для удаления") ||
                            reply.Message.Contains("Вы не можете снять данную заявку")
                            ))
                        {
                            Logger.Warn().Print($"Failed kill transaction will be treated as successfull, status will be changed from {errCode} to 3");
                            messagesToRiseInEvent.Add(new TransactionReply
                            {
                                Success = true,
                                Message = $"{reply.Message}",
                                TransactionId = transaction.TransactionId
                            });
                        }
                        else
                        {
                            HandleError(LogMessage.Make(
                                $"Error while sending transaction {LogFields.TransactionId(transaction.TransactionId)}",
                                LogFields.ErrorCode(errCode),
                                LogFields.Message(reply.Message)));
                            messagesToRiseInEvent.Add(new TransactionReply
                            {
                                Success = false,
                                Message = $"Error code: {errCode}, Message: {reply.Message}",
                                TransactionId = transaction.TransactionId
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleExceptionError(
                        ex,
                        LogMessage.MakeString(
                            "Error while sending transaction",
                            LogFields.TransactionId(transaction?.TransactionId),
                            LogFields.ErrorCode(errCode))
                        );

                    messagesToRiseInEvent.Add(new TransactionReply
                    {
                        Success = false,
                        Message = ex.Message,
                        TransactionId = transaction?.TransactionId ?? Guid.Empty
                    });
                }
            }

            // выбрасываем все сообщения подписчикам, находясь уже вне лока
            RiseEvents(messagesToRiseInEvent);
        }

        #endregion

        #region SendNewOrderTransaction

        /// <summary>
        /// Отправка транзакции на постановку новой заявки
        /// </summary>
        private void SendNewOrderTransaction(NewOrderTransaction newOrder)
        {
            var data = instrumentIsinResolver.GetCGateInstrumentData(newOrder.Instrument);
            if (data == null)
            {
                OnMessageReceived(new TransactionReply
                {
                    Success = false,
                    Message = $"Unable to find instrument {newOrder.Instrument.Code}",
                    TransactionId = newOrder.TransactionId
                });
                return;
            }

            var iType = instrumentIsinResolver.GetInstrumentType(data.Symbol);

            using (locker.WriteLock())
            {
                if (iType == InstrumentType.Futures)
                {
                    var futAddOrder = CreateFutAddOrder(newOrder, data);
                    Logger.Debug().PrintFormat("Sending {0}", futAddOrder);
                    container.AddTransaction(futAddOrder.ExtId, newOrder);
                    container.AddOrder((uint)futAddOrder.ExtId, new Order(newOrder));
                    cgAdapter.SendMessage(futAddOrder);
                }
                else if (iType == InstrumentType.Option)
                {
                    var optAddOrder = CreateOptAddOrder(newOrder, data);
                    Logger.Debug().PrintFormat("Sending {0}", optAddOrder);
                    container.AddTransaction(optAddOrder.ExtId, newOrder);
                    container.AddOrder((uint)optAddOrder.ExtId, new Order(newOrder));
                    cgAdapter.SendMessage(optAddOrder);
                }
            }
        }

        private CgmFutAddOrder CreateFutAddOrder(NewOrderTransaction newOrder, InstrumentData data)
        {
            return new CgmFutAddOrder
            {
                Isin = instrumentIsinResolver.GetIsinByShortIsin(data.Symbol),
                Amount = (int)newOrder.Quantity,
                Price = newOrder.Price.ToString("0.######", CultureInfo.InvariantCulture),
                BrokerCode = brokerCode,
                ClientCode = newOrder.Account.Substring(4),
                Dir = newOrder.Operation == OrderOperation.Buy ? 1 : 2,
                ExtId = IncrementOrderTransId(),
                Type = 1
            };
        }

        private CgmOptAddOrder CreateOptAddOrder(NewOrderTransaction newOrder, InstrumentData data)
        {
            return new CgmOptAddOrder
            {
                Isin = instrumentIsinResolver.GetIsinByShortIsin(data.Symbol),
                Amount = (int)newOrder.Quantity,
                Price = newOrder.Price.ToString("0.######", CultureInfo.InvariantCulture),
                BrokerCode = brokerCode,
                ClientCode = newOrder.Account.Substring(4),
                Dir = newOrder.Operation == OrderOperation.Buy ? 1 : 2,
                ExtId = IncrementOrderTransId(),
                Type = 1
            };
        }

        /// <summary>
        /// Обработка ответа (return value) на транзакцию постановки новой заявки
        /// </summary>
        /// <param name="reply"></param>
        private void HandleAddOrderReply(CGateAddOrderReply reply)
        {
            Logger.Debug().PrintFormat("Handle: {0}", reply);
            Transaction transaction = null;
            var transId = reply.UserId;
            var errCode = reply.Code;
            // сообщения, которые нужно будет выкинуть в конце после обработки, когда выйдем из под лока
            var messagesToRiseInEvent = new List<Message>();

            try
            {
                using (locker.WriteLock())
                {
                    if (!container.RemoveTransaction(transId, out transaction))
                    {
                        HandleError(LogMessage.MakeString(
                            "Transaction reply error: transaction is not found",
                            LogFields.TransactionId(transId),
                            LogFields.ErrorCode(errCode),
                            LogFields.Message(reply.Message))
                            ); // ok
                        return;
                    }

                    if (errCode == CGateReturnCodes.Success)
                    {
                        messagesToRiseInEvent.AddRange(ProcessSuccesfullAddOrderReply(reply, (NewOrderTransaction)transaction));
                    }
                    else if (errCode == CGateReturnCodes.FillOrKillErrorCode)
                    {
                        messagesToRiseInEvent.AddRange(ProcessFillOrKillNewOrderError(reply, (KillOrderTransaction)transaction));
                    }
                    else
                    {
                        HandleError(LogMessage.MakeString(
                            "Transaction reply error",
                            LogFields.TransactionId(transaction.TransactionId),
                            LogFields.ErrorCode(errCode))
                            ); // ok
                        messagesToRiseInEvent.Add(new TransactionReply
                        {
                            Success = false,
                            Message = $"Error code: {errCode}, Message: {reply.Message}",
                            TransactionId = transaction.TransactionId
                        });
                    }
                }

                RiseEvents(messagesToRiseInEvent);
            }
            catch (Exception ex)
            {
                HandleExceptionError(
                    ex,
                    LogMessage.MakeString(
                        "Transaction reply error",
                        LogFields.TransactionId(transaction?.TransactionId),
                        LogFields.ErrorCode(errCode)
                        ));
                OnMessageReceived(new TransactionReply
                {
                    Success = false,
                    Message = ex.Message,
                    TransactionId = transaction?.TransactionId ?? Guid.Empty
                });
            }
        }

        /// <summary>
        /// Обработка ошибки постановки заявки fill or kill
        /// </summary>
        private IEnumerable<Message> ProcessFillOrKillNewOrderError(CGateAddOrderReply reply, KillOrderTransaction transaction)
        {
            var transId = reply.UserId;
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug().Print(
                    "Fill-Or-Kill order wasn't placed",
                    LogFields.TransactionId(transaction.TransactionId),
                    LogFields.Message(reply.Message)
                    );
            }

            // сообщения, которые нужно будет выкинуть в конце после обработки, когда выйдем из под лока
            var messagesToRiseInEvent = new List<Message>();

            Order newOrder = null;

            if (container.TryRemoveOrder(transId, out newOrder))
            {
                newOrder.State = OrderState.Cancelled;
                var oscm = new OrderStateChangeMessage
                {
                    FilledQuantity = 0,
                    ActiveQuantity = newOrder.Quantity,
                    State = OrderState.Cancelled,
                    TransactionId = newOrder.TransactionId
                };
                messagesToRiseInEvent.Add(oscm);
                messagesToRiseInEvent.AddRange(ProcessOrderStateChange(oscm));
            }

            messagesToRiseInEvent.Add(new TransactionReply
            {
                Success = true,
                Message = $"Fill-Or-Kill Order {transaction.TransactionId} wasn't placed",
                TransactionId = transaction.TransactionId
            });

            return messagesToRiseInEvent;
        }

        /// <summary>
        /// Обработка успешной постановки новой заявки
        /// </summary>
        private IEnumerable<Message> ProcessSuccesfullAddOrderReply(CGateAddOrderReply reply, NewOrderTransaction newOrderTransaction)
        {
            var transId = reply.UserId;
            var orderIdString = reply.OrderId.ToString();
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug().Print(
                    "Order successfully placed",
                    LogFields.TransactionId(newOrderTransaction.TransactionId),
                    LogFields.ExchangeOrderId(orderIdString),
                    LogFields.Message(reply.Message)
                    );
            }
            // сообщения, которые нужно будет выкинуть в конце после обработки, когда выйдем из под лока
            var messagesToRiseInEvent = new List<Message>();

            // если ещё не было статусов по ней, переложим в другую коллекцию, запишем id и отправим сообщение
            Order order;
            if (!container.TryGetOrder(orderIdString, out order))
            {
                if (container.TryRemoveOrder(transId, out order))
                {
                    container.AddOrder(orderIdString, order);

                    order.State = OrderState.Active;
                    order.ActiveQuantity = newOrderTransaction.Quantity;
                    order.OrderExchangeId = orderIdString;

                    var oscm = new OrderStateChangeMessage
                    {
                        FilledQuantity = 0,
                        ActiveQuantity = order.ActiveQuantity,
                        OrderExchangeId = orderIdString,
                        State = OrderState.Active,
                        TransactionId = order.TransactionId
                    };
                    messagesToRiseInEvent.Add(oscm);
                    messagesToRiseInEvent.AddRange(ProcessOrderStateChange(oscm));
                }
            }

            // тут отправим Reply, так как первый статус по заявке уже был
            messagesToRiseInEvent.Add(new TransactionReply
            {
                Success = true,
                Message = $"Order {newOrderTransaction.TransactionId} successfully placed",
                TransactionId = newOrderTransaction.TransactionId
            });

            return messagesToRiseInEvent;
        }

        #endregion

        #region Handle order state change

        /// <summary>
        /// Обработка изменения статуса заявки
        /// </summary>
        /// <param name="reply"></param>
        private void HandleOrderStateChange(CGateOrder reply)
        {
            Logger.Debug().PrintFormat("Handle: {0}", reply);

            // сообщения, которые нужно будет выкинуть в конце после обработки, когда выйдем из под лока
            var messagesToRiseInEvent = new List<Message>();

            try
            {
                string instrumentCode;
                var orderIdString = reply.IdOrd.ToString();
                var isExternalOrder = false;

                if (!CheckInstrumentCode(reply, out instrumentCode))
                {
                    return;
                }

                if (!CheckAccountPermission(reply))
                {
                    return;
                }

                if (!FilterByComment(reply))
                {
                    return;
                }

                using (locker.WriteLock())
                {
                    Order order;
                    // если по заявке ещё не было получено никаких статусов и ранее не был известен её биржевой номер
                    if (!container.TryGetOrder(orderIdString, out order))
                    {
                        isExternalOrder = ProcessExternalOrderStateChange(reply, instrumentCode, ref order);
                    }

                    // Если завяка уже была снята реньше, то ничего дальше делать не нужно
                    if (order.State == OrderState.Cancelled)
                    {
                        Logger.Warn().PrintFormat(
                            "Order {0} is {1}. Exitting {2} method for {3}.",
                            LogFields.ExchangeOrderId(order.OrderExchangeId),
                            OrderState.Cancelled,
                            nameof(HandleOrderStateChange),
                            reply
                            );
                        return;
                    }

                    var oscm = new OrderStateChangeMessage(order.TransactionId, order.OrderExchangeId, reply.Moment);
                    messagesToRiseInEvent.Add(oscm);

                    if (isExternalOrder)
                    {
                        order.DateTime = oscm.ChangeTime;
                        messagesToRiseInEvent.Add(new ExternalOrderMessage { Order = order });
                    }

                    SetStateAndQuantities(reply, order, oscm);
                    messagesToRiseInEvent.AddRange(ProcessOrderStateChange(oscm));
                    messagesToRiseInEvent.AddRange(ProcessPendingTransactionReplies(order));
                }

                RiseEvents(messagesToRiseInEvent);
            }
            catch (Exception ex)
            {
                HandleExceptionError(ex, "Error while processing orders_log table record"); // ok
            }
        }

        /// <summary>
        /// Вызывает событие OnMessageReceived для всех сообщений в коллекции
        /// </summary>
        private void RiseEvents(List<Message> messagesToRiseInEvent)
        {
            if (messagesToRiseInEvent.Count > 0)
            {
                messagesToRiseInEvent.ForEach(OnMessageReceived);
            }
        }

        /// <summary>
        /// Обработка отложенных ответов на транзакции, которые дожидаются соответствующих изменений заявок. Например, мы не
        /// отправляем ответ на транзакцию на снятие до тех пор, пока не получим oscm.Cancelled.
        /// </summary>
        private IEnumerable<Message> ProcessPendingTransactionReplies(Order order)
        {
            // ждём ли мы этот статус по этой заявке для отправки TransactionReply
            TransactionReplyHelper replyHelper;

            if (container.TryRemovePendingReply(order.OrderExchangeId, out replyHelper))
            {
                if (replyHelper.PendingStates.Count > 0)
                {
                    Logger.Info().Print($"Processing {replyHelper.PendingStates.Count} pending OSCMs");
                }

                for (var i = 0; i < replyHelper.PendingStates.Count; i++)
                {
                    var pendingState = replyHelper.PendingStates[i];
                    // если это именно тот статус
                    if (pendingState.OrderExchangeId == order.OrderExchangeId &&
                        (pendingState.PendingForCancel || order.State != OrderState.Cancelled))
                    {
                        // выкидываем его 
                        Logger.Debug().Print("Remove pending OSCM", LogFields.PendingForCancel(pendingState.PendingForCancel));
                        replyHelper.PendingStates.RemoveAt(i);
                        break;
                    }
                }

                // в случае, если мы все статусы уже дождались, шлём сообщение
                if (replyHelper.ReadyToSend)
                {
                    Logger.Debug().Print($"Fire transaction reply: {replyHelper.Reply}");

                    yield return replyHelper.Reply;
                }
            }
        }

        /// <summary>
        /// Заполнение параметров State, Quantity, FilledQuantity и ActiveQuantity в заявке и сообщение об изменении заявки
        /// </summary>
        private static void SetStateAndQuantities(CGateOrder reply, Order order, OrderStateChangeMessage oscm)
        {
            switch (reply.Action)
            {
                // заявка удалена
                case 0:
                    order.State = OrderState.Cancelled;
                    oscm.State = order.State;
                    oscm.ActiveQuantity = order.ActiveQuantity;
                    break;
                // заявка добавлена
                case 1:
                    order.State = OrderState.Active;
                    order.ActiveQuantity = (uint)reply.AmountRest;

                    oscm.State = order.State;
                    oscm.ActiveQuantity = order.ActiveQuantity;
                    oscm.Quantity = (uint)reply.Amount;
                    oscm.FilledQuantity = 0;
                    break;
                // заявка сведена в сделку
                case 2:
                    order.ActiveQuantity = (uint)reply.AmountRest;
                    order.State = order.ActiveQuantity > 0 ? OrderState.PartiallyFilled : OrderState.Filled;

                    oscm.State = order.State;
                    oscm.FilledQuantity = (uint)reply.Amount;
                    oscm.ActiveQuantity = order.ActiveQuantity;
                    oscm.State = order.State;
                    break;
            }
        }

        /// <summary>
        /// Проверяет, есть ли уже в системе информация по этому инструменту, и если нет, откладываем обработку сообщения
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="instrumentCode"></param>
        /// <returns></returns>
        private bool CheckInstrumentCode(CGateOrder reply, out string instrumentCode)
        {
            instrumentCode = instrumentIsinResolver.GetShortIsinByIsinId(reply.IsinId);

            if (string.IsNullOrEmpty(instrumentCode))
            {
                Logger.Warn().Print($"Empty instrument code for OSCM, postpone processing: {reply}");
                EnqueuePendingMessage(reply);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Обработка OSCM для внешней заявки, не выставленной из прикладного приложения
        /// </summary>
        private bool ProcessExternalOrderStateChange(CGateOrder reply, string instrumentCode, ref Order order)
        {
            var transId = reply.ExtId;
            var orderId = reply.IdOrd;
            var orderIdString = orderId.ToString();
            var action = reply.Action;
            bool isExternalOrder = false;

            // то ищем её по идентификатору транзакции на постановку этой заявки
            if (container.TryRemoveOrder((uint)transId, out order))
            {
                // присваиваем биржевой номер и переалкдываем в коллекцию заявок с биржевым номером
                order.OrderExchangeId = orderId.ToString();
                container.AddOrder(orderIdString, order);
            }
            else
            {
                // TODO По идее сюда мы попадём каждый раз, когда будем грузить транзакции из предыдущих сессий
                if (ReceiveExternalOrders &&
                    (action == 1 /*  OrderState.Active */|| action == 2
                        /* OrderState.PartiallyFilled  or OrderState.Filled */))
                {
                    var data = instrumentIsinResolver.GetInstrument(instrumentCode);
                    order = new Order
                    {
                        Instrument = data.Instrument,
                        Account = reply.ClientCode,
                        ActiveQuantity = (uint)reply.AmountRest,
                        ClientCode = reply.ClientCode,
                        Comment = reply.Comment,
                        Operation = reply.Dir == 2 ? OrderOperation.Sell : OrderOperation.Buy,
                        OrderExchangeId = reply.IdOrd.ToString(),
                        Price = (decimal)reply.Price,
                        Quantity = (uint)reply.Amount,
                        State = OrderState.New,
                        TransactionId = Guid.NewGuid() // NOTE проверить
                    };

                    isExternalOrder = true;
                    container.UpdateOrder(orderIdString, order);
                }
                else
                {
                    Logger.Debug().Print(
                        "Order state change received for order, which wasn't sent through this system.",
                        LogFields.TransactionId(transId)
                        );
                    return isExternalOrder;
                }
            }
            return isExternalOrder;
        }

        /// <summary>
        /// Обработка изменения статуса заявки. 
        /// </summary>
        private IEnumerable<Message> ProcessOrderStateChange(OrderStateChangeMessage stateMsg)
        {
            var rValue = new List<Message>();

            if (string.IsNullOrEmpty(stateMsg.OrderExchangeId)) // TODO Тут было stateMsg.OrderExchangeId == 0
            {
                throw new ArgumentException($"Empty order exchange id. {stateMsg}");
                //OnMessageReceived(stateMsg);
                //return;
            }

            // выставляем флаг, который говорит, что статус уже приходил
            container.AddActivatedOrder(stateMsg.OrderExchangeId);

            //rValue.Add(stateMsg);

            // Если по данной заявке ранее приходили сделки (до получения статуса по заявке), то их необходимо обработать и отправить подписчикам.
            // При нормальной работе шлюза таких ситуаций возникать не должно.
            LinkedList<FillMessage> fillsList;
            if (container.TryRemovePendingFill(stateMsg.OrderExchangeId, out fillsList))
            {
                if (fillsList.Count > 0)
                {
                    Logger.Info().Print($"{fillsList.Count} pending fills will be reprocessed");
                }

                foreach (var fill in fillsList)
                {
                    AddFill(fill);
                    rValue.Add(fill);
                }
            }

            return rValue;
        }

        /// <summary>
        /// Проверяет, разрешена ли торговля по этому счёту
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        private bool CheckAccountPermission(CGateOrder reply)
        {
            if (!IsPermittedAccount(reply.ClientCode))
            {
                Logger.Warn().Print($"Account is not permited, OSCM will be skipped: {reply}");
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Потокобезопасно увеличивает на единицу значение <see cref="newTransactionId"/>.
        /// </summary>
        /// <returns>Увеличенное значение.</returns>
        private int IncrementOrderTransId() => Interlocked.Increment(ref newTransactionId);

        /// <summary>
        /// Обработчик события резолва нового isin_id
        /// </summary>
        private void CGateIsinResolverOnNewIsinResolved(object sender, EventArgs eventArgs) => processPendingMessagesEvent.Set();

        private void CGateAdapterStreamMessageHandler(object sender, CGateMessageEventArgs message) => message.Message.Accept(this);

        private void ProcessPendingMessages()
        {
            var waitHandles = new[] { cancellationTokenSource.Token.WaitHandle, processPendingMessagesEvent };
            int i;
            while ((i = WaitHandle.WaitAny(waitHandles, 100)) != WaitHandle.WaitTimeout)
            {
                if (i == 0)
                {
                    Logger.Debug().Print("Stopping ProcessPendingMessages task");
                    return;
                }

                CGateMessage message;
                while (pendingIsinResolutionMessages.TryDequeue(out message) &&
                       !cancellationTokenSource.IsCancellationRequested)
                {
                    message.Accept(this);
                }
            }
        }

        /// <summary>
        /// Добавить сообщение в очередь необработанных. Сюда помещаются CGateMessage сообщения, которые пока невозможно обработать, 
        /// из-за отсутствия информации, которая должна прийти в других CGateMessage сообщениях.
        /// </summary>
        /// <param name="message"></param>
        private void EnqueuePendingMessage(CGateMessage message) => pendingIsinResolutionMessages.Enqueue(message);

        /// <summary>
        /// Фильтрация заявок по комментариям, отбрасываются заявки не из текущей сессии, если включена соотв. настройка
        /// </summary>
        private bool FilterByComment(CGateOrder reply)
        {
            var comment = reply.Comment;

            var isCommentChecked = false;
            if (CheckComment && comment != null)
            {
                if (comment.Length >= CommentLength)
                {
                    var customComment = comment.Substring(comment.Length - CommentLength, CommentLength);
                    isCommentChecked = customComment.StartsWith(SessionUid);
                }
                else if (comment.Length >= SessionUidLength)
                {
                    isCommentChecked = comment.EndsWith(SessionUid);
                }
            }

            var rValue = !CheckComment || isCommentChecked;

            if (!rValue)
            {
                Logger.Info().Print($"Filtered by comment", LogFields.Comment(reply.Comment), LogFields.Message(reply));
            }

            return rValue;
        }

        /// <summary>
        /// Обрабокта новой сделки
        /// </summary>
        private void HandleFill(CGateDeal deal)
        {
            Logger.Debug().Print($"Handle: {deal}");
            // сообщения, которые нужно будет выкинуть в конце после обработки, когда выйдем из под лока
            var messagesToRiseInEvent = new List<Message>();
            try
            {
                var codeSell = deal.CodeSell;
                var codeBuy = deal.CodeBuy;

                if (string.IsNullOrEmpty(codeSell) && string.IsNullOrEmpty(codeBuy))
                {
                    HandleError("Both (buyer and seller) codes in trade are empty"); // ok
                    return;
                }

                var isBuy = string.IsNullOrEmpty(codeSell);

                var account = isBuy ? codeBuy : codeSell;
                if (!IsPermittedAccount(account))
                {
                    return;
                }

                var isinId = deal.IsinId;

                using (locker.WriteLock())
                {
                    var ip = instrumentParamsEmitter.GetInstrumentParams(isinId);
                    if (ip == null)
                    {
                        Logger.Debug().PrintFormat("Received order state change for an order with unknown {0}", LogFields.IsinId(isinId));
                        return;
                    }

                    var moment = deal.Moment;
                    var idDeal = (long)deal.IdDeal;
                    var price = (decimal)deal.Price; // TODO Тут приходит белиберда
                    var amount = deal.Amount;
                    var orderId = isBuy ? deal.IdOrdBuy : deal.IdOrdSell;
                    var comment = (isBuy ? deal.CommentBuy : deal.CommentSell);

                    var fill = new FillMessage
                    {
                        Account = account,
                        Instrument = ip.Instrument,
                        ExchangeId = idDeal.ToString(),
                        Operation = isBuy ? OrderOperation.Buy : OrderOperation.Sell,
                        Price = price,
                        Quantity = (uint)amount,
                        DateTime = moment,
                        ExchangeOrderId = orderId.ToString()
                        //ServiceCode = Code
                    };

                    // Для сделок всегда проверяем комментарий
                    var thisSession = false;
                    if (comment != null &&
                        comment.Length >= CommentLength)
                    {
                        var customComment = comment.Substring(comment.Length - CommentLength, CommentLength);
                        thisSession = customComment.StartsWith(SessionUid);
                    }

                    Logger.Debug().Print(
                        "New trade received",
                        LogFields.ExchangeId(fill.ExchangeId),
                        LogFields.ExchangeOrderId(fill.ExchangeOrderId));

                    // проверим, из этой ли сессии и если да, то были ли уже статусы по заявке, и тогда добавим их в словарик);
                    if (thisSession && !container.IsOrderActivated(fill.ExchangeOrderId))
                    {
                        LinkedList<FillMessage> fillsList;

                        if (!container.TryGetPendingFill(fill.ExchangeOrderId, out fillsList))
                        {
                            fillsList = new LinkedList<FillMessage>();
                            container.AddPendingFills(fill.ExchangeOrderId, fillsList);
                        }

                        fillsList.AddLast(fill);
                    }
                    // иначе просто шлём
                    else
                    {
                        AddFill(fill);
                        messagesToRiseInEvent.Add(fill);
                    }
                }

                RiseEvents(messagesToRiseInEvent);
            }
            catch (Exception ex)
            {
                HandleExceptionError(ex, "Error while processing user_deal table"); // ok
            }
        }

        #endregion

        #region IDisposable

        public override void Dispose() { }

        #endregion

        #region ICGateMessageHandler overrides

        public void Handle(StreamStateChange message)
        {
            // если поток с информацией об участниках переходит в режим ONLINE, то мы уже получили
            // всех клиентов из шлюза и можем уведомить об этом систему
            if (message.StreamName == "FORTS_PART_REPL" && message.StreamRegime == StreamRegime.ONLINE)
            {
                var init = new InitResponseMessage()
                {
                    OrderRouters = new Dictionary<string, string[]>
                    {
                        {"CGate", AvailableAccounts.ToArray()}
                    }
                };

                OnMessageReceived(init);
            }
        }

        public void Handle(CgmPart message)
        {
            Logger.Debug().PrintFormat("{0} - {1}", nameof(CgmPart), LogFields.ClientCode(message.ClientCode));
            var clientCode = message.ClientCode;

            if (clientCode.Length == 4)
            {
                brokerCode = clientCode;
                return;
            }

            AddAccount(clientCode);
        }

        public void Handle(CgmPosition message)
        {
            try
            {
                var code = instrumentIsinResolver.GetShortIsinByIsinId(message.IsinId);

                if (string.IsNullOrEmpty(code))
                {
                    EnqueuePendingMessage(message);
                    return;
                }

                var data = instrumentIsinResolver.GetInstrument(code);
                var pos = new PositionMessage
                {
                    Account = message.ClientCode,
                    Instrument = data.Instrument,
                    ClientCode = message.ClientCode,
                    Quantity = message.Pos
                };

                AddAccount(pos.ClientCode);

                OnMessageReceived(pos);
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to handle {message}");
            }
        }

        public void Handle(CGateOrder message)
        {
            HandleOrderStateChange(message);
        }

        public void Handle(CgmOrdersLog cgm)
        {
            Logger.Debug().PrintFormat("Handle: {0}", cgm);
            HandleOrderStateChange(CGateOrder.Create(cgm));
        }

        public void Handle(CgmSysEvents message) { }

        public void Handle(CGateAdapter.Messages.OptTrades.CgmOrdersLog cgm)
        {
            Logger.Debug().PrintFormat("Handle: {0}", cgm);
            HandleOrderStateChange(CGateOrder.Create(cgm));
        }

        /// <summary>
        /// Обработка ответа на постановку новой заявки
        /// </summary>
        public void Handle(CgmFortsMsg101 reply)
        {
            Logger.Debug().PrintFormat("Handle: {0}", reply);
            HandleAddOrderReply(CGateAddOrderReply.Create(reply));
        }

        public void Handle(CgmFortsMsg109 reply)
        {
            Logger.Debug().PrintFormat("Handle: {0}", reply);
            HandleAddOrderReply(CGateAddOrderReply.Create(reply));
        }

        public void Handle(CgmOptDelOrder message) { }

        /// <summary>
        /// Callback на отправку kill транзакции по фьючерсам
        /// </summary>
        public void Handle(CgmFortsMsg102 reply)
        {
            Logger.Debug().PrintFormat("Handle: {0}", reply);
            HandleDelOrderReply(CGateDelOrderReply.Create(reply));
        }

        /// <summary>
        /// Callback на отправку kill транзакции по опционам
        /// </summary>
        public void Handle(CgmFortsMsg110 reply)
        {
            Logger.Debug().PrintFormat("Handle: {0}", reply);
            HandleDelOrderReply(CGateDelOrderReply.Create(reply));
        }

        /// <summary>
        /// Callback на транзакцию move
        /// </summary>
        public void Handle(CgmFortsMsg105 reply)
        {
            //Transaction transaction = null;
            //long or = reply.OrderId;
            //var errCode = reply.Code;

            //lock (SyncRoot)
            //{
            //    try
            //    {
            //        if (transactions.TryGetValue(or, out transaction))
            //            transactions.Remove(or);

            //        if (transaction == null)
            //        {
            //            HandleError("Ошибка при отправке транзакции: не найдена транзакция с кодом {0}. Код ошибки: {1}", or, reply.Code); // ok
            //            return;
            //        }

            //        if (errCode == 0x0000)
            //        {
            //            var code = reply.Code;

            //            if (code == 0)
            //            {
            //                ProcessMoveOrderReply((MoveOrdersTransaction)transaction, reply);
            //            }
            //            else
            //            {
            //                HandleError("Ошибка при отправке транзакции {0}: {1}", transaction.TransactionId, reply.Message); // ok

            //                OnMessageReceived(new TransactionReply
            //                {
            //                    Client = transaction.Client,
            //                    Success = false,
            //                    Message = reply.Message,
            //                    TransactionId = transaction.TransactionId
            //                });
            //            }
            //        }
            //        else if (errCode == 0x6004) // Timeout
            //        {
            //            HandleError("Таймаут при отправке транзакции {0}", transaction.TransactionId); // ok

            //            OnMessageReceived(
            //                new TransactionReply
            //                {
            //                    Client = transaction.Client,
            //                    Success = false,
            //                    Message = "Таймаут при отправке транзакции.",
            //                    TransactionId = transaction.TransactionId
            //                });

            //            return;
            //        }
            //        else
            //        {
            //            HandleError("Ошибка при отправке транзакции {0}. Код ошибки: {1:x}", transaction.TransactionId, errCode); // ok

            //            OnMessageReceived(new TransactionReply
            //            {
            //                Client = transaction.Client,
            //                Success = false,
            //                Message = string.Format("Код ошибки: {0:x4}", errCode),
            //                TransactionId = transaction.TransactionId
            //            });
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        HandleError("Ошибка при отправке транзакции {0}: {1}. ErrCode: {2}.", transaction.TransactionId, ex.Message, errCode);

            //        OnMessageReceived(
            //            new TransactionReply
            //            {
            //                Client = transaction.Client,
            //                Success = false,
            //                Message = ex.Message,
            //                TransactionId = transaction.TransactionId
            //            });
            //    }

            //}
        }

        /// <summary>
        /// Callback общей системной ошибки
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CgmFortsMsg100 message)
        {
            Logger.Error().Print(message.ToString());
        }

        /// <summary>
        /// Callback флуд защиты
        /// </summary>
        /// <param name="reply"></param>
        public void Handle(CgmFortsMsg99 reply)
        {
            Logger.Error().Print(reply.ToString());

            Transaction transaction = null;
            var or = reply.UserId;

            if (!container.RemoveTransaction(or, out transaction))
            {
                HandleError(LogMessage.Make(
                    $"Error while sending transaction: transaction {or} is not found",
                    LogFields.TransactionId(or),
                    LogFields.ErrorCode(reply.Message)
                    )); // ok
                return;
            }

            // queue_size i4 Количество сообщений в очереди для данного пользователя
            var queueSize = reply.QueueSize;
            // penalty_remain i4 Время в миллисекундах, по прошествии которого будет успешно принято следущее сообщение от этого пользователя
            var penaltyRemain = reply.PenaltyRemain;
            // message c128 Текст сообщения об ошибке
            var message = reply.Message;

            var formattedError = LogMessage.MakeString(
                "Transaction rejected by flood defence system",
                LogFields.TransactionId(transaction.TransactionId),
                LogFields.QueueSize(queueSize),
                LogFields.PenaltyRemain(penaltyRemain),
                LogFields.Message(message)
                );

            HandleError(formattedError); // ok

            OnMessageReceived(
                new TransactionReply
                {
                    Success = false,
                    Message = formattedError.Print(PrintOption.Default),
                    TransactionId = transaction.TransactionId
                });
        }

        /// <summary>
        /// Callback новой сделки по фьючерсам
        /// </summary>
        public void Handle(CgmUserDeal cgm)
        {
            Logger.Debug().PrintFormat("Handle: {0}", cgm);
            HandleFill(CGateDeal.Create(cgm));
        }

        /// <summary>
        /// Callback новой сделки по опционам
        /// </summary>
        public void Handle(CGateAdapter.Messages.OptTrades.CgmUserDeal cgm)
        {
            Logger.Debug().PrintFormat("Handle: {0}", cgm);
            HandleFill(CGateDeal.Create(cgm));
        }

        public void Handle(CgmSession rec)
        {
            try
            {
                var state = rec.State;

                var isTrading = state == 1; // TODO Уточнить. Скорее всего нужно смотреть еще на состояние клиринга

                var begin = rec.Begin;
                var end = rec.End;

                var eveBegin = rec.EveBegin;
                var eveEnd = rec.EveEnd;

                var sessionId = rec.SessId;

                Logger.Info().Print(
                    "Session info received",
                    LogFields.Id(sessionId),
                    LogFields.SessionBeginTime(begin),
                    LogFields.SessionEndTime(end),
                    LogFields.EveningSessionBeginTime(eveBegin),
                    LogFields.EveningSessionEndTime(eveEnd),
                    LogFields.State(state)
                    );

                // 0 Сессия назначена. Нельзя ставить заявки, но можно удалять.
                // 1 Сессия идет. Можно ставить и удалять заявки.
                // 2 Приостановка торгов по всем инструментам. Нельзя ставить заявки, но можно удалять.
                // 3 Сессия принудительно завершена. Нельзя ставить и удалять заявки.
                // 4 Сессия завершена по времени. Нельзя ставить и удалять заявки.
                if (state == 3 || state == 4)
                {
                    // Сбрасываем мапу инструментов
                    //mapIsinId2InstrumentParams.Clear();
                }

                using (locker.WriteLock())
                {
                    sessionInfo.IsTrading = isTrading;
                    sessionInfo.StartTime = begin.TimeOfDay;
                    sessionInfo.EndTime = end.TimeOfDay;
                    sessionInfo.EveningStartTime = eveBegin.TimeOfDay;
                    sessionInfo.EveningEndTime = eveEnd.TimeOfDay;

                    sessionInfoStatus |= SessionInfoStatus.SessionInfoReceived;

                    if ((sessionInfoStatus & SessionInfoReady) != SessionInfoReady)
                    {
                        return;
                    }
                }

                if (processSessionInfo)
                {
                    OnMessageReceived(sessionInfo);
                }
            }
            catch (Exception ex)
            {
                HandleExceptionError(ex, "Error while processing session table"); // ok
            }
        }

        /// <summary>
        /// Обработка хартбита потока, по нему определяется серверное время.
        /// </summary>
        /// <param name="rec"></param>
        public void Handle(CgmHeartbeat rec)
        {
            // пропускаем все хартбиты, если явно указано не процессить их, а также если поток с хартбитами не вошёл в 
            // состояние online, потому что в снэпшоте их слишком много и они не нужны
            if (!processSessionInfo || rec.StreamRegime != StreamRegime.ONLINE)
            {
                return;
            }

            try
            {
                var time = rec.ServerTime;

                sessionInfo.ServerTime = time;
                sessionInfoStatus |= SessionInfoStatus.ServerTimeReceived;

                if ((sessionInfoStatus & SessionInfoReady) != SessionInfoReady)
                {
                    return;
                }

                OnMessageReceived(sessionInfo);
            }
            catch (Exception ex)
            {
                HandleExceptionError(ex, "Error while processing heartbeat table");
            }
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="T:CGateAdapter.Messages.FutTradesHeartbeat.CgmHeartbeat"/>
        /// </summary>
        public void Handle(CGateAdapter.Messages.FutTradesHeartbeat.CgmHeartbeat rec)
        {
            // пропускаем все хартбиты, если явно указано не процессить их, а также если поток с хартбитами не вошёл в 
            // состояние online, потому что в снэпшоте их слишком много и они не нужны
            if (!processSessionInfo || rec.StreamRegime != StreamRegime.ONLINE)
            {
                return;
            }

            try
            {
                var time = rec.ServerTime;

                sessionInfo.ServerTime = time;
                sessionInfoStatus |= SessionInfoStatus.ServerTimeReceived;

                if ((sessionInfoStatus & SessionInfoReady) != SessionInfoReady)
                {
                    return;
                }

                OnMessageReceived(sessionInfo);
            }
            catch (Exception ex)
            {
                HandleExceptionError(ex, "Error while processing heartbeat table");
            }
        }

        #endregion
    }
}