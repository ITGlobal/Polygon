using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Connector;
using Polygon.Messages;
using Order = Polygon.Messages.Order;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Роутер CQG Continuum
    /// </summary>
    internal sealed class CQGCRouter : OrderRouter
    {
        #region Класс для роутинга транзакций

        private sealed class TransactionDispatcher : ITransactionVisitor
        {
            private readonly CQGCRouter router;

            public TransactionDispatcher(CQGCRouter router)
            {
                this.router = router;
            }

            public void Dispatch(Transaction transaction)
            {
                transaction.Accept(this);
            }

            void ITransactionVisitor.Visit(NewOrderTransaction transaction)
            {
                router.SendTransactionInternal(transaction);
            }

            void ITransactionVisitor.Visit(KillOrderTransaction transaction)
            {
                router.SendTransactionInternal(transaction);
            }

            void ITransactionVisitor.Visit(ModifyOrderTransaction transaction)
            {
                router.SendTransactionInternal(transaction);
            }

            void ITransactionVisitor.Visit(Transaction transaction)
            {
                router.TransactionIsNotSupported(transaction);
            }
        }

        #endregion

        #region fields

        private const string ServiceCode = "CQGC";
        internal const string CommentAttributeName = "comment";

        private readonly CQGCAdapter adapter;
        private readonly CQGCInstrumentResolver instrumentResolver;
        private readonly TransactionDispatcher dispatcher;

        private readonly IRwLockObject accountsLock = DeadlockMonitor.ReaderWriterLock<CQGCRouter>("accountsLock");
        private readonly Dictionary<int, string> accountCodesById = new Dictionary<int, string>();
        private readonly Dictionary<string, int> accountIdsByCode = new Dictionary<string, int>();

        private readonly ILockObject ordersLock = DeadlockMonitor.Cookie<CQGCRouter>("ordersLock");
        private readonly Dictionary<string, Order> ordersByOrderExchangeId = new Dictionary<string, Order>();
        private readonly Dictionary<Guid, Order> ordersByTransactionId = new Dictionary<Guid, Order>();
        private readonly Dictionary<string, OrderStatus> orderStatusByChainOrderId = new Dictionary<string, OrderStatus>();

        private readonly ILockObject processedFillsLock = DeadlockMonitor.Cookie<CQGCRouter>("processedFillsLock");
        private readonly HashSet<string> processedFillIds = new HashSet<string>();

        private readonly ILockObject transactionIdsByRequestIdLock = DeadlockMonitor.Cookie<CQGCRouter>("transactionIdsByRequestIdLock");
        private readonly Dictionary<uint, Guid> transactionIdsByRequestId = new Dictionary<uint, Guid>();
        private readonly Dictionary<Guid, uint> transactionRequestIds = new Dictionary<Guid, uint>();

        /// <summary>
        /// Токен отмены фоновых задач
        /// </summary>
        private CancellationTokenSource cts;

        /// <summary>
        /// Событие добавление нового отложенного ордера
        /// </summary>
        private readonly AutoResetEvent pendingOrderAddedEvent = new AutoResetEvent(false);

        #endregion

        #region .ctor

        public CQGCRouter(CQGCAdapter adapter, CQGCInstrumentResolver instrumentResolver)
        {
            this.adapter = adapter;
            this.instrumentResolver = instrumentResolver;

            dispatcher = new TransactionDispatcher(this);

            this.adapter.AccountResolved += AccountResolved;
            this.adapter.PositionStatusReceived += PositionStatusReceived;
            this.adapter.TradeSnapshotCompletionReceived += TradeSnapshotCompletionReceived;
            this.adapter.TradeSubscriptionStatusReceived += TradeSubscriptionStatusReceived;
            this.adapter.OrderStatusReceived += OrderStatusReceived;
            this.adapter.CollateralStatusReceived += CollateralStatusReceived;
            this.adapter.OrderRequestRejectReceived += OrderRequestRejectReceived;
            instrumentResolver.InstrumentResolved += InstrumentResolved;
        }

        #endregion

        #region IOrderRouter

        /// <summary>
        ///     Отправляет заявки на биржу.
        /// </summary>
        /// <param name="transaction">
        ///     Экземпляр заявки для отправки.
        /// </param>
        protected override void SendTransactionImp(Transaction transaction)
        {
            dispatcher.Dispatch(transaction);
        }

        /// <summary>
        ///     Запускает сервис.
        /// </summary>
        public override void Start()
        {
            cts = new CancellationTokenSource();
            PendingOrdersProcessing();
        }

        /// <summary>
        ///     Останавливает сервис.
        /// </summary>
        public override void Stop()
        {
            cts.Cancel();
        }

        #endregion

        #region Обработка событий

        /// <summary>
        ///     Обработчик события получения списка счетов
        /// </summary>
        private void AccountResolved(AdapterEventArgs<AccountsReport> args)
        {
            args.MarkHandled();

            // Инициализируем счета
            if (args.Message.brokerage != null)
            {
                using (accountsLock.WriteLock())
                {
                    foreach (
                        var account in
                            from brokerage in args.Message.brokerage
                            from ss in brokerage.sales_series
                            from account in ss.account
                            select account)
                    {
                        Logger.Debug().PrintFormat("Account resolved: {0};{1};{2}", account.brokerage_account_id, account.account_id, account.name);
                        AddAccount(account.name);

                        accountCodesById[account.account_id] = account.name;
                        accountIdsByCode[account.name] = account.account_id;
                    }
                }

                OnMessageReceived(
                    new InitResponseMessage
                    {
                        OrderRouters = new Dictionary<string, string[]>
                        {
                            {ServiceCode, AvailableAccounts.ToArray()}
                        }
                    });
            }

            // Подписываемся на счета
            var subscription = new TradeSubscription
            {
                publication_type = (uint)TradeSubscription.PublicationType.ALL_AUTHORIZED,
                subscription_scope =
                {
                    (uint) TradeSubscription.SubscriptionScope.ORDERS,
                    (uint) TradeSubscription.SubscriptionScope.POSITIONS,
                    (uint) TradeSubscription.SubscriptionScope.COLLATERAL
                },
                subscribe = true,
                id = adapter.GetNextRequestId()
            };
            adapter.SendMessage(subscription);
        }

        private readonly ILockObject openPositionsLock = DeadlockMonitor.Cookie<CQGCRouter>("openPositionsLock");

        /// <summary>
        ///     Словарь словарей словарей, в котором лежат открытые позы по счету, инструменту и номеру
        /// </summary>
        private readonly Dictionary<int, Dictionary<uint, Dictionary<int, OpenPosition>>> openPositions
            = new Dictionary<int, Dictionary<uint, Dictionary<int, OpenPosition>>>();


        /// <summary>
        ///     Обработчик события получения позиции
        /// </summary>
        private async void PositionStatusReceived(AdapterEventArgs<PositionStatus> args)
        {
            try
            {
                args.MarkHandled();

                // Ищем счет для позиции
                string accountCode;
                using (accountsLock.ReadLock())
                {
                    if (!accountCodesById.TryGetValue(args.Message.account_id, out accountCode))
                    {
                        Logger.Error().PrintFormat(
                            "Unable to process position on contract #{0}: account #{1} is unknown",
                            args.Message.contract_id,
                            args.Message.account_id);
                        return;
                    }
                }

                // Обрабатываем метаданные контрактов
                if (args.Message.contract_metadata != null)
                {
                    await
                        instrumentResolver.HandleMetadataAsync(args.Message.contract_metadata,
                            $"A position in \"{accountCode}\" account");
                }

                // Ищем инструмент для позиции
                var instrument = instrumentResolver.GetInstrument(args.Message.contract_id);
                if (instrument == null)
                {
                    Logger.Warn().PrintFormat(
                        "Received a position on contract #{0} in account {1} but no matching instrument is found",
                        args.Message.contract_id,
                        accountCode);
                    return;
                }

                // Рассчитываем позицию
                var position = new PositionMessage
                {
                    Account = accountCode,
                    Instrument = instrument
                };

                var message = new StringBuilder();
                message.AppendFormat("Position received: {0} [ ", instrument.Code);

                using (openPositionsLock.Lock())
                {
                    Dictionary<uint, Dictionary<int, OpenPosition>> d1;
                    if (!openPositions.TryGetValue(args.Message.account_id, out d1))
                    {
                        d1 = new Dictionary<uint, Dictionary<int, OpenPosition>>();
                        openPositions.Add(args.Message.account_id, d1);
                    }

                    Dictionary<int, OpenPosition> d2;
                    if (!d1.TryGetValue(args.Message.contract_id, out d2))
                    {
                        d2 = new Dictionary<int, OpenPosition>();
                        d1.Add(args.Message.contract_id, d2);
                    }

                    if (args.Message.is_snapshot)
                    {
                        d2.Clear();
                    }

                    position.Quantity = 0;
                    foreach (var p in args.Message.open_position)
                    {
                        var fmt = ObjectLogFormatter.Create(PrintOption.Nested, "CQGC_POSITION");
                        fmt.AddField(LogFieldNames.Id, p.id);
                        fmt.AddField(LogFieldNames.Price, p.price);
                        fmt.AddField(LogFieldNames.Quantity, p.qty);
                        fmt.AddField(LogFieldNames.TradeDate, p.trade_date);
                        fmt.AddField(LogFieldNames.TradeUtcTime, p.trade_utc_time);
                        fmt.AddField(LogFieldNames.StatementDate, p.statement_date);

                        message.Append(fmt.Print(PrintOption.Nested));
                        message.Append(", ");

                        position.Quantity = (int) p.qty;
                        position.Price = (decimal) p.price;

                        d2[p.id] = p;
                    }

                    var volume = 0d;
                    var quantity = 0u;

                    foreach (var pair in d2)
                    {
                        volume += pair.Value.price * pair.Value.qty;
                        quantity += pair.Value.qty;
                    }

                    position.Quantity = (int) quantity;
                    position.Price = quantity != 0u ? (decimal) (volume / quantity) : 0;
                }

                if (args.Message.is_short_open_position)
                {
                    position.Quantity *= -1;
                }

                message.Append("]: ");
                message.Append(LogFields.Price(position.Price));
                message.Append(LogFields.Quantity(position.Quantity));
                message.Append(LogFields.IsSnapshot(args.Message.is_snapshot));

                Logger.Debug().Print(message.ToString().Preformatted());

                // Отправляем сообщение
                OnMessageReceived(position);
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to process {args.Message}");
            }
        }

        /// <summary>
        ///     Обработчик события завершения выгрузки снапшота
        /// </summary>
        private void TradeSnapshotCompletionReceived(AdapterEventArgs<TradeSnapshotCompletion> args)
        {
            args.MarkHandled();

            var scopes = args.Message.subscription_scope
                .Select(x => ((TradeSubscription.SubscriptionScope)x).ToString("G"));

            Logger.Debug().PrintFormat("Trade snapshot completed ({0})", string.Join(", ", scopes).Preformatted());
        }

        /// <summary>
        ///     Обработчик события изменения статуса подписки
        /// </summary>
        private void TradeSubscriptionStatusReceived(AdapterEventArgs<TradeSubscriptionStatus> args)
        {
            args.MarkHandled();

            Logger.Debug().Print(
                "Trade subscription status received",
                LogFields.Id(args.Message.id),
                LogFields.Status((TradeSubscriptionStatus.StatusCode)args.Message.status_code),
                LogFields.Message(args.Message.text_message)
                );
        }

        /// <summary>
        ///     Обработчик события по статусу заявки
        /// </summary>
        private async void OrderStatusReceived(AdapterEventArgs<OrderStatus> args)
        {
            try
            {
                Logger.Debug().Print(
                    "Order status received",
                    LogFields.ExchangeOrderId(args.Message.order_id),
                    LogFields.ChainOrderId(args.Message.chain_order_id),
                    LogFields.State(ConvertionHelper.GetOrderState(args.Message)),
                    LogFields.AccountId(args.Message.account_id),
                    LogFields.ExecOrderId(args.Message.exec_order_id)
                );

                args.MarkHandled();

                // Обрабатываем метаданные контрактов
                if (args.Message.contract_metadata != null)
                {
                    foreach (var metadata in args.Message.contract_metadata)
                    {
                        await instrumentResolver.HandleMetadataAsync(metadata);
                    }
                }

                // Пытаемся выбрать заявку из контейнера
                Order order;
                using (ordersLock.Lock())
                    ordersByOrderExchangeId.TryGetValue(args.Message.chain_order_id, out order);

                Message message;
                // Обрабатываем изменение статуса заявки
                if (order != null)
                {
                    message = HandleOrderStateAsOrderStateChange(order, args.Message);
                }
                // Обрабатываем заявку как новую
                else
                {
                    message = HandleOrderStateAsNewOrder(args.Message, out order);
                }

                // Отправляем сообщение и TransactionReply
                if (message != null)
                {
                    OnMessageReceived(message);
                    TryEmitTransactionReplies(args.Message);

                    // Обрабатываем сделки
                    TryEmitFills(args.Message, order);
                }
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to process {args.Message}");
            }
        }

        /// <summary>
        /// Отложенные заявки, для которых не удалось зарезолвить инструмента
        /// </summary>
        private readonly Queue<OrderStatus> pendingOrders = new Queue<OrderStatus>();

        /// <summary>
        ///     Обработка заявки как новой
        /// </summary>
        private Message HandleOrderStateAsNewOrder(OrderStatus message, out Order order)
        {
            Logger.Debug().Print(
                "New order received",
                LogFields.ExchangeOrderId(message.order_id),
                LogFields.ChainOrderId(message.chain_order_id),
                LogFields.State(ConvertionHelper.GetOrderState(message)),
                LogFields.AccountId(message.account_id),
                LogFields.ExecOrderId(message.exec_order_id)
                );

            // Ищем счет для заявки
            string accountCode;
            using (accountsLock.ReadLock())
            {
                if (!accountCodesById.TryGetValue(message.account_id, out accountCode))
                {
                    Logger.Error().Print(
                        "Unable to process order: account is unknown",
                        LogFields.ExchangeOrderId(message.order_id),
                        LogFields.AccountId(message.account_id)
                        );
                    order = null;
                    return null;
                }
            }

            // Ищем инструмент для заявки
            var instrument = instrumentResolver.GetInstrument(message.order.contract_id);
            if (instrument == null)
            {
                // Пришла заявка по неизвестному инструменту
                Logger.Error().Print(
                    "Received an order but no matching instrument is found. Put to pending orders",
                    LogFields.ExchangeOrderId(message.order_id),
                    LogFields.ContractId(message.order.contract_id),
                    LogFields.Account(accountCode)
                    );

                using (ordersLock.Lock())
                {
                    pendingOrders.Enqueue(message);
                    pendingOrderAddedEvent.Set();
                }

                order = null;
                return null;
            }

            using (ordersLock.Lock())
            {
                if (ordersByOrderExchangeId.TryGetValue(message.chain_order_id, out order))
                {
                    // Race condition, такая заявка уже есть, надобно обработать ее через OSCM
                    return HandleOrderStateAsOrderStateChange(order, message);
                }

                Guid transactionId;
                if (Guid.TryParse(message.order.cl_order_id, out transactionId))
                {
                    if (ordersByTransactionId.TryGetValue(transactionId, out order))
                    {
                        // Такая заявка уже есть, надобно обработать ее через OSCM
                        order.OrderExchangeId = message.chain_order_id;
                        ordersByOrderExchangeId.Add(message.chain_order_id, order);

                        return HandleOrderStateAsOrderStateChange(order, message);
                    }
                }
                else
                {
                    transactionId = Guid.Empty;
                }

                // Создаем новую внешнюю заявку
                order = new Order
                {
                    OrderExchangeId = message.chain_order_id,
                    Instrument = instrument,
                    Type = ConvertionHelper.GetOrderType(message),
                    Account = accountCode,
                    Price = instrumentResolver.ConvertPriceBack(message.order.contract_id, message.order.limit_price),
                    Quantity = message.order.qty,
                    ActiveQuantity = message.remaining_qty,
                    Operation = message.order.side == (uint)WebAPI.Order.Side.BUY ? OrderOperation.Buy : OrderOperation.Sell,
                    State = ConvertionHelper.GetOrderState(message),
                    DateTime = adapter.ResolveDateTime(message.status_utc_time),
                    Comment = ConvertionHelper.GetComment(message),
                    TransactionId = transactionId
                };

                ordersByOrderExchangeId.Add(message.chain_order_id, order);
                orderStatusByChainOrderId[message.chain_order_id] = message;

                return new ExternalOrderMessage { Order = order };
            }
        }

        /// <summary>
        ///     Обработка заявки как уже существующей
        /// </summary>
        private OrderStateChangeMessage HandleOrderStateAsOrderStateChange(Order order, OrderStatus message)
        {
            Logger.Debug().Print(
               "Order state change received",
               LogFields.ExchangeOrderId(message.order_id),
               LogFields.ChainOrderId(message.chain_order_id),
               LogFields.State(ConvertionHelper.GetOrderState(message)),
               LogFields.AccountId(message.account_id),
               LogFields.ExecOrderId(message.exec_order_id)
               );

            using (ordersLock.Lock())
            {
                Guid transactionId;
                if (!Guid.TryParse(message.order.cl_order_id, out transactionId))
                {
                    Logger.Debug().Print(
                        $"Unable to parse {LogFieldNames.TransactionId} from {LogFields.ClOrderId(message.order.cl_order_id)}. Use order's {LogFieldNames.TransactionId}"
                        );
                    transactionId = order.TransactionId;
                }

                var change = new OrderStateChangeMessage
                {
                    OrderExchangeId = message.chain_order_id,
                    TransactionId = transactionId,
                    ActiveQuantity = message.remaining_qty,
                    FilledQuantity = message.fill_qty,
                    Price = instrumentResolver.ConvertPriceBack(order.Instrument, message.order.limit_price),
                    ChangeTime = adapter.ResolveDateTime(message.status_utc_time),
                    Quantity = message.remaining_qty + message.fill_qty,
                    State = ConvertionHelper.GetOrderState(message)
                };

                // Обработка изменения order_id (происходит при модификации заявки)
                if (order.OrderExchangeId != change.OrderExchangeId)
                {
                    ordersByOrderExchangeId.Remove(order.OrderExchangeId);
                    ordersByOrderExchangeId.Add(change.OrderExchangeId, order);
                }

                orderStatusByChainOrderId[message.chain_order_id] = message;

                //order.AcceptStateMessage(change);
                return change;
            }
        }

        /// <summary>
        ///     Обработка сообщения по ГО
        /// </summary>
        private void CollateralStatusReceived(AdapterEventArgs<CollateralStatus> args)
        {
            args.MarkHandled();

            // Ищем счет для MP
            string accountCode;
            using (accountsLock.ReadLock())
            {
                if (!accountCodesById.TryGetValue(args.Message.account_id, out accountCode))
                {
                    Logger.Error().Print("Unable to process collateral: account is unknown", LogFields.AccountId(args.Message.account_id));
                    return;
                }
            }

            // Собираем  Money Position
            var moneyPosition = new MoneyPosition
            {
                Account = accountCode
            };
            moneyPosition[MoneyPositionPropertyNames.Ote] = (decimal)args.Message.ote;
            moneyPosition[MoneyPositionPropertyNames.Mvo] = (decimal)args.Message.mvo;
            moneyPosition[MoneyPositionPropertyNames.PurchasingPower] = (decimal)args.Message.purchasing_power;
            moneyPosition[MoneyPositionPropertyNames.TotalMargin] = (decimal)args.Message.total_margin;
            moneyPosition[MoneyPositionPropertyNames.Currency].Value = args.Message.currency;

            OnMessageReceived(moneyPosition);
        }

        /// <summary>
        ///     Обработка отказа по заявке
        /// </summary>
        private void OrderRequestRejectReceived(AdapterEventArgs<OrderRequestReject> args)
        {
            args.MarkHandled();

            var message = $"[{args.Message.reject_code}] {args.Message.text_message}";
            Logger.Error().Print("CQG order rejected", LogFields.Message(message));

            TrySendTransactionReplyRejected(args.Message.request_id, message);
        }

        private void InstrumentResolved(object sender, InstrumentResolverEventArgs e)
        {
            pendingOrderAddedEvent.Set();
        }

        /// <summary>
        /// Обработчик события резолва инструмента. Процессит отложенные заявки
        /// </summary>
        private void PendingOrdersProcessing()
        {
            var waiter = new ContinueOrCancelWaiter(cts.Token, pendingOrderAddedEvent);

            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (waiter.Wait())
                    {
                        using (ordersLock.Lock())
                        {
                            if (pendingOrders.Any())
                            {
                                Logger.Debug().Print($"Pending orders processing ({pendingOrders.Count})");
                                var queueCopy = new Queue<OrderStatus>();

                                while (pendingOrders.Any())
                                {
                                    queueCopy.Enqueue(pendingOrders.Dequeue());
                                }

                                while (queueCopy.Any())
                                {
                                    var item = queueCopy.Dequeue();
                                    if (item.contract_metadata != null && item.contract_metadata.Any())
                                    {
                                        foreach (var contractMetadata in item.contract_metadata)
                                        {
                                            instrumentResolver.HandleMetadata(contractMetadata);
                                        }
                                    }

                                    if (instrumentResolver.GetInstrument(item.order.contract_id) != null)
                                    {
                                        OrderStatusReceived(new AdapterEventArgs<OrderStatus>(item));
                                    }
                                    else
                                    {
                                        pendingOrders.Enqueue(item);
                                    }
                                }
                            }
                        }
                    }

                    Logger.Debug().Print("Pending order processing task stopped");
                }
                catch (Exception ex)
                {
                    Logger.Error().Print(ex, "Pending order processing task error");
                }
            });
        }

        #endregion

        #region Работа с транзакциями

        /// <summary>
        ///     Постановка заявки
        /// </summary>
        private async void SendTransactionInternal(NewOrderTransaction transaction)
        {
            try
            {
                // Получаем счет для транзакции
                int accountId;
                var hasAccountId = true;
                using (accountsLock.ReadLock())
                {
                    if (!accountIdsByCode.TryGetValue(transaction.Account, out accountId))
                    {
                        hasAccountId = false;
                    }
                }

                if (!hasAccountId)
                {
                    OnMessageReceived(TransactionReply.Rejected(
                        transaction,
                        $"Account \"{transaction.Account}\" is unknown"));
                    return;
                }

                // Получаем инструмент для транзации
                uint contractId;
                try
                {
                    contractId = await instrumentResolver.GetContractIdAsync(transaction.Instrument);

                    if (contractId == uint.MaxValue)
                        return;
                }
                catch (OperationCanceledException)
                {
                    OnMessageReceived(TransactionReply.Rejected(
                        transaction,
                        $"Instrument \"{transaction.Instrument.Code}\" is unknown"));
                    return;
                }

                // Пребразовываем цену
                var price = instrumentResolver.ConvertPrice(contractId, transaction.Price);
                if (price == null)
                {
                    OnMessageReceived(TransactionReply.Rejected(transaction, "Unable to convert price"));
                    return;
                }

                // Формируем запрос
                var msg = new OrderRequest
                {
                    new_order = new NewOrder
                    {
                        order = new WebAPI.Order
                        {
                            cl_order_id = transaction.TransactionId.ToString("N"),
                            account_id = accountId,
                            contract_id = contractId,
                            side = (uint) ConvertionHelper.GetSide(transaction.Operation),
                            order_type = (uint) ConvertionHelper.GetOrderType(transaction.Type),
                            duration = (uint) ConvertionHelper.GetDuration(transaction.ExecutionCondition),
                            limit_price = price.Value,
                            qty = transaction.Quantity,
                            is_manual = transaction.IsManual,

                            user_attribute =
                            {
                                new UserAttribute
                                {
                                    name = CommentAttributeName,
                                    value = transaction.Comment
                                }
                            }
                        },
                        suspend = false
                    },
                    request_id = adapter.GetNextRequestId()
                };

                // Запоминаем заявку
                using (ordersLock.Lock())
                {
                    var order = new Order(transaction);
                    ordersByTransactionId.Add(transaction.TransactionId, order);
                }

                // Запоминаем транзакцию
                StoreTransaction(msg.request_id, transaction);

                Logger.Debug().PrintFormat("Sending {0}", transaction);
                adapter.SendMessage(msg);
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to send {transaction}");
                OnMessageReceived(TransactionReply.Rejected(transaction, "Unable to send order"));
            }
        }

        /// <summary>
        ///     Снятие заявки
        /// </summary>
        private void SendTransactionInternal(KillOrderTransaction transaction)
        {
            // Получаем счет для транзакции
            int accountId;
            var hasAccountId = true;
            using (accountsLock.ReadLock())
            {
                if (!accountIdsByCode.TryGetValue(transaction.Account, out accountId))
                {
                    hasAccountId = false;
                }
            }

            if (!hasAccountId)
            {
                OnMessageReceived(TransactionReply.Rejected(
                    transaction,
                    $"Account \"{transaction.Account}\" is unknown"));
                return;
            }

            // Поиск заявки
            OrderStatus orderStatus;
            using (ordersLock.Lock())
            {
                orderStatusByChainOrderId.TryGetValue(transaction.OrderExchangeId, out orderStatus);
            }
            if (orderStatus == null)
            {
                OnMessageReceived(TransactionReply.Rejected(
                    transaction,
                    $"Order \"{transaction.OrderExchangeId}\" is not found"));
                return;
            }

            // Формируем запрос
            var msg = new OrderRequest
            {
                cancel_order = new CancelOrder
                {
                    cl_order_id = transaction.TransactionId.ToString("N"),
                    account_id = accountId,
                    order_id = orderStatus.order_id,
                    orig_cl_order_id = orderStatus.order.cl_order_id,
                    when_utc_time = adapter.ResolveDateTime(DateTime.UtcNow)
                },
                request_id = adapter.GetNextRequestId()
            };

            // Запоминаем транзакцию
            StoreTransaction(msg.request_id, transaction);

            // Отправляем заявку

            try
            {
                Logger.Debug().PrintFormat("Sending {0}", transaction);
                adapter.SendMessage(msg);
            }
            catch (Exception)
            {
                OnMessageReceived(TransactionReply.Rejected(transaction, "Unable to send order"));
            }
        }

        /// <summary>
        ///     Модификация заявки
        /// </summary>
        private async void SendTransactionInternal(ModifyOrderTransaction transaction)
        {
            try
            {
                // Получаем счет для транзакции
                int accountId;
                var hasAccountId = true;
                using (accountsLock.ReadLock())
                {
                    if (!accountIdsByCode.TryGetValue(transaction.Account, out accountId))
                    {
                        hasAccountId = false;
                    }
                }

                if (!hasAccountId)
                {
                    OnMessageReceived(TransactionReply.Rejected(
                        transaction,
                        $"Account \"{transaction.Account}\" is unknown"));
                    return;
                }

                // Получаем инструмент для транзации
                uint contractId;
                try
                {
                    contractId = await instrumentResolver.GetContractIdAsync(transaction.Instrument);

                    if (contractId == uint.MaxValue)
                        return;
                }
                catch (OperationCanceledException)
                {
                    OnMessageReceived(TransactionReply.Rejected(
                        transaction,
                        $"Instrument \"{transaction.Instrument.Code}\" is unknown"));
                    return;
                }

                // Формируем запрос
                OrderRequest msg;
                using (ordersLock.Lock())
                {
                    OrderStatus orderStatus;
                    using (ordersLock.Lock())
                    {
                        orderStatusByChainOrderId.TryGetValue(transaction.OrderExchangeId, out orderStatus);
                    }

                    if (orderStatus == null)
                    {
                        OnMessageReceived(TransactionReply.Rejected(
                            transaction,
                            $"Order \"{transaction.OrderExchangeId}\" doesn't exist or is not active"));
                        return;
                    }

                    // Пребразовываем цену
                    var price = instrumentResolver.ConvertPrice(contractId, transaction.Price);
                    if (price == null)
                    {
                        OnMessageReceived(TransactionReply.Rejected(transaction, "Unable to convert price"));
                        return;
                    }

                    msg = new OrderRequest
                    {
                        modify_order = new ModifyOrder
                        {
                            order_id = orderStatus.order_id,
                            cl_order_id = transaction.TransactionId.ToString("N"),
                            orig_cl_order_id = orderStatus.order.cl_order_id,
                            account_id = accountId,
                            limit_price = price.Value,
                            qty = transaction.Quantity,
                            when_utc_time = adapter.ResolveDateTime(DateTime.UtcNow)
                        },
                        request_id = adapter.GetNextRequestId()
                    };
                }

                // Запоминаем транзакцию
                StoreTransaction(msg.request_id, transaction);

                // Отправляем заявку
                Logger.Debug().PrintFormat("Sending {0}", transaction);
                adapter.SendMessage(msg);
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to send {transaction}");
                OnMessageReceived(TransactionReply.Rejected(transaction, "Unable to send order"));
            }
        }

        /// <summary>
        ///     Транзакция не поддерживается
        /// </summary>
        private void TransactionIsNotSupported(Transaction transaction)
        {
            OnMessageReceived(TransactionReply.Rejected(transaction, $"Transaction is not supported by CQGC: {transaction}"));
        }

        /// <summary>
        ///     Сгенерировать <see cref="FillMessage"/>
        /// </summary>
        private void TryEmitFills(OrderStatus orderStatus, Order order)
        {
            if (orderStatus.transaction_status != null)
            {
                foreach (var transactionStatus in orderStatus.transaction_status)
                {
                    switch ((shared_1.TransactionStatus.Status)transactionStatus.status)
                    {
                        case shared_1.TransactionStatus.Status.FILL:
                            if (transactionStatus.trade == null)
                            {
                                continue;
                            }

                            foreach (var trade in transactionStatus.trade)
                            {
                                if (trade != null)
                                {
                                    using (processedFillsLock.Lock())
                                    {
                                        if (!processedFillIds.Add(trade.trade_id))
                                        {
                                            // Сделка уже обработана
                                            continue;
                                        }
                                    }

                                    var fill = new FillMessage
                                    {
                                        Instrument = order.Instrument,
                                        DateTime = adapter.ResolveDateTime(trade.trade_utc_time),
                                        Account = order.Account,
                                        Operation = ConvertionHelper.GetOrderOperation((WebAPI.Order.Side)trade.side),
                                        Price = instrumentResolver.ConvertPriceBack(trade.contract_id, trade.price),
                                        Quantity = trade.qty,
                                        ExchangeId = trade.trade_id,
                                        ExchangeOrderId = order.OrderExchangeId
                                    };

                                    OnMessageReceived(fill);
                                }
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        ///     Сгенерировать <see cref="TransactionReply"/>
        /// </summary>
        private void TryEmitTransactionReplies(OrderStatus message)
        {
            if (message.transaction_status == null)
            {
                return;
            }

            foreach (var transactionStatus in message.transaction_status)
            {
                Guid transactionId;
                if (!Guid.TryParse(transactionStatus.cl_order_id, out transactionId))
                {
                    continue;
                }

                switch ((shared_1.TransactionStatus.Status)transactionStatus.status)
                {
                    case shared_1.TransactionStatus.Status.REJECTED:
                    case shared_1.TransactionStatus.Status.REJECT_MODIFY:
                    case shared_1.TransactionStatus.Status.REJECT_CANCEL:
                        // Транзация не прошла
                        TrySendTransactionReplyRejected(
                            transactionId,
                            $"{transactionStatus.text_message} [{transactionStatus.reject_code}]"
                            );
                        break;

                    case shared_1.TransactionStatus.Status.ACK_PLACE:
                    case shared_1.TransactionStatus.Status.ACK_MODIFY:
                    case shared_1.TransactionStatus.Status.ACK_CANCEL:
                        // Транзация прошла
                        TrySendTransactionReplyAccepted(transactionId);
                        break;
                }
            }
        }

        /// <summary>
        ///     Запомнить транзакцию
        /// </summary>
        private void StoreTransaction(uint requestId, Transaction transaction)
        {
            using (transactionIdsByRequestIdLock.Lock())
            {
                transactionIdsByRequestId[requestId] = transaction.TransactionId;
                transactionRequestIds[transaction.TransactionId] = requestId;
            }
        }

        private void TrySendTransactionReplyRejected(uint requestId, string errorMessage)
        {
            Guid transactionId;
            using (transactionIdsByRequestIdLock.Lock())
            {
                if (!transactionIdsByRequestId.TryGetValue(requestId, out transactionId))
                {
                    return;
                }

                transactionIdsByRequestId.Remove(requestId);
                transactionRequestIds.Remove(transactionId);
            }

            OnMessageReceived(TransactionReply.Rejected(transactionId, errorMessage));
        }

        private void TrySendTransactionReplyAccepted(Guid transactionId)
        {
            using (transactionIdsByRequestIdLock.Lock())
            {
                uint requestId;
                if (transactionRequestIds.TryGetValue(transactionId, out requestId))
                {
                    transactionIdsByRequestId.Remove(requestId);
                }

                if (!transactionRequestIds.Remove(transactionId))
                {
                    return;
                }
            }

            OnMessageReceived(TransactionReply.Accepted(transactionId));
        }

        private void TrySendTransactionReplyRejected(Guid transactionId, string errorMessage)
        {
            using (transactionIdsByRequestIdLock.Lock())
            {
                uint requestId;
                if (transactionRequestIds.TryGetValue(transactionId, out requestId))
                {
                    transactionIdsByRequestId.Remove(requestId);
                }

                if (!transactionRequestIds.Remove(transactionId))
                {
                    return;
                }
            }

            OnMessageReceived(TransactionReply.Rejected(transactionId, errorMessage));
        }

        #endregion
    }
}

