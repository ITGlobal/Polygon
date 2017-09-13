using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Diagnostics;
using Polygon.Messages;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using QuickFix.Transport;
using ILog = Polygon.Diagnostics.ILog;
using FixMessage = QuickFix.Message;
using Message = Polygon.Messages.Message;

namespace Polygon.Connector.SpectraFix
{
    /// <summary>
    ///     Адаптер для FIX шлюза срочного рынка Московской биржи
    /// </summary>
    internal sealed class SpectraFixConnector :
        IConnector,
        IConnectionStatusProvider,
        IOrderRouter,
        IApplication,
        IApplicationExt,
        ITransactionVisitor,
        IInstrumentConverterContext<InstrumentData>
    {
        #region fields

        private static readonly ILog _Log = LogManager.GetLogger<SpectraFixConnector>();

        private readonly SpectraFixSettings _settings;
        private readonly SocketInitiator _initiator;
        private readonly SessionID _sessionId;
        private readonly Session _session;

        private readonly ManualResetEventSlim _loggedIn = new ManualResetEventSlim();
        private readonly ManualResetEventSlim _loggedOut = new ManualResetEventSlim();

        private readonly OrderContainer _orders = new OrderContainer();
        private readonly TransactionContainer _newOrderTransactions;
        private readonly TransactionContainer _killOrderTransactions;
        private readonly TransactionContainer _modifyOrderTransactions;

        #endregion

        #region .ctor
        
        /// <summary>
        ///     .ctor
        /// </summary>
        public SpectraFixConnector(SpectraFixSettings settings)
        {
            _settings = settings;
            ConnectionStatusProviders = new IConnectionStatusProvider[] { this };
            _newOrderTransactions = new TransactionContainer(SendMessage);
            _killOrderTransactions = new TransactionContainer(SendMessage);
            _modifyOrderTransactions = new TransactionContainer(SendMessage);

            var fixSettings = settings.CreateSessionSettings();
            _sessionId = fixSettings.GetSessions().First();

            _initiator = new SocketInitiator(this, new MemoryStoreFactory(), fixSettings, new LogFactory());
        }

        #endregion

        #region IConnector

        /// <summary>
        ///     Название транспорта
        /// </summary>
        public string Name => "SpectraFix";

        /// <summary>
        ///     Фид транспорта
        /// </summary>
        public IFeed Feed => null;

        /// <summary>
        ///     Раутер транспорта
        /// </summary>
        public IOrderRouter Router => this;

        /// <summary>
        ///     Подписчик на параметры инструментов
        /// </summary>
        public IInstrumentParamsSubscriber InstrumentParamsSubscriber => null;

        /// <summary>
        ///     Подписчик на стаканы по инструментам
        /// </summary>
        public IOrderBookSubscriber OrderBookSubscriber => null;

        /// <summary>
        ///     Поиск инструментов по коду
        /// </summary>
        public IInstrumentTickerLookup InstrumentTickerLookup => null;

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

        /// <summary>
        ///     Запуск транспорта
        /// </summary>
        public void Start()
        {
            if (_initiator.IsLoggedOn)
            {
                return;
            }

            _Log.Info().Print("Connecting to Spectra FIX gate...");
            ChangeConnectionStatus(ConnectionStatus.Connecting);

            _loggedIn.Reset();
            _loggedOut.Reset();

            _initiator.Start();

            var i = WaitHandle.WaitAny(new[] { _loggedIn.WaitHandle, _loggedOut.WaitHandle });
            if (i != 0)
            {
                _initiator.Stop(force: true);
                _Log.Error().Print("Failed to connect to Spectra FIX gate");
                ChangeConnectionStatus(ConnectionStatus.Disconnected);
                return;
            }

            ChangeConnectionStatus(ConnectionStatus.Connected);
            _Log.Info().Print("Connected to Spectra FIX gate");
        }

        /// <summary>
        ///     Останов транспорта
        /// </summary>
        public void Stop()
        {
            if (!_initiator.IsLoggedOn)
            {
                return;
            }

            _Log.Info().Print("Disconnecting from Spectra FIX gate...");
            _loggedIn.Reset();
            _loggedOut.Reset();

            _initiator.Stop();

            var i = WaitHandle.WaitAny(new[] { _loggedIn.WaitHandle, _loggedOut.WaitHandle });
            if (i != 1)
            {
                _Log.Error().Print("Failed to gracefully disconnect from Spectra FIX gate");
                _initiator.Stop(force: true);
                return;
            }

            ChangeConnectionStatus(ConnectionStatus.Disconnected);
            _Log.Info().Print("Disconnected from Spectra FIX gate");
        }

        /// <summary>
        ///     Поддерживается ли модификация заявок по счету <paramref name="account"/>
        /// </summary>
        public bool SupportsOrderModification(string account)
        {
            return true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _initiator.Dispose();
        }

        #endregion

        #region IConnectionStatusProvider

        /// <summary>
        ///     Название соединения
        /// </summary>
        public string ConnectionName => "SPECTRA/FIX";

        /// <summary>
        ///     Текущее состояние соединения
        /// </summary>
        public ConnectionStatus ConnectionStatus => (ConnectionStatus)_status;
        // Содержит значения типа ConnectionStatus
        private int _status = (int)ConnectionStatus.Undefined;


        /// <summary>
        ///     Вызывается при изменении состояния соединения
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        private void ChangeConnectionStatus(ConnectionStatus newStatus)
        {
            if (Interlocked.Exchange(ref _status, (int)newStatus) != (int)newStatus)
            {
                ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(ConnectionStatus, ConnectionName));
            }
        }

        #endregion

        #region IApplication

        void IApplication.ToAdmin(FixMessage message, SessionID sessionId)
        {
            if (_Log.IsTraceEnabled)
            {
                _Log.Trace().PrintFormat("[{0}] >=> {1}", sessionId, message.PrettyPrint().Preformatted());
            }
        }

        void IApplication.FromAdmin(FixMessage message, SessionID sessionId)
        {
            if (_Log.IsTraceEnabled)
            {
                _Log.Trace().PrintFormat("[{0}] <=< {1}", sessionId, message.PrettyPrint().Preformatted());
            }
        }

        void IApplication.ToApp(FixMessage message, SessionID sessionId)
        {
            if (_Log.IsTraceEnabled)
            {
                _Log.Trace().PrintFormat("[{0}] >-> {1}", sessionId, message.PrettyPrint().Preformatted());
            }

            var seqNumber = message.Header.GetInt(34 /* MsgSeqNum */);
            switch (message)
            {
                case NewOrderSingle msg:
                    // Запоминаем связку (N сообщения, ID транзакции)
                    _newOrderTransactions.RememberSeqNumber(seqNumber, msg.ClOrdID.Obj);
                    break;
                case OrderCancelRequest msg:
                    // Запоминаем связку (N сообщения, ID транзакции)
                    _killOrderTransactions.RememberSeqNumber(seqNumber, msg.ClOrdID.Obj);
                    break;
                case OrderCancelReplaceRequest msg:
                    // Запоминаем связку (N сообщения, ID транзакции)
                    _modifyOrderTransactions.RememberSeqNumber(seqNumber, msg.ClOrdID.Obj);
                    break;
            }
        }

        void IApplication.FromApp(FixMessage message, SessionID sessionId)
        {
            if (_Log.IsTraceEnabled)
            {
                _Log.Trace().PrintFormat("[{0}] <-< {1}", sessionId, message.PrettyPrint().Preformatted());
            }

            switch (message)
            {
                case Reject msg:
                    Handle(msg);
                    break;

                case ExecutionReport msg:
                    Handle(msg);
                    break;

                case OrderCancelReject msg:
                    Handle(msg);
                    break;

                default:
                    _Log.Warn().PrintFormat("Unexpected message: {0}", message.GetType().Name);
                    break;
            }
        }

        void IApplication.OnCreate(SessionID sessionId) { }

        void IApplication.OnLogon(SessionID sessionId)
        {
            _loggedIn.Set();
        }

        void IApplication.OnLogout(SessionID sessionId)
        {
            _loggedOut.Set();
        }

        #endregion

        #region IApplicationExt

        void IApplicationExt.FromEarlyIntercept(FixMessage message, SessionID sessionId)
        {
            // HACK Биржа присылает сообщения ExecutionReport с группой Parties, но QuickFIX не понимает, в чем дело,
            // HACK и падает с валидацией
            switch (message)
            {
                case ExecutionReport msg:
                    msg.RepeatedTags.Clear();
                    break;
            }
        }

        #endregion

        #region FIX message handlers

        private void Handle(Reject msg)
        {
            var rejectReason = $"#{msg.SessionRejectReason?.Obj:-1} {msg.Text?.Obj}";
            _newOrderTransactions.Reject(msg.RefSeqNum.Obj, rejectReason);
            _killOrderTransactions.Reject(msg.RefSeqNum.Obj, rejectReason);
            _modifyOrderTransactions.Reject(msg.RefSeqNum.Obj, rejectReason);
        }

        private void Handle(OrderCancelReject msg)
        {
            var reason = "";
            if (msg.IsSetCxlRejReason())
            {
                switch (msg.CxlRejReason.Obj)
                {
                    case CxlRejReason.TOO_LATE_TO_CANCEL:
                        reason = "Too late to cancel";
                        break;
                    case CxlRejReason.ORDER_ALREADY_IN_PENDING_CANCEL_OR_PENDING_REPLACE_STATUS:
                        reason = "Order already in PendingCancel or PendingReplace status";
                        break;
                    default:
                        reason = $"#{msg.CxlRejReason.Obj}";
                        break;
                }
            }

            _killOrderTransactions.Reject(msg.ClOrdID.Obj, reason);
            _modifyOrderTransactions.Reject(msg.ClOrdID.Obj, reason);
        }

        private async void Handle(ExecutionReport msg)
        {
            var clOrderId = msg.ClOrdID.Obj;
            string origClOrderId = null;
            if (msg.IsSetOrigClOrdID())
            {
                origClOrderId = msg.OrigClOrdID.Obj;
            }

            var state = msg.IsSetOrdStatus() ? ConvertOrderState(msg.OrdStatus) : null;
            var price = msg.IsSetPrice() ? msg.Price.Obj : (decimal?)null;
            price = price != 0M ? price : null;
            var qty = msg.IsSetOrderQty() ? (uint)msg.OrderQty.Obj : (uint?)null;
            var filledQty = msg.IsSetLastQty() ? (uint)msg.LastQty.Obj : (uint?)null;
            var activeQty = msg.IsSetLeavesQty() ? (uint)msg.LeavesQty.Obj : (uint?)null;
            var time = msg.IsSetTransactTime() ? ParseTransactTime() : DateTime.Now;
            var orderExchangeId = msg.IsSetOrderID() ? msg.OrderID.Obj : null;

            var transactionId = _orders.GetOrCreateOrderTransactionId(origClOrderId ?? clOrderId);

            var oscm = new OrderStateChangeMessage
            {
                TransactionId = transactionId,
                State = state,
                Price = price,
                Quantity = qty,
                FilledQuantity = filledQty,
                ActiveQuantity = activeQty,
                ChangeTime = time,
                OrderExchangeId = orderExchangeId
            };

            switch (msg.ExecType.Obj)
            {
                case ExecType.NEW:
                    _newOrderTransactions.Accept(clOrderId);
                    _orders.SaveOrderExchangeId(transactionId, orderExchangeId);
                    SendMessage(oscm);
                    break;

                case ExecType.TRADE:
                    _newOrderTransactions.Accept(clOrderId);
                    _orders.SaveOrderExchangeId(transactionId, orderExchangeId);

                    SendMessage(oscm);
                    await EmitFillAsync();

                    if (oscm.State == OrderState.Filled)
                    {
                        _newOrderTransactions.Forget(transactionId);
                        _killOrderTransactions.Forget(msg.ClOrdID.Obj);
                        _orders.ForgetOrder(transactionId);
                    }
                    break;

                case ExecType.FILL:
                    _newOrderTransactions.Accept(clOrderId);

                    _orders.SaveOrderExchangeId(transactionId, orderExchangeId);
                    SendMessage(oscm);
                    await EmitFillAsync();

                    if (oscm.State == OrderState.Filled)
                    {
                        _newOrderTransactions.Forget(transactionId);
                        _killOrderTransactions.Forget(msg.ClOrdID.Obj);
                        _orders.ForgetOrder(transactionId);
                    }
                    break;

                case ExecType.PARTIAL_FILL:
                    _newOrderTransactions.Accept(clOrderId);
                    _orders.SaveOrderExchangeId(transactionId, orderExchangeId);
                    SendMessage(oscm);
                    await EmitFillAsync();
                    break;

                case ExecType.REJECTED:
                    string rejectReason;
                    switch (msg.OrdRejReason.Obj)
                    {
                        case OrdRejReason.UNKNOWN_SYMBOL:
                            rejectReason = "Unknown symbol";
                            break;
                        case OrdRejReason.EXCHANGE_CLOSED:
                            rejectReason = "Exchange closed";
                            break;
                        case OrdRejReason.ORDER_EXCEEDS_LIMIT:
                            rejectReason = "Order exceeds limit";
                            break;
                        case OrdRejReason.DUPLICATE_ORDER:
                            rejectReason = "Duplicate order";
                            break;
                        default:
                            rejectReason = $"#{msg.OrdRejReason.Obj}";
                            break;
                    }
                    if (!string.IsNullOrEmpty(msg.Text?.Obj))
                    {
                        rejectReason = $"{rejectReason}. {msg.Text?.Obj}";
                    }
                    _newOrderTransactions.Reject(clOrderId, rejectReason);
                    _orders.ForgetOrder(transactionId);
                    break;

                case ExecType.PENDING_CANCEL:
                    _killOrderTransactions.Accept(msg.ClOrdID.Obj);
                    SendMessage(oscm);
                    break;

                case ExecType.CANCELED:
                    _killOrderTransactions.Accept(msg.ClOrdID.Obj);
                    _newOrderTransactions.Forget(transactionId);
                    _killOrderTransactions.Forget(msg.ClOrdID.Obj);
                    _orders.ForgetOrder(transactionId);
                    SendMessage(oscm);
                    break;

                case ExecType.PENDING_REPLACE:
                    _modifyOrderTransactions.Accept(clOrderId);
                    break;

                case ExecType.REPLACED:
                    _newOrderTransactions.Forget(origClOrderId);
                    _killOrderTransactions.Forget(clOrderId);
                    _orders.UpdateOrderParams(
                        orderExchangeId: orderExchangeId, 
                        origClOrderId: origClOrderId, 
                        clOrderId: clOrderId, 
                        symbol: msg.Symbol.Obj,
                        qty: msg.OrderQty.Obj,
                        side: msg.Side.Obj);

                    SendMessage(oscm);
                    break;

                default:
                    _Log.Warn().PrintFormat("Unknown ExecType: {0}", msg.ExecType.Obj);
                    break;
            }

            OrderState? ConvertOrderState(OrdStatus field)
            {
                switch (field.Obj)
                {
                    case OrdStatus.PENDING_NEW: return OrderState.New;
                    case OrdStatus.NEW: return OrderState.Active;
                    case OrdStatus.CANCELED: return OrderState.Cancelled;
                    case OrdStatus.FILLED: return OrderState.Filled;
                    case OrdStatus.PARTIALLY_FILLED: return OrderState.PartiallyFilled;
                    case OrdStatus.REJECTED: return OrderState.Error;
                    case OrdStatus.PENDING_CANCEL: return null;
                    case OrdStatus.PENDING_REPLACE: return null;
                    default:
                        _Log.Warn().PrintFormat("Unknown OrdStatus: {0}", field.Obj);
                        return null;
                }
            }

            DateTime ParseTransactTime()
            {
                var maxLen = "20170912-09:39:57.2761221".Length;

                var str = msg.GetString(60 /*TransactTime*/);
                if (str.Length > maxLen)
                {
                    str = str.Substring(0, maxLen);
                }
                var t = DateTime.ParseExact(str, "yyyyMMdd-HH:mm:ss.fffffff", null, DateTimeStyles.AssumeUniversal);
                return t;
            }

            async Task EmitFillAsync()
            {
                if (!msg.IsSetSecondaryExecID())
                {
                    return;
                }

                var fillExchangeId = msg.SecondaryExecID.Obj;
                var fillPrice = msg.IsSetLastPx() ? msg.LastPx.Obj : (decimal?)null;
                price = price != 0M ? price : null;
                var fillQty = msg.IsSetLastQty() ? (uint)msg.LastQty.Obj : (uint?)null;
                if (fillPrice == null || fillQty == null)
                {
                    return;
                }
                
                Instrument instrument;

                try
                {
                    instrument = await _settings.InstrumentConverter.ResolveSymbolAsync(this, msg.Symbol.Obj);
                }
                catch (Exception e)
                {
                    _Log.Error().Print(e, $"Failed to resolve instrument {msg.Symbol.Obj}");
                    return;
                }
                
                var fill = new FillMessage
                {
                    Price = fillPrice.Value,
                    Quantity = fillQty.Value,
                    DateTime = time,
                    Instrument = instrument,
                    Account = msg.Account.Obj,
                    Operation = msg.Side.Obj == Side.BUY ? OrderOperation.Buy : OrderOperation.Sell,
                    ExchangeId = fillExchangeId,
                    ExchangeOrderId = orderExchangeId
                };
                SendMessage(fill);
            }
        }

        #endregion

        #region IOrderRouter

        /// <summary>
        ///     Транслировать ли ошибки в виде ErrorMessage
        /// </summary>
        public bool SendErrorMessages { get; set; }

        /// <summary>
        ///     Вызывается при получении сообщения из фида.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private void SendMessage(Message message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }

        /// <summary>
        ///     Список доступных для торговли счетов.
        /// </summary>
        public List<string> AvailableAccounts { get; } = new List<string>();

        /// <summary>
        ///     Доступна ли работа по данному счёту
        /// </summary>
        public bool IsPermittedAccount(string account) => true;

        /// <summary>
        ///     Отправляет заявки.
        /// </summary>
        /// <param name="transaction">
        ///     Экземпляр заявки для отправки.
        /// </param>
        public void SendTransaction(Transaction transaction)
        {
            if (!_initiator.IsLoggedOn)
            {
                SendMessage(TransactionReply.Rejected(transaction, "Router is off-line"));
            }

            transaction.Accept(this);
        }

        #endregion

        #region ITransactionVisitor

        /// <summary>
        ///     Обработать транзакцию <see cref="NewOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        async void ITransactionVisitor.Visit(NewOrderTransaction transaction)
        {
            InstrumentData instrumentData;

            try
            {
                instrumentData = await _settings.InstrumentConverter.ResolveInstrumentAsync(this, transaction.Instrument);
            }
            catch (Exception e)
            {
                _Log.Error().Print(e, $"Failed to resolve instrument {transaction.Instrument}");
                SendMessage(TransactionReply.Rejected(transaction, "Failed to resolve instrument"));
                return;
            }

            if (instrumentData == null)
            {
                _Log.Error().Print($"Instrument {transaction.Instrument} hasn't been resolved");
                SendMessage(TransactionReply.Rejected(transaction, "Instrument hasn't been resolved"));
                return;
            }

            var msg = new NewOrderSingle();
            msg.TransactTime = new TransactTime(DateTime.UtcNow);
            msg.OrdType = new OrdType(OrdType.LIMIT);
            msg.Symbol = new Symbol(instrumentData.Symbol);
            msg.Account = new Account(transaction.Account);
            switch (transaction.ExecutionCondition)
            {
                case OrderExecutionCondition.PutInQueue:
                    msg.TimeInForce = new TimeInForce(TimeInForce.DAY);
                    break;

                case OrderExecutionCondition.FillOrKill:
                    msg.TimeInForce = new TimeInForce(TimeInForce.FILL_OR_KILL);
                    break;

                default:
                    SendMessage(TransactionReply.Rejected(transaction, $"Unknown execution condition: {transaction.ExecutionCondition}"));
                    return;
            }

            switch (transaction.Operation)
            {
                case OrderOperation.Buy:
                    msg.Side = new Side(Side.BUY);
                    break;
                case OrderOperation.Sell:
                    msg.Side = new Side(Side.SELL);
                    break;
                default:
                    SendMessage(TransactionReply.Rejected(transaction, $"Unknown operation: {transaction.Operation}"));
                    return;
            }

            switch (transaction.Type)
            {
                case OrderType.Limit:
                    break;
                default:
                    SendMessage(TransactionReply.Rejected(transaction, $"Unsupported order type: {transaction.Type}"));
                    return;
            }

            msg.OrderQty = new OrderQty(transaction.Quantity);
            msg.Price = new Price(transaction.Price);

            var clOrderId = _newOrderTransactions.Add(transaction);
            msg.ClOrdID = new ClOrdID(clOrderId);

            if (!string.IsNullOrEmpty(transaction.Comment))
            {
                var comment = transaction.Comment;
                comment = comment.Length > 20 ? comment.Substring(0, 20) : comment;
                msg.SecondaryClOrdID = new SecondaryClOrdID(comment);
            }

            _orders.SaveOrderParams(transaction.TransactionId, clOrderId, msg.Symbol.Obj, msg.OrderQty.Obj, msg.Side.Obj);
            SendFixMessage(msg);
        }

        /// <summary>
        ///     Обработать транзакцию <see cref="ModifyOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void ITransactionVisitor.Visit(ModifyOrderTransaction transaction)
        {
            if (!_orders.GetOrderParams(transaction.OrderExchangeId, out var _, out var symbol, out var _, out var side))
            {
                SendMessage(TransactionReply.Rejected(transaction, $"Unknown order: {transaction.OrderExchangeId}"));
                return;
            }

            var msg = new OrderCancelReplaceRequest();
            msg.OrderID = new OrderID(transaction.OrderExchangeId);
            msg.Price = new Price(transaction.Price);
            msg.OrderQty = new OrderQty(transaction.Quantity);
            msg.Symbol = new Symbol(symbol);
            msg.TransactTime = new TransactTime(DateTime.UtcNow);
            msg.Side = new Side(side);

            var clOrderId = _modifyOrderTransactions.Add(transaction);
            msg.ClOrdID = new ClOrdID(clOrderId);

            SendFixMessage(msg);
        }

        /// <summary>
        ///     Обработать транзакцию <see cref="KillOrderTransaction"/>
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void ITransactionVisitor.Visit(KillOrderTransaction transaction)
        {
            if (!_orders.GetOrderParams(transaction.OrderExchangeId, out var _, out var symbol, out var qty, out var side))
            {
                SendMessage(TransactionReply.Rejected(transaction, $"Unknown order: {transaction.OrderExchangeId}"));
                return;
            }

            var msg = new OrderCancelRequest();
            var clOrderId = _killOrderTransactions.Add(transaction);

            msg.ClOrdID = new ClOrdID(clOrderId);
            msg.OrderID = new OrderID(transaction.OrderExchangeId);
            msg.Symbol = new Symbol(symbol);
            msg.Side = new Side(side);
            msg.OrderQty = new OrderQty(qty);
            msg.TransactTime = new TransactTime(DateTime.UtcNow);

            SendFixMessage(msg);
        }

        /// <summary>
        ///     Обработать прочие транзакции
        /// </summary>
        /// <param name="transaction">
        ///     Транзакция для обработки
        /// </param>
        void ITransactionVisitor.Visit(Transaction transaction)
        {
            SendMessage(TransactionReply.Rejected(transaction, $"Unknown transaction type: {transaction.GetType().Name}"));
        }

        #endregion

        #region IInstrumentConverterContext<InstrumentData>

        /// <summary>
        ///     Интерфейс для проверки подписки на инструмент по его коду
        /// </summary>
        ISubscriptionTester<InstrumentData> IInstrumentConverterContext<InstrumentData>.SubscriptionTester => null;

        #endregion

        #region FIX

        private void SendFixMessage(FixMessage message)
        {
            Session.SendToTarget(message, _sessionId);
        }

        #endregion
    }
}

