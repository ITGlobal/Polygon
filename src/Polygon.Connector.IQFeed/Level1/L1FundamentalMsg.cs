using System;
using System.Diagnostics.CodeAnalysis;

namespace Polygon.Connector.IQFeed.Level1
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct L1FundamentalMsg
    {
        private const int FIELD_SYMBOL = 0;
        private const int FIELD_DECIMAL_PLACES = 39;

        public string Symbol;
        public uint DecimalPlaces;
        public decimal PriceStep;
        public decimal? PriceStepValue;

        public static void Parse(IQMessageArgs args, out L1FundamentalMsg msg)
        {
            msg = new L1FundamentalMsg();

            var fields = args.Message.Split(',');
            msg.Symbol = fields[FIELD_SYMBOL];
            msg.DecimalPlaces = IQFeedParser.ParseUint(fields[FIELD_DECIMAL_PLACES]);
            msg.PriceStep = (decimal)(1.0 / (Math.Pow(10, msg.DecimalPlaces)));
            msg.PriceStepValue = null; // TODO
        }
    }
}

