using System.Diagnostics.CodeAnalysis;

namespace Polygon.Connector.IQFeed.Level1
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct L1UpdateMsg
    {
        public const string FIELD_SYMBOL = "Symbol";
        public const string FIELD_BID = "Bid";
        public const string FIELD_BID_SIZE = "Bid Size";
        public const string FIELD_ASK = "Ask";
        public const string FIELD_ASK_SIZE = "Ask Size";
        public const string FIELD_LAST = "Last";
        public const string FIELD_SETTLE = "Settle";
        public const string FIELD_EXTENDED_TRADE = "Extended Trade";
        public const string FIELD_EXTENDED_TRADING_CHANGE = "Extended Trading Change";

        public string Symbol;
        public decimal BestBidPrice;
        public long BestBidQuantity;
        public decimal BestOfferPrice;
        public long BestOfferQuantity;
        public decimal LastPrice;
        public decimal Settlement;
        public decimal PreviousSettlement;

        public static bool TryParse(IQMessageArgs args, L1FieldIndex index, out L1UpdateMsg msg)
        {
            string rawValue;
            msg = new L1UpdateMsg();
            var fields = args.Message.Split(',');

            #region Symbol
            if (!index.TryGetField(fields, FIELD_SYMBOL, out msg.Symbol))
            {
                return false;
            }
            #endregion

            #region BestBidPrice
            if (!index.TryGetField(fields, FIELD_BID, out rawValue))
            {
                return false;
            }
            msg.BestBidPrice = IQFeedParser.ParseDecimal(rawValue);
            #endregion

            #region BestOfferPrice
            if (!index.TryGetField(fields, FIELD_ASK, out rawValue))
            {
                return false;
            }
            msg.BestOfferPrice = IQFeedParser.ParseDecimal(rawValue);
            #endregion

            #region BestBidQuantity
            if (!index.TryGetField(fields, FIELD_BID_SIZE, out rawValue))
            {
                return false;
            }
            msg.BestBidQuantity = IQFeedParser.ParseLong(rawValue);
            #endregion

            #region BestOfferQuantity
            if (!index.TryGetField(fields, FIELD_ASK_SIZE, out rawValue))
            {
                return false;
            }
            msg.BestOfferQuantity = IQFeedParser.ParseLong(rawValue);
            #endregion

            #region LastPrice
            if (!index.TryGetField(fields, FIELD_LAST, out rawValue))
            {
                return false;
            }
            msg.LastPrice = IQFeedParser.ParseDecimal(rawValue);
            #endregion

            //msg.LastChangeTime = fields[16];
            
            #region Settlement

            if (!index.TryGetField(fields, FIELD_SETTLE, out rawValue))
            {
                return false;
            }

            msg.Settlement = IQFeedParser.ParseDecimal(rawValue);

            #endregion

            #region PreviousSettlement

            if (!index.TryGetField(fields, FIELD_EXTENDED_TRADE, out rawValue))
            {
                return false;
            }
            var extendedTrade = IQFeedParser.ParseDecimal(rawValue);

            if (!index.TryGetField(fields, FIELD_EXTENDED_TRADING_CHANGE, out rawValue))
            {
                return false;
            }
            var extendedTradingChange = IQFeedParser.ParseDecimal(rawValue);

            // IQFeed API не выставляет наружу PreviousSettlement, но в документации есть два интересных поля:
            // | 37 | Extended Trading Change | float | IQFeed 4.9 | Extended Trade (field 76) minus Yesterday's close                                | Calculated by IQConnect.exe                         |
            // | 76 | Extended Trade          | float | IQFeed 5.0 | Price of the most recent extended trade (last qualified trades + Form T trades). | Provided by the exchange or 3rd party data provider |
            // Зная эти два поля, можно посчитать Yesterday's close, худо-бедно сойдет
            msg.PreviousSettlement = extendedTrade - extendedTradingChange;

            #endregion

            //msg.BottomPriceLimit
            //msg.TopPriceLimit

            //msg.Vola = ParseDecimal(fields[43]) * 100;

            //msg.Go
            //msg.LotSize
            //msg.PremiumStyle
            //msg.TheorPrice

            return true;
        }
    }
}

