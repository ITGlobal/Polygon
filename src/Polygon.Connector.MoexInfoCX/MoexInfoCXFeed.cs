using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.Connector.MoexInfoCX.Stomp;
using Polygon.Connector.MoexInfoCX.Stomp.Messages;
using Polygon.Connector.MoexInfoCX.Transport;
using Newtonsoft.Json.Linq;
using Polygon.Connector.Common;
using Polygon.Connector.MoexInfoCX.Common;
using Polygon.Messages;

namespace Polygon.Connector.MoexInfoCX
{
    public class MoexInfoCXFeed : 
        Feed, 
        IConnectionStatusProvider, 
        IInstrumentParamsSubscriber, 
        IOrderBookSubscriber,
        ISubscriptionTester<InfoCXInstrumentData>,
        IInstrumentConverterContext<InfoCXInstrumentData>
    {
        private readonly MoexInfoCXParameters settings;
        private readonly StompConnection connection;
        private readonly WebsocketTransportSettings transportSettings = new WebsocketTransportSettings();


        private readonly Dictionary<Instrument, InstrumentParams> paramDictionary = new Dictionary<Instrument, InstrumentParams>();

        public MoexInfoCXFeed(MoexInfoCXParameters settings)
        {
            this.settings = settings;

            connection = new StompConnection(
                new Uri(settings.BrokerUrl), 
                new WebsocketTransportFactory(transportSettings),
                new StompWireFormat(),
                new StompLogger(Logger)
            );


            connection.MessageReceived += Connection_MessageReceived;
            connection.StateChanged += Connection_StateChanged;
        }

        public string Session => connection.Session;

        private void Connection_StateChanged(object sender, StompConnectionStateChangedEventArgs e)
        {
            switch (connection.State)
            {
                case StompConnectionState.Connected:
                    var connect = new Connect(
                        host: settings.BrokerUrl,
                        login: settings.Username,
                        passcode: settings.Password,
                        domain: settings.Domain);
                    connection.Send(connect);
                    break;

                case StompConnectionState.Connecting:
                    ChangeConnectionStatus(ConnectionStatus.Connecting);
                    break;
                case StompConnectionState.Disconnected:
                    ChangeConnectionStatus(ConnectionStatus.Disconnected);
                    break;
            }
        }

        private const string Market = "FORTS";
        private const string SubMarket = "RFUD";

        //private const string Market = "MXSE";
        //private const string SubMarket = "TQBR";

        private const string Prefix = Market + "." + SubMarket + ".";
        private const string Securities = Market + ".securities";
        private const string Orderbooks = Market + ".orderbooks";

        /// <summary>
        ///     Обработка входящего сообщения
        /// </summary>
        private void Connection_MessageReceived(object sender, StompMessageReceivedEventArgs e)
        {
            TaskCompletionSource<SubscriptionResult> tcs;
            Instrument instrument;

            switch (e.Message.Command)
            {
                case StompCommands.CONNECTED:

                    ChangeConnectionStatus(ConnectionStatus.Connected);

                    //var request = new Request(
                    //    destination: "SEARCH.ticker",
                    //    id: Guid.NewGuid().ToString(),
                    //    selector: "pattern=\"*BR*\""
                    //    //selector: "pattern=\"\""
                    //    );

                    //connection.Send(request);

                    break;

                case StompCommands.ERROR:
                    var error = new Error(e.Message);
                    var sub = error.ReceiptId ?? e.Message.GetHeader("subscription");

                    if (ipSubscriptionResults.TryGetValue(sub, out tcs) &&
                        ipIdToInstrument.TryGetValue(sub, out instrument))
                    {
                        ipSubscriptionResults.Remove(sub);
                        tcs.SetResult(SubscriptionResult.Error(instrument, error.Message));
                    }

                    break;

                case StompCommands.RECEIPT:
                    var reci = new Receipt(e.Message);

                    if (ipSubscriptionResults.TryGetValue(reci.ReceiptId, out tcs) &&
                        ipIdToInstrument.TryGetValue(reci.ReceiptId, out instrument))
                    {
                        ipSubscriptionResults.Remove(reci.ReceiptId);
                        tcs.SetResult(SubscriptionResult.OK(instrument));
                    }

                    break;

                case StompCommands.MESSAGE:
                    var subscription = e.Message.GetHeader("subscription", isRequired: false);
                    var destination = e.Message.GetHeader("destination", isRequired: false);

                    if (destination == Securities)
                    {
                        if (subscription != null &&
                            ipSubscriptionResults.TryGetValue(subscription, out tcs) &&
                            ipIdToInstrument.TryGetValue(subscription, out instrument))
                        {
                            ipSubscriptionResults.Remove(subscription);
                            tcs.SetResult(SubscriptionResult.OK(instrument));
                        }

                        try
                        {
                            ParseParams(e.Message, subscription);
                        }
                        catch { }
                    }
                    else if (destination == Orderbooks)
                    {
                        try
                        {
                            ParseBook(e.Message, subscription);
                        }
                        catch { }

                    }
                    break;

                case "REPLY":
                    break;

                default:
                    return;
            }
        }

        /// <summary>
        ///     Парсинг стакана
        /// </summary>
        private void ParseBook(IStompFrame frame, string subscription)
        {
            if (!obIdToInstrument.TryGetValue(subscription, out var instrument))
            {
                return;
            }

            var json = JObject.Parse(frame.Body);

            var columns = (JArray) json["columns"];
            var allData = (JArray) json["data"];

            var ob = new OrderBook(allData.Count)
            {
                Instrument = instrument
            };

            var ip = GetInstrumentParams(instrument);

            OrderBookItem prev = null, curr;
            for (var i = allData.Count - 1; i >= 0; i--)
            {
                var data = (JArray) allData[i];
                var dict = new JArrayDictionary(columns, data);

                ob.Items.Add(curr = new OrderBookItem
                {
                    Operation = dict["BUYSELL"].ToString() == "B" ? OrderOperation.Buy : OrderOperation.Sell,
                    Price = (decimal) dict["PRICE"][0],
                    Quantity = (int) dict["QUANTITY"],
                });

                if (prev?.Operation == OrderOperation.Sell && curr.Operation == OrderOperation.Buy)
                {
                    ip.BestOfferPrice = prev.Price;
                    ip.BestOfferQuantity = prev.Quantity;

                    ip.BestBidPrice = curr.Price;
                    ip.BestBidQuantity = curr.Quantity;
                }

                prev = curr;
            }

            OnMessageReceived(ip);
            OnMessageReceived(ob);
        }

        /// <summary>
        ///     Парсинг параметров
        /// </summary>
        private void ParseParams(IStompFrame frame, string subscription)
        {
            if (!ipIdToInstrument.TryGetValue(subscription, out var instrument))
            {
                return;
            }

            var json = JObject.Parse(frame.Body);

            var columns = (JArray)json["columns"];
            var allData = (JArray)json["data"];


            foreach (JArray data in allData)
            {
                var dict = new JArrayDictionary(columns, data);

                //var ticker = dict["TICKER"].ToString();

                var ip = GetInstrumentParams(instrument);

                ip.LastPrice = (decimal) dict["LAST"][0];

                ip.BestBidPrice = (decimal) dict["BID"][0];
                ip.BestOfferPrice = (decimal) dict["OFFER"][0];

                //ip.Settlement = (decimal) dict["WAPRICE"][0];

                //foreach (var kvp in dict)
                //{
                //    switch (kvp.Key)
                //    {
                //        case "LAST":
                //            ip.LastPrice = (decimal)kvp.Value[0];
                //            break;

                //        case "BID":
                //            ip.BestBidPrice = (decimal)kvp.Value[0];
                //            break;
                //        //case "BIDDEPTHT":
                //        //    ip.BestBidQuantity = (int)kvp.Value;
                //        //    break;

                //        case "OFFER":
                //            ip.BestOfferPrice = (decimal)kvp.Value[0];
                //            break;
                //        //case "OFFERDEPTHT":
                //        //    ip.BestOfferQuantity = (int)kvp.Value;
                //        //    break;

                //        case "WAPRICE":
                //            ip.Settlement = (decimal)kvp.Value[0];
                //            break;

                //        case "PREVPRICE":
                //            ip.PreviousSettlement = (decimal)kvp.Value[0];
                //            break;

                //        default:
                //            break;
                //    }
                //}

                OnMessageReceived(ip);
            }
        }

        private InstrumentParams GetInstrumentParams(Instrument instrument)
        {
            if (!paramDictionary.TryGetValue(instrument, out var ip))
            {
                paramDictionary[instrument] = ip = new InstrumentParams
                {
                    Instrument = instrument,
                };
            }

            return ip;
        }

        #region Nested types

        /// <summary>
        ///     Запрос данных
        /// </summary>
        class Request : ClientMessage
        {
            public Request(
                string id,
                string destination,
                string selector = null
            ) 
                : base("REQUEST")
            {
                if (string.IsNullOrEmpty(destination))
                    throw new ArgumentNullException(nameof(destination), $"\"destination\" is required for {StompCommands.SUBSCRIBE} frame");
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException(nameof(id), $"\"id\" is required for {StompCommands.SUBSCRIBE} frame");

                SetHeader("destination", destination);
                SetHeader("id", id);

                SetHeader("selector", selector);
            }
        }

        /// <summary>
        ///  Парсер словаря
        /// </summary>
        class JArrayDictionary : IReadOnlyDictionary<string, JToken>
        {
            private readonly JArray columns;
            private readonly JArray data;

            public JArrayDictionary(JArray columns, JArray data)
            {
                this.columns = columns;
                this.data = data;

                Count = columns.Count;

                if (Count != data.Count)
                {
                    throw new ArgumentException();
                }
            }

            #region IReadOnlyDictionary

            public int Count { get; }

            public IEnumerable<string> Keys => columns.Select(_ => _.ToString());

            public IEnumerable<JToken> Values => data;

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
            {
                for (int i = 0; i < Count; i++)
                {
                    yield return new KeyValuePair<string, JToken>(columns[i].ToString(), data[i]);
                }
            }

            public bool ContainsKey(string key)
            {
                return columns.Contains(key);
            }

            public bool TryGetValue(string key, out JToken value)
            {
                var idx = columns.IndexOf(_ => _.ToString() == key);

                if (idx == -1)
                {
                    value = null;
                    return false;
                }

                value = data[idx];
                return true;
            }

            public JToken this[string key] => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();

            #endregion
        }

        #endregion

        #region Feed

        public override void Stop()
        {
            ipInstrumentToId.Keys.ToArray().ForEach(_ => (this as IInstrumentParamsSubscriber).Unsubscribe(_));
            obInstrumentToId.Keys.ToArray().ForEach(_ => (this as IOrderBookSubscriber).UnsubscribeOrderBook(_));

            connection.Send(new Disconnect("disconnect"));

            connection.Disconnect();
        }

        public override void Start()
        {
            connection.Connect();
        }

        #endregion

        #region IConnectionStatusProvider

        public string ConnectionName => "InfoCX Feed";

        public ConnectionStatus ConnectionStatus => (ConnectionStatus)status;
        // Содержит значения типа ConnectionStatus
        private int status = (int)ConnectionStatus.Undefined;

        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        private void ChangeConnectionStatus(ConnectionStatus newStatus)
        {
            if (Interlocked.Exchange(ref status, (int)newStatus) != (int)newStatus)
            {
                ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(ConnectionStatus, ConnectionName));
            }
        }

        #endregion

        #region IInstrumentParamsSubscriber

        private readonly Dictionary<Instrument, string> ipInstrumentToId = new Dictionary<Instrument, string>();
        private readonly Dictionary<string, Instrument> ipIdToInstrument = new Dictionary<string, Instrument>();

        private readonly Dictionary<string, TaskCompletionSource<SubscriptionResult>> ipSubscriptionResults = new Dictionary<string, TaskCompletionSource<SubscriptionResult>>();

        Task<SubscriptionResult> IInstrumentParamsSubscriber.Subscribe(Instrument instrument)
        {
            var id = Guid.NewGuid().ToString();

            ipInstrumentToId[instrument] = id;
            ipIdToInstrument[id] = instrument;

            var tcs = ipSubscriptionResults[id] = new TaskCompletionSource<SubscriptionResult>();

            var subscribe = new Subscribe(
                destination: Securities,
                id: id,
                selector: $"TICKER=\"{Prefix}{instrument.Code}\" and LANGUAGE=\"en\""
            );

            connection.Send(subscribe);

            return tcs.Task;
        }

        void IInstrumentParamsSubscriber.Unsubscribe(Instrument instrument)
        {
            if (!ipInstrumentToId.TryGetValue(instrument, out var id))
            {
                return;
            }


            var unsubscribe = new Unsubscribe(
                id: id
            );

            connection.Send(unsubscribe);

            ipInstrumentToId.Remove(instrument);
            ipIdToInstrument.Remove(id);
        }

        #endregion

        #region IOrderBookSubscriber

        private readonly Dictionary<Instrument, string> obInstrumentToId = new Dictionary<Instrument, string>();
        private readonly Dictionary<string, Instrument> obIdToInstrument = new Dictionary<string, Instrument>();

        void IOrderBookSubscriber.SubscribeOrderBook(Instrument instrument)
        {
            var id = Guid.NewGuid().ToString();

            obInstrumentToId[instrument] = id;
            obIdToInstrument[id] = instrument;

            var subscribe = new Subscribe(
                destination: Orderbooks,
                id: id,
                selector: $"TICKER=\"{Prefix}{instrument.Code}\" and LANGUAGE=\"en\""
            );

            connection.Send(subscribe);
        }

        void IOrderBookSubscriber.UnsubscribeOrderBook(Instrument instrument)
        {
            if (!obInstrumentToId.TryGetValue(instrument, out var id))
            {
                return;
            }

            var unsubscribe = new Unsubscribe(
                id: id
            );

            connection.Send(unsubscribe);

            obInstrumentToId.Remove(instrument);
            obIdToInstrument.Remove(id);
        }

        #endregion

        #region ISubscriptionTester

        public Task<SubscriptionTestResult> TestSubscriptionAsync(InfoCXInstrumentData data)
        {
            //TODO
            throw new NotImplementedException();
        }
        
        #endregion

        #region IInstrumentConverterContext

        public ISubscriptionTester<InfoCXInstrumentData> SubscriptionTester => this;

        #endregion
    }
}
