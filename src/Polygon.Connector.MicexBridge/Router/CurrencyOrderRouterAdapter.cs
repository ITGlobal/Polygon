using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;

namespace Polygon.Connector.MicexBridge.Router
{
    class CurrencyOrderRouterAdapter : MicexSectionOrderRouterAdapter
    {
        protected internal CurrencyOrderRouterAdapter(IEnumerable<TableType> tablesTypes, IEnumerable<TransactionType> transactionsType)
            : base(tablesTypes, transactionsType)
        {
        }

        public override MicexSecionType SecionType
        {
            get { return MicexSecionType.Currency; }
        }


        #region TransacionProvider

        public override string GetParamsForSendOrder(Field[] inputFields, NewOrderTransaction transaction, string sessionId, uint transId, int decimals)
        {
            throw new NotImplementedException();
        }

        public override string GetParamsForDelByOrderIdOrder(Field[] inputFields, Int64 id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region OrderRouter


        //public override string InfoTableParams
        //{
        //    get
        //    {
        //        //return "        ";
        //        return "CURRCETS";
        //    }
        //}


        public override string GetExtRefFromOrderRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override long GetOrderIdFromOrderRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override int GetRestFromOrderRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override TimeSpan GetSetTimeFromOrderRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override TimeSpan GetCancelTimeFromOrderRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override string GetOrderStateFromOrderRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override string GetInstrumentFromOrderRow(MTERow row)
        {
            throw new NotImplementedException();
        }


        public override Order GetOrderFromOrderRow(MTERow row, int decimals)
        {
            throw new NotImplementedException();
        }


        public override long GetOrderIdFromFillRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override long GetIdFromFillRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override Instrument GetInstrumentFromFillRow(MTERow row)
        {
            throw new NotImplementedException();
        }


        public override Fill GetFillFromRow(MTERow row, int decimals)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Positions

        public override int MoneyTableIndex
        {
            get { return 0; }
        }

        public override int PositionsTableIndex
        {
            get { return 17; }
        }

        public override string MoneyTableOpenParams
        {
            get { throw new NotImplementedException(); }
        }

        public override string GetAccountFromAccountsRow(MTERow row)
        {
            return row[0];
        }

        public override string GetAccountFromMoneyRow(MTERow row)
        {
            throw new NotImplementedException();
        }


        public override string GetPosCodeFromMoneyRow(MTERow row)
        {
            throw new NotImplementedException();
        }


        public override string GetAccountFromPosRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override string GetPosCodeFromPosRow(MTERow row)
        {
            throw new NotImplementedException();
        }

        public override string GetPositionsTableParams(Field[] inputFields)
        {
            return string.Empty;
        }


        public override Position UpdatePos(MTERow row)
        {
            return new Position
                {
                    Instrument = new Instrument{ClassCode = "1", Code = "2"},
                    Account = row[0]
                };
        }

        public override MoneyPosition UpdateMoneyPosition(MTERow row)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
