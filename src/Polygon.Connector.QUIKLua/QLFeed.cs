using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Connector;
using Polygon.Messages;
using Polygon.Connector.QUIKLua.Adapter;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua
{
    internal sealed class QLFeed : Feed, IOrderBookSubscriber, IInstrumentParamsSubscriber, IFortsDataProvider, IInstrumentTickerLookup
    {
        #region Fields

        private readonly QLAdapter adapter;

        private readonly ILockObject fullCodesLock = DeadlockMonitor.Cookie<QLFeed>("fullCodesLock");
        private readonly Dictionary<Instrument, string> fullCodes = new Dictionary<Instrument, string>();

        #endregion

        #region .ctor

        public QLFeed(QLAdapter adapter)
        {
            this.adapter = adapter;
        }

        #endregion

        #region GatewayService

        /// <summary>
        ///   Запускает сервис.
        /// </summary>
        public override void Start()
        {
            adapter.MessageReceived += adapter_MessageReceived;
        }

        /// <summary>
        ///   Останавливает сервис.
        /// </summary>
        public override void Stop()
        {
            adapter.MessageReceived -= adapter_MessageReceived;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Обработка сообщения от адаптера квика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adapter_MessageReceived(object sender, QLMessageEventArgs e)
        {
            switch (e.Message.message_type)
            {
                case QLMessageType.InstrumentParams:
                    Handle((QLInstrumentParams)e.Message);
                    break;
                case QLMessageType.OrderBook:
                    Handle((QLOrderBook)e.Message);
                    break;
            }
        }

        private void Handle(QLOrderBook message)
        {
            try
            {
                var ob = new OrderBook { Instrument = new Instrument { Code = message.instrument } };

                if (message.offers != null)
                {
                    message.offers.ForEach(_ => ob.Items.Add(new OrderBookItem
                    {
                        Price = _.p,
                        Quantity = _.q,
                        Operation = OrderOperation.Sell
                    }));
                }

                if (message.bids != null)
                {
                    message.bids.ForEach(_ => ob.Items.Add(new OrderBookItem
                    {
                        Price = _.p,
                        Quantity = _.q,
                        Operation = OrderOperation.Buy
                    }));
                }

                OnMessageReceived(ob);
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to handle {message}");
            }
        }

        private void Handle(QLInstrumentParams message)
        {
            try
            {
                decimal pricemin = 0, pricemax = 0;
                decimal.TryParse(message.pricemin, out pricemin);
                decimal.TryParse(message.pricemax, out pricemax);

                // цены шага
                var stepPrices = new[]
                    {message.stepPriceT, message.stepPrice, message.stepPriceCl, message.stepPricePrCl};

                // первая ненулевая цена шага
                var stepPriceValue = stepPrices.FirstOrDefault(x => x != 0);

                // проверить утверждение: любая цена шага равна либо stepPriceValue, либо 0
                //Debug.Assert(stepPrices.All(x => x == stepPriceValue || x == 0));
                // NOTE выяснилось, что утверждение иногда не выполняется. Замечен случай с BRM6 stepPricePrCl=stepPrice=stepPriceT=6.61737, stepPriceCl=6.63015

                var ip = new InstrumentParams
                {
                    Instrument = new Instrument { Code = message.code },
                    BestBidPrice = message.bid,
                    LastPrice = message.last,
                    LotSize = message.lotsize,
                    BestOfferPrice = message.offer,
                    Vola = message.volatility,
                    BestBidQuantity = message.bidQuantity,
                    BestOfferQuantity = message.offerQuantity,
                    PriceStep = message.priceStep,
                    PriceStepValue = stepPriceValue,
                    Settlement = message.settlement,
                    PreviousSettlement = message.previousSettlement,
                    BottomPriceLimit = pricemin,
                    TopPriceLimit = pricemax,
                    VolaTranslatedByFeed = true,
                    SessionEndTime = message.endTime,
                    OpenInterest = message.openinterest
                };

                OnMessageReceived(ip);

                using (fullCodesLock.Lock())
                {
                    fullCodes[ip.Instrument] = message.fullCode;
                }
            }
            catch (Exception e)
            {
                Logger.Error().Print(e, $"Failed to handle {message}");
            }
        }

        #endregion

        #region IOrderBookSubscriber

        /// <summary>
        ///     Подписаться на стакан по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public async void SubscribeOrderBook(Instrument instrument)
        {
            Instrument transportInstrument = await adapter.InstrumentConverter.ResolveTransportInstrumentAsync(instrument);
            adapter.SendMessage(new QLOrderBookSubscriptionRequest(transportInstrument.Code));
        }

        /// <summary>
        ///     Отписаться от стакана по инструменту.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public async void UnsubscribeOrderBook(Instrument instrument)
        {
            Instrument transportInstrument = await adapter.InstrumentConverter.ResolveTransportInstrumentAsync(instrument);
            adapter.SendMessage(new QLOrderBookUnsubscriptionRequest(transportInstrument.Code));
        }

        #endregion

        #region IInstrumentParamsSubscriber

        /// <summary>
        ///     Подписаться на инструмент.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для подписки.
        /// </param>
        public async Task<SubscriptionResult> Subscribe(Instrument instrument)
        {
            Instrument transportInstrument = await adapter.InstrumentConverter.ResolveTransportInstrumentAsync(instrument);
            SubscriptionResult result;
            if (transportInstrument == null)
            {
                // может вернуться null если подключить внешний конвертер и передать ему из прикладного приложения неопозаваемый инструмент
                result = SubscriptionResult.Error(instrument);
            }
            else
            {
                adapter.SendMessage(new QLInstrumentParamsSubscriptionRequest(transportInstrument.Code));
                result = SubscriptionResult.OK(instrument);
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        ///     Отписаться от инструмента.
        /// </summary>
        /// <param name="instrument">
        ///     Инструмент для отписки.
        /// </param>
        public async void Unsubscribe(Instrument instrument)
        {
            Instrument transportInstrument = await adapter.InstrumentConverter.ResolveTransportInstrumentAsync(instrument);
            adapter.SendMessage(new QLInstrumentParamsUnsubscriptionRequest(transportInstrument.Code));
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
        public string QueryFullCode(Instrument instrument)
        {
            using (fullCodesLock.Lock())
            {
                string fullCode;
                if (!fullCodes.TryGetValue(instrument, out fullCode))
                {
                    return null;
                }

                return fullCode;
            }
        }

        #endregion

        #region IInstrumentTickerLookup

        /// <summary>
        ///     Поиск тикеров по (частичному) коду
        /// </summary>
        public string[] LookupInstruments(string code, int maxResults = 10)
        {
            string[] results;
            using (fullCodesLock.Lock())
            {
                results = fullCodes
                    .Where(_ => _.Key.Code.IndexOf(code, StringComparison.OrdinalIgnoreCase) >= 0)
                    .Select(_ => _.Key.Code)
                    .ToArray();
            }

            var orderedResults = results
                .OrderBy(result =>
                {
                    var distance = LevenshteinDistance(result, code);
                    var relevance = 1d / (1d + distance);
                    return relevance;
                })
                .Take(maxResults)
                .ToArray();
            return orderedResults;

            // Расчет расстояния Левенштейна для пары строк
            // См. http://stackoverflow.com/a/6944095
            int LevenshteinDistance(string s, string t)
            {
                if (string.IsNullOrEmpty(s))
                {
                    if (string.IsNullOrEmpty(t))
                    {
                        return 0;
                    }

                    return t.Length;
                }

                if (string.IsNullOrEmpty(t))
                {
                    return s.Length;
                }

                var n = s.Length;
                var m = t.Length;
                var d = new int[n + 1, m + 1];

                // initialize the top and right of the table to 0, 1, 2, ...
                for (var i = 0; i <= n; d[i, 0] = i++) { }
                for (var j = 1; j <= m; d[0, j] = j++) { }

                for (var i = 1; i <= n; i++)

                {
                    for (var j = 1; j <= m; j++)
                    {
                        var cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                        var min1 = d[i - 1, j] + 1;
                        var min2 = d[i, j - 1] + 1;
                        var min3 = d[i - 1, j - 1] + cost;
                        d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                    }
                }

                return d[n, m];
            }
        }

        #endregion
    }
}

