using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;
using Polygon.Messages;

namespace Polygon.Connector.MicexBridge.Feed
{
    public class CurrencyFeedAdapter : MicexSectionFeedAdapter
    {
        public override MicexSecionType SecionType
        {
            get { return MicexSecionType.Currency; }
        }

        protected internal CurrencyFeedAdapter(IEnumerable<TableType> tableTypes) : base(tableTypes)
		{
		}

        public override string InfoTableParams
        {
            get
            {
              //return "        ";
              return "CURRCETS";
            }
        }

        public override string QuoteTableParams
        {
            get
            {
                return "                  ";
            }
        }

        public override string ClassCode(MTERow row)
        {
            return row[0];
        }

        public override int GetDecimals(MTERow row)
        {
            return (int)Math.Pow(10, row.GetInt(14));
        }

        public override void UpdateInstrumentParams(MTERow row, InstrumentParams instrumentParamsToUpdate)
        {
            for (byte i = 0; i < row.FieldNumbers.Length; ++i)
            {
                var decimals = (int)instrumentParamsToUpdate.DecimalPlaces;

                switch (row.FieldNumbers[i])
                {
                    case 44:
                        // TODO ExpirationDate?
                        //instrumentParamsToUpdate.ExpirationDate = row.GetDateTimeDirect(i);
                        break;

                    case 10:
                        instrumentParamsToUpdate.PriceStep = row.GetDecimalDirect(i, decimals);
                        break;

                    case 28:
                        instrumentParamsToUpdate.LastPrice = row.GetDecimalDirect(i, decimals);
                        break;

                    case 31:
                        // TODO LastChangeTime ?
                        //instrumentParamsToUpdate.LastChangeTime = DateTime.Today + row.GetTimeSpanDirect(i);
                        break;

                    case 17:
                        instrumentParamsToUpdate.BestBidPrice = row.GetDecimalDirect(i, decimals);
                        break;

                    case 18:
                        instrumentParamsToUpdate.BestBidQuantity = row.GetLongDirect(i);
                        break;

                    case 21:
                        instrumentParamsToUpdate.BestOfferPrice = row.GetDecimalDirect(i, decimals);
                        break;

                    case 22:
                        instrumentParamsToUpdate.BestOfferQuantity = row.GetLongDirect(i);
                        break;
                }
            }
        }
    }
}
