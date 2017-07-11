using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ru.micexrts.cgate;

namespace CGateAdapter
{
    /// <summary>
    /// Обёртка над подключением
    /// </summary>
    [DebuggerDisplay("CGateConnection({_name.ToUpper()})")]
    internal sealed class CGateConnection : IDisposable
    {
        #region Fields

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly string _name;
        private readonly ICGateLogger _logger;

        /// <summary>
        /// Таймаут перед следующим переподключением в случае разрыва соединения. Увеличивается с увеличением числа неудачных попыток
        /// </summary>
        private int _reconnectTimeout = 5000;

        private Connection _connection;

        private readonly string _connectionString;

        private Task _connectionPollingTask;

        // ReSharper disable once InconsistentNaming
        private const int STATE_OFF = 0;
        // ReSharper disable once InconsistentNaming
        private const int STATE_ON = 1;

        private int _state;

        private SpinLock _spinLock = new SpinLock();

        /// <summary>
        /// Количество циклов полинга, с момента последней проверки состояний потоков
        /// </summary>
        private int _cyclesBetweenStreamStateCheck;

        /// <summary>
        /// Количество циклов полинга подключения, после которого мы делаем проверку состояния всех потоков
        /// </summary>
        private const int MaxCyclesBetweenStreamStateCheck = 1000;

        /// <summary>
        /// Потоки данных
        /// </summary>
        private readonly List<CGateSubconnection> _streamsAndPublishers1, _streamsAndPublishers2;

        private readonly ICGateConnectionCallback _callback;
        
        #endregion

        #region ctor

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя соединения</param>
        /// <param name="ip">IP адрес, по котором удоступен роутер plaza2</param>
        /// <param name="port">Порт, по котором удоступен роутер plaza2</param>
        /// <param name="credential">Логин и пароль</param>
        /// <param name="secondSubconnections">Потоки и паблишеры</param>
        /// <param name="firstSubconnections">Потоки и паблишеры</param>
        /// <param name="logger">Логгер</param>
        /// <param name="callback"></param>
        public CGateConnection(
            string name,
            IPAddress ip,
            ushort port,
            NetworkCredential credential,
            IEnumerable<CGateSubconnection> firstSubconnections,
            IEnumerable<CGateSubconnection> secondSubconnections,
            ICGateLogger logger,
            ICGateConnectionCallback callback)
        {
            _name = name;
            _logger = logger;
            _callback = callback;

            if(!string.IsNullOrEmpty(credential.UserName) && !string.IsNullOrEmpty(credential.Password))
            {
                _connectionString = $"p2tcp://{ip}:{port};app_name={credential.UserName};local_pass={credential.Password};";
            }
            else
            {
                _connectionString = $"p2tcp://{ip}:{port};";
            }

            _streamsAndPublishers1 = new List<CGateSubconnection>(firstSubconnections.Where(_ => _ != null));
            _streamsAndPublishers2 = secondSubconnections != null
                ? new List<CGateSubconnection>(secondSubconnections.Where(_ => _ != null))
                : null;

            _streamsAndPublishers1.ForEach(_ => _.StateMightBeenChanged += StateMightBeenChangedHandler);

            _logger.Debug($"{_name} connection string is: {_connectionString}");

            callback.RegisterConnection(this);
        }

        /// <summary>
        /// Обработка события возможного изменения состояния потока
        /// </summary>
        private void StateMightBeenChangedHandler(string streamName, StreamRegime regime)
        {
            _logger.Info($"Stream {streamName} state changed to {regime}");
            var lockTaken = false;
            try
            {
                _logger.Debug("Trying to lock spinLock");
                _spinLock.Enter(ref lockTaken);
                _logger.Debug("Changing needToCheckSubconnectionsState to true");
            }
            finally
            {
                if (lockTaken)
                {
                    _logger.Debug("SpinLock was taken and");
                    _spinLock.Exit();
                }
                else
                {
                    _logger.Debug("Spin lock wasn't taken and needToCheckSubconnectionsState != true");
                }
            }
        }

        #endregion

        #region Public methods

        public void Open()
        {
            try
            {
                _connection = new Connection(_connectionString);

                foreach (var stream in _streamsAndPublishers1)
                {
                    stream.SetConnection(_connection);
                }

                if (_streamsAndPublishers2 != null)
                {
                    foreach (var stream in _streamsAndPublishers2)
                    {
                        stream.SetConnection(_connection);
                    }
                }

                _logger.Info($"{_name} connection: opening...");
                _connection.Open("");

                Interlocked.Exchange(ref _state, STATE_ON);
                _connectionPollingTask = Task.Factory.StartNew(ProcessConnection, TaskCreationOptions.LongRunning);
            }
            catch (CGateException e)
            {
                _logger.Error(e, $"Failed to open connection: [E_{e.ErrCode:G}] {e.Message}");
                _connection = null;
                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Failed to open connection: {e.Message}");
                _connection = null;
                throw;
            }
        }

        public void Close()
        {
            Interlocked.Exchange(ref _state, STATE_OFF);

            _connectionPollingTask?.Wait();

            foreach (var stream in _streamsAndPublishers1)
            {
                stream.Close();
            }

            if (_streamsAndPublishers2 != null)
            {
                foreach (var stream in _streamsAndPublishers2)
                {
                    stream.Close();
                }
            }

            _connection.Close();

            _logger.Info($"{_name} connection stopped");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Полинг конекшена на предмет новых сообщений и изменения состояния
        /// </summary>
        private void ProcessConnection()
        {
            Thread.CurrentThread.Name = $"{_name.ToUpperInvariant()}";

            while (true)
            {
                var s = Interlocked.CompareExchange(ref _state, STATE_OFF, STATE_OFF);
                if (s == STATE_OFF)
                {
                    _logger.Warn($"Exit ProcessConnection, state is {_state}");
                    break;
                }

                s = Interlocked.CompareExchange(ref _state, STATE_OFF, STATE_OFF);
                if (s == STATE_OFF)
                {
                    _logger.Warn($"Exit ProcessConnection, state is {_state}");
                    break;
                }

                try
                {
                    var state = _connection.State;
                    LogStateChange();
                    if (state == State.Error)
                    {
                        _callback.ConnectionError(this);

                        _logger.Info($"{_name}: Closing error stated connection...");
                        _connection.Close();
                    }
                    else if (state == State.Closed)
                    {
                        _callback.ConnectionClosed(this);

                        _logger.Info($"{_name}: opening...");
                        _connection.Open("");
                    }
                    else if (state == State.Opening)
                    {
                        _callback.ConnectionOpening(this);

                        var result = _connection.Process(1);
                        if (result != ErrorCode.Ok && result != ErrorCode.TimeOut)
                        {
                            _logger.Error($"{_name}: request failed: {CGate.GetErrorDesc(result)}");
                        }
                    }
                    else if (state == State.Active)
                    {
                        _callback.ConnectionActive(this);

                        var result = _connection.Process(1);

                        if (result == ErrorCode.Ok)
                        {
                            while ((result = _connection.Process(0)) == ErrorCode.Ok)
                            {
                            }

                            if (result != ErrorCode.TimeOut)
                            {
                                _logger.Error($"{_name}: request failed: {CGate.GetErrorDesc(result)}");
                            }
                        }
                        else if (result != ErrorCode.TimeOut)
                        {
                            _logger.Error($"{_name}: request failed: {CGate.GetErrorDesc(result)}");
                        }

                        if (++_cyclesBetweenStreamStateCheck >= MaxCyclesBetweenStreamStateCheck)
                        {
                            _cyclesBetweenStreamStateCheck = 0;
                            //_logger.Info("Check streams states and reopen if needed");
                            foreach (var subconnection in _streamsAndPublishers1)
                            {
                                subconnection.TryOpen();
                                subconnection.TryOpen();
                            }

                            if (_streamsAndPublishers2 != null)
                            {
                                if (_streamsAndPublishers1.All(_ => _.Regime == StreamRegime.ONLINE))
                                {
                                    foreach (var subconnection in _streamsAndPublishers2)
                                    {
                                        subconnection.TryOpen();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (CGateException e)
                {
                    _logger.Error(
                        e,
                        $"{_name}: Error while polling, CGateException. Error = {CGate.GetErrorDesc(e.ErrCode)}, message = {e.Message}");
                    Thread.Sleep(_reconnectTimeout);
                    _reconnectTimeout += 1000; // постепенно увеличиваем таймаут между попытками переподключиться
                }
                catch (Exception e)
                {
                    _logger.Error(
                        e,
                        $"{_name}: Error while polling, Exception. Error = {e.Message}");
                    Thread.Sleep(_reconnectTimeout);
                    _reconnectTimeout += 1000; // постепенно увеличиваем таймаут между попытками переподключиться
                }
            }
        }

        private State _prevState = STATE_OFF;

        /// <summary>
        /// Залогировать изменение статуса соединения
        /// </summary>
        private void LogStateChange()
        {
            if (_connection.State != _prevState)
            {
                _logger.Debug($"{_name}: State change: {_prevState} => {_connection.State}");
                _prevState = _connection.State;
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Interlocked.Exchange(ref _state, STATE_OFF);
            foreach (var sps in new[] { _streamsAndPublishers1, _streamsAndPublishers2 })
            {
                if (sps != null && sps.Any())
                {
                    foreach (var stream in sps)
                    {
                        stream.Dispose();
                    }
                }
            }

            _connection?.Dispose();
        }

        #endregion
    }
}
