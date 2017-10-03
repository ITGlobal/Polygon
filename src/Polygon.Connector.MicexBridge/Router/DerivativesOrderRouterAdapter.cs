using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;

namespace Polygon.Connector.MicexBridge.Router
{
    class DerivativesOrderRouterAdapter : MicexSectionOrderRouterAdapter
    {

        protected internal DerivativesOrderRouterAdapter(IEnumerable<TableType> tablesTypes, IEnumerable<TransactionType> transactionsType)
            : base(tablesTypes, transactionsType)
        {
        }

        public override MicexSecionType SecionType
        {
            get { return MicexSecionType.Derivatives; }
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
                transaction.IsMarketMakerOrder ? "M" : " ",
                "FOB",
                transaction.Instrument.Code,
                transaction.Price,
                (int)transaction.Quantity,
                string.Empty,
                sessionId + transId);
        }
        
        public override string GetParamsForDelByOrderIdOrder(Field[] inputFields, Int64 id)
        {
            return Field.GenerateFields(inputFields, 0, (int)id);
        }
        
        #endregion

        #region OrderRouter

        
        //public override string InfoTableParams
        //{
        //    get { return string.Empty; }
        //}


        public override string GetExtRefFromOrderRow(MTERow row)
        {
            return row[18];
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
            return row.GetTimeSpan(19);
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
            var transactId = new Guid(GetExtRefFromOrderRow(row));

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
                           Instrument = new Instrument { Code = GetInstrumentFromOrderRow(row), ClassCode = "FOB" },
                           Price = row.GetDecimal(13, decimals),
                           Operation = row[4] == "B" ? OrderOperation.Buy : OrderOperation.Sell,
                           OrderExchangeId = GetOrderIdFromOrderRow(row),
                           TransactionId = transactId,
                           State = status,
                           //Type = (OrderType) Enum.Parse(typeof(OrderType), row[3]),
                           Quantity = (uint) quantity,
                           ActiveQty = (uint) saldo
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
            return new Instrument {Code = row[10], ClassCode = "FOB"};
        }


        public override Fill GetFillFromRow(MTERow row, int decimals)
        {
            return new Fill
                       {
                           Account = row[8],
                           //Client = posOrder.Client,
                           DateTime = DateTime.Today + row.GetTimeSpan(2),
                           //Instrument = GetInstrumentFromFillRow(row),
                           Price = row.GetDecimal(11, decimals),
                           Operation = row[3] == "B" ? OrderOperation.Buy : OrderOperation.Sell,
                           Quantity = (uint) row.GetInt(12)
                       };
            //return new Position(
            //                    posOrder.trans_id, posOrder.instrument, row[8], posOrder.operation,
            //                    row.GetDecimal(11, decimals), row.GetInt(12));
        }

        #endregion

        #region Positions

        public override int MoneyTableIndex
        {
            get { return 10; }
        }

        public override int PositionsTableIndex
        {
            get { return 11; }
        }
        
        public override string MoneyTableOpenParams
        {
            get { return "A "; }
        }

        public override string GetAccountFromAccountsRow(MTERow row)
        {
            return row[1];
        }

        public override string GetAccountFromMoneyRow(MTERow row)
        {
            return row[4];
        }


        public override string GetPosCodeFromMoneyRow(MTERow row)
        {
            return row[0]; //.GetIntDirect(0);
        }

        //public void UpdateMoney(MTERow row, int decimals, MoneyPosition position)
        //{
        //    for (byte i = 0; i < row.FieldNumbers.Length; ++i)
        //        switch (row.FieldNumbers[i])
        //        {
        //            //тут занятые!!
        //            case 10:
        //                position.VariosMargin = row.GetDecimalDirect(i, decimals);
        //                break;
        //            case 6:
        //                position.CurrentPurePosition = row.GetDecimalDirect(i, decimals);
        //                break;
        //            case 11:
        //                position.OpenLimit = row.GetDecimalDirect(i, decimals);
        //                break;
        //            case 8:
        //                position.PlanPurePosition = row.GetDecimalDirect(i, decimals);
        //                break;
        //        }
        //}


        public override string GetAccountFromPosRow(MTERow row)
        {
            return row[4];
        }

        public override string GetPosCodeFromPosRow(MTERow row)
        {
            return row[0];
        }

        public override string GetPositionsTableParams(Field[] inputFields)
        {
            return Field.GenerateFields(inputFields, 0, 0, string.Empty, "A", string.Empty);
        }

        private Dictionary<string, Position> InstrumentPositionsOnCodes = new Dictionary<string, Position>();
        public override Position UpdatePos(MTERow row)
        {
            string code = GetPosCodeFromPosRow(row);

            Position position;

            if (!InstrumentPositionsOnCodes.TryGetValue(code, out position))
            {
                string account = GetAccountFromPosRow(row);
                InstrumentPositionsOnCodes.Add(code, position = new Position { Account = account });
            }

            var instrument = new Instrument { Code = row[1], ClassCode = "FOB" };

            for (byte i = 0; i < row.FieldNumbers.Length; ++i)
                switch (row.FieldNumbers[i])
                {
                    //тут чистая позиция    
                    case 10:
                        position.Quantity = row.GetIntDirect(i);
                        position.Instrument = instrument;
                        return position;
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
                        //тут занятые!!
                    case 11:
                        moneyPosition.VM = row.GetDecimalDirect(i, moneyDecimals);
                        break;
                    case 7:
                        moneyPosition.CurrentPurePosition = row.GetDecimalDirect(i, moneyDecimals);
                        break;
                    case 12:
                        moneyPosition.OpenLimit = row.GetDecimalDirect(i, moneyDecimals);
                        break;
                    case 9:
                        moneyPosition.PlannedPurePosition = row.GetDecimalDirect(i, moneyDecimals);
                        break;
                }

            return moneyPosition;
        }

        #endregion
    }
}