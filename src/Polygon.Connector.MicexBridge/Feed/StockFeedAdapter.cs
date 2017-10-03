using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;
using Polygon.Messages;

namespace Polygon.Connector.MicexBridge.Feed
{
	public class StockFeedAdapter : MicexSectionFeedAdapter
	{
        public override MicexSecionType SecionType
        {
            get { return MicexSecionType.Stock; }
        }

        protected internal StockFeedAdapter(IEnumerable<TableType> tableTypes)
            : base(tableTypes)
		{
			var index = 0;
			foreach (var table in tableTypes)
			{
				if (table.Name == "INDEXES")
				{
					StockIndexesTableIndex = index;
					break;
				}
				index++;
			}
		}

		public int StockIndexesTableIndex { get; set; }

		public string GetIndexCode(MTERow row)
		{
			return row[1];
		}

		public decimal GetIndexValue(MTERow row, int decimals)
		{
			return row.GetDecimalDirect(2, decimals);
		}

		public TimeSpan GetIndexLastChangeTime(MTERow row)
		{
			return row.GetTimeSpan(8);
		}

		public override string ClassCode(MTERow row)
		{
			return row[0];
		}

		public override int GetDecimals(MTERow row)
		{
			return (int)Math.Pow(10, row.GetInt(15));
		}

		public override string InfoTableParams => "FOND    ";

	    public override void UpdateInstrumentParams(MTERow row, InstrumentParams instrumentParamsToUpdate)
		{
			for (byte i = 0; i < row.FieldNumbers.Length; ++i)
			{
				var decimals = (int)instrumentParamsToUpdate.DecimalPlaces;

				switch (row.FieldNumbers[i])
				{
					case 9:
						instrumentParamsToUpdate.LotSize = row.GetLongDirect(i);
						break;

					case 10:
						instrumentParamsToUpdate.PriceStep = (decimal)row.GetDoubleDirect(i, decimals);
						break;

					case 39:
						instrumentParamsToUpdate.BestBidPrice = row.GetDecimalDirect(i, decimals);
						break;

					case 40:
						instrumentParamsToUpdate.BestBidQuantity = row.GetLongDirect(i);
						break;

					case 43:
						instrumentParamsToUpdate.BestOfferPrice = row.GetDecimalDirect(i, decimals);
						break;

					case 44:
						instrumentParamsToUpdate.BestOfferQuantity = row.GetLongDirect(i);
						break;

					case 50:
						instrumentParamsToUpdate.LastPrice = row.GetDecimalDirect(i, decimals);
						break;

                    //case 14:
                    //    instrumentParamsToUpdate.PrevClosePrice = row.GetDecimalDirect(i, decimals);
                    //    break;

					case 53:
                        // TODO LastChangeTime?
                        //instrumentParamsToUpdate.LastChangeTime = DateTime.Today + row.GetTimeSpanDirect(i);
						break;

                    case 14:
                        // TODO PrevClosePrice?
                        //instrumentParamsToUpdate.PrevClosePrice = row.GetDecimalDirect(i, decimals);
                        break;

						//case 9:
						//    instrumentParamsToUpdate.ExpirationDate = row.GetDateTimeDirect(i);
						//    break;

						//case 12:
						//    instrumentParamsToUpdate.PriceStep = row.GetDecimalDirect(i, decimals);
						//    break;

						//case 44:
						//    instrumentParamsToUpdate.Settlement = row.GetDecimalDirect(i, decimals);
						//    break;

						//case 37:
						//    instrumentParamsToUpdate.LastPrice = row.GetDecimalDirect(i, decimals);
						//    break;

						//case 40:
						//    instrumentParamsToUpdate.LastChangeTime = DateTime.Now + row.GetTimeSpanDirect(i);
						//    break;

						//case 28:
						//    instrumentParamsToUpdate.BestBidPrice = row.GetDecimalDirect(i, decimals);
						//    break;

						//case 29:
						//    instrumentParamsToUpdate.BestBidQuantity = (uint)row.GetIntDirect(i);
						//    break;

						//case 32:
						//    instrumentParamsToUpdate.BestOfferPrice = row.GetDecimalDirect(i, decimals);
						//    break;

						//case 33:
						//    instrumentParamsToUpdate.BestOfferQuantity = (uint)row.GetIntDirect(i);
						//    break;
				}
			}
		}
	}
}