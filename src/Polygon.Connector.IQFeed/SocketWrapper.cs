using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Microsoft.Win32;
using Polygon.Diagnostics;
using Polygon.Connector.IQFeed.Level1;

namespace Polygon.Connector.IQFeed
{
    internal abstract class SocketWrapper
    {
        private readonly SocketConnectionType socketConnectionType;
        private static readonly char[] _Separator = {','};

        private const int BufferSize = 8096;
        
        private readonly ILog log;
        private readonly IPEndPoint endPoint;
        private readonly AsyncCallback callback;
        private readonly ILockObject socketSyncLock = DeadlockMonitor.Cookie<SocketWrapper>("socketSyncLock");
        private readonly Socket socket;
        private readonly byte[] socketBuffer = new byte[BufferSize];

        // NOTE может быть тут лучше StringBuilder?
        private string incompleteRecord = "";

        #region .ctor

        protected SocketWrapper(IPAddress address, SocketConnectionType socketConnectionType)
        {
            log = LogManager.GetLogger(GetType());
            this.socketConnectionType = socketConnectionType;
            endPoint = new IPEndPoint(address, GetIQFeedPort(socketConnectionType));
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            callback = OnReceive;
        }

        #endregion

        public event ProcessMessageDelegate OnErrorMsg;

        #region Connect/Disconnect

        public bool Connect()
        {
            try
            {
                using (socketSyncLock.Lock())
                {
                    socket.Connect(endPoint);
                }

                OnConnected();
                WaitForData();
            }
            catch (Exception e)
            {
                log.Error().Print(e, $"Failed to connect to IQConnect ({socketConnectionType})");
                return false;
            }

            SendCommand(CommandFormatter.GetConnectCommand(socketConnectionType));
            return true;
        }

        public void Disconnect()
        {
            using (socketSyncLock.Lock())
            {
                SendCommand(CommandFormatter.GetDisconnectCommand());
                
                try
                {
#if NET45
                    socket.Disconnect(false); 
#endif
#if NETSTANDARD1_6
                    socket.Shutdown(SocketShutdown.Both);
#endif
                }
                catch (Exception e)
                {
                    log.Error().Print(e, $"Failed to safely disconnect from IQConnect ({socketConnectionType})");
                }
            }
        }

        #endregion

        #region Messages

        protected void SendCommand(string command)
        {
            log.Trace().PrintFormat("{0} > {1}", socketConnectionType, command);
            var buffer = Encoding.ASCII.GetBytes(command);

            try
            {
                int bytesSent;
                using (socketSyncLock.Lock())
                {
                    bytesSent = socket.Send(buffer, buffer.Length, SocketFlags.None);
                }

                if (bytesSent != buffer.Length)
                {
                    RaiseEvent(OnErrorMsg, new IQMessageArgs($"Error: {command.TrimEnd("\r\n".ToCharArray())} command not sent to IQConnect"));
                }

            }
            catch (Exception e)
            {
                log.Error().Print(e, $"Failed to send command {command} to IQConnect");
                var message = $"Error: {command.TrimEnd("\r\n".ToCharArray())} command not sent to IQConnect, connection lost.";
                RaiseEvent(OnErrorMsg, new IQMessageArgs(message));
            }
        }

        #endregion
#if NETSTANDARD1_6
        private delegate void Receiver(byte[] buffer, int offset, int size, SocketFlags socketFlags);

        private class SocketReceiveAsyncResult : IAsyncResult
        {
            public SocketReceiveAsyncResult(int receivedBytes)
            {
                ReceivedBytes = receivedBytes;
            }
            public object AsyncState { get; }
            public WaitHandle AsyncWaitHandle { get; }
            public bool CompletedSynchronously { get; }
            public bool IsCompleted { get; }
            public int ReceivedBytes { get; }
        }

        private void ReceiveData(byte[] bytes, int offset, int size, SocketFlags socketFlags)
        {
            using (socketSyncLock.Lock())
            {
                var receivedBytes = socket.Receive(socketBuffer, 0, BufferSize, SocketFlags.None);
                callback(new SocketReceiveAsyncResult(receivedBytes));
            }
        }
#endif
        #region Callback

        private void WaitForData()
        {
            using (socketSyncLock.Lock())
            {
#if NET45
                socket.BeginReceive(socketBuffer, 0, BufferSize, SocketFlags.None, callback, null);
#endif
#if NETSTANDARD1_6
                Task.Run(() => ReceiveData(socketBuffer, 0, BufferSize, SocketFlags.None));
#endif
            }
        }
        private void OnReceive(IAsyncResult asyncResult)
        {
            try
            {
                int receivedBytes;
                
                using (socketSyncLock.Lock())
                {
                    if (!socket.Connected)
                    {
                        return;
                    }
#if NET45
                    receivedBytes = socket.EndReceive(asyncResult);
#endif
                }
#if NET45
                var data = Encoding.ASCII.GetString(socketBuffer, 0, receivedBytes);
#endif
#if NETSTANDARD1_6
                var receiveCallResult = asyncResult as SocketReceiveAsyncResult;
                var data = Encoding.ASCII.GetString(socketBuffer, 0, receiveCallResult.ReceivedBytes);
#endif
                data = incompleteRecord + data;
                incompleteRecord = "";

                while (data.Length > 0)
                {
                    var newLinePos = data.IndexOf("\n", StringComparison.Ordinal);
                    if (newLinePos > 0)
                    {
                        var line = data.Substring(0, newLinePos);
                        var lParts = line.Split(_Separator, 2);
                        var type = lParts[0];
                        var message = lParts[1];

                        log.Trace().PrintFormat("{0} < {1},{2}", socketConnectionType, type, message);
                        ProcessMessage(type, message);

                        data = data.Substring(line.Length + 1);
                    }
                    else
                    {
                        incompleteRecord = data;
                        data = "";
                    }
                }

                WaitForData();
            }
            catch (Exception e)
            {
                log.Error().Print(e, $"Connection to IQConnect has been lost ({socketConnectionType})");
                log.Debug().PrintFormat("{0} < {1},{2}", socketConnectionType, L1MessageTypes.System, L1SystemMsg.SERVER_DISCONNECTED);
                ProcessMessage(L1MessageTypes.System, L1SystemMsg.SERVER_DISCONNECTED);
            }
        }

#endregion

        #region Helpers

        private static int GetIQFeedPort(SocketConnectionType sType)
        {
            // TODO проверить, работает ли это под Linux
            var port = 0;
            var key = Registry.CurrentUser.OpenSubKey("Software\\DTN\\IQFeed\\Startup");
            if (key != null)
            {
                var value = "";
                switch (sType)
                {
                    case SocketConnectionType.Level1:
                        // the default port for Level 1 data is 5009.
                        value = key.GetValue("Level1Port", "5009").ToString();
                        break;
                    case SocketConnectionType.Lookup:
                        // the default port for Lookup data is 9100.
                        value = key.GetValue("LookupPort", "9100").ToString();
                        break;
                    case SocketConnectionType.Level2:
                        // the default port for Level 2 data is 9200.
                        value = key.GetValue("Level2Port", "9200").ToString();
                        break;
                    case SocketConnectionType.Admin:
                        // the default port for Admin data is 9300.
                        value = key.GetValue("AdminPort", "9200").ToString();
                        break;
                }

                int.TryParse(value, out port);
            }
            
            return port;
        }

        #endregion

        protected abstract void ProcessMessage(string messageType, string message);

        protected static void RaiseEvent(ProcessMessageDelegate handler, IQMessageArgs args)
        {
            if (handler != null)
            {
                handler(args);
            }
        }

        protected void RaiseError(IQMessageArgs args)
        {
            RaiseEvent(OnErrorMsg, args);
        }

        protected virtual void OnConnected() { }
    }
}

