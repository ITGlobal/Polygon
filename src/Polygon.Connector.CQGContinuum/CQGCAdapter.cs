using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using ProtoBuf;
using WebSocket4Net;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;
using Polygon.Connector.CQGContinuum.WebAPI;
using DataReceivedEventArgs = WebSocket4Net.DataReceivedEventArgs;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Обёртка над CQG Continuum API
    /// </summary>
    internal sealed class CQGCAdapter : IDisposable, ISubscriptionTester<InstrumentData>, IInstrumentConverterContext<InstrumentData>
    {
        #region Private fields

        /// <summary>
        ///     Версия протокола
        /// </summary>
        private const string ClientVersion = "1.32";

        /// <summary>
        ///     Логгер
        /// </summary>
        internal static readonly ILog Log = LogManager.GetLogger(typeof(CQGCAdapter));

        /// <summary>
        ///     Токен для завершения всех тасков
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        ///     Статус соединения
        /// </summary>
        private ConnectionStatus connectionStatus;

        /// <summary>
        ///     Объект синхронизации
        /// </summary>
        private readonly IRwLockObject locker = DeadlockMonitor.ReaderWriterLock(typeof(CQGCAdapter), "CQGCAdapter");

        /// <summary>
        ///     Событие райзится при ошибке сокета. По нему таска переподключения пытается переподключиться
        /// </summary>
        private readonly AutoResetEvent socketErrorEvent = new AutoResetEvent(false);

        /// <summary>
        ///     События разлогинивания
        /// </summary>
        private readonly ManualResetEvent logoffEfent = new ManualResetEvent(true);

        /// <summary>
        ///     Событие закрытия сокета
        /// </summary>
        private readonly ManualResetEvent socketClosedEvent = new ManualResetEvent(true);

        /// <summary>
        ///     Лок для доступа к сокету
        /// </summary>
        private readonly ILockObject socketLock = DeadlockMonitor.Cookie<CQGCAdapter>("socketLock");

        /// <summary>
        ///     Сокет, через который происходит взаимодействие с сервером
        /// </summary>
        private WebSocket socket;

        /// <summary>
        ///     Настройки
        /// </summary>
        private readonly CQGCParameters settings;

        private DateTime baseTime;

        /// <summary>
        ///     Выставляется в true при создании и при вызове метода Stop. В методе Start выставляется в false
        /// </summary>
        private bool stopped = true;

        #endregion

        #region .ctor

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="settings">
        ///     Настройки
        /// </param>
        /// <param name="telemetry">
        ///     Служба телеметрии
        /// </param>
        /// <param name="transportFactory">
        ///     Фабрика транспорта
        /// </param>
        public CQGCAdapter(CQGCParameters settings)
        {
            this.settings = settings;

            MarketDataResolved += OnMarketDataResolved;
            MarketDataNotResolved += OnMarketDataNotResolved;
        }

        #endregion

        #region Events

        #region ConnectionStatusChanged

        public ConnectionStatus ConnectionStatus { get; private set; }

        /// <summary>
        ///     Событие изменения состояния подключения
        /// </summary>
        public event EventHandler ConnectionStatusChanged;

        private void OnConnectionStatusChanged(ConnectionStatus status)
        {
            ConnectionStatus = status;

            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion

        #region AccountResolved

        private readonly AdapterEvent<AccountsReport> accountResolvedEvent = new AdapterEvent<AccountsReport>();

        /// <summary>
        ///     Событие получения информации о счетах
        /// </summary>
        public event AdapterEventHandler<AccountsReport> AccountResolved
        {
            add { accountResolvedEvent.Add(value); }
            remove { accountResolvedEvent.Remove(value); }
        }

        #endregion

        #region AccountsReport

        private readonly AdapterEvent<ContractMetadata> contractMetadataReceivedEvent = new AdapterEvent<ContractMetadata>();

        /// <summary>
        ///     Событие получения метаданных
        /// </summary>
        public event AdapterEventHandler<ContractMetadata> ContractMetadataReceived
        {
            add { contractMetadataReceivedEvent.Add(value); }
            remove { contractMetadataReceivedEvent.Remove(value); }
        }

        #endregion

        #region MarketDataResolved

        private readonly AdapterEvent<InformationReport> marketDataResolvedEvent = new AdapterEvent<InformationReport>();

        /// <summary>
        ///     Событие получения информации о рыночных данных
        /// </summary>
        public event AdapterEventHandler<InformationReport> MarketDataResolved
        {
            add { marketDataResolvedEvent.Add(value); }
            remove { marketDataResolvedEvent.Remove(value); }
        }

        #endregion

        #region MarketDataNotResolved

        private readonly AdapterEvent<InformationReport> marketDataNotResolvedEvent = new AdapterEvent<InformationReport>();

        /// <summary>
        ///     Событие неполучения информации о рыночных данных
        /// </summary>
        public event AdapterEventHandler<InformationReport> MarketDataNotResolved
        {
            add { marketDataNotResolvedEvent.Add(value); }
            remove { marketDataNotResolvedEvent.Remove(value); }
        }

        #endregion

        #region RealTimeMarketDataReceived

        private readonly AdapterEvent<RealTimeMarketData> realTimeMarketDataReceivedEvent = new AdapterEvent<RealTimeMarketData>();

        /// <summary>
        ///     Событие получения текущих рыночных данных
        /// </summary>
        public event AdapterEventHandler<RealTimeMarketData> RealTimeMarketDataReceived
        {
            add { realTimeMarketDataReceivedEvent.Add(value); }
            remove { realTimeMarketDataReceivedEvent.Remove(value); }
        }

        #endregion

        #region MarketDataSubscriptionStatusReceived

        private readonly AdapterEvent<MarketDataSubscriptionStatus> marketDataSubscriptionStatusReceivedEvent = new AdapterEvent<MarketDataSubscriptionStatus>();

        /// <summary>
        ///     Событие получения статуса подписки
        /// </summary>
        public event AdapterEventHandler<MarketDataSubscriptionStatus> MarketDataSubscriptionStatusReceived
        {
            add { marketDataSubscriptionStatusReceivedEvent.Add(value); }
            remove { marketDataSubscriptionStatusReceivedEvent.Remove(value); }
        }

        #endregion

        #region PositionStatusReceived

        private readonly AdapterEvent<PositionStatus> positionStatusReceivedEvent = new AdapterEvent<PositionStatus>();

        /// <summary>
        ///     Событие получения статуса позиции
        /// </summary>
        public event AdapterEventHandler<PositionStatus> PositionStatusReceived
        {
            add { positionStatusReceivedEvent.Add(value); }
            remove { positionStatusReceivedEvent.Remove(value); }
        }

        #endregion

        #region TradeSnapshotCompletionReceived

        private readonly AdapterEvent<TradeSnapshotCompletion> tradeSnapshotCompletionReceivedEvent = new AdapterEvent<TradeSnapshotCompletion>();

        /// <summary>
        ///     Событие получения сообщения о завешении снапшопа
        /// </summary>
        public event AdapterEventHandler<TradeSnapshotCompletion> TradeSnapshotCompletionReceived
        {
            add { tradeSnapshotCompletionReceivedEvent.Add(value); }
            remove { tradeSnapshotCompletionReceivedEvent.Remove(value); }
        }

        #endregion

        #region OrderStatusReceived

        private readonly AdapterEvent<OrderStatus> orderStatusReceivedEvent = new AdapterEvent<OrderStatus>();

        /// <summary>
        ///     Событие получения сообщения о статусе подписки
        /// </summary>
        public event AdapterEventHandler<OrderStatus> OrderStatusReceived
        {
            add { orderStatusReceivedEvent.Add(value); }
            remove { orderStatusReceivedEvent.Remove(value); }
        }

        #endregion

        #region TradeSubscriptionStatusReceived

        private readonly AdapterEvent<TradeSubscriptionStatus> tradeSubscriptionStatusReceivedEvent = new AdapterEvent<TradeSubscriptionStatus>();

        /// <summary>
        ///     Событие получения сообщения о статусе подписки
        /// </summary>
        public event AdapterEventHandler<TradeSubscriptionStatus> TradeSubscriptionStatusReceived
        {
            add { tradeSubscriptionStatusReceivedEvent.Add(value); }
            remove { tradeSubscriptionStatusReceivedEvent.Remove(value); }
        }

        #endregion

        #region CollateralStatusReceived

        private readonly AdapterEvent<CollateralStatus> collateralStatusReceivedEvent = new AdapterEvent<CollateralStatus>();

        /// <summary>
        ///     Событие получения сообщения о статусе подписки
        /// </summary>
        public event AdapterEventHandler<CollateralStatus> CollateralStatusReceived
        {
            add { collateralStatusReceivedEvent.Add(value); }
            remove { collateralStatusReceivedEvent.Remove(value); }
        }

        #endregion

        #region UserMessageReceived

        private readonly AdapterEvent<OrderRequestReject> orderRequestRejectReceivedEvent = new AdapterEvent<OrderRequestReject>();

        /// <summary>
        ///     Событие получения отказа по заявке
        /// </summary>
        public event AdapterEventHandler<OrderRequestReject> OrderRequestRejectReceived
        {
            add { orderRequestRejectReceivedEvent.Add(value); }
            remove { orderRequestRejectReceivedEvent.Remove(value); }
        }

        #endregion

        #region TimeBarReportReceived

        private readonly AdapterEvent<TimeBarReport> timeBarReportReceivedEvent = new AdapterEvent<TimeBarReport>();

        /// <summary>
        ///     Событие получения баров
        /// </summary>
        public event AdapterEventHandler<TimeBarReport> TimeBarReportReceived
        {
            add { timeBarReportReceivedEvent.Add(value); }
            remove { timeBarReportReceivedEvent.Remove(value); }
        }

        #endregion

        #region UserMessageReceived

        private readonly AdapterEvent<UserMessage> userMessageReceivedEvent = new AdapterEvent<UserMessage>();

        /// <summary>
        ///     Событие получения пользовательского сообщения
        /// </summary>
        public event AdapterEventHandler<UserMessage> UserMessageReceived
        {
            add { userMessageReceivedEvent.Add(value); }
            remove { userMessageReceivedEvent.Remove(value); }
        }

        #endregion

        #endregion

        #region Request IDs

        /// <summary>
        ///     Инкрементируемый id очередного запроса
        /// </summary>
        private int requestId = (int)0u;

        public uint GetNextRequestId()
        {
            var id = (uint)Interlocked.Increment(ref requestId);
            return id;
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Запуск адаптера 
        /// </summary>
        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Connect();
            stopped = false;
        }

        /// <summary>
        ///     Останов адаптера 
        /// </summary>
        public void Stop()
        {
            try
            {
                using (socketLock.Lock())
                {
                    if (socket == null || stopped)
                    {
                        return;
                    }
                }

                stopped = true;
                Log.Info().Print("Stopping CQGCAdapter");
                SendLogoff();
                cancellationTokenSource.Cancel();
                Log.Info().Print("Waiting for logoff reply");

                WaitHandle.WaitAny(new WaitHandle[] { socketClosedEvent, logoffEfent });
            }
            catch (CQGCAdapterConnectionException e)
            {
                // Адаптер не был подключен, logoff не нужен
                Log.Warn().Print(e, "No active connection on stop");
            }
        }

        public void Terminate()
        {
            using (socketLock.Lock())
            {
                if (socket != null)
                {
                    socket.Dispose();
                    socket = null;
                }
            }

            connectionStatus = ConnectionStatus.Disconnected;
            OnConnectionStatusChanged(ConnectionStatus.Disconnected);
        }

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(Logon msg) => SendMessageImpl(new ClientMsg { logon = msg });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(Logoff msg) => SendMessageImpl(new ClientMsg { logoff = msg });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(PasswordChange msg) => SendMessageImpl(new ClientMsg { password_change = msg });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(ObtainDemoCredentials msg) => SendMessageImpl(new ClientMsg { obtain_demo_credentials = msg });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(UserSessionStateRequest msg) => SendMessageImpl(new ClientMsg { user_session_state_request = msg });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(InformationRequest msg) => SendMessageImpl(new ClientMsg { information_request = { msg } });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(IEnumerable<InformationRequest> msgs)
        {
            var clientMessage = new ClientMsg();
            foreach (var msg in msgs)
            {
                clientMessage.information_request.Add(msg);
            }
            SendMessageImpl(clientMessage);
        }

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(TradeSubscription msg) => SendMessageImpl(new ClientMsg { trade_subscription = { msg } });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(MarketDataSubscription msg) => SendMessageImpl(new ClientMsg { market_data_subscription = { msg } });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(IEnumerable<MarketDataSubscription> msgs)
        {
            var clientMessage = new ClientMsg();
            foreach (var msg in msgs)
            {
                clientMessage.market_data_subscription.Add(msg);
            }
            SendMessageImpl(clientMessage);
        }

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(OrderRequest msg) => SendMessageImpl(new ClientMsg { order_request = { msg } });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(ReadUserAttributeRequest msg) => SendMessageImpl(new ClientMsg { read_user_attribute_request = { msg } });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(TimeAndSalesRequest msg) => SendMessageImpl(new ClientMsg { time_and_sales_request = { msg } });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        public void SendMessage(TimeBarRequest msg) => SendMessageImpl(new ClientMsg { time_bar_request = { msg } });

        /// <summary>
        ///     Отправить сообщение в сокет
        /// </summary>
        private void SendMessageImpl(ClientMsg msg)
        {
            using (socketLock.Lock())
            {
                if (socket == null ||
                    socket.State == WebSocketState.Closed ||
                    socket.State == WebSocketState.Closing ||
                    socket.State == WebSocketState.Connecting ||
                    socket.State == WebSocketState.None
                    )
                {
                    Log.Error().Print($"CQG adapter is not connected", LogFields.Login(settings?.Username));
                    return;
                }

                try
                {
                    byte[] serverMessageRaw;
                    using (var memoryStream = new MemoryStream())
                    {
                        Serializer.Serialize(memoryStream, msg);
                        serverMessageRaw = memoryStream.ToArray();
                    }

                    //[ENABLE_CQGC_TRACE]WebApiTrace.OutMessage(msg);
                    socket.Send(serverMessageRaw, 0, serverMessageRaw.Length);
                }
                catch (Exception ex)
                {
                    // В случае сбоя - отключаемся
                    Log.Error().Print(ex, "Failed to send a message");

                    if (socket != null)
                    {
                        Stop();
                        socket?.Dispose();
                    }

                    connectionStatus = ConnectionStatus.Disconnected;
                }
            }
        }

        public DateTime ResolveDateTime(long time) => baseTime.AddMilliseconds(time);

        public long ResolveDateTime(DateTime time)
        {
            if (time.Kind != DateTimeKind.Utc)
            {
                time = time.ToUniversalTime();
            }

            return (long)time.Subtract(baseTime).TotalMilliseconds;
        }

        public void Dispose()
        {
            MarketDataResolved -= OnMarketDataResolved;
            MarketDataNotResolved -= OnMarketDataNotResolved;

            using (socketLock.Lock())
            {
                if (socket != null)
                {
                    Stop();
                    socket?.Dispose();
                }
            }

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }

        #endregion

        #region Private methods

        #region Jobs

        /// <summary>
        ///     Задача (пере)открытия сокета
        /// </summary>
        private void Connect()
        {
            Task.Factory.StartNew(() =>
            {
                var cancellationToken = cancellationTokenSource.Token;
                Thread.CurrentThread.Name = "CQGC_CONN";

                // количество попыток коделчения нужно ограничивать
                int attempts = 0, attemptsLimit = 5;

                var major = (int)ProtocolVersionMajor.PROTOCOL_VERSION_MAJOR;
                var minor = (int)ProtocolVersionMinor.PROTOCOL_VERSION_MINOR;
                Log.Info().Print($"Using CQG Continuum protocol v{major}.{minor}");

                while (!cancellationToken.IsCancellationRequested)
                {
                    var shouldSleep = false;
                    var shouldWait = false;

                    // если была ошибка авторизации, то мы не можем дальше продолжать попытки реконнекта
                    if (connectionStatus == ConnectionStatus.Terminated)
                    {
                        Log.Error().Print(
                            "Cancelling reconnection task",
                            LogFields.ConnectionStatus(connectionStatus),
                            LogFields.Login(settings?.Username)
                            );
                        break;
                    }

                    using (socketLock.Lock())
                    {
                        if (socket == null)
                        {
                            if (++attempts > attemptsLimit)
                            {
                                Log.Error().Print("CQG transport was stopped due to relogon attempts limit", LogFields.Login(settings?.Username));
                                break;
                            }

                            CreateAndOpenSocket();
                            shouldSleep = true;
                        }
                        else
                        {
                            switch (socket.State)
                            {
                                case WebSocketState.Open:
                                    shouldWait = true;
                                    break;
                                case WebSocketState.None:
                                case WebSocketState.Closed:
                                case WebSocketState.Closing:
                                case WebSocketState.Connecting:
                                default:
                                    break;
                            }
                        }
                    }

                    if (shouldWait)
                    {
                        Log.Debug().Print("Waiting for task cancellation or connection error to reconnect");
                        // замораживаем поток в ожидании отмены всех задач или разрыва соединения сокета
                        if (WaitContinueOrCancel(cancellationToken, socketErrorEvent))
                        {
                            Log.Debug().Print(
                                "Task has been cancelled, thread exited",
                                LogFields.ConnectionStatus(connectionStatus),
                                LogFields.State(socket?.State),
                                LogFields.Login(settings?.Username)
                                );
                            return;
                        }
                    }

                    if (shouldSleep)
                    {
                        Log.Debug().Print(
                            $"Sleep for 10 sec",
                            LogFields.ConnectionStatus(connectionStatus),
                            LogFields.State(socket?.State),
                            LogFields.Login(settings?.Username)
                            );
                        Thread.Sleep(10000);
                    }
                }
                Log.Debug().Print(
                    $"Task completed, thread exited",
                    LogFields.ConnectionStatus(connectionStatus),
                    LogFields.State(socket?.State),
                    LogFields.Login(settings?.Username)
                    );
            }, TaskCreationOptions.LongRunning);
        }

        #endregion

        #region Socket events

        /// <summary>
        ///     Socket connection open event handler. Called when connection opened successfully.
        /// </summary>
        private void OnSocketOpen(object sender, EventArgs args)
        {
            Log.Debug().Print("WS:OnSocketOpen", LogFields.Login(settings?.Username));

            try
            {
                socketClosedEvent.Reset();
                SendLogon();
            }
            catch (Exception e)
            {
                Log.Error().Print(e, "WS:OnSocketOpen failed to process an event");
            }
        }

        /// <summary>
        ///     Socket connection data sending event handler. Called on response to requested or subscribed data.
        /// </summary>
        private void OnSocketData(object sender, DataReceivedEventArgs e)
        {
            try
            {
                using (locker.WriteLock())
                {
                    using (var stream = new MemoryStream(e.Data))
                    {
                        stream.Position = 0;
                        var msg = Serializer.Deserialize<ServerMsg>(stream);

                        //[ENABLE_CQGC_TRACE]WebApiTrace.InMessage(msg);

                        if (msg.logon_result != null)
                        {
                            Handle(msg.logon_result);
                        }
                        if (msg.logged_off != null)
                        {
                            Handle(msg.logged_off);
                        }
                        //if (msg.ping != null)
                        //    processPing(msg.ping);
                        //if (msg.pong != null)
                        //    processPong(msg.pong);
                        if (msg.information_report.Count > 0)
                        {
                            foreach (var info in msg.information_report)
                            {
                                Handle(info);
                            }
                        }
                        if (msg.market_data_subscription_status.Count > 0)
                        {
                            foreach (var subscriptionStatus in msg.market_data_subscription_status)
                            {
                                marketDataSubscriptionStatusReceivedEvent.Raise(subscriptionStatus);
                            }
                        }
                        if (msg.real_time_market_data.Count > 0)
                        {
                            foreach (var realTimeMarketData in msg.real_time_market_data)
                            {
                                realTimeMarketDataReceivedEvent.Raise(realTimeMarketData);
                            }
                        }
                        if (msg.trade_subscription_status.Count > 0)
                        {
                            foreach (var status in msg.trade_subscription_status)
                            {
                                tradeSubscriptionStatusReceivedEvent.Raise(status);
                            }
                        }
                        if (msg.trade_snapshot_completion.Count > 0)
                        {
                            foreach (var snapshot in msg.trade_snapshot_completion)
                            {
                                tradeSnapshotCompletionReceivedEvent.Raise(snapshot);
                            }
                        }
                        if (msg.order_status.Count > 0)
                        {
                            foreach (var orderStatus in msg.order_status)
                            {
                                if (orderStatus.contract_metadata != null)
                                {
                                    foreach (var metadata in orderStatus.contract_metadata)
                                    {
                                        contractMetadataReceivedEvent.Raise(metadata);
                                    }
                                }

                                orderStatusReceivedEvent.Raise(orderStatus);
                            }
                        }
                        if (msg.position_status.Count > 0)
                        {
                            foreach (var positionStatus in msg.position_status)
                            {
                                if (positionStatus.contract_metadata != null)
                                {
                                    contractMetadataReceivedEvent.Raise(positionStatus.contract_metadata);
                                }

                                positionStatusReceivedEvent.Raise(positionStatus);
                            }
                        }
                        if (msg.collateral_status.Count > 0)
                        {
                            foreach (var collateralStatus in msg.collateral_status)
                            {
                                collateralStatusReceivedEvent.Raise(collateralStatus);
                            }
                        }
                        if (msg.time_bar_report.Count > 0)
                        {
                            foreach (var report in msg.time_bar_report)
                            {
                                timeBarReportReceivedEvent.Raise(report.request_id, report);
                            }
                        }
                        //if (msg.time_and_sales_report.Count > 0)
                        //    foreach (TimeAndSalesReport report in msg.time_and_sales_report)
                        //       processTimeAndSalesReport(report);
                        if (msg.order_request_reject.Count > 0)
                        {
                            foreach (var message in msg.order_request_reject)
                            {
                                orderRequestRejectReceivedEvent.Raise(message.request_id, message);
                            }
                        }
                        if (msg.user_message.Count > 0)
                        {
                            foreach (var userMessage in msg.user_message)
                            {
                                userMessageReceivedEvent.Raise(userMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error().Print(ex, "WS:OnSocketData failed to process an event");
            }
        }

        /// <summary>
        ///     Socket connection close event handler. Called when connection closed.
        /// </summary>
        private void OnSocketClose(object sender, EventArgs e)
        {
            Log.Debug().Print("WS:OnSocketClose", LogFields.Login(settings?.Username));

            // статус Terminated мы не меняем, он финальный, сбрасывается только при перезапуске транспорта
            connectionStatus = connectionStatus != ConnectionStatus.Terminated ? ConnectionStatus.Disconnected : connectionStatus;
            OnConnectionStatusChanged(connectionStatus);

            using (socketLock.Lock())
            {
                try
                {
                    socket?.Dispose();
                }
                catch (Exception ex)
                {
                    Log.Error().Print(ex, "WS:OnSocketClose failed to process an event");
                }
                finally
                {
                    socketClosedEvent.Set();
                    socket = null;
                }
            }
        }

        /// <summary>
        ///     Socket connection error event handler. Called when error rose up.
        /// </summary>
        private void OnSocketError(object sender, ErrorEventArgs e)
        {
            Log.Error().Print(e.Exception, "WS:OnSocketError", LogFields.Login(settings?.Username));

            // статус Terminated мы не меняем, он финальный, сбрасывается только при перезапуске транспорта
            connectionStatus = connectionStatus != ConnectionStatus.Terminated ? ConnectionStatus.Disconnected : connectionStatus;
            OnConnectionStatusChanged(connectionStatus);

            using (socketLock.Lock())
            {
                if (socket != null)
                {
                    try
                    {
                        if (socket.State == WebSocketState.Open)
                        {
                            socket.Close();
                        }

                        // NOTE вообще тут надо бы делать socket.Dispose(), но из-за этого возникает race condition 
                        // socket.Dispose();
                    }
                    catch (Exception exception)
                    {
                        Log.Error().Print(exception, "WS:OnSocketError: Error while disposing error socket");
                    }
                    finally
                    {
                        socket = null;
                    }
                }
            }

            socketErrorEvent.Set();
        }

        #endregion

        #region Server messages processing

        /// <summary>
        ///     Обработка результата логина
        /// </summary>
        private void Handle(LogonResult msg)
        {
            var resultCode = (LogonResult.ResultCode)msg.result_code;
            switch (resultCode)
            {
                case LogonResult.ResultCode.SUCCESS:
                    Log.Info().Print($"Connected to CQG with login {settings?.Username}");
                    if (!DateTime.TryParseExact(
                        msg.base_time,
                        "yyyy-MM-ddTHH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal,
                        out baseTime))
                    {
                        baseTime = DateTime.UtcNow;
                        Log.Warn().Print($"Unable to parse base_time = msg.base_time. Order and fill timestamps might be corrupted. Historical data won't work at all.");
                    }

                    //[ENABLE_CQGC_TRACE]WebApiTrace.SetBaseTime(baseTime);
                    connectionStatus = ConnectionStatus.Connected;
                    OnConnectionStatusChanged(connectionStatus);
                    logoffEfent.Reset();
                    RequestInitializingData();

                    return;

                case LogonResult.ResultCode.FAILURE:
                    break;
                case LogonResult.ResultCode.NO_ONETIME_PASSWORD:
                    break;
                case LogonResult.ResultCode.PASSWORD_EXPIRED:
                    return;
                case LogonResult.ResultCode.CONCURRENT_SESSION:
                    break;
                case LogonResult.ResultCode.REDIRECTED:
                    break;
            }

            Log.Error().Print("Unable to log in", LogFields.Result(resultCode), LogFields.Message(msg.text_message));
            connectionStatus = ConnectionStatus.Terminated;
            OnConnectionStatusChanged(connectionStatus);

            using (socketLock.Lock())
            {
                Log.Debug().Print("Closing CQG socket");
                socket?.Close();
            }
        }

        /// <summary>
        ///     Обработка результата выхода из системы
        /// </summary>
        private void Handle(LoggedOff msg)
        {
            if (msg != null)
            {
                Log.Info().Print($"Logoff received. Closing socket", LogFields.Login(settings?.Username));
                using (socketLock.Lock())
                {
                    socket?.Close();
                }

                logoffEfent.Set();
            }

            connectionStatus = ConnectionStatus.Connecting;
            OnConnectionStatusChanged(connectionStatus);
        }

        private void Handle(InformationReport report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report), "Invalid information report");

            switch ((InformationReport.StatusCode)report.status_code)
            {
                case InformationReport.StatusCode.FAILURE:
                case InformationReport.StatusCode.DISCONNECTED:
                case InformationReport.StatusCode.DROPPED:
                case InformationReport.StatusCode.NOT_FOUND:
                    marketDataNotResolvedEvent.Raise(report.id, report);
                    return;
                case InformationReport.StatusCode.SUCCESS:
                    if (report.accounts_report != null)
                    {
                        accountResolvedEvent.Raise(report.accounts_report);
                    }
                    else if (report.symbol_resolution_report != null)
                    {
                        if (report.symbol_resolution_report.contract_metadata != null)
                        {
                            contractMetadataReceivedEvent.Raise(report.symbol_resolution_report.contract_metadata);
                        }

                        marketDataResolvedEvent.Raise(report.id, report);
                    }
                    //else if (report.session_information_report != null)
                    //{
                    //    sessionsResolved(report.session_information_report);
                    //}
                    //else if (report.last_statement_balances_report != null)
                    //{
                    //    lastStatementBalancesResolved(report.last_statement_balances_report);
                    //}
                    //else if (report.historical_orders_report != null)
                    //{
                    //    HistoricalOrdersRequestResolved(report.historical_orders_report);
                    //}
                    break;
                case InformationReport.StatusCode.SUBSCRIBED:
                    break;
                case InformationReport.StatusCode.UPDATE:
                    break;
            }
        }

        #endregion

        #region Message sending

        /// <summary>
        ///     Отправить сообщение логина
        /// </summary>
        private void SendLogon()
        {
            var logon = new Logon
            {
                user_name = settings.Username,
                password = settings.Password,
                client_app_id = "ItGlobal",
                client_version = ClientVersion,
                collapsing_level = (uint)RealTimeCollapsing.Level.DOM_BBA_TRADES,
                drop_concurrent_session = true,
                protocol_version_major = (uint)ProtocolVersionMajor.PROTOCOL_VERSION_MAJOR,
                protocol_version_minor = (uint)ProtocolVersionMinor.PROTOCOL_VERSION_MINOR
            };

            Log.Debug().Print("Sending logon message", LogFields.Login(settings?.Username));
            SendMessage(logon);
        }

        private void SendLogoff()
        {
            Log.Debug().Print("Sending logoff message", LogFields.Login(settings?.Username));
            SendMessage(new Logoff());
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     Создаёт и открывает сокет
        /// </summary>
        private void CreateAndOpenSocket()
        {
            using (socketLock.Lock())
            {
                Log.Info().Print("Connecting to CQG Continuum", LogFields.URL(settings.ConnectionUrl), LogFields.Login(settings.Username));
                socket?.Dispose();

                socket = new WebSocket(settings.ConnectionUrl);
                socket.Opened += OnSocketOpen;
                socket.Closed += OnSocketClose;
                socket.Error += OnSocketError;
                socket.DataReceived += OnSocketData;
                socket.Open();
            }
        }

        /// <summary>
        ///     Хелпер для одновременного ожидания отмены или продолжения задачи
        /// </summary>
        /// <param name="token">
        ///     Токен отмены
        /// </param>
        /// <param name="continuationHandles">
        ///     WaitHandle-ы продолжения обработки (например, AutoResetEvent)
        /// </param>
        /// <returns>
        ///     true если дождались отмены, иначе дождались одного из хэндлов продолжения задачи
        /// </returns>
        private static bool WaitContinueOrCancel(CancellationToken token, params WaitHandle[] continuationHandles)
        {
            var size = continuationHandles?.Length + 1;
            var handles = new WaitHandle[size.Value];
            handles[0] = token.WaitHandle;
            for (var i = 1; i < size; i++)
            {
                handles[i] = continuationHandles[i - 1];
            }

            var index = WaitHandle.WaitAny(handles);
            return index == 0;
        }

        /// <summary>
        ///     Запрос необходимых данных после успешного логина:
        ///     - список счетов
        ///     - позиции
        ///     - сделки
        ///     - заявки
        /// </summary>
        private void RequestInitializingData()
        {
            var request = new InformationRequest
            {
                id = IncrementRequestId(),
                accounts_request = new AccountsRequest()
            };
            SendMessage(request);
        }

        /// <summary>
        ///     Возвращает уникальный id для следующего сообщения
        /// </summary>
        private uint IncrementRequestId()
        {
            return (uint)(Interlocked.Increment(ref requestId));
        }

        #endregion

        #endregion

        #region IInstrumentConverterContext

        ISubscriptionTester<InstrumentData> IInstrumentConverterContext<InstrumentData>.SubscriptionTester => this;

        #endregion

        #region ISubscriptionTester

        private readonly ILockObject resolutionRequestsLock = DeadlockMonitor.Cookie<CQGCAdapter>("resolutionRequestsLock");
        private readonly Dictionary<uint, ResolutionRequest> resolutionRequestsById = new Dictionary<uint, ResolutionRequest>();

        private sealed class ResolutionRequest : TaskCompletionSource<SubscriptionTestResult>
        {
            public string Symbol { get; }

            public uint RequestId { get; set; }

            public ResolutionRequest(string symbol, uint requestId)
            {
                Symbol = symbol;
                RequestId = requestId;
            }
        }

        /// <summary>
        ///     Проверить подписку 
        /// </summary>
        async Task<SubscriptionTestResult> ISubscriptionTester<InstrumentData>.TestSubscriptionAsync(InstrumentData data)
        {
            var id = GetNextRequestId();
            var request = new ResolutionRequest(data.Symbol, id);
            using (resolutionRequestsLock.Lock())
            {
                resolutionRequestsById.Add(id, request);
            }

            var message = new InformationRequest
            {
                symbol_resolution_request = new SymbolResolutionRequest { symbol = data.Symbol },
                id = id
            };

            SendMessage(message);

            return await request.Task;
        }

        private void OnMarketDataResolved(AdapterEventArgs<InformationReport> args)
        {
            using (resolutionRequestsLock.Lock())
            {
                if (!resolutionRequestsById.TryGetValue(args.Message.id, out var request))
                {
                    return;
                }

                resolutionRequestsById.Remove(args.Message.id);

                var resolvedSymbol = args.Message.symbol_resolution_report.contract_metadata.contract_symbol;

                if (resolvedSymbol.EndsWith(request.Symbol))
                {
                    request.TrySetResult(SubscriptionTestResult.Passed(
                        ContractMetadataToString(args.Message.symbol_resolution_report.contract_metadata))
                    );
                }
                else
                {
                    request.TrySetResult(SubscriptionTestResult.Failed(
                        ContractMetadataToString(args.Message.symbol_resolution_report.contract_metadata))
                    );
                }
            }

            //request.TrySetResult(Tuple.Create(true,args.Message.ToString()));
            args.MarkHandled();
        }

        private void OnMarketDataNotResolved(AdapterEventArgs<InformationReport> args)
        {
            ResolutionRequest request;
            using (resolutionRequestsLock.Lock())
            {
                if (!resolutionRequestsById.TryGetValue(args.Message.id, out request))
                {
                    return;
                }

                resolutionRequestsById.Remove(args.Message.id);
            }

            request.TrySetResult(SubscriptionTestResult.Failed(args.Message.text_message));
            args.MarkHandled();
        }

        /// <summary>
        /// Конвертация в строку CQGContinuum.WebAPI.ContractMetadata
        /// </summary>
        private string ContractMetadataToString(ContractMetadata cmd)
        {
            var fmt = ObjectLogFormatter.Create(PrintOption.Nested, "CONTRACT_METADATA");
            fmt.AddField("sym", cmd.contract_symbol);
            fmt.AddField("und", cmd.underlying_contract_symbol);
            fmt.AddField("title", cmd.title);
            fmt.AddField("desc", cmd.description);
            return fmt.ToString();
        }

        #endregion
    }
}

