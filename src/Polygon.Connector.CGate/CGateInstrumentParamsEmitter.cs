using System;
using System.Collections.Generic;
using System.Linq;
using ITGlobal.DeadlockDetection;
using Polygon.Diagnostics;
using Polygon.Messages;
using JetBrains.Annotations;

namespace Polygon.Connector.CGate
{
    /// <summary>
    /// В cgate параметры инструментов приходят в 5-и типах сообщений. Два для опционов и два для фьючерсов и поток волатильности. Каждую пару нужно 
    /// склеить в один объект InstrumentParams и приделать к ней волатильность. Этот класс потребляет все 5 типов сообщений cgate и на выход выдаёт готовый IP.
    /// </summary>
    internal class CGateInstrumentParamsEmitter
    {
        #region Fields

        private readonly IRwLockObject containerLock = DeadlockMonitor.ReaderWriterLock(typeof(CGateInstrumentParamsEmitter), "containerLock");

        /// <summary>
        /// Словарь isin id на InstrumentParams. Мэпим именно на isni id (int), а не на строковый isin.
        /// </summary>
        private readonly Dictionary<int, InstrumentParams> mapIsinId2InstrumentParams = new Dictionary<int, InstrumentParams>();

        private readonly CGateInstrumentResolver instrumentResolver;
        private TimeSpan sessionEndTime;

        #endregion

        #region .ctor

        public CGateInstrumentParamsEmitter(CGateInstrumentResolver instrumentResolver)
        {
            this.instrumentResolver = instrumentResolver;
            sessionEndTime = TimeSpan.FromDays(1);
        }

        #endregion

        #region Public methods

        public IEnumerable<InstrumentParams> SetSessionEndTime(TimeSpan endTime)
        {
            if (sessionEndTime == endTime) yield break;
            sessionEndTime = endTime;

            // обновить параметры всех инструментов
            InstrumentParams[] ips;
            using (containerLock.ReadLock())
            {
                ips = mapIsinId2InstrumentParams.Values
                    .Where(ip => ip.SessionEndTime != sessionEndTime)
                    .ToArray();
            }
            foreach (var ip in ips)
            {
                ip.SessionEndTime = sessionEndTime;
                yield return ip.Clone();
            }
        }

        public InstrumentParams GetUpdatedInstrumentParams(CGateAdapter.Messages.FutCommon.CgmCommon message)
        {
            var ip = GetUpdatedInstrumentParams(message.IsinId);
            if (ip == null)
            {
                return null;
            }

            ip.BestOfferPrice = (decimal)message.BestSell;
            ip.BestBidPrice = (decimal)message.BestBuy;
            ip.BestOfferQuantity = message.AmountSell;
            ip.BestBidQuantity = message.AmountBuy;
            ip.LastPrice = (decimal)message.Price; //last
            // message.Amount;//last qty
            // message.deal_time;//last time
            // message.max_price;//
            // message.min_price;
            // message.open_price;

            if (string.IsNullOrEmpty(ip.Instrument.Code))
            {
                return null;
            }

            return ip.Clone();
        }

        public InstrumentParams GetUpdatedInstrumentParams(CGateAdapter.Messages.FutInfo.CgmFutSessContents message)
        {
            var ip = GetUpdatedInstrumentParams(message.IsinId);
            if (ip == null)
            {
                return null;
            }

            // цены шага
            var stepPrices = new[] { message.StepPrice, message.StepPriceClr, message.StepPriceInterclr };

            // первая положительная цена шага
            var stepPriceValue = stepPrices.FirstOrDefault(x => x > 0);

            ip.TopPriceLimit = (decimal)message.LimitUp;
            ip.BottomPriceLimit = (decimal)message.LimitDown;
            //message.buy_deposit;
            //message.sell_deposit;
            ip.Settlement = (decimal)message.OldKotir; // скорректированный сэтлмент прошлой сессии
            ip.PriceStep = (decimal)message.MinStep;
            ip.PriceStepValue = (decimal)stepPriceValue;
            ip.DecimalPlaces = (uint)message.Roundto;
            ip.LotSize = message.LotVolume;
            // message.step_price; // стоимость шага цены
            // message.d_exp; // дата экспирации

            return ip.Clone();
        }

        public InstrumentParams GetUpdatedInstrumentParams(CGateAdapter.Messages.OptCommon.CgmCommon message)
        {
            var ip = GetUpdatedInstrumentParams(message.IsinId);
            if (ip == null)
            {
                return null;
            }

            ip.BestOfferPrice = (decimal)message.BestSell;
            ip.BestBidPrice = (decimal)message.BestBuy;
            ip.BestOfferQuantity = message.AmountSell;
            ip.BestBidQuantity = message.AmountBuy;
            ip.LastPrice = (decimal)message.Price; //last
            // message.Amount;//last qty
            // message.deal_time;//last time
            // message.max_price;//
            // message.min_price;
            // message.open_price;

            if (string.IsNullOrEmpty(ip.Instrument.Code))
            {
                return null;
            }

            return ip.Clone();
        }

        public InstrumentParams GetUpdatedInstrumentParams(CGateAdapter.Messages.OptInfo.CgmOptSessContents message)
        {
            var ip = GetUpdatedInstrumentParams(message.IsinId);
            if (ip == null)
            {
                return null;
            }

            ip.TopPriceLimit = (decimal)message.LimitUp;
            ip.BottomPriceLimit = (decimal)message.LimitDown;
            //message.buy_deposit;
            //message.sell_deposit;
            ip.Settlement = (decimal)message.OldKotir; // скорректированный сэтлмент прошлой сессии
            ip.PriceStep = (decimal)message.MinStep;
            ip.PriceStepValue = (decimal)message.StepPrice;
            ip.DecimalPlaces = (uint)message.Roundto;
            ip.LotSize = message.LotVolume;
            // message.step_price; // стоимость шага цены
            // message.d_exp; // дата экспирации

            return ip.Clone();
        }

        public InstrumentParams GetUpdatedInstrumentParams(CGateAdapter.Messages.Volat.CgmVolat message)
        {
            var ip = GetUpdatedInstrumentParams(message.IsinId);
            if (ip == null)
            {
                return null;
            }

            ip.Vola = (decimal)message.Volat;

            if (string.IsNullOrEmpty(ip.Instrument.Code))
            {
                return null;
            }

            return ip.Clone();
        }

        public InstrumentParams GetInstrumentParams(int isinId)
        {
            var ip = GetUpdatedInstrumentParams(isinId);
            return ip?.Clone();
        }

        public InstrumentParams GetInstrumentParams(Instrument instrument)
        {
            InstrumentData data = instrumentResolver.GetCGateInstrumentData(instrument);
            var isinId = instrumentResolver.GetIsinIdByShortIsin(data.Symbol ?? "");
            if (isinId == int.MinValue)
            {
                return null;
            }

            return GetInstrumentParams(isinId);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Возвращает ранее созданный InstrumentParams из словаря или создаёт новый.
        /// </summary>
        [CanBeNull]
        private InstrumentParams GetUpdatedInstrumentParams(int isinId)
        {
            InstrumentParams ip = null;

            using (containerLock.UpgradableReadLock())
            {
                if (!mapIsinId2InstrumentParams.TryGetValue(isinId, out ip))
                {
                    using (containerLock.WriteLock())
                    {
                        var code = instrumentResolver.GetShortIsinByIsinId(isinId);
                        if (!string.IsNullOrEmpty(code))
                        {
                            InstrumentData data = instrumentResolver.GetInstrument(code);
                            if (!data.Equals(default(InstrumentData)))
                            {
                                ip = new InstrumentParams
                                {
                                    Instrument = data.TransportInstrument,
                                    VolaTranslatedByFeed = true,
                                    SessionEndTime = sessionEndTime
                                };
                            }
                        }

                        if (ip != null)
                        {
                            mapIsinId2InstrumentParams.Add(isinId, ip);
                        }
                    }
                }
            }

            return ip;
        }

        #endregion
    }
}

