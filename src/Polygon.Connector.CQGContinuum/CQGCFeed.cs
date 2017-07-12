using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Messages;
using Polygon.Connector;

namespace Polygon.Connector.CQGContinuum
{
    /// <summary>
    ///     Фид CQG Continuum
    /// </summary>
    internal sealed class CQGCFeed : IFeed, IInstrumentParamsSubscriber, IOrderBookSubscriber
    {
        #region флаги

        [Flags]
        private enum SubscriptionFlags
        {
            None = 0x00,
            InstrumentParams = 0x01,
            OrderBook = 0x02
        }

        [Flags]
        private enum SendMessageFlags
        {
            None = 0x00,
            InstrumentParams = 0x01,
            OrderBook = 0x02
        }

        #endregion

        #region класс-обработчик подписки

        private sealed class InstrumentSubscription
        {
            private readonly CQGCInstrumentResolver instrumentResolver;

            private readonly ILockObject lockCookie;
            private bool level2QuoteReceived; // признак того, что хотя бы один раз была получена котировка, отличная от BBA

            private readonly SortedList<decimal, OrderBookItem> domBids = new SortedList<decimal, OrderBookItem>();
            private readonly SortedList<decimal, OrderBookItem> domAsks = new SortedList<decimal, OrderBookItem>();

            private readonly Stopwatch realtimeDataTimer = new Stopwatch();

            public InstrumentSubscription(
                CQGCInstrumentResolver instrumentResolver,
                Instrument instrument,
                uint contractId)
            {
                this.instrumentResolver = instrumentResolver;
                Instrument = instrument;
                lockCookie = DeadlockMonitor.Cookie<InstrumentSubscription>("lockCookie-" + ContractId);
                ContractId = contractId;

                InstrumentParams = new InstrumentParams { Instrument = instrument };
                InstrumentParams.VolaTranslatedByFeed = false;
                OrderBook = new OrderBook();
            }

            public Instrument Instrument { get; }
            public uint ContractId { get; }

            /// <summary>
            /// Шаги цены инструмента в зависимости от цены
            /// </summary>
            public List<TickSizeByPrice> TickSizesByPrice { get; set; }

            public InstrumentParams InstrumentParams { get; }

            public OrderBook OrderBook { get; }

            public SubscriptionFlags Flags { get; set; }

            public void AcquireFlagsLock() => DeadlockMonitor.Enter(lockCookie);

            public void ReleaseFlagsLock() => DeadlockMonitor.Exit(lockCookie);

            public void StartWaitForData(MarketDataSubscription.Level level)
            {
                if (!realtimeDataTimer.IsRunning)
                {
                    realtimeDataTimer.Reset();
                    realtimeDataTimer.Start();

                    _Log.Debug().PrintFormat("InstrumentSubscription({0}) - waiting for realtime {1} data", Instrument, level);
                }
            }

            public SendMessageFlags Handle(ContractMetadata metadata)
            {
                var result = SendMessageFlags.None;

                var tickSize = (decimal)metadata.tick_size;
                if (InstrumentParams.PriceStep != tickSize)
                {
                    InstrumentParams.PriceStep = tickSize;
                    result |= SendMessageFlags.InstrumentParams;
                }

                var tickValue = (decimal)metadata.tick_value;
                if (InstrumentParams.PriceStepValue != tickValue)
                {
                    InstrumentParams.PriceStepValue = tickValue;
                    result |= SendMessageFlags.InstrumentParams;
                }

                // ситуация, когда шаг цены инструмента - плавающая
                if (metadata.tick_sizes_by_price != null && metadata.tick_sizes_by_price.Count > 0)
                {
                    TickSizesByPrice = metadata.tick_sizes_by_price.OrderBy(_ => _.minimum_price).ToList();
                }

                var decimalPlaces = metadata.display_price_scale <= 15 // См. описание поля display_price_scale в WebAPI.proto
                        ? metadata.display_price_scale
                        : InstrumentParams.GetDecimalPlaces(); // DecimalPlaces считаются по PriceStep

                if (InstrumentParams.DecimalPlaces != decimalPlaces)
                {
                    InstrumentParams.DecimalPlaces = decimalPlaces;
                    result |= SendMessageFlags.InstrumentParams;
                }

                return result;
            }

            public SendMessageFlags Handle(RealTimeMarketData marketData)
            {
                SendMessageFlags result;

                if (marketData.is_snapshot)
                {
                    if (marketData.market_values != null)
                    {
                        InstrumentParams.LastPrice = instrumentResolver.ConvertPriceBack(ContractId,
                            marketData.market_values.close_price != 0
                                ? marketData.market_values.close_price
                                : marketData.market_values.yesterday_close);

                        InstrumentParams.Settlement = instrumentResolver.ConvertPriceBack(ContractId,
                            marketData.market_values.yesterday_close != 0
                                ? marketData.market_values.yesterday_close
                                : marketData.market_values.yesterday_settlement);

                        InstrumentParams.PreviousSettlement = instrumentResolver.ConvertPriceBack(ContractId, marketData.market_values.yesterday_settlement);
                        UpdatePriceStep(InstrumentParams.LastPrice);
                        result = SendMessageFlags.InstrumentParams;
                        result |= HandleQuotes(marketData.quote);

                        EndWaitForData();

                        return result;
                    }

                    return SendMessageFlags.None;
                }

                if (marketData.quote == null)
                {
                    // Нет данных по котировкам
                    return SendMessageFlags.None;
                }

                result = HandleQuotes(marketData.quote);

                if ((result & SendMessageFlags.OrderBook) == SendMessageFlags.OrderBook)
                {
                    RebuildOrderBook();
                }

                if (result != SendMessageFlags.None)
                {
                    EndWaitForData();
                }

                return result;
            }

            /// <summary>
            /// Обработка Quote-ов. Могут приходить в разных сообщениях
            /// </summary>
            private SendMessageFlags HandleQuotes(List<Quote> quotes)
            {
                var result = SendMessageFlags.None;

                if (quotes == null || quotes.Count == 0)
                    return result;

                foreach (var quote in quotes)
                {
                    // marketData.contract_id
                    var price = instrumentResolver.ConvertPriceBack(ContractId, quote.price);
                    var volume = (long)quote.volume;

                    switch ((Quote.Type)quote.type)
                    {
                        case Quote.Type.BESTBID:
                            if (InstrumentParams.BestBidPrice != price ||
                                InstrumentParams.BestBidQuantity != volume)
                            {
                                InstrumentParams.BestBidPrice = price;
                                InstrumentParams.BestBidQuantity = volume;

                                if (!level2QuoteReceived)
                                {
                                    domBids.Clear();
                                    domBids[price] = new OrderBookItem(OrderOperation.Buy, price, volume);
                                    result |= SendMessageFlags.OrderBook;
                                }
                                result |= SendMessageFlags.InstrumentParams;
                            }
                            break;

                        case Quote.Type.BESTASK:
                            if (InstrumentParams.BestOfferPrice != price ||
                                InstrumentParams.BestOfferQuantity != volume)
                            {
                                InstrumentParams.BestOfferPrice = price;
                                InstrumentParams.BestOfferQuantity = volume;

                                if (!level2QuoteReceived)
                                {
                                    domAsks.Clear();
                                    domAsks[-price] = new OrderBookItem(OrderOperation.Sell, price, volume);
                                    result |= SendMessageFlags.OrderBook;
                                }
                                result |= SendMessageFlags.InstrumentParams;
                            }
                            break;

                        case Quote.Type.BID:
                            level2QuoteReceived = true;
                            if (volume == 0)
                                domBids.Remove(price);
                            else
                                domBids[price] = new OrderBookItem(OrderOperation.Buy, price, volume);

                            result |= SendMessageFlags.OrderBook;
                            break;

                        case Quote.Type.ASK:
                            level2QuoteReceived = true;
                            // Цена с минусом, чтобы был правильный порядок сортировки
                            if (volume == 0)
                                domAsks.Remove(-price);
                            else
                                domAsks[-price] = new OrderBookItem(OrderOperation.Sell, price, volume);
                            result |= SendMessageFlags.OrderBook;
                            break;

                        case Quote.Type.SETTLEMENT:
                            if (InstrumentParams.Settlement != price)
                            {
                                InstrumentParams.Settlement = price;

                                if (InstrumentParams.LastPrice == 0)
                                    InstrumentParams.LastPrice = price;

                                UpdatePriceStep(price);
                                result |= SendMessageFlags.InstrumentParams;
                            }
                            break;

                        case Quote.Type.TRADE:
                            if (InstrumentParams.LastPrice != price)
                            {
                                InstrumentParams.LastPrice = price;
                                UpdatePriceStep(price);
                                result |= SendMessageFlags.InstrumentParams;
                            }
                            break;
                    }
                }
                return result;
            }

            /// <summary>
            /// Изменение шага цены инстурмента при изменении его цены (для инструментов, у которых есть такая зависимость)
            /// </summary>
            private void UpdatePriceStep(decimal contractPrice)
            {
                if (TickSizesByPrice != null)
                {
                    foreach (var tickSizeByPrice in TickSizesByPrice)
                    {
                        if (contractPrice >= (decimal)tickSizeByPrice.minimum_price)
                        {
                            InstrumentParams.PriceStep = (decimal)tickSizeByPrice.tick_size;
                            InstrumentParams.PriceStepValue = (decimal)tickSizeByPrice.tick_value;
                        }
                    }
                }
            }

            private void RebuildOrderBook()
            {
                OrderBook.Items = new List<OrderBookItem>(domAsks.Values.Concat(domBids.Values));
            }

            private void EndWaitForData()
            {
                if (realtimeDataTimer.IsRunning)
                {
                    realtimeDataTimer.Stop();
                    _Log.Debug().PrintFormat("InstrumentSubscription({0}) - realtime data received in {1}ms", Instrument, realtimeDataTimer.ElapsedMilliseconds);
                }
            }
        }

        #endregion

        #region fields

        private static readonly ILog _Log = LogManager.GetLogger<CQGCFeed>();

        private readonly CQGCAdapter adapter;
        private readonly CQGCInstrumentResolver instrumentResolver;

        private readonly ILockObject subscriptionsLock = DeadlockMonitor.Cookie<CQGCFeed>("subscriptionsLock");
        private readonly Dictionary<Instrument, InstrumentSubscription> subscriptionsByInstrument = new Dictionary<Instrument, InstrumentSubscription>();
        private readonly Dictionary<uint, InstrumentSubscription> subscriptionsByContractId = new Dictionary<uint, InstrumentSubscription>();
        private readonly Dictionary<uint, ContractMetadata> contractMetadatas = new Dictionary<uint, ContractMetadata>();

        private readonly Timer requestBatchTimer;
        private readonly ILockObject requestBatchLock = DeadlockMonitor.Cookie<CQGCFeed>("requestBatchLock");
        private readonly List<MarketDataSubscription> requestBatch = new List<MarketDataSubscription>();
        private const int RequestBatchTimerInterval = 250;

        #endregion

        #region .ctor

        public CQGCFeed(
            CQGCAdapter adapter,
            CQGCInstrumentResolver instrumentResolver)
        {
            this.adapter = adapter;
            this.instrumentResolver = instrumentResolver;

            adapter.MarketDataResolved += MarketDataResolved;
            adapter.RealTimeMarketDataReceived += RealTimeMarketDataReceived;
            adapter.MarketDataSubscriptionStatusReceived += MarketDataSubscriptionStatusReceived;
            adapter.ContractMetadataReceived += ContractMetadataReceived;

            requestBatchTimer = new Timer(_ => SendBatchRequest(), null, 0, RequestBatchTimerInterval);
        }

        #endregion

        #region IFeed

        /// <summary>
        ///     Транслировать ли ошибки в виде ErrorMessage
        /// </summary>
        public bool SendErrorMessages { get; set; }

        /// <summary>
        ///     Вызывается при получении сообщения из фида.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private void OnMessageReceived(Message message)
        {
            var handler = MessageReceived;
            if (handler != null)
            {
                handler(this, new MessageReceivedEventArgs(message));
            }
        }

        public void Dispose() { }

        #endregion

        #region Подписка на параметры инструмента

        /// <summary>
        ///     Подписаться на инструмент.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public Task<SubscriptionResult> Subscribe(Instrument instrument) => SubscribeAsync(instrument, SubscriptionFlags.InstrumentParams);

        /// <summary>
        ///     Отписаться от инструмента.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public void Unsubscribe(Instrument instrument) => Unsubscribe(instrument, SubscriptionFlags.InstrumentParams);

        #endregion

        #region Подписка на стаканы

        /// <summary>
        ///     Подписаться на стакан по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public void SubscribeOrderBook(Instrument instrument)
        {
            SubscribeAsync(instrument, SubscriptionFlags.OrderBook).Ignore();
        }

        /// <summary>
        ///     Отписаться от стакана по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public void UnsubscribeOrderBook(Instrument instrument) => Unsubscribe(instrument, SubscriptionFlags.OrderBook);

        #endregion

        #region Подписка и отписка (обобщенная)

        private async Task<SubscriptionResult> SubscribeAsync(Instrument instrument, SubscriptionFlags flags)
        {
            using (LogManager.Scope())
            {
                InstrumentSubscription subscription = null;
                ContractMetadata metadata = null;

                var hasSubscriptionLock = false;
                try
                {
                    // Получаем параметры подписки на инструмент
                    using (subscriptionsLock.Lock())
                    {
                        subscriptionsByInstrument.TryGetValue(instrument, out subscription);
                    }

                    if (subscription == null)
                    {
                        // Получаем ID инструмента
                        var contractId = await instrumentResolver.GetContractIdAsync(instrument);
                        if (contractId == uint.MaxValue)
                        {
                            return SubscriptionResult.Error(instrument, "Symbol is not resolved in tree node for CQG");
                        }

                        using (subscriptionsLock.Lock())
                        {
                            if (!subscriptionsByInstrument.TryGetValue(instrument, out subscription))
                            {
                                if (!subscriptionsByContractId.TryGetValue(contractId, out subscription))
                                {
                                    subscription = new InstrumentSubscription(instrumentResolver, instrument, contractId);
                                    // Сразу захватываем блокировку
                                    subscription.AcquireFlagsLock();
                                    hasSubscriptionLock = true;
                                    subscriptionsByContractId.Add(contractId, subscription);

                                    contractMetadatas.TryGetValue(contractId, out metadata);
                                }

                                subscriptionsByInstrument.Add(instrument, subscription);
                            }
                        }
                    }
                    else
                    {
                        // Захватываем блокировку
                        subscription.AcquireFlagsLock();
                        hasSubscriptionLock = true;
                    }

                    // Подписываемся на инструмент с учетом флагов подписки
                    if ((subscription.Flags & flags) != flags)
                    {
                        // Нужные флаги подписки не проставлены, требуется доподписаться
                        MarketDataSubscription.Level? level = null;
                        switch (subscription.Flags | flags)
                        {
                            case SubscriptionFlags.InstrumentParams:
                                level = MarketDataSubscription.Level.TRADES_BBA_VOLUMES;
                                break;
                            case SubscriptionFlags.OrderBook:
                                level = MarketDataSubscription.Level.TRADES_BBA_DOM;
                                break;
                            case SubscriptionFlags.InstrumentParams | SubscriptionFlags.OrderBook:
                                level = MarketDataSubscription.Level.TRADES_BBA_DOM;
                                break;
                        }

                        if (level != null)
                        {
                            subscription.Flags |= flags;
                            RequestMarketDataSubscription(subscription, level.Value);
                        }
                    }

                    // При необходимости обрабатываем метаданные и выбрасываем события
                    if (metadata != null)
                    {
                        Process(subscription, metadata, (s, data) => s.Handle(data));
                    }

                    // Готово
                    var result = SubscriptionResult.OK(instrument);
                    return result;
                }
                catch (OperationCanceledException)
                {
                    _Log.Warn().Print($"Unable to subscribe to {instrument} (operation has been cancelled)");
                    return SubscriptionResult.Error(instrument, "Operation has been cancelled");
                }
                catch (Exception e)
                {
                    _Log.Error().Print(e, $"Unable to subscribe to {instrument}");
                    return SubscriptionResult.Error(instrument, e.Message);
                }
                finally
                {
                    if (subscription != null && hasSubscriptionLock)
                    {
                        subscription.ReleaseFlagsLock();
                    }
                }
            }
        }

        private void Unsubscribe(Instrument instrument, SubscriptionFlags flags)
        {
            InstrumentSubscription subscription;
            using (subscriptionsLock.Lock())
            {
                if (!subscriptionsByInstrument.TryGetValue(instrument, out subscription))
                {
                    // Подписки и не было
                    return;
                }
            }

            try
            {
                subscription.AcquireFlagsLock();

                // Отписываемся от инструмента с учетом флагов подписки
                if ((subscription.Flags & flags) == flags)
                {
                    // Нужные флаги подписки проставлены, требуется отписаться
                    var level = MarketDataSubscription.Level.NONE;
                    switch (subscription.Flags & ~flags)
                    {
                        case SubscriptionFlags.InstrumentParams:
                            level = MarketDataSubscription.Level.TRADES_BBA_VOLUMES;
                            break;
                        case SubscriptionFlags.OrderBook:
                            level = MarketDataSubscription.Level.TRADES_BBA_DOM;
                            break;
                        case SubscriptionFlags.InstrumentParams | SubscriptionFlags.OrderBook:
                            level = MarketDataSubscription.Level.TRADES_BBA_DOM;
                            break;
                    }

                    subscription.Flags &= ~flags;
                    RequestMarketDataSubscription(subscription, level);

                    if (subscription.Flags == SubscriptionFlags.None)
                    {
                        // Полностью выкидываем подписку
                        using (subscriptionsLock.Lock())
                        {
                            subscriptionsByInstrument.Remove(subscription.Instrument);
                            subscriptionsByContractId.Remove(subscription.ContractId);
                        }
                    }
                }
            }
            finally
            {
                subscription.ReleaseFlagsLock();
            }
        }

        private void RequestMarketDataSubscription(InstrumentSubscription subscription, MarketDataSubscription.Level level)
        {
            var marketDataSubscription = new MarketDataSubscription
            {
                contract_id = subscription.ContractId,
                level = (uint)level
            };

            subscription.StartWaitForData(level);
            using (requestBatchLock.Lock())
            {
                requestBatch.Add(marketDataSubscription);
            }
        }

        private void SendBatchRequest()
        {
            List<MarketDataSubscription> requests;
            using (requestBatchLock.Lock())
            {
                if (requestBatch.Count == 0)
                {
                    return;
                }

                requests = requestBatch.ToList();
                requestBatch.Clear();
            }

            adapter.SendMessage(requests);
        }

        #endregion

        #region Обработка событий

        private void MarketDataResolved(AdapterEventArgs<InformationReport> args)
        {
            var isHandled = Process(
                args.Message.symbol_resolution_report.contract_metadata.contract_id,
                args.Message,
                (subscription, data) => subscription.Handle(data.symbol_resolution_report.contract_metadata)
                );
            if (isHandled)
            {
                args.MarkHandled();
            }
        }

        private void RealTimeMarketDataReceived(AdapterEventArgs<RealTimeMarketData> args)
        {
            var isHandled = Process(
                args.Message.contract_id,
                 args.Message,
                (subscription, data) => subscription.Handle(data)
                );
            if (isHandled)
            {
                args.MarkHandled();
            }
        }

        private static void MarketDataSubscriptionStatusReceived(AdapterEventArgs<MarketDataSubscriptionStatus> args)
        {
            args.MarkHandled();

            _Log.Debug().Print(
                $"Subscription status for contract #{args.Message.contract_id}: Level={1}, Status={2}. {3}",
                LogFields.Level((MarketDataSubscription.Level)args.Message.level),
                LogFields.Status((MarketDataSubscriptionStatus.StatusCode)args.Message.status_code),
                LogFields.Message(args.Message.text_message)
                );
        }

        private void ContractMetadataReceived(AdapterEventArgs<ContractMetadata> args)
        {
            using (subscriptionsLock.Lock())
            {
                contractMetadatas[args.Message.contract_id] = args.Message;
            }

            var isHandled = Process(
                args.Message.contract_id,
                args.Message,
                (subscription, data) => subscription.Handle(data)
                );
            if (isHandled)
            {
                args.MarkHandled();
            }
        }

        private void Process<T>(
            InstrumentSubscription subscription,
            T message,
            Func<InstrumentSubscription, T, SendMessageFlags> handler)
        {
            var result = handler(subscription, message);

            if ((result & SendMessageFlags.InstrumentParams) == SendMessageFlags.InstrumentParams)
            {
                OnMessageReceived(subscription.InstrumentParams);
            }

            if ((result & SendMessageFlags.OrderBook) == SendMessageFlags.OrderBook)
            {
                OnMessageReceived(subscription.OrderBook);
            }
        }

        private bool Process<T>(
            uint contractId,
            T message,
            Func<InstrumentSubscription, T, SendMessageFlags> handler)
        {
            InstrumentSubscription subscription;
            using (subscriptionsLock.Lock())
            {
                if (!subscriptionsByContractId.TryGetValue(contractId, out subscription))
                {
                    return false;
                }
            }

            Process(subscription, message, handler);

            return true;
        }

        #endregion
    }
}

