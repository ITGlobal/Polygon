using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;

namespace Polygon.Connector.MicexBridge.Router
{
    class StockOrderRouterAdapter : MicexSectionOrderRouterAdapter
    {
        protected internal StockOrderRouterAdapter(IEnumerable<TableType> tablesTypes, IEnumerable<TransactionType> transactionsType)
            : base(tablesTypes, transactionsType)
        {
        }

        public override MicexSecionType SecionType
        {
            get { return MicexSecionType.Stock; }
        }

        #region TransacionProvider

        public override string GetParamsForSendOrder(Field[] inputFields, NewOrderTransaction transaction, string sessionId, uint transId, int decimals)
        {
            return Field.GenerateFields(
                inputFields,
                decimals,

                transaction.Account,
                transaction.Operation == OrderOperation.Buy ? "B" : "S",
                transaction.Type,
                "S",
                " ",

                "P",
                transaction.IsMarketMakerOrder ? "M" : " ",
                transaction.Instrument.ClassCode,
                transaction.Instrument.Code,
                transaction.Price,

                (int)transaction.Quantity,
                string.Empty,
                sessionId + transId,
                "      ",
                transaction.ClientCode);
        }

        public override string GetParamsForDelByOrderIdOrder(Field[] inputFields, Int64 id)
        {
            return Field.GenerateFields(inputFields, 0, id);
        }

        #endregion


        #region OrderRouter


        //public override string InfoTableParams
        //{
        //    get { return "FOND    "; }
        //}


        public override string GetExtRefFromOrderRow(MTERow row)
        {
            return row[22];
        }

        public override long GetOrderIdFromOrderRow(MTERow row)
        {
            return row.GetLong(0);
        }

        public override int GetRestFromOrderRow(MTERow row)
        {
            return row.GetInt(15);
        }

        public override TimeSpan GetSetTimeFromOrderRow(MTERow row)
        {
            return row.GetTimeSpan(1);
        }

        public override TimeSpan GetCancelTimeFromOrderRow(MTERow row)
        {
            return row.GetTimeSpan(27);
        }

        public override string GetOrderStateFromOrderRow(MTERow row)
        {
            return row[2];
        }

        public override string GetInstrumentFromOrderRow(MTERow row)
        {
            return row[12];
        }


        public override Order GetOrderFromOrderRow(MTERow row, int decimals)
        {
            Guid transactId = new Guid(GetExtRefFromOrderRow(row));

            OrderState status;

            int quantity = row.GetInt(14);
            int saldo = GetRestFromOrderRow(row);

            switch (GetOrderStateFromOrderRow(row))
            {
                case "O": //Активная
                    status = quantity == saldo ? OrderState.Active : OrderState.PartiallyFilled;
                    break;
                case "M": //Исполнена
                    status = OrderState.Filled;
                    break;
                default: //Снята
                    status = OrderState.Cancelled;
                    break;
            }

            return new Order
            {
                Account = row[10],
                Instrument = new Instrument { Code = GetInstrumentFromOrderRow(row), ClassCode = row[11] },
                Price = row.GetDecimal(13, decimals),
                Operation = row[4] == "B" ? OrderOperation.Buy : OrderOperation.Sell,
                OrderExchangeId = GetOrderIdFromOrderRow(row),
                TransactionId = transactId,
                State = status,
                //Type = (OrderType)Enum.Parse(typeof(OrderType), row[3]),
                Quantity = (uint)quantity,
                ActiveQty = (uint)saldo
            };
        }


        public override long GetOrderIdFromFillRow(MTERow row)
        {
            return row.GetLong(1);
        }

        public override long GetIdFromFillRow(MTERow row)
        {
            return row.GetLong(0);
        }

        public override Instrument GetInstrumentFromFillRow(MTERow row)
        {
            return new Instrument { Code = row[10], ClassCode = row[9] };
        }


        public override Fill GetFillFromRow(MTERow row, int decimals)
        {
            return new Fill
            {
                Account = row[8],
                ClientCode = row[35],
                //Client = posOrder.Client,
                DateTime = DateTime.Today + row.GetTimeSpan(2),
                //Instrument = GetInstrumentFromFillRow(row),
                Price = row.GetDecimal(11, decimals),
                Operation = row[3] == "B" ? OrderOperation.Buy : OrderOperation.Sell,
                Quantity = (uint)row.GetInt(12)
            };

            //return new Fill
            //{
            //    Account = row[8],
            //    //Client = posOrder.Client,
            //    DateTime = DateTime.Now + row.GetTimeSpan(2),
            //    //Instrument = posOrder.Instrument,
            //    Price = row.GetDecimal(11, decimals),
            //    //Operation = posOrder.Operation,
            //    Quantity = (uint)row.GetInt(12)
            //};
            //return new Position(
            //                    posOrder.trans_id, posOrder.instrument, row[8], posOrder.operation,
            //                    row.GetDecimal(11, decimals), row.GetInt(12));
        }

        //public override Trade GetTradeFromRow(MTERow row, Order posOrder, int decimals)
        //{
        //    return new Trade
        //    {
        //        Client = posOrder.Client,
        //        DateTime = DateTime.Now + row.GetTimeSpan(2),
        //        Instrument = posOrder.Instrument,
        //        Price = (double)row.GetDecimal(11, decimals),
        //        Operation = posOrder.Operation,
        //        Quantity = (uint)row.GetInt(12)
        //    };
        //}

        #endregion


        #region Positions

        public override int MoneyTableIndex
        {
            get { return 19; }
        }

        public override int PositionsTableIndex
        {
            get { return 0; }
        }


        public override string MoneyTableOpenParams
        {
            get { return string.Empty; }
        }

        public override string GetAccountFromAccountsRow(MTERow row)
        {
            return row[0];
        }

        public override string GetAccountFromMoneyRow(MTERow row)
        {
            return "S01-00000F00";
        }


        public override string GetPosCodeFromMoneyRow(MTERow row)
        {
            return row[2] + GetAccountFromMoneyRow(row);
        }


        public override string GetAccountFromPosRow(MTERow row)
        {
            return row[11];
        }

        public override string GetPosCodeFromPosRow(MTERow row)
        {
            return row[2] + GetAccountFromPosRow(row);
        }

        public override string GetPositionsTableParams(Field[] inputFields)
        {
            return "                        ";
        }

        private Dictionary<string, Position> InstrumentPositionsOnCodes = new Dictionary<string, Position>();
        public override Position UpdatePos(MTERow row)
        {
            string code = GetPosCodeFromPosRow(row);

            Position position;

            if (!InstrumentPositionsOnCodes.TryGetValue(code, out position))
            {
                string account = GetAccountFromPosRow(row);
                InstrumentPositionsOnCodes.Add(code, position = new Position
                                                                    {
                                                                        Account = account,
                                                                        Instrument = new Instrument { Code = row[2], ClassCode = string.Empty }
                                                                    });
            }


            for (byte i = 0; i < row.FieldNumbers.Length; ++i)
                switch (row.FieldNumbers[i])
                {
                    //тут чистая позиция    
                    case 5:
                        position.Quantity = (int)row.GetLongDirect(i);
                        break;
                    case 3:
                        position.MorningQuantity = (int)row.GetLongDirect(i);
                        break;
                }

            return position;
        }

        private Dictionary<string, MoneyPosition> MoneyPositionsOnCodes = new Dictionary<string, MoneyPosition>();
        public override MoneyPosition UpdateMoneyPosition(MTERow row)
        {
            const int moneyDecimals = 100;

            string code = GetPosCodeFromMoneyRow(row);

            MoneyPosition moneyPosition;

            if (!MoneyPositionsOnCodes.TryGetValue(code, out moneyPosition))
            {
                string account = GetAccountFromMoneyRow(row);
                MoneyPositionsOnCodes.Add(code, moneyPosition = new MoneyPosition { Account = account });
            }

            for (byte i = 0; i < row.FieldNumbers.Length; ++i)
                switch (row.FieldNumbers[i])
                {
                    case 6:
                        moneyPosition.CurrentPurePosition = row.GetDecimalDirect(i, moneyDecimals);
                        break;
                    case 5:
                        moneyPosition.OpenLimit = row.GetDecimalDirect(i, moneyDecimals);
                        break;
                    case 7:
                        moneyPosition.PlannedPurePosition = row.GetDecimalDirect(i, moneyDecimals);
                        break;
                }

            return moneyPosition;
        }

        #endregion


    }
}
