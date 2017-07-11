using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Messages;
using SpimexAdapter.FTE;
using OrderType = SpimexAdapter.FTE.OrderType;

namespace Polygon.Connector.Spimex
{
    static class TransactionExtentions
    {
        public static OrderType GetOrderType(this NewOrderTransaction transaction) 
            => transaction.Type == Messages.OrderType.Market ? OrderType.MARKET : OrderType.LIMIT;

        public static OrderParams GetOrderParams(this NewOrderTransaction transaction)
        {
            switch (transaction.ExecutionCondition)
            {
                case OrderExecutionCondition.FillOrKill:
                    return OrderParams.FOK;

                case OrderExecutionCondition.KillBalance:
                    return OrderParams.IOC;

                default:
                case OrderExecutionCondition.PutInQueue:
                    return OrderParams.PIQ;
            }
        }

        public static BuySell GetOperation(this NewOrderTransaction transaction)
            => transaction.Operation == OrderOperation.Buy ? BuySell.BUY : BuySell.SELL;

    }
}
