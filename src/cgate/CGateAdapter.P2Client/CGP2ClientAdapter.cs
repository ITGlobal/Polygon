using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CGateAdapter.Messages;
using CGateAdapter.Messages.FutCommon;
using ru.micexrts.cgate;

namespace CGateAdapter
{
    /// <summary>
    ///     Адаптер к интерфейсу cgate
    /// </summary>
    public sealed class CGP2ClientAdapter : ICGAdapter, ICGateStreamCallback, ICGateConnectionCallback
    {
        #region Fields

        private readonly string _workingDirectory;

        private readonly CGateConnection _dataConnection;
        private readonly CGateConnection _transactionConnection;

        /// <summary>
        /// Объект синхронизации
        /// </summary>
        private object _syncRoot = new object();

        /// <summary>
        /// Объект синхронизации при ошибках старта
        /// </summary>
        private object _startCancelSyncRoot = new object();

        /// <summary>
        /// Токен для отмены всех фоновых задач
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        ///     Событие прихода нового сообщения маркетдаты
        /// </summary>
        private readonly AutoResetEvent _newMarketdataMessageEvent = new AutoResetEvent(false);

        /// <summary>
        ///     Событие прихода нового сообщения связанного с исполнением заявок
        /// </summary>
        private readonly AutoResetEvent _newExecutionMessageEvent = new AutoResetEvent(false);

        /// <summary>
        ///     Очередь сообщений рыночных данных
        /// </summary>
        private readonly ConcurrentQueue<CGateMessage> _marketdataMessages = new ConcurrentQueue<CGateMessage>();

        /// <summary>
        ///     Очередь сообщений о заявках и сделках
        /// </summary>
        private readonly ConcurrentQueue<CGateMessage> _executionMessages = new ConcurrentQueue<CGateMessage>();

        /// <summary>
        ///     Задача по отдаче сообщений маркетдаты подписчику адаптера
        /// </summary>
        private Task _marketdataMessagePostingTask;

        /// <summary>
        ///     Задача по отдаче сообщений о заявках подписчику адаптера
        /// </summary>
        private Task _executionMessagePostingTask;

        private readonly CGAdapterConfiguration _configuration;
        private readonly CGatePublisher _ordersPublisher;

        // Реально тут лежат значения из CGConnectionState
        private int _connectionState = (int)CGConnectionState.Shutdown;

        #endregion

        #region ctor

        /// <summary>
        ///     Конструктор
        /// </summary>
        public CGP2ClientAdapter(
            CGAdapterConfiguration configuration, 
            string workingDirectory = @"cgate",
            bool openOrderBooksStreams = true, 
            bool openVolatStream = true, 
            bool openHeartbeatStream = true)
        {
            _configuration = configuration;
            _workingDirectory = workingDirectory;

            configuration.Logger.Debug("Creating CGAdapter: ip={0}, port={1}, ini_folder={2}", configuration.Address, configuration.Port, configuration.IniFolder);

            var futInfoStream = new CGateStream(
                name: "FORTS_FUTINFO_REPL", 
                streamType: CGateStreamType.FutInfo, 
                schemeFileName: $"{configuration.IniFolder}fut_info.ini",
                schemeName: "CustReplScheme",
                converter: new Messages.FutInfo.StreamDataMessageConverter(), 
                callback: this, 
                logger: configuration.Logger);
            var optInfoStream = new CGateStream(
                name: "FORTS_OPTINFO_REPL", 
                streamType: CGateStreamType.OptInfo, 
                schemeFileName: $"{configuration.IniFolder}opt_info.ini", 
                schemeName: "CustReplScheme", 
                converter: new Messages.OptInfo.StreamDataMessageConverter(),
                callback: this,
                logger: configuration.Logger);

            #region data connection
            {
                var firstSubconnections = new[] { futInfoStream, optInfoStream };
                var secondSubconnections = new[]
                {
                    openHeartbeatStream
                        ? new CGateStream(
                            name: "FORTS_FUTTRADE_REPL",
                            streamType: CGateStreamType.FutTrades,
                            schemeFileName: $"{configuration.IniFolder}fut_trades_heartbeat.ini",
                            schemeName: "CustReplScheme",
                            converter: new Messages.FutTrades.StreamDataMessageConverter(),
                            callback: this,
                            logger: configuration.Logger,
                            dependOnStreams: null,
                            mode: "online",
                            transactional: true)
                        : null,
                    new CGateStream(
                        name: "FORTS_FUTCOMMON_REPL",
                        streamType: CGateStreamType.FutCommon,
                        schemeFileName: $"{configuration.IniFolder}fut_common.ini",
                        schemeName: "CustReplScheme",
                        converter: new StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger),
                    new CGateStream(
                        name: "FORTS_OPTCOMMON_REPL",
                        streamType: CGateStreamType.OptCommon,
                        schemeFileName: $"{configuration.IniFolder}opt_common.ini",
                        schemeName: "CustReplScheme",
                        converter: new Messages.OptCommon.StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger),
                    new CGateStream(
                        name: "RTS_INDEX_REPL",
                        streamType: CGateStreamType.RtsIndex,
                        schemeFileName: $"{configuration.IniFolder}rts_index.ini",
                        schemeName: "CustReplScheme",
                        converter: new Messages.RtsIndex.StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger),
                    openVolatStream
                        ? new CGateStream(
                            name: "FORTS_VOLAT_REPL",
                            streamType: CGateStreamType.Volat,
                            schemeFileName: $"{configuration.IniFolder}volat.ini",
                            schemeName: "CustReplScheme",
                            converter: new Messages.Volat.StreamDataMessageConverter(),
                            callback: this,
                            logger: configuration.Logger)
                        : null,
                    openOrderBooksStreams
                        ? new CGateStream(
                            name: "FORTS_FUTAGGR20_REPL",
                            streamType: CGateStreamType.OrdersAggr,
                            schemeFileName: $"{configuration.IniFolder}orders_aggr.ini",
                            schemeName: "CustReplScheme",
                            converter: new Messages.OrdersAggr.StreamDataMessageConverter(),
                            callback: this,
                            logger: configuration.Logger,
                            dependOnStreams: null,
                            mode: "online",
                            useReplStateWhenReconnect: false)
                        : null,
                    openOrderBooksStreams
                        ? new CGateStream(
                            name: "FORTS_OPTAGGR20_REPL",
                            streamType: CGateStreamType.OrdersAggr,
                            schemeFileName: $"{configuration.IniFolder}orders_aggr.ini",
                            schemeName: "CustReplScheme",
                            converter: new Messages.OrdersAggr.StreamDataMessageConverter(),
                            callback: this,
                            logger: configuration.Logger,
                            dependOnStreams: null,
                            mode: "online",
                            useReplStateWhenReconnect: false)
                        : null
                };

                _dataConnection = new CGateConnection(
                    name: "CG_DATA_CONN",
                    ip: configuration.Address,
                    port: configuration.Port,
                    credential: configuration.DataConnectionCredential,
                    firstSubconnections: firstSubconnections,
                    secondSubconnections: secondSubconnections,
                    logger: configuration.Logger,
                    callback: this);
            }

            #endregion

            #region transaction connection

            {
                _ordersPublisher = new CGatePublisher(
                    name: "CG_ORDS_SNDR",
                    schemeFileName: $"{configuration.IniFolder}forts_messages.ini",
                    schemeName: "message",
                    converter: new Messages.FortsMessages.StreamDataMessageConverter(),
                    callback: this,
                    logger: configuration.Logger);

                var firstSubconnections = new CGateSubconnection[]
                {
                    _ordersPublisher,
                    new CGateStream(
                        name: "FORTS_FUTTRADE_REPL",
                        streamType: CGateStreamType.FutTrades,
                        schemeFileName: $"{configuration.IniFolder}fut_trades.ini",
                        schemeName: "CustReplScheme",
                        converter: new Messages.FutTrades.StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger,
                        dependOnStreams: new[] {futInfoStream, optInfoStream}, transactional: true),
                    new CGateStream(name: "FORTS_OPTTRADE_REPL",
                        streamType: CGateStreamType.OptTrades,
                        schemeFileName: $"{configuration.IniFolder}opt_trades.ini",
                        schemeName: "CustReplScheme",
                        converter: new Messages.OptTrades.StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger,
                        dependOnStreams: new[] {futInfoStream, optInfoStream}, transactional: true),
                    new CGateStream(
                        name: "FORTS_POS_REPL",
                        streamType: CGateStreamType.Pos,
                        schemeFileName: $"{configuration.IniFolder}pos.ini",
                        schemeName: "CustReplScheme",
                        converter: new Messages.Pos.StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger,
                        dependOnStreams: new[] {futInfoStream, optInfoStream}, transactional: true),
                    new CGateStream(
                        name: "FORTS_PART_REPL",
                        streamType: CGateStreamType.Part,
                        schemeFileName: $"{configuration.IniFolder}part.ini",
                        schemeName: "CustReplScheme",
                        converter: new Messages.Part.StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger,
                        dependOnStreams: new[] {futInfoStream, optInfoStream}, transactional: true),
                    new CGateStream(
                        name: "FORTS_VM_REPL",
                        streamType: CGateStreamType.Vm,
                        schemeFileName: $"{configuration.IniFolder}vm.ini",
                        schemeName: "CustReplScheme",
                        converter: new Messages.Vm.StreamDataMessageConverter(),
                        callback: this,
                        logger: configuration.Logger,
                        dependOnStreams: new[] {futInfoStream, optInfoStream}, transactional: true),
                };

                _transactionConnection = new CGateConnection(
                    "CG_TRNS_CONN",
                    configuration.Address,
                    configuration.Port,
                    configuration.TransactionConnectionCredential,
                    firstSubconnections,
                    null,
                    configuration.Logger,
                    this);
            }

            #endregion
        }

        #endregion

        #region ICGAdapter

        /// <summary>
        ///     Состояние соединения
        /// </summary>
        public CGConnectionState ConnectionState
        {
            [DebuggerStepThrough]
            get
            {
                return (CGConnectionState)
                        Interlocked.CompareExchange(
                            ref _connectionState,
                            (int)CGConnectionState.Disconnected,
                            (int)CGConnectionState.Disconnected);
            }
        }

        /// <summary>
        ///     Запустить адаптер
        /// </summary>
        public void Start()
        {
            try
            {
                lock (_syncRoot)
                {
                    _cts = new CancellationTokenSource();
                    UpdateConnectionState(CGConnectionState.Connecting);
                    _configuration.Logger.Debug("Starting CGA");

                    _marketdataMessagePostingTask = Task.Factory.StartNew(PostNewDataMessagesToSubscribers,
                        TaskCreationOptions.LongRunning);

                    _executionMessagePostingTask = Task.Factory.StartNew(PostNewExecutionMessagesToSubscribers,
                        TaskCreationOptions.LongRunning);

                    string fileName = Path.Combine(_workingDirectory, "netrepl_spectra.ini");
                    CreateFileIfNeeded(_workingDirectory, fileName);

                    CGate.Open($@"ini={fileName};key={_configuration.Key}");

                    _dataConnection.Open();
                    _transactionConnection.Open();
                    UpdateConnectionState(CGConnectionState.Connected);
                }
            }
            catch (CGateException e)
            {
                lock (_startCancelSyncRoot)
                {
                    _configuration.Logger.Error(e, $"Error while starting CGA: error = {CGate.GetErrorDesc(e.ErrCode)}, message = {e.Message}");

                    _cts.Cancel();
                    DisposeCGateObjects();
                    //CGate.Close();
                    UpdateConnectionState(CGConnectionState.Shutdown);
                }
                throw;
            }
            catch (Exception e)
            {
                lock (_startCancelSyncRoot)
                {
                    _configuration.Logger.Error(e, $"Unknown error while starting CGA: {e.Message}");
                    _cts.Cancel();
                    DisposeCGateObjects();
                    UpdateConnectionState(CGConnectionState.Shutdown);
                }
                throw;
            }
        }

        /// <summary>
        ///     Остановить адаптер
        /// </summary>
        public void Stop()
        {
            if (_cts.IsCancellationRequested)
            {
                return;
            }
            
            lock (_syncRoot)
            {
                if (_cts.IsCancellationRequested)
                {
                    return;
                }

                _cts.Cancel();
                _configuration.Logger.Debug("Stopping CGA");
                _dataConnection.Close();
                _transactionConnection.Close();

                if (_marketdataMessagePostingTask != null)
                {
                    _newMarketdataMessageEvent.Set();
                    if (!_marketdataMessagePostingTask.Wait(30000))
                    {
                        _configuration.Logger.Error("Can't stop _marketdataMessagePostingTask");
                    }
                    else
                    {
                        _marketdataMessagePostingTask.Dispose(); 
                    }
                }

                if (_executionMessagePostingTask != null)
                {
                    _newExecutionMessageEvent.Set();
                    if (!_executionMessagePostingTask.Wait(30000))
                    {
                        _configuration.Logger.Error("Can't stop _executionMessagePostingTask");
                    }
                    else
                    {
                        _executionMessagePostingTask.Dispose(); 
                    }
                }

                _configuration.Logger.Debug("Message posting task stopped");

                DisposeCGateObjects();
                _configuration.Logger.Debug("CGA stopped");
                UpdateConnectionState(CGConnectionState.Disconnected);
            }
        }

        /// <summary>
        ///     Отправить сообщение
        /// </summary>
        public void SendMessage(CGateMessage message)
        {
            _ordersPublisher.Publish(message);
        }

        /// <summary>
        ///     Событие получения нового потокового сообщения рыночных данных
        /// </summary>
        public event EventHandler<CGateMessageEventArgs> MarketdataMessageReceived;

        /// <summary>
        ///     Событие получения нового потокового сообщения о заявках
        /// </summary>
        public event EventHandler<CGateMessageEventArgs> ExecutionMessageReceived;

        private void RaiseMarketdataMessageReceived(CGateMessage message)
            => MarketdataMessageReceived?.Invoke(this, new CGateMessageEventArgs(message));

        private void RaiseExecutionMessageReceived(CGateMessage message) 
            => ExecutionMessageReceived?.Invoke(this, new CGateMessageEventArgs(message));


        private CGConnectionState _state;

        /// <summary>
        ///     Событие изменения состояния соединения
        /// </summary>
        public event EventHandler<CGConnectionStateEventArgs> ConnectionStateChanged;

        private void RaiseConnectionStateChanged(CGConnectionState state)
        {
            _state = state;
            ConnectionStateChanged?.Invoke(this, new CGConnectionStateEventArgs(state));
        }

        #endregion

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            Stop();
            //если тут параллельный Stop, но мы ещё не отключились, то тут тупо выйти, как будто так и надо
            if (ConnectionState != CGConnectionState.Disconnected && ConnectionState != CGConnectionState.Shutdown)
            {
                return;

                //throw new InvalidOperationException(
                //    $"CGAdapter can't be disposed while running. Status is {ConnectionState}");
            }

            _dataConnection.Dispose();
            _transactionConnection.Dispose();
        }

        #endregion

        #region Private methods

        private void UpdateConnectionState(CGConnectionState connectionState)
        {
            var i = 0;
            while (true)
            {
                var oldState = _connectionState;
                var s = (CGConnectionState)Interlocked.CompareExchange(ref _connectionState, (int)connectionState, oldState);
                if (s == connectionState)
                {
                    break;
                }

                _configuration.Logger.Warn($"Eternal rush detected: {connectionState}");
                i++;
            }

            if (i > 0)
            {
                _configuration.Logger.Debug($"Connection state is {connectionState}");
                RaiseConnectionStateChanged(connectionState);
            }
        }


        /// <summary>
        ///     Метод ждёт появления новых сообщений в очереди и постит их подписчикам события OnStreamMessageRecieved. 
        ///     Метод запускается в отдельной задаче.
        /// </summary>
        private void PostNewExecutionMessagesToSubscribers()
        {
            Thread.CurrentThread.Name = "CG_POST_EXEC";

            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    if (!_executionMessages.TryDequeue(out var message))
                    {
                        _newExecutionMessageEvent.WaitOne(1000);
                        continue;
                    }

                    RaiseExecutionMessageReceived(message);
                }

            }
            catch (Exception exception)
            {
                _configuration.Logger.Error($"Exception in PostNewExecutionMessagesToSubscribers thread: {exception.Message}, {exception.StackTrace}");
            }
            finally
            {
                _configuration.Logger.Info($"PostNewExecutionMessagesToSubscribers thread stopped");
            }
        }


        /// <summary>
        ///     Метод ждёт появления новых сообщений в очереди и постит их подписчикам события OnStreamMessageRecieved. 
        ///     Метод запускается в отдельной задаче.
        /// </summary>
        private void PostNewDataMessagesToSubscribers()
        {
            Thread.CurrentThread.Name = "CG_POST_MRKD";

            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    if (!_marketdataMessages.TryDequeue(out var message))
                    {
                        _newMarketdataMessageEvent.WaitOne(1000);
                        continue;
                    }

                    RaiseMarketdataMessageReceived(message);
                }

            }
            catch (Exception exception)
            {
                _configuration.Logger.Error($"Exception in PostNewDataMessagesToSubscribers thread: {exception.Message}, {exception.StackTrace}");
            }
            finally
            {
                _configuration.Logger.Debug("PostNewDataMessagesToSubscribers thread stopped");
            }
        }

        /// <summary>
        ///     Диспозит внешние объекты cgate
        /// </summary>
        private void DisposeCGateObjects()
        {
            _configuration.Logger.Debug("CGA objects disposing started");
            _dataConnection.Dispose();
            _transactionConnection.Dispose();

            // NOTE лютый костыль! http://forum.moex.com/viewtopic.asp?t=25336&start=30
            var cancelToken = new CancellationTokenSource();
            var tsk = Task.Factory.StartNew(CGate.Close, cancelToken.Token);
            var result = tsk.Wait(new TimeSpan(0, 0, 0, 5));
            if (!result)
            {
                cancelToken.Cancel();
            }
            _configuration.Logger.Info("CGA objects disposed");
        }

        private static void CreateFileIfNeeded(string workingDirectory, string fileName)
        {
            if (!File.Exists(fileName)) // Нужно создать ini файл
            {
                // Создаём каталог если требуется
                if (!Directory.Exists(workingDirectory))
                {
                    Directory.CreateDirectory(workingDirectory);
                }

                using (var writer = new StreamWriter(fileName, false, Encoding.Default)) 
                { 
                    using (var resStream = typeof(CGP2ClientAdapter).Assembly.GetManifestResourceStream("CGateAdapter.netrepl.ini" /* TODO check this */))
                    {
                        if (resStream == null)
                            throw new InvalidDataException();

                        using (var reader = new StreamReader(resStream))
                        {
                            var sourceText = reader.ReadToEnd();
                            var filledText = string.Format(sourceText, workingDirectory);

                            writer.Write(filledText);
                        }
                    }
                }
            }
        }


        #endregion

        #region ICGateStreamCallback

        private readonly object _streamStatesLock = new object();
        private readonly Dictionary<CGateStream, bool> _streamStates = new Dictionary<CGateStream, bool>();
        private bool _newState, _currentState;

        void ICGateStreamCallback.RegisterStream(CGateStream stream)
        {
            lock (_streamStatesLock)
            {
                _streamStates[stream] = false;
            }
        }

        void ICGateStreamCallback.HandleMessage(CGateStream stream, CGateMessage message)
        {
            lock (_streamStatesLock)
            {
                _currentState = _streamStates.All(_ => _.Value);
                _streamStates[stream] = stream.State == State.Active;
                _newState = _streamStates.All(_ => _.Value);
            }

            if (_newState != _currentState)
                UpdateConnectionState(_newState ? CGConnectionState.Connected : CGConnectionState.Disconnected);
            
            switch (stream.StreamType)
            {
                case CGateStreamType.FutInfo:
                case CGateStreamType.OptInfo:
                    PutMessageToBothEventQueues(message);
                    break;
                case CGateStreamType.FortsMessages:
                case CGateStreamType.OrdLogTrades:
                case CGateStreamType.FutTrades:
                case CGateStreamType.OptTrades:
                case CGateStreamType.Pos:
                case CGateStreamType.Part:
                case CGateStreamType.Vm:
                    PutMessageToTransactionalEventQueue(message);
                    break;
                default:
                    PutMessageToMarketdataEventQueue(message);
                    break;
            }
        }

        private void PutMessageToBothEventQueues(CGateMessage message)
        {
            PutMessageToMarketdataEventQueue(message);
            PutMessageToTransactionalEventQueue(message);
        }

        private void PutMessageToMarketdataEventQueue(CGateMessage message)
        {
            _marketdataMessages.Enqueue(message);
            _newMarketdataMessageEvent.Set();
        }

        private void PutMessageToTransactionalEventQueue(CGateMessage message)
        {
            _executionMessages.Enqueue(message);
            _newExecutionMessageEvent.Set();
        }

        void ICGateConnectionCallback.RegisterConnection(CGateConnection connection)
        { }

        void ICGateConnectionCallback.ConnectionClosed(CGateConnection connection)
        {
            UpdateConnectionState(CGConnectionState.Disconnected);
        }

        void ICGateConnectionCallback.ConnectionError(CGateConnection connection)
        {

        }

        void ICGateConnectionCallback.ConnectionActive(CGateConnection connection)
        {
            UpdateConnectionState(CGConnectionState.Connected);
        }

        void ICGateConnectionCallback.ConnectionOpening(CGateConnection connection)
        {

        }

        #endregion
    }
}
