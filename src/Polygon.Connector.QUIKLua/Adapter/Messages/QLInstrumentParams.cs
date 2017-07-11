using System;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLInstrumentParams)]
    internal class QLInstrumentParams : QLMessage
    {
        public string code { get; set; }
        public string classCode { get; set; }
        public string fullCode { get; set; }
        public decimal bid { get; set; }
        public int bidQuantity { get; set; }
        public decimal offer { get; set; }
        public int offerQuantity { get; set; }
        public decimal last { get; set; }
        public decimal priceStep { get; set; }
        public string time { get; set; }
        public decimal strike { get; set; }
        public decimal volatility { get; set; }
        public int lotsize { get; set; }
        public string optiontype { get; set; }
        public string optionbase { get; set; }
        public string pricemax { get; set; }
        public string pricemin { get; set; }
        public decimal settlement { get; set; }
        public int openinterest { get; set; }
        public decimal previousSettlement { get; set; }

        /// <summary>
        /// Стоимость шага цены
        /// </summary>
        public decimal stepPriceT { get; set; }

        /// <summary>
        /// Стоимость шага цены (для новых контрактов FORTS и RTS Standard)
        /// </summary>
        public decimal stepPrice { get; set; }

        /// <summary>
        /// Стоимость шага цены для клиринга
        /// </summary>
        public decimal stepPriceCl { get; set; }

        /// <summary>
        /// Стоимость шага цены для промклиринга
        /// </summary>
        public decimal stepPricePrCl { get; set; }

        /// <summary>
        /// Окончание основной сессии
        /// </summary>
        public TimeSpan endTime { get; set; } = TimeSpan.FromDays(1);
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Code, code);
            fmt.AddField(LogFieldNames.ClassCode, classCode);
            fmt.AddField(LogFieldNames.FullCode, fullCode);
            fmt.AddField(LogFieldNames.BestBidPrice, bid);
            fmt.AddField(LogFieldNames.BestBidQuantity, bidQuantity);
            fmt.AddField(LogFieldNames.BestOfferPrice, offer);
            fmt.AddField(LogFieldNames.BestOfferQuantity, offerQuantity);
            fmt.AddField(LogFieldNames.LastPrice, last);
            fmt.AddField(LogFieldNames.PriceStepValue, priceStep);
            fmt.AddField(LogFieldNames.Time, time);
            fmt.AddField(LogFieldNames.Strike, strike);
            fmt.AddField(LogFieldNames.Vola, volatility);
            fmt.AddField(LogFieldNames.LotSize, lotsize);
            fmt.AddField(LogFieldNames.Type, optiontype);
            fmt.AddField(LogFieldNames.OptionBase, optionbase);
            fmt.AddField(LogFieldNames.TopPriceLimit, pricemax);
            fmt.AddField(LogFieldNames.BottomPriceLimit, pricemin);
            fmt.AddField(LogFieldNames.Settlement, settlement);
            fmt.AddField(LogFieldNames.OpenInterest, openinterest);
            fmt.AddField(LogFieldNames.PreviousSettlement, previousSettlement);
            fmt.AddField(LogFieldNames.StepPriceT, stepPriceT);
            fmt.AddField(LogFieldNames.StepPrice, stepPrice);
            fmt.AddField(LogFieldNames.StepPriceCl, stepPriceCl);
            fmt.AddField(LogFieldNames.StepPricePrCl, stepPricePrCl);
            fmt.AddField(LogFieldNames.StepPricePrCl, stepPricePrCl);
            fmt.AddField(LogFieldNames.EndTime, endTime);
            return fmt.ToString();
        }
    }
}

