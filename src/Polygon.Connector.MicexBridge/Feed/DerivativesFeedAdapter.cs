using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;
using Polygon.Messages;

namespace Polygon.Connector.MicexBridge.Feed
{
	public class DerivativesFeedAdapter : MicexSectionFeedAdapter
	{
        public override MicexSecionType SecionType
        {
            get { return MicexSecionType.Derivatives; }
        }

        protected internal DerivativesFeedAdapter(IEnumerable<TableType> tableTypes)
            : base(tableTypes)
		{
		}

		public override int GetDecimals(MTERow row)
		{
			return (int)Math.Pow(10, row.GetInt(19));
		}

		public override string ClassCode(MTERow row)
		{
			return "FOB";
		}

		public override void UpdateInstrumentParams(MTERow row, InstrumentParams instrumentParamsToUpdate)
		{
			for (byte i = 0; i < row.FieldNumbers.Length; ++i)
			{
				var decimals = (int)instrumentParamsToUpdate.DecimalPlaces;

				switch (row.FieldNumbers[i])
				{
					case 23:
                        // TODO UnderlyingCode?
                        //instrumentParamsToUpdate.UnderlyingCode = row.FieldData[i];
						break;
					case 9:
                        // TODO ExpirationDate
                        //instrumentParamsToUpdate.ExpirationDate = row.GetDateTimeDirect(i);
						//info.ExpireDaysLeft = (short)(row.GetDateTimeDirect(i) - DateTime.Now).Days;
						break;

					case 12:
						instrumentParamsToUpdate.PriceStep = row.GetDecimalDirect(i, decimals);
						//info.PriceStep = (float)row.GetDoubleDirect(i, info.Decimals);
						break;

					case 44:
						instrumentParamsToUpdate.Settlement = row.GetDecimalDirect(i, decimals);
						//info.SettlementPrice = row.GetDecimalDirect(i, info.Decimals);
						break;

					case 37:
						instrumentParamsToUpdate.LastPrice = row.GetDecimalDirect(i, decimals);
						//info.LastPrice = row.GetDecimalDirect(i, info.Decimals);
						break;

					case 40:
                        // TODO LastChangeTime
                        //instrumentParamsToUpdate.LastChangeTime = DateTime.Today + row.GetTimeSpanDirect(i);
						//info.LastTime = MMVBGate.NowDate + row.GetTimeSpanDirect(i);
						break;

					case 28:
						instrumentParamsToUpdate.BestBidPrice = row.GetDecimalDirect(i, decimals);
						//info.Bid = row.GetDecimalDirect(i, info.Decimals);
						break;

					case 29:
						instrumentParamsToUpdate.BestBidQuantity = row.GetLongDirect(i);
						//info.BidNumber = row.GetIntDirect(i);
						break;

					case 32:
						instrumentParamsToUpdate.BestOfferPrice = row.GetDecimalDirect(i, decimals);
						//info.Offer = row.GetDecimalDirect(i, info.Decimals);
						break;

					case 33:
						instrumentParamsToUpdate.BestOfferQuantity = row.GetLongDirect(i);
						//info.OfferNumber = row.GetIntDirect(i);
						break;
				}
			}
		}

		public override string InfoTableParams
		{
			get
			{
				return string.Empty;
			}
		}
	}
}