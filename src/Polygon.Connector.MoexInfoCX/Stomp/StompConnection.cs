using System;
using System.Threading;
using Polygon.Connector.MoexInfoCX.Transport;
using Newtonsoft.Json.Linq;
using Polygon.Connector.MoexInfoCX.Common;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal sealed class StompConnection : IStompConnection, ITransportEventHandler
    {
        #region fields

        private readonly Uri _brokerUri;
        private readonly ITransportFactory _transportFactory;
        private readonly IWireFormat _wireFormat;
        private readonly ILogger _logger;
        private ITransport _transport;

        private int _state = (int)StompConnectionState.Disconnected;

        #endregion

        #region .ctor

        public StompConnection(Uri brokerUri, ITransportFactory transportFactory, IWireFormat wireFormat, ILogger logger)
        {
            _brokerUri = brokerUri;
            _transportFactory = transportFactory;
            _wireFormat = wireFormat;
            _logger = logger;
        }

        #endregion

        #region IStompConnection

        public StompConnectionState State
        {
            get { return (StompConnectionState)Interlocked.CompareExchange(ref _state, 0, 0); }
            private set
            {
                var oldValue = _state;
                while (true)
                {
                    var actualOldValue = Interlocked.CompareExchange(ref _state, value: (int)value, comparand: oldValue);
                    if (actualOldValue == oldValue)
                    {
                        if (oldValue != (int)value)
                        {
                            OnStateChanged(value);
                        }
                        return;
                    }

                    if (actualOldValue == (int)value)
                    {
                        return;
                    }

                    oldValue = actualOldValue;
                }
            }
        }

        public event EventHandler<StompConnectionStateChangedEventArgs> StateChanged;

        private void OnStateChanged(StompConnectionState state)
        {
            _logger.Debug($"Connection state: {state}");
            StateChanged?.Invoke(this, new StompConnectionStateChangedEventArgs(state));
        }

        public event EventHandler<StompMessageReceivedEventArgs> MessageReceived;

        private void OnMessageReceived(IStompFrame frame)
            => MessageReceived?.Invoke(this, new StompMessageReceivedEventArgs(frame));

        public void Send(IStompFrame frame)
        {
            try
            {
                if (State != StompConnectionState.Connected)
                {
                    throw new StompConnectionException("STOMP connection is off-line");
                }

                var rawMessage = _wireFormat.WriteFrame(frame);
                _logger.ClientFrame(rawMessage);

                _transport.SendMessage(rawMessage);
            }
            catch (StompWireFormatException e)
            {
                _logger.Error("Protocol error while sending", e);
                throw;
            }
            catch (Exception e)
            {
                _logger.Error("Error while sending", e);
                throw;
            }
        }

        public void Connect()
        {
            try
            {
                var state = (StompConnectionState)Interlocked.CompareExchange(
                    ref _state,
                    value: (int)StompConnectionState.Connecting,
                    comparand: (int)StompConnectionState.Disconnected
                );

                switch (state)
                {
                    case StompConnectionState.Disconnected:
                        // Соединение было разорвано, сейчас состояние - Connecting
                        // Текущий поток должен выполнить подключение
                        OnStateChanged(StompConnectionState.Connecting);
                        _transport = _transportFactory.Create(_brokerUri, this);
                        _transport.Connect();
                        break;

                    case StompConnectionState.Connecting:
                        // Соединение устанавливается, следует дождаться изменения состояния
                        while (State == StompConnectionState.Connecting) { }
                        break;

                    case StompConnectionState.Disconnecting:
                        throw new StompConnectionException("Unable to connect while disconnecting");

                    default:
                        return;
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error while connecting", e);
                throw;
            }
        }

        public void Disconnect()
        {
            Disconnect(throwOnError: true);
        }

        private void Disconnect(bool throwOnError)
        {
            try
            {
                var state = (StompConnectionState)Interlocked.CompareExchange(
                    ref _state,
                    value: (int)StompConnectionState.Disconnecting,
                    comparand: (int)StompConnectionState.Connected
                );

                switch (state)
                {
                    case StompConnectionState.Connected:
                        // Соединение было установлено, сейчас состояние - Disconnecting
                        // Текущий поток должен выполнить отключение
                        OnStateChanged(StompConnectionState.Disconnecting);

                        try
                        {
                            _transport?.Dispose();
                        }
                        catch (Exception)
                        {
                            if (throwOnError)
                            {
                                throw;
                            }
                        }
                        finally
                        {
                            _transport = null;
                            State = StompConnectionState.Disconnected;
                        }
                        break;

                    case StompConnectionState.Disconnecting:
                        // Соединение разрывается, следует дождаться изменения состояния
                        while (State == StompConnectionState.Disconnecting) { }
                        break;

                    case StompConnectionState.Connecting:
                        if (throwOnError)
                        {
                            throw new StompConnectionException("Unable to disconnect while connecting");
                        }

                        return;

                    default:
                        return;
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error while disconnecting", e);
                throw;
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Disconnect(throwOnError: false);
        }

        #endregion

        #region ITransportEventHandler

        void ITransportEventHandler.OnConnected()
        {
            State = StompConnectionState.Connected;
        }

        void ITransportEventHandler.OnDisconnected()
        {
            State = StompConnectionState.Disconnected;
        }

        void ITransportEventHandler.OnMessageReceived(string rawMessage)
        {
            try
            {
                Session = JObject.Parse(rawMessage)["X-CspHub-Session"].ToString();
            }
            catch (Exception e)
            {
                _logger.Error("Protocol error", e);
            }
        }

        void ITransportEventHandler.OnDataReceived(string rawMessage)
        {
            _logger.ServerFrame(rawMessage);
            IStompFrame frame;
            try
            {
                frame = _wireFormat.ReadFrame(rawMessage);
            }
            catch (StompWireFormatException e)
            {
                _logger.Error("Protocol error", e);
                return;
            }
            catch (Exception e)
            {
                _logger.Error("Failed to decode incoming message", e);
                return;
            }

            OnMessageReceived(frame);
        }

        void ITransportEventHandler.OnError(string message, Exception exception)
        {
            _logger.Error(message, exception);
            Disconnect(throwOnError: false);
        }

        #endregion

        public string Session { get; private set; }
    }
}