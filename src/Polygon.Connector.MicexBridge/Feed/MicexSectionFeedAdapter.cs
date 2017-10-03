using System;
using System.Collections.Generic;
using System.Linq;
using Polygon.Connector.MicexBridge.MTETypes;
using Polygon.Diagnostics;
using Polygon.Messages;

namespace Polygon.Connector.MicexBridge.Feed
{
	public abstract class MicexSectionFeedAdapter
	{
        #region Abstract

	    public abstract MicexSecionType SecionType { get; }

        #endregion

        #region Static

        public static MicexSectionFeedAdapter CreateAdapter(MicexSecionType section, IEnumerable<TableType> tableTypes)
        {
            // создаём адаптер, соответствующий секции ММВБ
            switch (section)
            {
                case MicexSecionType.Derivatives:
                    return new DerivativesFeedAdapter(tableTypes);
                case MicexSecionType.Stock:
                    return new StockFeedAdapter(tableTypes);
                case MicexSecionType.Currency:
                    return new CurrencyFeedAdapter(tableTypes);
            }

            throw new ArgumentException("Неверная секция", "section");
        }

        #endregion


		#region Private members

		protected ILog Logger;

		private readonly int infoTableIndex;

		private readonly int allDealsTableIndex;

		private readonly int quoteTableIndex;

		#endregion

		protected MicexSectionFeedAdapter(IEnumerable<TableType> tblTypes)
		{
			Logger = LogManager.GetLogger(GetType());

			var index = 0;
			foreach (var table in tblTypes)
			{
				switch (table.Name)
				{
					case "ALL_TRADES":
						allDealsTableIndex = index;
						break;
					case "SECURITIES":
						infoTableIndex = index;
						break;
					case "ORDERBOOK":
                    case "EXT_ORDERBOOK":
						quoteTableIndex = index;
						break;
				}

				index++;
			}
		}

		#region Public properties

        public abstract string InfoTableParams { get; }

        /// <summary>
        /// Входные параметры для открытия таблицы со стаканами
        /// </summary>
	    public virtual string QuoteTableParams => "                  ";

	    public int InfoTableIndex => infoTableIndex;

	    public int AllDealsTableIndex => allDealsTableIndex;

	    public int QuoteTableIndex => quoteTableIndex;

	    #endregion

		#region Public methods

		public string GetInstrumentCodeFromParams(MTERow row)
		{
			return row[1];
		}

		public abstract string ClassCode(MTERow row);

		public abstract int GetDecimals(MTERow row);

		public string GetInstrumentCodeFromBookRow(MTERow row)
		{
			return row[1];
		}

		public string GetOperationFromOrderBookRow(MTERow row)
		{
			return row[2];
		}

		public decimal GetPriceFromBookRow(MTERow row, int decimals)
		{
			return row.GetDecimal(3, decimals);
		}

		public int GetAmountFromBookRow(MTERow row)
		{
			return row.GetInt(4);
		}

		public abstract void UpdateInstrumentParams(MTERow row, InstrumentParams instrumentParamsToUpdate);

		public IEnumerable<Trade> GetTradesFromTable(MTETable table)
		{
			return from row in table.Rows
				   select new Trade
				   {
					   DateTime = DateTime.Today + row.GetTimeSpan(1),
					   Instrument = GetInstrumentFromRow(row),
					   Quantity = (uint)row.GetInt(5),
					   Price = row.GetDouble(4, 4)
				   };
		}

		/// <summary>
		/// Вызывается при обновлении стаканов в фиде.
		/// </summary>
		/// <param name="table">Таблица из фида.</param>
		/// <param name="instrumentsParams">Набор параметров инструментов.</param>
		/// <param name="orderBooks">Набор текущих стаканов.</param>
		/// <returns>Набор обновленных стаканов.</returns>
		public IEnumerable<OrderBook> GetOrderBookUpdatesFromTable(
			MTETable table,
			Dictionary<Instrument, InstrumentParams> instrumentsParams,
			Dictionary<Instrument, OrderBook> orderBooks)
		{
			if (table.Rows.Length == 0)
			{
				return null;
			}

			var updates = new Dictionary<Instrument, OrderBook>();

			Instrument oldInstrument = null;
			OrderBook oldBook = null;
			var oldDecimals = 0;

			foreach (var row in table.Rows)
			{
				var instrument = GetInstrumentFromRow(row);
				
				if (instrument != oldInstrument)
				{
					OrderBook orderBook;
					if (!orderBooks.TryGetValue(instrument, out orderBook))
					{
                        orderBooks[instrument] = orderBook = new OrderBook { Instrument = instrument, Items = new List<OrderBookItem>() };
                        //Logger.WarnFormat("Не удалось обновить стакан по инструменту {0}, т.к. он не был инициализирован.", instrument);
                        //continue;
					}

					if (!updates.ContainsKey(instrument))
					{
						orderBook.Items = new List<OrderBookItem>();
						updates[instrument] = orderBook;
					}
					oldInstrument = instrument;
					oldBook = orderBook;
					oldDecimals = (int)instrumentsParams[oldInstrument].DecimalPlaces;
				}

				AddOrderBookItem(row, oldBook, oldDecimals);
			}
			return updates.Values;
		}

		protected Instrument GetInstrumentFromRow(MTERow row)
		{
            // TODO classCode?
			return new Instrument { Code = GetInstrumentCodeFromBookRow(row)/*, ClassCode = ClassCode(row)*/ };
		}

		/// <summary>
		/// Вызывается при начале работы фида для инициализации всех стаканов.
		/// </summary>
		/// <param name="table">Таблица из фида.</param>
		/// <param name="instrumentsParams">Набор параметров инструментов.</param>
		/// <param name="orderBooks">Набор стаканов.</param>
		public void GetOrderBooksFromTable(
			MTETable table,
			Dictionary<Instrument, InstrumentParams> instrumentsParams,
			Dictionary<Instrument, OrderBook> orderBooks)
		{
			Instrument oldInstrument = null;
			OrderBook oldBook = null;
			var oldDecimals = 0;

			foreach (var row in table.Rows)
			{
				var instrument = GetInstrumentFromRow(row);
				if (instrument != oldInstrument)
				{
					InstrumentParams instrumentParams;
					if (!instrumentsParams.TryGetValue(instrument, out instrumentParams))
					{
						Logger.Warn().Print($"Не удалось добавить подписку на инструмент {instrument}, т.к. не обнаружены его параметры.");
						continue;
					}
					OrderBook orderBook;
					if (!orderBooks.TryGetValue(instrument, out orderBook))
					{
						orderBooks[instrument] = orderBook = new OrderBook { Instrument = instrument, Items = new List<OrderBookItem>() };
					}
					oldInstrument = instrument;
					oldBook = orderBook;
					oldDecimals = (int)instrumentsParams[oldInstrument].DecimalPlaces;
				}

				AddOrderBookItem(row, oldBook, oldDecimals);
			}
		}

		protected void AddOrderBookItem(MTERow row, OrderBook orderBook, int decimalPlaces)
		{
			if (row.FieldData.Length > 2)
			{
				orderBook.Items.Add(
					new OrderBookItem
						{
							Operation = row[2] == "B" ? OrderOperation.Buy : OrderOperation.Sell,
							Price = GetPriceFromBookRow(row, decimalPlaces),
							Quantity = GetAmountFromBookRow(row)
						});
			}
		}

		#endregion
	}
}