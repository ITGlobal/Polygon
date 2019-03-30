using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polygon.Diagnostics;
using Polygon.Connector.QUIKLua.Adapter.Messages;
using Polygon.Messages;

namespace Polygon.Connector.QUIKLua.Adapter
{
    /// <summary>
    ///     Коннектор к квику 
    /// </summary>
    internal sealed class QLAdapter : ISubscriptionTester<InstrumentData>, IInstrumentConverterContext<InstrumentData>
    {
        #region Fields

        internal static readonly ILog Log = LogManager.GetLogger<QLAdapter>();
        private const int ReceiveTimeout = 10, SendTimeout = 10;

        private readonly IPAddress ip;
        private readonly int port;
        private readonly bool receiveMarketdata;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ConcurrentQueue<string> incomingMessages = new ConcurrentQueue<string>();
        private readonly ConcurrentQueue<QLMessage> outgoingMessages = new ConcurrentQueue<QLMessage>();
        
        private TcpClient tcpCient;
        private NetworkStream readerStream;
        private StreamReader reader;
        private NetworkStream writerStream;
        private StreamWriter writer;
        private CancellationTokenSource cancellationTokenSource;
        private Task receivingTask, deserializingTask, sendingTask;

        private readonly ManualResetEvent disconnectedEvent = new ManualResetEvent(true);

        /// <summary>
        /// Семафор для ожидания подключения. Т.к. потока для работы с сокетом 2 - отправка и получение, то эвентом воспользоваться не удаётся
        /// </summary>
        private readonly Semaphore connectedSemaphore = new Semaphore(0, 2);

        private readonly string heartBeatJson = JsonConvert.SerializeObject(new QLHeartbeat());
        private DateTime lastHearbeatTime = DateTime.Now;

        #endregion

        #region .ctor

        public QLAdapter(string ipAddress, int port, IDateTimeProvider dateTimeProvider, bool receiveMarketdata, InstrumentConverter<InstrumentData> instrumentConverter)
        {
            this.dateTimeProvider = dateTimeProvider;
            ip = IPAddress.Parse(!string.IsNullOrEmpty(ipAddress) ? ipAddress : "127.0.0.1");
            this.port = port == 0 ? 1248 : port;
            this.receiveMarketdata = receiveMarketdata;
            InstrumentConverter = instrumentConverter;
        }

        #endregion

        #region Properties

        public InstrumentConverter<InstrumentData> InstrumentConverter { get; }

        #endregion

        #region Public methods

        public async void Start()
        {
            try
            {
                cancellationTokenSource = new CancellationTokenSource();

                CreateTcpClient();

                receivingTask = StartReceiving();
                deserializingTask = StartDeserializingReceivedMessages();
                sendingTask = StartSending();

                SendMessage(new QLQuikSideSettings(receiveMarketdata));

                await Connect();
            }
            catch (Exception e)
            {
                Log.Error().Print(e, "Failed to start adapter");
            }
        }

        public void Stop()
        {

#if NET45
            tcpCient.Close(); 
#endif
#if NETSTANDARD1_6
            tcpCient.Dispose();
#endif
            cancellationTokenSource.Cancel();

            if (receivingTask != null)
            {
                receivingTask.Wait();
            }

            if (deserializingTask != null)
            {
                deserializingTask.Wait();
            }

            if (sendingTask != null)
            {
                sendingTask.Wait();
            }
        }

        /// <summary>
        ///     Отправить сообщение в квик
        /// </summary>
        public void SendMessage(QLMessage message)
        {
            outgoingMessages.Enqueue(message);
        }

        public async Task<string> ResolveSymbolAsync(Instrument instrument)
        {
            var data = await InstrumentConverter.ResolveInstrumentAsync(this, instrument);
            return data?.Symbol;
        }

        public async Task<Instrument> ResolveInstrumentAsync(string symbol)
        {
            var instrument = await InstrumentConverter.ResolveSymbolAsync(this, symbol);
            return instrument;
        }

        #endregion

        #region Events

        public event EventHandler<QLMessageEventArgs> MessageReceived;

        private void OnMessageReceived(QLMessage message)
        {
            try
            {
                var handler = MessageReceived;
                handler?.Invoke(this, new QLMessageEventArgs(message));
            }
            catch (Exception ex)
            {
                Log.Error().Print(ex, $"Error QL messages handling: {ex.Message}");
            }
        }

        public ConnectionStatus ConnectionStatus { get; private set; } = ConnectionStatus.Undefined;

        private void OnConnectionStatusChanged()
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Вызывается при изменении состояния соединения
        /// </summary>
        public event EventHandler ConnectionStatusChanged;

        #endregion

        #region Private methods

        private void RecreateTcpClient()
        {
            try
            {
#if NET45
                tcpCient.Close();
#endif
#if NETSTANDARD1_6
                tcpCient.Dispose();
#endif
            }
            catch (Exception ex)
            {
                Log.Error().PrintFormat(ex, "Error closing socket: {0}", ex.Message);
            }

            tcpCient = null;
            CreateTcpClient();
        }

        private void CreateTcpClient()
        {
            if (tcpCient == null)
            {
                Log.Debug().Print("(Re)creating TcpClient", LogFields.IP(ip), LogFields.Port(port));
                tcpCient = new TcpClient
                {
                    ExclusiveAddressUse = true,
                    NoDelay = true,
                    ReceiveBufferSize = 1000000
                };
            }
        }

        /// <summary>
        /// Подключение к сокету квика
        /// </summary>
        /// <returns></returns>
        private Task Connect()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.Name = "QL_CONN";

                while (!cancellationTokenSource.Token.WaitHandle.WaitOne(1000))
                {
                    try
                    {
                        CreateTcpClient();

                        if (tcpCient == null)
                        {
                            RecreateTcpClient();
                            continue;
                        }

                        if (tcpCient.Connected)
                        {
                            var eventHappened = WaitHandle.WaitAny(new[] { disconnectedEvent, cancellationTokenSource.Token.WaitHandle });
                            if (eventHappened == 1) // если cancellationTokenSource закэнселили, то выходим
                            {
                                return;
                            }

                            Log.Error().Print("QUIK disconnected, trying to reconnect.");
                            ConnectionStatus = ConnectionStatus.Disconnected;
                            OnConnectionStatusChanged();
                            RecreateTcpClient();
                        }
                        Log.Debug().Print("Begin connect to QUIK");

                        ConnectionStatus = ConnectionStatus.Connecting;
                        OnConnectionStatusChanged();
#if NET45
                        tcpCient.Connect(ip, port);
#endif
#if NETSTANDARD1_6
                        tcpCient.ConnectAsync(ip, port).Wait(); 
#endif
                        if (!tcpCient.Connected)
                        {
                            Log.Warn().Print("QUIK connection wasn't established");
                            continue;
                        }

                        Log.Info().Print("QUIK connected.");

                        ConnectionStatus = ConnectionStatus.Connected;
                        OnConnectionStatusChanged();

                        readerStream = new NetworkStream(tcpCient.Client);
                        reader = new StreamReader(readerStream, Encoding.GetEncoding(1251));
                        writerStream = new NetworkStream(tcpCient.Client);
                        writer = new StreamWriter(writerStream, Encoding.GetEncoding(1251));

                        disconnectedEvent.Reset();

                        // освобождаем 2 места в семафоре (два потока, получение и отправка). Нельзя вызывать Release c параметром 2, т.к. может быть превышение
                        while (connectedSemaphore.Release() < 1) { }
                    }
                    catch (SocketException ex)
                    {
                        Log.Error().PrintFormat(ex, "SocketException while connecting to QUIK: {0}", ex.Message);
                        RecreateTcpClient();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        Log.Error().PrintFormat(ex, "ObjectDisposedException while connecting to QUIK: {0}", ex.Message);
                        //throw ex;
                    }
                    catch (Exception ex)
                    {
                        Log.Error().PrintFormat(ex, "Exception while connecting to QUIK: {0}", ex.Message);
                        RecreateTcpClient();
                    }
                }
            });
        }

        private Task StartReceiving()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.Name = "QL_RECV";

                Log.Debug().Print("Thread started");
                while (!cancellationTokenSource.Token.WaitHandle.WaitOne(ReceiveTimeout))
                {
                    try
                    {
                        if (!EnsureSocketConnected())
                        {
                            Log.Debug().PrintFormat("Thread stopped since EnsureSocketConnected returns {0}", false);
                            return;
                        }

                        var response = reader.ReadLine();

                        if (!string.IsNullOrEmpty(response))
                        {
                            Log.Trace().PrintFormat("CHUNK: {0}", response);
                            incomingMessages.Enqueue(response);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error().PrintFormat(e, "Error while reading message from QUIK. Possibly socket is closed. {0}", e.Message);
                        Thread.Sleep(1000);
                    }
                }
                Log.Debug().Print("Thread stopped");
            });
        }

        /// <summary>
        /// Метод для ожидания подключения сокета к квику
        /// </summary>
        private bool EnsureSocketConnected()
        {
            if (tcpCient.Connected)
            {
                return true;
            }

            // если сокет отвалился, райзим об этом эвент для потока, занимающегося реконнектом
            disconnectedEvent.Set();

            // и уходим в ожидание подключения или остановки всего адаптера
            var eventHappened = WaitHandle.WaitAny(new[] { connectedSemaphore, cancellationTokenSource.Token.WaitHandle });
            if (eventHappened == 1)
            // если cancellationTokenSource закэнселили, то возвращаем фолс, чтобы вызывающая задача могла завершить while true
            {
                return false;
            }

            return true;
        }

        private Task StartSending()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.Name = "QL_SEND";

                Log.Debug().Print("Thread started");

                while (!cancellationTokenSource.Token.WaitHandle.WaitOne(SendTimeout))
                {
                    try
                    {
                        if (!EnsureSocketConnected())
                            return;

                        QLMessage message;
                        if (outgoingMessages.TryDequeue(out message))
                        {
                            var json = JsonConvert.SerializeObject(message,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                            writer.WriteLine(json);
                            writer.Flush();
                        }
                        else
                        {
                            // если больше нечего отправить, то шлём хартбит, только постоянно что-то отправляя мы можем обнаружить разрыв соединения
                            if ((DateTime.Now - lastHearbeatTime).TotalSeconds >= 2)
                            {
                                lastHearbeatTime = DateTime.Now;
                                writer.WriteLine(heartBeatJson);
                                writer.Flush();
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        Log.Error().PrintFormat(e, "Error while sending message to QUIK. Possibly socket is closed. {0}", e.Message);
                        Thread.Sleep(1000);
                    }
                }
                Log.Debug().Print("Thread stopped");
            });
        }

        private Task StartDeserializingReceivedMessages()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.Name = "QL_DESER";

                var jsonSettings = new JsonSerializerSettings
                {
                    Converters =
                    {
                        new QLMessageJsonCreationConverter(),
                        new JsonDateTimeConverter(dateTimeProvider),
                        new JsonTimeSpanConverter()
                    }
                };

                Log.Debug().Print("Thread started");
                while (!cancellationTokenSource.Token.WaitHandle.WaitOne(ReceiveTimeout))
                {
                    try
                    {
                        string chunk;
                        if (incomingMessages.TryDequeue(out chunk))
                        {
                            var envelopeJson = chunk;

                            if (!IsJsonValid(envelopeJson))
                            {
                                throw new Exception($"Corrupted json received: {envelopeJson}");
                            }

                            var envelope = JsonConvert.DeserializeObject<QLEnvelope>(envelopeJson, jsonSettings);
                            
                            outgoingMessages.Enqueue(new QLEnvelopeAcknowledgment { id = envelope.id });

                            foreach (var message in envelope.body)
                            {
                                OnMessageReceived(message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error().Print(ex, $"Error while QL messages parsing: {ex.Message}");
                    }
                }

                Log.Debug().Print("Thread stopped");
            });
        }

        private bool IsJsonValid(string json)
        {
            try
            {
                JToken.Parse(json);
            }
            catch (Exception ex) //some other exception
            {
                Log.Error().PrintFormat(ex, "Corrupted JSON. Exception: {0}. \n JSON: {1}", ex.Message, json);
                return false;
            }
            return true;
        }

        #endregion

        #region IInstrumentConverterContext

        ISubscriptionTester<InstrumentData> IInstrumentConverterContext<InstrumentData>.SubscriptionTester => this;

        #endregion

        #region ISubscriptionTester implementation

        /// <summary>
        ///     Проверить подписку 
        /// </summary>
        Task<SubscriptionTestResult> ISubscriptionTester<InstrumentData>.TestSubscriptionAsync(InstrumentData data)
        {
            var symbol = data.Symbol;
            var result = SubscriptionTestResult.Failed();

            const int timeToWait = 10000;
            var isResolved = false;
            var source = new CancellationTokenSource(timeToWait);
            var cancellationToken = source.Token;
            MessageReceived += (sender, args) =>
            {
                if (args.Message.message_type == QLMessageType.InstrumentParams)
                {
                    var ip = args.Message as QLInstrumentParams;
                    if (ip.code == symbol)
                    {
                        isResolved = true;
                        source.Cancel();
                    }
                }
            };

            SendMessage(new QLInstrumentParamsSubscriptionRequest(symbol));
            cancellationToken.WaitHandle.WaitOne();

            if (isResolved)
            {
                SendMessage(new QLInstrumentParamsUnsubscriptionRequest(symbol));

                result = SubscriptionTestResult.Passed();
            }

            return Task.FromResult(result);
        }

        #endregion
    }
}

