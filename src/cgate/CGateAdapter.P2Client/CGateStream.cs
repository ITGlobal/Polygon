using System;
using System.IO;
using System.Linq;
using CGateAdapter.Messages;
using ru.micexrts.cgate;
using ru.micexrts.cgate.message;

namespace CGateAdapter
{
    /// <summary>
    /// Базовый класс потока данных
    /// </summary>
    internal sealed class CGateStream : CGateSubconnection
    {
        #region Fields

        /// <summary>
        ///     Признак того, что этот поток является потоком ответов на сообщения, отправленные в паблишер
        /// </summary>
        private readonly bool _isPublisherReplyStream;
        private readonly ICGateStreamCallback _callback;
        private readonly ICGateLogger _logger;

        /// <summary>
        /// Признак того, что поток сконфигурирован правильно и его можно открывать. Если false, то попытки открыть поток будут игнорироваться
        /// </summary>
        private readonly bool _configuredWell;

        /// <summary>
        /// Режим открытия потока, по умолчанию snapshot+online
        /// </summary>
        private readonly string _mode = "snapshot+online";

        /// <summary>
        /// Использовать ли ReplState при переподключении после разрыва потока
        /// </summary>
        private readonly bool _useReplStateWhenReconnect;

        private Listener _listener;

        /// <summary>
        /// Точка восстановление потока после разрыва
        /// </summary>
        private string ReplState { get; set; }

        /// <summary>
        /// Потоки, от которых зависит текущий поток. При подключении нужно сначала дождаться их подключения, и только потом подключаться самому
        /// </summary>
        private readonly CGateStream[] _dependOnStreams;

        #endregion

        #region Protected properties

        private string RefListener { get; set; }

        /// <summary>
        /// Признак того, что поток транзакционный, то есть в нем приходит OSCMы и сделки
        /// </summary>
        public bool Transactional { get; set; }

        /// <summary>
        /// Конвертер потоковых сообщений StreamDataMessage в типизированные сообщения адаптера. У каждого потока свой конвертер. См. наследники Stream
        /// </summary>
        private IMessageConverter Converter { get; set; }

        private string NameForLogging
        {
            get { return string.IsNullOrEmpty(RefListener) ? Name : RefListener; }
        }

        protected override string ConnectionString
        {
            get
            {
                if (_isPublisherReplyStream)
                    return $"p2mqreply://;ref={RefListener}";

                return $"p2repl://{Name};scheme=|FILE|{SchemeFileName}|{SchemeName};";
            }
        }

        #endregion

        #region Public properties

        public uint LifeNumber { get; set; }

        public override State State
        {
            get
            {
                if (_listener == null || _disposed)
                    return State.Error;

                return _listener.State;
            }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Конструктор для создания потока репликации общей информации
        /// </summary>
        /// <param name="name">Название потока</param>
        /// <param name="streamType">Тип потока</param>
        /// <param name="schemeFileName">Имя файла схемы (ini)</param>
        /// <param name="schemeName">Имя схемы</param>
        /// <param name="converter">Конвертер сообщений</param>
        /// <param name="callback">Интерфейс который нужно дёргать для передачи ему полученных сообщений</param>
        /// <param name="logger">Логгер</param>
        /// <param name="dependOnStreams">Потоки, от которых зависит данный поток. Сначала нужно дождаться их подключения, потом подключаться самому</param>
        /// <param name="mode">Режим, в котором должен работать поток</param>
        /// <param name="useReplStateWhenReconnect">Использовать ли replstate при переподключении или стартовать с нуля</param>
        /// <param name="transactional">Признак того, что поток транзакционный, то есть в нем приходит OSCMы и сделки</param>
        public CGateStream(
            string name, 
            CGateStreamType streamType, 
            string schemeFileName, 
            string schemeName,
            IMessageConverter converter, 
            ICGateStreamCallback callback, 
            ICGateLogger logger, 
            CGateStream[] dependOnStreams = null, 
            string mode = "", 
            bool useReplStateWhenReconnect = true,
            bool transactional = false)
        {
            _callback = callback;
            _logger = logger;
            
            Name = name;
            StreamType = streamType;
            SchemeFileName = schemeFileName;
            SchemeName = schemeName;
            Converter = converter;
            Transactional = transactional;
            _dependOnStreams = dependOnStreams;

            if (!string.IsNullOrEmpty(mode))
            {
                _mode = mode;
            }

            _useReplStateWhenReconnect = useReplStateWhenReconnect;

            if (!File.Exists(schemeFileName))
            {
                _logger.Error($"File {schemeFileName} doesn't exist, stream {Name} won't be started");
                _configuredWell = false;
                return;
            }

            _callback.RegisterStream(this);
            _configuredWell = true;
        }

        /// <summary>
        ///     Конструктор для создания потока сообщений, привязанного к паблишеру
        /// </summary>
        public CGateStream(
            string refListener, 
            CGateStreamType streamType, 
            IMessageConverter converter, 
            ICGateStreamCallback callback, 
            ICGateLogger logger, 
            string mode = "", 
            bool transactional = true)
        {
            _callback = callback;
            _logger = logger;
            Transactional = transactional;
            _isPublisherReplyStream = true;
            RefListener = refListener;
            Converter = converter;
            StreamType = streamType;

            if (!string.IsNullOrEmpty(mode))
            {
                _mode = mode;
            }

            _callback.RegisterStream(this);
            _configuredWell = true;
        }

        #endregion

        #region Protected methods

        private int Handler(Connection conn, Listener listener, Message msg)
        {
            try
            {
                if (!_configuredWell)
                {
                    return -1;
                }

                if (Name == "FORTS_FUTTRADE_REPL" && SchemeFileName.Contains("fut_trades.ini"))
                {
                    _logger.Info($"#FTR: {msg.GetType()}");
                }

                if (Name == "FORTS_OPTTRADE_REPL" && SchemeFileName.Contains("opt_trades.ini"))
                {
                    _logger.Info($"#OTR: {msg.GetType()}");
                }

                // если получены потоковые данные, то процессим их специфическим для каждого потока конвертером
                if (msg.Type == MessageType.MsgStreamData)
                {
                    var message = Converter.ConvertToAdapterMessage((StreamDataMessage)msg);
                    message.StreamRegime = Regime;
                    message.StreamName = Name;
                    _callback.HandleMessage(this, message);
                    _logger.Trace("{0}", message);
                }
                else if (msg.Type == MessageType.MsgData)
                {
                    var dm = (DataMessage)msg;
                    var message = Converter.ConvertToAdapterMessage(dm);
                    message.StreamRegime = Regime;
                    message.StreamName = Name;
                    message.UserId = dm.UserId;
                    _callback.HandleMessage(this, message);
                    _logger.Debug($"UserId={((DataMessage)msg).UserId}; Message={message}");
                }

                // если это общее для всех потоков сообщение, то процессим его в базовом классе
                else
                {
                    ProcessMessagesCommonForAllStreams(msg);
                }

                return 0;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{NameForLogging}: Exception: {exception.Message}");
                throw;
            }
        }

        /// <summary>
        /// Метод для обработки сообщений, общих для всех потоков (открытие, закрытие и т.п.)
        /// </summary>
        /// <returns>true если сообщение обработано, false - если нет и вызывающий метод должен обработать его самостоятельно</returns>
        private bool ProcessMessagesCommonForAllStreams(Message msg)
        {
            if (!_configuredWell)
                return true;

            switch (msg.Type)
            {
                case MessageType.MsgP2ReplOnline:
                    {
                        Regime = StreamRegime.ONLINE;
                        _logger.Info($"{NameForLogging}: Stream is ONLINE");
                        _callback.HandleMessage(this, new StreamStateChange(StreamType, Name, Regime));
                        RiseStateMightBeenChanged(Name, Regime);
                        break;
                    }
                case MessageType.MsgTnBegin:
                    {
                        _callback.HandleMessage(this, new CGateDataBegin(Name, Regime));
                        break;
                    }
                case MessageType.MsgTnCommit:
                    {
                        _callback.HandleMessage(this, new CGateDataEnd(Name, Regime));
                        break;
                    }
                case MessageType.MsgOpen:
                    {
                        Regime = StreamRegime.SNAPSHOT;
                        _logger.Info($"{NameForLogging}: Stream is OPEN");
                        _callback.HandleMessage(this, new StreamStateChange(StreamType, Name, Regime));
                        break;
                    }
                case MessageType.MsgClose:
                    {
                        Regime = StreamRegime.CLOSED;
                        _logger.Warn($"{NameForLogging}: Stream is CLOSED");
                        _callback.HandleMessage(this, new StreamStateChange(StreamType, Name, Regime));
                        RiseStateMightBeenChanged(Name, Regime);
                        break;
                    }
                case MessageType.MsgP2ReplLifeNum:
                    {
                        LifeNumber = ((P2ReplLifeNumMessage)msg).LifeNumber;

                        _logger.Debug($"{NameForLogging}: Life number changed to: {LifeNumber}");
                        break;
                    }
                case MessageType.MsgP2ReplReplState:
                    {
                        ReplState = ((P2ReplStateMessage)msg).ReplState;
                        _logger.Debug($"{NameForLogging}: Message {ReplState}");
                        break;
                    }
                case MessageType.MsgP2ReplClearDeleted:
                    {
                        var message = (P2ReplClearDeletedMessage)msg;
                        _callback.HandleMessage(this, new CGateClearTableMessage(message.TableIdx, message.TableRev, StreamType, Name, Regime));
                        break;
                    }
                default:
                    {
                        return false;
                    }

            }
            return true;
        }

        #endregion

        #region Public methods

        public override void SetConnection(Connection connection)
        {
            _listener = new Listener(connection, ConnectionString);
            _listener.Handler += Handler;
        }

        /// <summary>
        /// Открыть поток
        /// </summary>
        public override void Open()
        {
            if (_dependOnStreams != null)
            {
                var dependOnStreamsConnected = _dependOnStreams.Aggregate(true, (current, stream) => current & stream.Regime == StreamRegime.ONLINE);
                if (!dependOnStreamsConnected)
                {
                    _logger.Debug($"Stream {NameForLogging} depends on {_dependOnStreams.Length} streams, can't be open now, waiting for those {_dependOnStreams.Length} streams to become ONLINE");
                    return;
                }
                _logger.Debug($"Stream {NameForLogging} depends on {_dependOnStreams.Length} streams, they all are ONLINE, this stream now can be opened");
            }

            _logger.Debug($"Trying to open {NameForLogging} stream, current state = {State}, regime = {Regime}");
            if (!_configuredWell)
            {
                return;
            }

            var settings = string.IsNullOrEmpty(ReplState) || !_useReplStateWhenReconnect
                ? ""
                : $"replstate={ReplState};";

            if (!string.IsNullOrEmpty(_mode))
            {
                settings = $"mode={_mode};{settings}";
            }

            try
            {
                _listener.Open(settings);
            }
            catch (CGateException exception)
            {
                _logger.Error(exception, $"{NameForLogging}: CGateException: ErrCode={exception.ErrCode}, Message={exception.Message}");
                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{NameForLogging}: Exception: {exception.Message}");
                throw;
            }
        }

        /// <summary>
        /// Закрыть поток
        /// </summary>
        public override void Close()
        {
            if (!_configuredWell)
            {
                return;
            }

            ReplState = "";
            try
            {
                if (!_disposed && _listener != null && _listener.State != State.Closed)
                {
                    _listener.Close();
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{NameForLogging}: Error while closing listener. {exception.Message}");
                throw;
            }
        }

        /// <summary>
        /// Открывает закрытый поток. Если поток в состоянии ошибки, закрывает его.
        /// </summary>
        public override void TryOpen()
        {
            if (!_configuredWell)
            {
                return;
            }

            if (State == State.Closed)
            {
                Open();
            }
            else if (State == State.Error)
            {
                Close();
            }
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            if (!_configuredWell)
            {
                return;
            }

            if (_listener != null)
            {
                _disposed = true;
                _listener.Dispose();
            }
        }

        #endregion
    }
}
