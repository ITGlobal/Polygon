using System;
using System.Collections.Generic;
using Polygon.Connector.MicexBridge.MTETypes;

namespace Polygon.Connector.MicexBridge.Router
{
    abstract class MicexSectionOrderRouterAdapter
    {
        #region Abstract

        public abstract MicexSecionType SecionType { get; }

        #endregion

        #region Static

        public static MicexSectionOrderRouterAdapter CreateAdapter(MicexSecionType section, IEnumerable<TableType> tablesTypes, IEnumerable<TransactionType> transactionsType)
        {
            // создаём адаптер, соответствующий секции ММВБ
            switch (section)
            {
                case MicexSecionType.Derivatives:
                    return new DerivativesOrderRouterAdapter(tablesTypes, transactionsType);
                case MicexSecionType.Stock:
                    return new StockOrderRouterAdapter(tablesTypes, transactionsType);
                case MicexSecionType.Currency:
                    return new CurrencyOrderRouterAdapter(tablesTypes, transactionsType);
            }

            throw new ArgumentException("Неверная секция", "section");
        }

        #endregion


        #region Private members

        private ILog logger;

        private int accountsTableIndex;
        private int dealTableIndex;
        private int orderTableIndex;
        private int infoTableIndex;
        
        //private int transactionIndexSendOrder;
        //private int transactionIndexDelOrderById;
        private int sendOrderTransactionIndex;
        private int delOrderByIdTransactionIndex;

        #endregion

        #region Construction

        protected MicexSectionOrderRouterAdapter(IEnumerable<TableType> tablesTypes, IEnumerable<TransactionType> transactionsType)
        {
            logger = LogManager.GetLogger(GetType());

            var enumeratorTables = tablesTypes.GetEnumerator();

            int index = 0;
            while (enumeratorTables.MoveNext())
            {
                var table = (TableType)enumeratorTables.Current;

                logger.DebugFormat("Имя таблицы {0}:\t{1}\t\t\t{2}", index, table.Name, table.Description);

                switch (table.Name)
                {
                    case "SECURITIES":
                        infoTableIndex = index;
                        break;
                    case "TRDACC":
                        accountsTableIndex = index;
                        break;
                    //case "FORM_PORTFOLIOS":
                    //    moneyTableIndex = index;
                    //    break;
                    //case "FORM_PORTFOLIO_POSN":
                    //    positionsTableIndex = index;
                    //    break;
                    case "TRADES":
                        dealTableIndex = index;
                        break;
                    case "ORDERS":
                        orderTableIndex = index;
                        break;
                }

                index++;
            }

            var enumeratorTrans = transactionsType.GetEnumerator();

            index = 0;
            while (enumeratorTrans.MoveNext())
            {
                var transaction = (TransactionType)enumeratorTrans.Current;

                logger.DebugFormat("Имя транзакции {0}:\t{1}\t\t\t{2}", index, transaction.Name, transaction.Description);

                switch (transaction.Name)
                {
                    case "ORDER":
                        sendOrderTransactionIndex = index;
                        break;
                    case "WD_ORDER_BY_NUMBER":
                        delOrderByIdTransactionIndex = index;
                        break;
                }

                index++;
            }
        }




        #endregion


        #region TransacionProvider

        //public int TransactionIndexSendOrder { get { return transactionIndexSendOrder; } }
        //public int TransactionIndexDelOrderById { get { return transactionIndexDelOrderById; } }

        abstract public string GetParamsForSendOrder(Field[] inputFields, NewOrderTransaction transaction, string userId, uint transId, int decimals);
        abstract public string GetParamsForDelByOrderIdOrder(Field[] inputFields, Int64 id);

        public int SendOrderTransactionIndex { get { return sendOrderTransactionIndex; } }
        public int DelOrderByIdTransactionIndex { get { return delOrderByIdTransactionIndex; } }

        #endregion

        #region OrderRouter

        public int DealTableIndex { get { return dealTableIndex; } }
        public int OrderTableIndex { get { return orderTableIndex; } }
        public int InfoTableIndex { get{ return infoTableIndex;} }
        //abstract public string InfoTableParams { get; }

        abstract public string GetExtRefFromOrderRow(MTERow row);
        abstract public long GetOrderIdFromOrderRow(MTERow row);
        abstract public int GetRestFromOrderRow(MTERow row);
        abstract public TimeSpan GetSetTimeFromOrderRow(MTERow row);
        abstract public TimeSpan GetCancelTimeFromOrderRow(MTERow row);
        abstract public string GetOrderStateFromOrderRow(MTERow row);
        abstract public string GetInstrumentFromOrderRow(MTERow row);
        abstract public long GetOrderIdFromFillRow(MTERow row);
        abstract public long GetIdFromFillRow(MTERow row);
        abstract public Instrument GetInstrumentFromFillRow(MTERow row);
        abstract public Order GetOrderFromOrderRow(MTERow row, int decimals);
        abstract public Fill GetFillFromRow(MTERow row, int decimals);

        #endregion

        #region Positions

        public int AccountsTableIndex { get{ return accountsTableIndex;} }
        abstract public int MoneyTableIndex { get; }
        abstract public string MoneyTableOpenParams { get; }
        abstract public int PositionsTableIndex { get; }

        abstract public string GetAccountFromAccountsRow(MTERow row);
        abstract public string GetAccountFromMoneyRow(MTERow row);
        abstract public string GetPosCodeFromMoneyRow(MTERow row);
        abstract public string GetAccountFromPosRow(MTERow row);
        abstract public string GetPosCodeFromPosRow(MTERow row);
        abstract public Position UpdatePos(MTERow row);
        abstract public string GetPositionsTableParams(Field[] inputFields);

        #endregion

        abstract public MoneyPosition UpdateMoneyPosition(MTERow row);
    }
}
