using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Messages;
using Polygon.Connector.QUIKLua.Adapter;
using Polygon.Connector.QUIKLua.Adapter.Messages;
using Polygon.Connector.QUIKLua.Adapter.Messages.Transactions;
using System.Linq;

namespace Polygon.Connector.QUIKLua
{
    internal sealed class QLRouter : OrderRouter, ITransactionVisitor
    {
        #region Fields

        private const string ServiceCode = "QUIKLua";

        private readonly QLAdapter adapter;

        private long transId;

        /// <summary>
        /// Регулярка для выдирания номера заявки из ответа на транзакцию. "5" в выражении взято от фонаря, с целью, чтобы 
        /// не перепутать номер заявки (пример - 17998924602) с какими-нибудь другими числами в сообщении
        /// </summary>
        private readonly Regex orderIdFromTransReplRegex = new Regex(@"^\D*(\d{5,}).*");

        /// <summary>
        /// Регулярка для выдирания неисполненного остатка из ответа на kill транзацию. Для сообщения с текстом "Неисполненный остаток"
        /// </summary>
        private readonly Regex quantityFromKillTransReplRegex1 = new Regex(@"^Заявка \d{5,} снята\. Неисполненный остаток: (?<quantity>\d+)\.");

        /// <summary>
        /// Регулярка для выдирания неисполненного остатка из ответа на kill транзацию. Для сообщения с текстом "Снятое количество"
        /// </summary>
        private readonly Regex quantityFromKillTransReplRegex2 = new Regex(@"^Заявка, с номером \d{5,} снята\. Снятое количество: (?<quantity>\d+)\.");

        private readonly Container container = new Container();
        private bool initialized;

        private CancellationTokenSource cts;
        private Task pendingTransactionRepliesProcessingTask;
        private readonly AutoResetEvent processPendingMessagesEvent = new AutoResetEvent(false);

        #endregion

        #region .ctor

        public QLRouter(QLAdapter adapter)
        {
            this.adapter = adapter;
        }

        #endregion

        #region OrderRouter

        /// <summary>
        ///   Запускает сервис.
        /// </summary>
        public override void Start()
        {
            cts = new CancellationTokenSource();
            pendingTransactionRepliesProcessingTask = Task.Factory.StartNew(PendingMessagesProcessing, cts.Token);
            adapter.MessageReceived += adapter_MessageReceived;
        }

        /// <summary>
        ///   Останавливает сервис.
        /// </summary>
        public override void Stop()
        {
            adapter.MessageReceived -= adapter_MessageReceived;
            cts?.Cancel();
            initialized = false;
        }

        protected override void SendTransactionImp(Transaction transaction)
        {
            transaction.Accept(this);
            // TODO send TransactionReply(Rejected)
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Задача обработки отложенных событий
        /// </summary>
        private void PendingMessagesProcessing()
        {
            LogManager.BreakScope();
            Thread.CurrentThread.Name = "QL_PNDMS";
            Logger.Debug().Print("Start pending message processing task");

            while (!cts.IsCancellationRequested)
            {
                if (WaitHandle.WaitAny(new[] { cts.Token.WaitHandle, processPendingMessagesEvent }) == 0)
                {
                    break;
                }

                var pendingTransactionReplies = container.GetPendingTransactionReplies();
                Logger.Debug().PrintFormat("Processing {0} pending transaction replies", pendingTransactionReplies.Count);
                pendingTransactionReplies.ForEach(Handle);

                var pendingFills = container.GetPendingFills();
                Logger.Debug().PrintFormat("Processing {0} pending fills", pendingFills.Count());

                foreach (var pendingFill in pendingFills)
                {
                     HandleAsync(pendingFill).Wait();
                }
            }

            Logger.Debug().Print("Stop pending message processing task");
        }

        /// <summary>
        ///     Обработка сообщения от адаптера квика
        /// </summary>
        private async void adapter_MessageReceived(object sender, QLMessageEventArgs e)
        {
            var message = e.Message;
            try
            {
                switch (message.message_type)
                {
                    case QLMessageType.TransactionReply:
                        Handle((QLTransactionReply)message);
                        break;
                    case QLMessageType.OrderStateChange:
                        Handle((QLOrderStateChange)message);
                        break;
                    case QLMessageType.MoneyPosition:
                        Handle((QLMoneyPosition)message);
                        break;
                    case QLMessageType.Position:
                        await HandleAsync((QLPosition)message);
                        break;
                    case QLMessageType.AccountsList:
                        Handle((QLAccountsList)message);
                        break;
                    case QLMessageType.Fill:
                        await HandleAsync((QLFill)message);
                        break;
                    case QLMessageType.InitEnd:
                        Handle((QLInitEnd)message);
                        break;
                    case QLMessageType.Heartbeat:
                        Handle((QLHeartbeat)message);
                        break;
                }
            }
            catch (Exception exception)
            {
                Logger.Error().Print(exception, $"Failed to handle {message}");
            }
        }

        private void Handle(QLHeartbeat message)
        {
            OnMessageReceived(new SessionInfo
            {
                ServerTime = message.time,
                StartTime = message.startTime,
                EndTime = message.endTime,
                EveningStartTime = message.evnStartTime,
                EveningEndTime = message.evnEndTime
            });
        }

        private void Handle(QLInitEnd message)
        {
            initialized = true;
        }

        private async Task HandleAsync(QLFill message)
        {
            try
            {
                Logger.Debug().Print($"Handle(QLFill): {message}");

                var instrument = await adapter.ResolveInstrumentAsync(message.sec_code);
                if (instrument == null)
                {
                    Logger.Error().Print($"Unable to resolve instrument for {message.sec_code}");
                    return;
                }

                // если заявка отправлялась в текущей сессии работы программы, то нужно убедиться, что oscm по ней уже отправлялся
                if (container.IsCurrentSessionOrder(message.order_num))
                {
                    var lastOscm = container.GetLastOrderStateChangeForOrderId(message.order_num);
                    if (lastOscm == null)
                    {
                        Logger.Debug()
                            .Print("Handle(QLFill): Fill will be processed later, no OSCM received",
                                LogFields.Message(message));
                        container.PutPendingFill(message);
                        return;
                    }
                }
                else if (container.HasUnrepliedTransactions())
                {
                    Logger.Debug()
                        .Print($"Handle(QLFill): Fill will be processed later, there are unreplied transactions",
                            LogFields.Message(message));
                    container.PutPendingFill(message);
                    return;
                }

                OnMessageReceived(new FillMessage
                {
                    Instrument = instrument,
                    Account = message.account,
                    Quantity = (uint)message.qty,
                    ClientCode = message.account,
                    ExchangeId = message.trade_num.ToString(),
                    ExchangeOrderId = message.order_num.ToString(),
                    Price = message.price,
                    Operation = message.Operation,
                    DateTime = message.Time
                });
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to handle {message}");
            }
        }

        private void Handle(QLAccountsList message)
        {
            Logger.Debug().PrintFormat("Handle: {0}", message);
            OnMessageReceived(
                new InitResponseMessage
                {
                    OrderRouters = new Dictionary<string, string[]>
                    {
                        {ServiceCode, message.accounts.ToArray()}
                    }
                });
        }

        private void Handle(QLTransactionReply message)
        {
            Logger.Debug().PrintFormat("Handle: {0}", message);

            var orderExchangeId = ParseOrderIdFromTransactionReply(message);
            if (orderExchangeId > 0)
            {
                container.PutOrderExchangeId(orderExchangeId);
            }

            var pendingOscmsToProcess = container.PutTransactionReply(message, orderExchangeId);

            if (pendingOscmsToProcess != null)
            {
                Logger.Debug().Print($"Handle(TR) fires {pendingOscmsToProcess.Count} pending OSCMs to process", LogFields.Message(message));
                foreach (var oscm in pendingOscmsToProcess)
                {
                    // если у нас был получен oscm по данному номеру заявки, но с нулевым trans_id (да да, такое бывает), то тут 
                    // у нас есть прекрасная возможность проставить oscm.trans_id и запроцессить oscm по нормальному алгоритму
                    if (oscm.order_num == orderExchangeId && oscm.trans_id == 0)
                        oscm.trans_id = message.trans_id;

                    Handle(oscm);
                }
            }

            PreprocessTransactionReply(message);

            if (message.Successful)
            {
                ProcessSuccessfulTransactionReply(message);
            }
            else
            {
                ProcessFailedTransactionReply(message);
            }
        }

        /// <summary>
        /// Предварительная обработка ответа на транзакцию
        /// </summary>
        /// <param name="message"></param>
        private void PreprocessTransactionReply(QLTransactionReply message)
        {
            if (!string.IsNullOrEmpty(message.result_msg) &&
                (
                    message.result_msg.Contains("Вы не можете снять данную заявку") ||
                    message.result_msg.Contains("Не найдена заявка для удаления")
                    ))
            {
                Logger.Warn().Print(
                    $"Failed kill transaction will be treated as successfull, status will be changed from {message.status} to 3",
                    LogFields.Message(message)
                    );
                message.status = 3;
            }
        }

        /// <summary>
        /// Обработка неудачной транзакции
        /// </summary>
        /// <param name="message"></param>
        private void ProcessFailedTransactionReply(QLTransactionReply message)
        {
            var newOrderTransaction = container.GetNewOrderTransaction(message.trans_id);
            var killOrderTransaction = container.GetKillOrderTransactionByTransId(message.trans_id);
            var modifyOrderTransaction = container.GetModifyOrderTransactionByTransId(message.trans_id);

            switch (message.status)
            {
                case 13: // отвергнута как кросс сделка
                    Logger.Error().Print("Transaction failed, due to potential cross trade", LogFields.Message(message));
                    break;
                default:
                    Logger.Error().Print("Transaction failed", LogFields.Message(message));
                    break;
            }

            OnMessageReceived(message: new TransactionReply
            {
                Success = message.Successful,
                Message = message.result_msg,
                TransactionId = newOrderTransaction?.TransactionId
                                ?? killOrderTransaction?.TransactionId
                                ?? modifyOrderTransaction?.TransactionId
                                ?? Guid.Empty
            });

            container.RemoveProcessedPendingReply(message);
        }

        /// <summary>
        /// Обработка успешной транзакции
        /// </summary>
        private void ProcessSuccessfulTransactionReply(QLTransactionReply message)
        {
            var newOrderTransaction = container.GetNewOrderTransaction(message.trans_id);
            var killOrderTransaction = container.GetKillOrderTransactionByTransId(message.trans_id);
            var modifyOrderTransaction = container.GetModifyOrderTransactionByTransId(message.trans_id);

            if (newOrderTransaction == null && killOrderTransaction == null && modifyOrderTransaction == null)
            {
                Logger.Warn().Print("TRANS_REPL received for transaction which wasn't sent from application", LogFields.Message(message));
                container.RemoveProcessedPendingReply(message);
                return;
            }

            QLOrderStateChange lastState;
            if ((lastState = container.GetLastOrderStateChangeForTransactionId(message)) == null)
            {
                Logger.Debug().Print("Postpone TrRepl processing, no last OSCM found", LogFields.Message(message));
                container.PutPendingTransactionReply(message);
                return;
            }

            // проверка соответствия последнего статуса типу транзакции
            // kill транзакция считатется завершённой, если заявка находится в одном из статусов ниже
            if (killOrderTransaction != null && lastState.State == OrderState.New)
            {
                Logger.Debug().Print($"Postpone KillTrRepl processing. Last ord state is {lastState.State}", LogFields.Message(message));
                container.PutPendingTransactionReply(message);
                return;
            }

            // new транзакция считатется завершённой, если заявка не находится в одном из статусов ниже
            if (newOrderTransaction != null &&
                (lastState.State == OrderState.New || lastState.State == OrderState.Undefined))
            {
                Logger.Debug().Print($"Postpone NewTrRepl processing. Last ord state is {lastState.State}", LogFields.Message(message));
                container.PutPendingTransactionReply(message);
                return;
            }

            if (modifyOrderTransaction != null &&
                (lastState.State == OrderState.New || lastState.State == OrderState.Undefined ||
                 lastState.trans_id != message.trans_id))
            {
                Logger.Debug().Print($"Postpone ModifyTrRepl processing. Last ord state is {lastState.State}", LogFields.Message(message));
                Logger.Error().Print("Handle(TR): for modify transaction not implemented");
                container.PutPendingTransactionReply(message);
                return;
            }

            if (killOrderTransaction != null)
            {
                var unfilledQuantity = ParseUnfilledQuantityFromTransactionReply(message);
                Logger.Debug().Print($"Handle(TR)[{message.trans_id}]: Create artifitial OSCM from successful kill transaction reply", LogFields.Message(message));

                OnMessageReceived(new OrderStateChangeMessage
                {
                    TransactionId = killOrderTransaction.TransactionId,
                    ActiveQuantity = (uint)unfilledQuantity,
                    OrderExchangeId = message.order_num.ToString(),
                    //FilledQuantity = (uint)(message.filled),
                    State = OrderState.Cancelled,
                    Price = message.price,
                    Quantity = (uint?)message.quantity
                });
            }

            OnMessageReceived(new TransactionReply
            {
                Success = message.Successful,
                Message = message.result_msg,
                TransactionId = newOrderTransaction?.TransactionId
                                ?? killOrderTransaction?.TransactionId
                                ?? modifyOrderTransaction?.TransactionId
                                ?? Guid.Empty
            });

            container.RemoveProcessedPendingReply(message);
        }

        /// <summary>
        /// Обработка сообщения об изменении статуса заявки
        /// </summary>
        /// <param name="message"></param>
        private void Handle(QLOrderStateChange message)
        {
            Logger.Debug().PrintFormat("Handle: {0}", message);

            // если у нас есть транзакции без ответа, то откладываем обратку статуса заявки, будем процессить их после получения transReply
            if (container.HasUnrepliedTransactions())
            {
                // бывает квик присылает и такое
                if (message.trans_id == 0)
                {
                    container.PutPendingOrderStateChange(message);
                    return;
                }

                // если есть неотвеченные транзакции, то по OSCM можно создать ответ на транзакцию
                if (container.IsTransactionUnreplied(message.trans_id))
                {
                    if (message.State == OrderState.Active || message.State == OrderState.PartiallyFilled || message.State == OrderState.Filled)
                    {
                        Logger.Info().Print($"Handle(OSCM): transaction with trans_id = {message.trans_id} is pending and order state {message.State} received. Create and handle artifitial QLTransactionReply");
                        Handle(new QLTransactionReply
                        {
                            status = 3,
                            result_msg = message.reject_reason,
                            trans_id = message.trans_id,
                            account = message.account,
                            order_num = message.order_num,
                            price = message.price,
                            balance = message.balance,
                            quantity = message.qty
                        });
                    }
                }
            }

            // Сохраняем сообщение. Если такое сообщение уже приходило, не обрабатываем его.
            if (!container.PutOrderStateChange(message))
            {
                Logger.Debug().PrintFormat("Handle: {0} - skip duplicate event", message);
                return;
            }

            // пробуем получить транзакцию, в результате которой была создана заявка, по которой мы получили данный OSCM

            // TODO Нужно пытаться получить newOrderTransaction не только по trans_id, но и по номеру заявки
            var newOrderTransaction = container.GetNewOrderTransaction(message.trans_id, message.order_num);
            var killOrderTransaction = container.GetKillOrderTransactionByOrderId(message.order_num);
            var modifyOrderTransaction = container.GetModifyOrderTransactionByOrderId(message.order_num);
            if (newOrderTransaction != null || killOrderTransaction != null)
            {
                ProcessKnownOrderStateChange(message, newOrderTransaction?.TransactionId,
                    killOrderTransaction?.TransactionId, modifyOrderTransaction?.TransactionId);
            }
            else
            {
                ProcessUnknownOrderStateChange(message);
            }

            processPendingMessagesEvent.Set();
        }

        private async Task HandleAsync(QLPosition position)
        {
            using (LogManager.Scope())
            {
                Logger.Debug().PrintFormat("Handle: {0}", position);

                var instrument = await adapter.ResolveInstrumentAsync(position.sec_code);
                if (instrument == null)
                {
                    Logger.Error().Print($"Unable to resolve instrument for {position.sec_code}");
                    return;
                }

                OnMessageReceived(new PositionMessage
                {
                    Account = position.trdaccid,
                    ClientCode = position.trdaccid,
                    Quantity = position.totalnet,
                    //MorningQuantity = position.startnet,
                    Instrument = instrument
                });
            }
        }

        private void Handle(QLMoneyPosition position)
        {
            //Logger.Debug("Money position received: {0}", position);
            var moneyPosition = new MoneyPosition
            {
                Account = position.trdaccid,
                ClientCode = position.trdaccid,
                [MoneyPositionPropertyNames.OpenLimit] = position.cbplimit,
                [MoneyPositionPropertyNames.PlannedPurePosition] = position.cbplplanned,
                [MoneyPositionPropertyNames.Commission] = position.ts_comission,
                [MoneyPositionPropertyNames.VariationMargin] = position.varmargin
            };
            OnMessageReceived(moneyPosition);
        }

        /// <summary>
        /// Разбор биржевого ID заявки из ответа на транзакцию
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private long ParseOrderIdFromTransactionReply(QLTransactionReply message)
        {
            if (string.IsNullOrEmpty(message.result_msg))
            {
                return -1;
            }

            var match = orderIdFromTransReplRegex.Match(message.result_msg);

            // должено быть ровно одно совпадение
            if (match.Length == 0 || match.Groups.Count != 2)
            {
                return -1;
            }

            long rValue = -1;
            long.TryParse(match.Groups[1].Value, out rValue);

            Logger.Debug().PrintFormat("Parsed order exchange id is: {0}", LogFields.ExchangeOrderId(rValue));

            return rValue;
        }

        /// <summary>
        /// Парсинг не снятого количества из ответа на kill транзакцию
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int ParseUnfilledQuantityFromTransactionReply(QLTransactionReply message)
        {
            if (string.IsNullOrEmpty(message.result_msg))
                return -1;

            var match = (message.result_msg.Contains("Неисполненный остаток") ? quantityFromKillTransReplRegex1 : quantityFromKillTransReplRegex2).Match(message.result_msg);

            // должено быть ровно одно совпадение
            if (match.Length == 0 || match.Groups.Count != 2)
                return -1;

            int rValue = -1;
            int.TryParse(match.Groups["quantity"].Value, out rValue);

            Logger.Debug().PrintFormat("Parsed unfilled quantity is: {0}", LogFields.ActiveQuantity(rValue));

            return rValue;
        }

        /// <summary>
        /// Обработка статуса по заявке, которая была выставлена транзакцией из прикладного приложения из текущей сессии
        /// </summary>
        /// <param name="message">Изменения</param>
        /// <param name="newTransactionId">ID связанной с заявкой транзакции на постановку</param>
        /// <param name="killTransactionId">ID связанной с заявкой транзакции на снятие</param>
        /// <param name="modifyTransactionId">ID связанной с заявок транзакции на изменение заявки</param>
        private void ProcessKnownOrderStateChange(QLOrderStateChange message, Guid? newTransactionId, Guid? killTransactionId, Guid? modifyTransactionId)
        {
            Guid transactionId = Guid.Empty;

            // если есть и new и kill транзакция для этого номера заявки, то нужно понять, результатом какой из транзакицй
            // является конкретно это изменение ордера
            if (newTransactionId.HasValue && killTransactionId.HasValue)
            {
                // если заявка снялась, то это результат килл транзакции и именно её transId мы проставляем в OSCM, чтобы правильно запроцессить в TransactionMessageHandler-е
                transactionId = message.State == OrderState.Cancelled
                    ? killTransactionId.Value
                    : newTransactionId.Value;
                Logger.Debug().PrintFormat(
                    "OSCM with two associated transactions received: not={0}, kot={1}. State is {2}, select {3} for further OSCM processing.",
                    newTransactionId,
                    killTransactionId,
                    message.State,
                    transactionId
                    );
            }
            // если есть только одна транзакция, то выбираем её
            else
            {
                transactionId = (Guid)(newTransactionId ?? killTransactionId);
            }

            OnMessageReceived(new OrderStateChangeMessage
            {
                TransactionId = transactionId,
                ActiveQuantity = (uint)message.balance,
                OrderExchangeId = message.order_num.ToString(),
                FilledQuantity = (uint)(message.filled),
                State = message.State,
                Price = message.price,
                Quantity = (uint?)message.qty,
                ChangeTime = message.Time
            });
        }

        /// <summary>
        ///     Обработка статуса по заявке, которая либо была выставлена не из прикладного приложения или из другой сессии, либо по которой пока не пришёл ответ на транзакцию
        /// </summary>
        private void ProcessUnknownOrderStateChange(QLOrderStateChange message)
        {
            try
            {
                var order = container.GetOrder(message.order_num);
                
                // если это первый статус по заявке, то отправляем ExternalOrderMessage
                if (order == null)
                {
                    var instrument = adapter.ResolveInstrumentAsync(message.sec_code).Result;
                    if (instrument == null)
                    {
                        Logger.Error().Print($"Unable to resolve instrument for {message.sec_code}");
                        return;
                    }

                    order = new Order
                    {
                        OrderExchangeId = message.order_num.ToString(),
                        Instrument = instrument,
                        Account = message.account,
                        ActiveQuantity = (uint)message.balance,
                        Quantity = (uint)message.qty,
                        State = message.State,
                        Price = message.price,
                        ClientCode = message.account,
                        Comment = ExtractCommentFromBrokerref(message.brokerref),
                        // TODO Тут проставляется 4100Y2b//00007, а мы для кодирования стратегий используется только то, что после //
                        Operation = message.Operation,
                        DateTime = message.Time,
                        TransactionId = Guid.NewGuid()
                    };

                    container.PutOrder(message.order_num, order);

                    OnMessageReceived(new ExternalOrderMessage { Order = order });

                    OnMessageReceived(new OrderStateChangeMessage
                    {
                        ActiveQuantity = (uint)message.balance,
                        OrderExchangeId = message.order_num.ToString(),
                        FilledQuantity = (uint)(message.filled),
                        State = message.State,
                        Price = message.price,
                        Quantity = (uint?)message.qty,
                        ChangeTime = message.Time,
                        TransactionId = order.TransactionId
                    });
                }
                // иначе мы уже отправляли информацию о внешней заявке и теперь должны слать статусы
                else
                {
                    OnMessageReceived(new OrderStateChangeMessage
                    {
                        ActiveQuantity = (uint)message.qty,
                        OrderExchangeId = message.order_num.ToString(),
                        FilledQuantity = (uint)(message.filled),
                        State = message.State,
                        Price = message.price,
                        Quantity = (uint?)message.balance,
                        ChangeTime = message.Time,
                        TransactionId = order.TransactionId
                    });
                }
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to process {message}");
            }
        }

        /// <summary>
        /// Увеличить счётчик транзакций
        /// </summary>
        /// <returns></returns>
        private long IncTransId()
        {
            return Interlocked.Increment(ref transId);
        }

        /// <summary>
        /// Вычленяет комментарий к заявке из свойства brokerref сообщения OSCM
        /// </summary>
        /// <param name="brokerref"></param>
        /// <returns></returns>
        private string ExtractCommentFromBrokerref(string brokerref)
        {
            if (string.IsNullOrEmpty(brokerref))
                return null;

            var indexOfDelimeter = brokerref.IndexOf(@"//");
            if (indexOfDelimeter >= 0)
                return brokerref.Substring(indexOfDelimeter + 2);

            return null;
        }

        #endregion

        #region ITransactionvisitor

        public void Visit(KillOrderTransaction transaction)
        {
            var symbol = adapter.ResolveSymbolAsync(transaction.Instrument).Result;
            if (symbol == null)
            {
                Logger.Error().Print($"Unable to resolve symbol for {transaction.Instrument}");
                return;
            }
            
            var newTransactionId = IncTransId();
            var qlTrans = new QLTransaction
            {
                ACTION = ACTION.KILL_ORDER,
                ACCOUNT = transaction.Account,
                CLIENT_CODE = transaction.Account,
                SECCODE = symbol,
                CLASSCODE = symbol.Length >= 5 ? "SPBOPT" : "SPBFUT",
                EXECUTION_CONDITION = EXECUTION_CONDITION.PUT_IN_QUEUE,
                TRANS_ID = newTransactionId.ToString(),
                ORDER_KEY = transaction.OrderExchangeId
            };
            Logger.Debug().PrintFormat("Visit: {0}", qlTrans);
            adapter.SendMessage(qlTrans);

            container.PutTransaction(newTransactionId, transaction);
        }

        public void Visit(ModifyOrderTransaction transaction)
        {
            var symbol = adapter.ResolveSymbolAsync(transaction.Instrument).Result;
            if (symbol == null)
            {
                Logger.Error().Print($"Unable to resolve symbol for {transaction.Instrument}");
                return;
            }

            var newTransactionId = IncTransId();

            var qlTrans = new QLTransaction
            {
                ACTION = ACTION.MOVE_ORDERS,
                ACCOUNT = transaction.Account,
                CLIENT_CODE = transaction.Account + @"//" + transaction.Comment,
                COMMENT = transaction.Comment,
                SECCODE = symbol,
                CLASSCODE = symbol.Length >= 5 ? "SPBOPT" : "SPBFUT",
                EXECUTION_CONDITION = EXECUTION_CONDITION.PUT_IN_QUEUE,
                TRANS_ID = newTransactionId.ToString(),
                TYPE = TYPE.L,
                FIRST_ORDER_NUMBER = transaction.OrderExchangeId,
                FIRST_ORDER_NEW_QUANTITY = transaction.Quantity.ToString(),
                FIRST_ORDER_NEW_PRICE = transaction.Price.ToString(),
                // TODO Тут похоже нужно проставлять ещё поле комментарий, нужно отследить по
                // таблице заявок квика, что становится с комментов заявки после modify
            };

            Logger.Debug().PrintFormat("Visit: {0}", qlTrans);
            adapter.SendMessage(qlTrans);

            container.PutTransaction(newTransactionId, transaction);
        }

        public void Visit(NewOrderTransaction transaction)
        {
            var symbol = adapter.ResolveSymbolAsync(transaction.Instrument).Result;
            if (symbol == null)
            {
                Logger.Error().Print($"Unable to resolve symbol for {transaction.Instrument}");
                return;
            }

            var newTransactionId = IncTransId();
            var qlTrans = new QLTransaction
            {
                ACTION = ACTION.NEW_ORDER,
                ACCOUNT = transaction.Account,
                CLIENT_CODE = transaction.Account + @"//" + transaction.Comment,
                COMMENT = transaction.Comment,
                SECCODE = symbol,
                CLASSCODE = symbol.Length >= 5 ? "SPBOPT" : "SPBFUT",
                EXECUTION_CONDITION = EXECUTION_CONDITION.PUT_IN_QUEUE,
                OPERATION = transaction.Operation == OrderOperation.Buy ? OPERATION.B : OPERATION.S,
                QUANTITY = transaction.Quantity.ToString(),
                PRICE = transaction.Price.ToString("0.######"),
                TRANS_ID = newTransactionId.ToString(),
                TYPE = TYPE.L,
            };

            Logger.Debug().PrintFormat("Visit: {0}", qlTrans);
            adapter.SendMessage(qlTrans);

            container.PutTransaction(newTransactionId, transaction);
        }

        public void Visit(Transaction transaction)
        {
            OnMessageReceived(TransactionReply.Rejected(
                transaction,
                $"Transaction of type {transaction.GetType()} is not supported by router"));
        }

        private void FailDueToUnknownInstrument(Transaction transaction)
        {
            OnMessageReceived(TransactionReply.Rejected(
                transaction,
                $"Unable to find tree node for \"{transaction.Instrument.Code}\""));
        }

        #endregion
    }
}

