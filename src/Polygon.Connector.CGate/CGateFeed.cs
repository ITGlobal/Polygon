using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CGateAdapter;
using CGateAdapter.Messages;
using CGateAdapter.Messages.FutInfo;
using CGateAdapter.Messages.FutTrades;
using CGateAdapter.Messages.OptInfo;
using CGateAdapter.Messages.OrdersAggr;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Messages;

namespace Polygon.Connector.CGate
{
    class CGateFeed : CGateMessageVisitor, IFeed, IInstrumentParamsSubscriber, IFortsDataProvider, IOrderBookSubscriber
    {
        #region fields

        private static readonly ILog _Log = LogManager.GetLogger(typeof(CGateFeed));

        private readonly ICGAdapter cgAdapter;

        /// <summary>
        /// Очередь сообщений, ожидающих резолва isin_id инструмента
        /// </summary>
        private readonly Queue<CGateMessage> pendingIsinResolutionMessages = new Queue<CGateMessage>();

        private readonly IRwLockObject pendingIsinResolutionMessagesLock = DeadlockMonitor.ReaderWriterLock(typeof(CGateFeed), "pendingIsinResolutionMessagesLock");

        private Task pendingMessagesProcessingTask;

        private readonly AutoResetEvent processPendingMessagesEvent = new AutoResetEvent(false);

        private readonly Queue<Message> messagesReadyToFire = new Queue<Message>();

        private readonly IRwLockObject messagesReadyToFireLock = DeadlockMonitor.ReaderWriterLock(typeof(CGateFeed), "messagesReadyToFireLock");

        private Task messagesReadyToFireTask;

        private readonly AutoResetEvent messagesReadyToFireEvent = new AutoResetEvent(false);

        private readonly HashSet<Instrument> subscribedInstruments = new HashSet<Instrument>();
        private readonly HashSet<Instrument> subscribedOrderBooks = new HashSet<Instrument>();

        private readonly IRwLockObject subscribedInstrumentsLock = DeadlockMonitor.ReaderWriterLock(typeof(CGateFeed), "subscribedInstrumentsLock");

        private CancellationTokenSource cancellationTokenSource;

        private readonly CGateOrderBookEmitter futOrderBookEmitter;
        private readonly CGateOrderBookEmitter optOrderBookEmitter;

        private readonly CGateInstrumentResolver instrumentResolver;

        private readonly CGateInstrumentParamsEmitter instrumentParamsEmitter;

        #endregion

        #region .ctor

        public CGateFeed(
            ICGAdapter cgAdapter, 
            CGateInstrumentResolver instrumentResolver, 
            CGateInstrumentParamsEmitter instrumentParamsEmitter)
        {
            this.cgAdapter = cgAdapter;
            this.cgAdapter.MarketdataMessageReceived += CGateAdapterStreamMessageHandler;
            this.instrumentResolver = instrumentResolver;
            this.instrumentParamsEmitter = instrumentParamsEmitter;

            futOrderBookEmitter = new CGateOrderBookEmitter("FORTS_FUTAGGR20_REPL", instrumentResolver);
            futOrderBookEmitter.OrderBookUpdated += orderBookEmitter_OrderBookUpdated;

            optOrderBookEmitter = new CGateOrderBookEmitter("FORTS_OPTAGGR20_REPL", instrumentResolver);
            optOrderBookEmitter.OrderBookUpdated += orderBookEmitter_OrderBookUpdated;

            instrumentResolver.OnNewIsinResolved += CGateIsinResolverOnNewIsinResolved;
        }

        #endregion

        #region IFeed

        public bool SendErrorMessages { get; set; }
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion

        #region Public methods

        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            pendingMessagesProcessingTask = Task.Factory.StartNew(PendingMessagesProcessing);
            messagesReadyToFireTask = Task.Factory.StartNew(MessagesReadyToFireProcessing);
            futOrderBookEmitter.Start();
            optOrderBookEmitter.Start();
        }

        public void Stop()
        {
            cancellationTokenSource?.Cancel();

            processPendingMessagesEvent.Set();
            messagesReadyToFireEvent.Set();

            pendingMessagesProcessingTask?.Wait();
            messagesReadyToFireTask?.Wait();

            futOrderBookEmitter?.Stop();
            optOrderBookEmitter?.Stop();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Обработчик события обновления стакана
        /// </summary>
        void orderBookEmitter_OrderBookUpdated(object sender, OrderBookUpdatedEventArgs args)
        {
            args.Books.ForEach(EnqueueTransportMessage);
        }

        /// <summary>
        /// Обработчик события резолва нового isin_id
        /// </summary>
        private void CGateIsinResolverOnNewIsinResolved(object sender, EventArgs eventArgs)
        {
            processPendingMessagesEvent.Set();
        }

        /// <summary>
        /// Обработчик сообщений адаптера cgate
        /// </summary>
        private void CGateAdapterStreamMessageHandler(object sender, CGateMessageEventArgs message)
        {
            message.Message.Accept(this);
        }

        /// <summary>
        /// Джоба обработки пендинг сообщений
        /// </summary>
        private void PendingMessagesProcessing()
        {
            LogManager.BreakScope();

            var objectsToWait = new WaitHandle[2] { processPendingMessagesEvent, cancellationTokenSource.Token.WaitHandle };
            int risedObjectIndex;

            while (true)
            {
                risedObjectIndex = WaitHandle.WaitAny(objectsToWait);
                if (risedObjectIndex == 1)
                {
                    break;
                }

                using (pendingIsinResolutionMessagesLock.UpgradableReadLock())
                {
                    var initialMessagesCount = pendingIsinResolutionMessages.Count;
                    var messagesProcessed = 0;

                    while (messagesProcessed < initialMessagesCount)
                    {
                        var message = pendingIsinResolutionMessages.Dequeue();
                        if (message == null)
                        {
                            continue;
                        }

                        message.Accept(this);
                        messagesProcessed++;
                    }
                }
            }

            _Log.Debug().Print("Exit PendingMessagesProcessing task");
        }

        /// <summary>
        /// Джоба обработки сообщений, готовых к отправке подписчикам эвента
        /// </summary>
        private void MessagesReadyToFireProcessing()
        {
            LogManager.BreakScope();

            var objectsToWait = new WaitHandle[2] { messagesReadyToFireEvent, cancellationTokenSource.Token.WaitHandle };
            int risedObjectIndex;

            while (true)
            {
                risedObjectIndex = WaitHandle.WaitAny(objectsToWait);
                if (risedObjectIndex == 1)
                {
                    break;
                }

                var handler = MessageReceived;
                using (messagesReadyToFireLock.WriteLock())
                {
                    while (messagesReadyToFire.Any())
                    {
                        if (handler != null)
                        {
                            // NOTE если handler == null, то очередь никогда не будет выгребаться
                            handler(this, new MessageReceivedEventArgs(messagesReadyToFire.Dequeue()));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Добавить сообщение в очередь необработанных. Сюда помещаются CGateMessage сообщения, которые пока невозможно обработать, 
        /// из-за отсутствия информации, которая должна прийти в других CGateMessage сообщениях.
        /// </summary>
        /// <param name="message"></param>
        private void EnqueuePendingMessage(CGateMessage message)
        {
            using (pendingIsinResolutionMessagesLock.WriteLock())
            {
                pendingIsinResolutionMessages.Enqueue(message);
            }
        }

        /// <summary>
        /// Добавить транспортное сообщение в очередь готовых к выдаче подписчику события фида.
        /// </summary>
        /// <param name="message"></param>
        private void EnqueueTransportMessage(Message message)
        {
            var instrumentMessage = (InstrumentMessage)message;
            if (!IsInstrumentSubscribed(instrumentMessage.Instrument))
            {
                return;
            }

            using (messagesReadyToFireLock.WriteLock())
            {
                messagesReadyToFire.Enqueue(message);
            }
            messagesReadyToFireEvent.Set();
        }

        /// <summary>
        /// Проверка наличия подписки на инструмент
        /// </summary>
        /// <param name="instrument"></param>
        /// <returns></returns>
        private bool IsInstrumentSubscribed(Instrument instrument)
        {
            using (subscribedInstrumentsLock.ReadLock())
            {
                return subscribedInstruments.Contains(instrument) || subscribedOrderBooks.Contains(instrument);
            }
        }

        #endregion

        #region DefaultCGateMessageHandler overrides

        public override void Handle(CgmSession message)
        {
            var ips = instrumentParamsEmitter.SetSessionEndTime(message.End.TimeOfDay);
            foreach (var ip in ips)
            {
                EnqueueTransportMessage(ip);
            }
        }

        public override void Handle(CgmFutSessContents message)
        {
            instrumentResolver.Handle(message);
            var ip = instrumentParamsEmitter.GetUpdatedInstrumentParams(message);
            if (ip != null)
            {
                EnqueueTransportMessage(ip);
            }
        }

        public override void Handle(CgmOptSessContents message)
        {
            instrumentResolver.Handle(message);
            var ip = instrumentParamsEmitter.GetUpdatedInstrumentParams(message);
            if (ip != null)
            {
                EnqueueTransportMessage(ip);
            }
        }

        public override void Handle(CGateAdapter.Messages.FutCommon.CgmCommon message)
        {
            var code = instrumentResolver.GetShortIsinByIsinId(message.IsinId);
            if (string.IsNullOrEmpty(code))
            {
                EnqueuePendingMessage(message);
                return;
            }

            var ip = instrumentParamsEmitter.GetUpdatedInstrumentParams(message);
            if (ip != null)
            {
                EnqueueTransportMessage(ip);
            }
        }

        public override void Handle(CGateAdapter.Messages.OptCommon.CgmCommon message)
        {
            var code = instrumentResolver.GetShortIsinByIsinId(message.IsinId);
            if (string.IsNullOrEmpty(code))
            {
                EnqueuePendingMessage(message);
                return;
            }

            var ip = instrumentParamsEmitter.GetUpdatedInstrumentParams(message);
            if (ip != null)
            {
                EnqueueTransportMessage(ip);
            }
        }

        public override void Handle(CGateAdapter.Messages.Volat.CgmVolat message)
        {
            var code = instrumentResolver.GetShortIsinByIsinId(message.IsinId);
            if (string.IsNullOrEmpty(code))
            {
                EnqueuePendingMessage(message);
                return;
            }

            var ip = instrumentParamsEmitter.GetUpdatedInstrumentParams(message);
            if (ip != null)
            {
                EnqueueTransportMessage(ip);
            }
        }

        public override void Handle(CgmOrdersAggr record)
        {
            var code = instrumentResolver.GetShortIsinByIsinId(record.IsinId);
            if (string.IsNullOrEmpty(code))
            {
                EnqueuePendingMessage(record);
                return;
            }

            futOrderBookEmitter.Handle(record);
            optOrderBookEmitter.Handle(record);
        }

        public override void Handle(CGateDataEnd message)
        {
            futOrderBookEmitter.Handle(message);
            optOrderBookEmitter.Handle(message);
        }

        public override void Handle(CGateClearTableMessage message)
        {
            futOrderBookEmitter.Handle(message);
            optOrderBookEmitter.Handle(message);
        }

        #endregion

        #region IInstrumentParamsSubscriber

        /// <summary>
        ///     Подписаться на инструмент.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public Task<SubscriptionResult> Subscribe(Instrument instrument)
        {
            instrument = instrumentResolver.InstrumentConverter.ResolveTransportInstrumentAsync(instrument).Result;
            using (subscribedInstrumentsLock.UpgradableReadLock())
            {
                if (!subscribedInstruments.Contains(instrument))
                {
                    using (subscribedInstrumentsLock.WriteLock())
                    {
                        subscribedInstruments.Add(instrument);
                    }
                }
            }

            // формируем отправку закэшированных параметров инстурментов и стакана
            var ip = instrumentParamsEmitter.GetInstrumentParams(instrument);
            if (ip != null)
            {
                EnqueueTransportMessage(ip);
            }

            var tcSource = new TaskCompletionSource<SubscriptionResult>();
            tcSource.SetResult(new SubscriptionResult(instrument, true));
            return tcSource.Task;
        }

        /// <summary>
        ///     Отписаться от инструмента.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public void Unsubscribe(Instrument instrument)
        {
            instrument = instrumentResolver.InstrumentConverter.ResolveTransportInstrumentAsync(instrument).Result;
            using (subscribedInstrumentsLock.UpgradableReadLock())
            {
                if (subscribedInstruments.Contains(instrument))
                {
                    using (subscribedInstrumentsLock.WriteLock())
                    {
                        subscribedInstruments.Remove(instrument);
                    }
                }
            }
        }

        #endregion

        #region IOrderBookSubscriber

        public async void SubscribeOrderBook(Instrument instrument)
        {
            using (LogManager.Scope())
            {
                instrument = await instrumentResolver.InstrumentConverter.ResolveTransportInstrumentAsync(instrument);
                try
                {
                    using (subscribedInstrumentsLock.UpgradableReadLock())
                    {
                        if (!subscribedOrderBooks.Contains(instrument))
                        {
                            using (subscribedInstrumentsLock.WriteLock())
                            {
                                subscribedOrderBooks.Add(instrument);
                            }
                        }
                    }

                    var instrumentType = instrumentResolver.GetInstrumentType(instrument.Code);
                    var ob =
                        (instrumentType == InstrumentType.Futures ? futOrderBookEmitter : optOrderBookEmitter)
                        .GetOrderBook(instrument);

                    if (ob != null)
                    {
                        EnqueueTransportMessage(ob);
                    }

                    var tcSource = new TaskCompletionSource<SubscriptionResult>();
                    tcSource.SetResult(new SubscriptionResult(instrument, true));
                    await tcSource.Task;
                }
                catch (Exception e)
                {
                    _Log.Error().Print(e, $"Failed to subscribe to order book on {instrument}");
                }
            }
        }

        public void UnsubscribeOrderBook(Instrument instrument)
        {
            instrument = instrumentResolver.InstrumentConverter.ResolveTransportInstrumentAsync(instrument).Result;
            using (subscribedInstrumentsLock.UpgradableReadLock())
            {
                if (subscribedOrderBooks.Contains(instrument))
                {
                    using (subscribedInstrumentsLock.WriteLock())
                    {
                        subscribedOrderBooks.Remove(instrument);
                    }
                }
            }
        }
        #endregion


        #region IFortsDataProvider

        /// <summary>
        ///     Получить FullCode для инструмента
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент
        /// </param>
        /// <returns>
        ///     FullCode либо null
        /// </returns>
        public string QueryFullCode(Instrument instrument) => instrumentResolver.GetIsinByShortIsin(instrument.Code);

        #endregion
    }
}

