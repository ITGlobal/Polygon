using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace Polygon.Connector.MoexInfoCX.Transport
{
    internal sealed class WebsocketTransport : ITransport
    {
        private const int MAX_WAIT_SHUTDOWN = 10 * 1000 /* 10 sec */;

        private readonly Uri _brokerUrl;
        private readonly ITransportEventHandler _handler;
        private readonly WebSocket _socket;

        private readonly ManualResetEventSlim _wsOpened = new ManualResetEventSlim();
        private readonly ManualResetEventSlim _wsClosed = new ManualResetEventSlim();
        private readonly ManualResetEventSlim _wsError = new ManualResetEventSlim();

        private Exception _wsErrorException;

        readonly List<string> packageParts = new List<string>();
        private int seq = 0;

        public WebsocketTransport(Uri brokerUrl, ITransportEventHandler handler, WebsocketTransportSettings settings)
        {
            _brokerUrl = brokerUrl;
            _handler = handler;
            _socket = new WebSocket(brokerUrl.ToString(),
                receiveBufferSize: settings.ReceiveBufferSize,
                customHeaderItems: settings.HttpHeaders
                    .Select(_ => new KeyValuePair<string, string>(_.Key, _.Value.ToString()))
                    .ToList()
            );
            _socket.Security.Certificates = settings.SslCertificates;

            _socket.Opened += OnWsOpened;
            _socket.Closed += OnWsClosed;
            _socket.Error += OnWsError;
            _socket.MessageReceived += OnWsMessageReceived;
            _socket.DataReceived += DataReceived;
        }



        public void Connect()
        {
            _wsOpened.Reset();
            _wsError.Reset();
            _wsClosed.Reset();

            _socket.Open();

            var i = WaitHandle.WaitAny(new[] { _wsOpened.WaitHandle, _wsError.WaitHandle, _wsClosed.WaitHandle });
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    throw new TransportException($"Error connecting to {_brokerUrl}.", _wsErrorException);
                default:
                    throw new TransportException($"Connection to {_brokerUrl} has been forcibly closed.");
            }
        }

        public void SendMessage(string message)
        {
            _socket.Send(message);
        }

        public void Dispose()
        {
            _socket.Close();
            _wsClosed.Wait(MAX_WAIT_SHUTDOWN);
            _socket.Dispose();
        }

        private void OnWsOpened(object sender, EventArgs e)
        {
            _handler.OnConnected();
            _wsOpened.Set();
        }

        private void OnWsClosed(object sender, EventArgs e)
        {
            _wsClosed.Set();
            _handler.OnDisconnected();
        }

        private void OnWsError(object sender, ErrorEventArgs e)
        {
            _wsErrorException = e.Exception;
            _wsError.Set();

            _handler.OnError("Websocket error", e.Exception);
        }

        private void OnWsMessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            _handler.OnMessageReceived(e.Message);
        }


        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            const int h1Len = 4;

            var h1 = Encoding.ASCII.GetString(e.Data, 0, h1Len);

            var h2Len = Int32.Parse(h1, NumberStyles.HexNumber);

            var h2 = Encoding.ASCII.GetString(e.Data, h1Len, h2Len);

            const int h2SubLen = 8;
            var numberStr = h2.Substring(0, h2SubLen);
            var bodyLenStr = h2.Substring(h2SubLen, h2SubLen);

            var number = Int32.Parse(numberStr, NumberStyles.HexNumber);
            var bodyLen = Int32.Parse(bodyLenStr, NumberStyles.HexNumber);
            
            if (number != seq + 1)
            {
                throw new ArgumentException($"Invalid seqnum: received={number}, expected={seq}");
            }

            seq = number;


            if (e.Data.Length != h1Len + h2Len + bodyLen)
            {
                throw new ArgumentException();
            }

            int bodyEnd;

            int start = h1Len + h2Len;

            while ((bodyEnd = Array.FindIndex(e.Data, start, _ => _ == 0)) != -1)
            {
                bodyEnd++;

                var part = Encoding.UTF8.GetString(e.Data, start, bodyEnd - start);

                string package;

                if (!packageParts.Any())
                {
                    package = part;
                }
                else
                {
                    var sb = new StringBuilder();

                    packageParts.ForEach(_ => sb.Append(_));
                    sb.Append(part);

                    packageParts.Clear();

                    package = sb.ToString();
                }

                _handler.OnDataReceived(package);

                start = bodyEnd;
            }

            if (start != e.Data.Length)
            {
                packageParts.Add(Encoding.UTF8.GetString(e.Data, start, e.Data.Length - start));
            }
        }
    }
}