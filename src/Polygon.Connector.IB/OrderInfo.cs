using System;
using Polygon.Connector;
using Polygon.Messages;
using IBOrder = IBApi.Order;

namespace Polygon.Connector.InteractiveBrokers
{
    internal sealed class OrderInfo
    {
        public OrderInfo(IBOrder order, Instrument instrument = null, string orderState = null)
        {
            // При создании OrderInfo из IBApi.Order у нас нет информации
            //  * о неисполненном объеме заявки. Есть надежда, что этот объем придет через orderState()
            //  * о времени постановки заявки. Тут ничего не поделаешь, проставляем текущую дату/время

            Account = order.Account;
            OrderId = order.OrderId;
            Operation = IBUtils.ParseOrderOperation(order.Action);
            Price = (decimal)order.LmtPrice;
            OrderRef = order.OrderRef;
            Type = IBUtils.ParseOrderType(order.OrderType) ?? OrderType.Limit;
            PermId = order.PermId;
            Quantity = order.TotalQuantity;
            ActiveQuantity = order.TotalQuantity;
            DateTime = DateTime.Now;
            Instrument = instrument;
            State = IBUtils.ParseOrderState(orderState) ?? OrderState.Active;
            NewOrderTransactionId = Guid.NewGuid();
        }

        public int OrderId { get; set; }
        public int PermId { get; set; }
        public string Account { get; set; }
        public string OrderRef { get; set; }
        public Instrument Instrument { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public int ActiveQuantity { get; set; }
        public int Quantity { get; set; }
        public OrderOperation Operation { get; set; }
        public OrderType Type { get; set; }
        public OrderState State { get; set; }

        public Guid NewOrderTransactionId { get; set; }
        public bool NewOrderTransactionReplySent { get; set; }

        public Guid? KillOrderTransactionId { get; set; }
        public bool KillOrderTransactionReplySent { get; set; }

        public bool IsExternal { get; set; }
        public bool ShouldEmitExternalOrderMessage { get; set; }

        public Order CreateOrder()
        {
            return new Order
            {
                Account = Account,
                ActiveQuantity = (uint)ActiveQuantity,
                Comment = OrderRef,
                Instrument = Instrument,
                DateTime = DateTime,
                Operation = Operation,
                OrderExchangeId = PermId.ToString(),
                Price = Price,
                Quantity = (uint)Quantity,
                State = State,
                TransactionId = NewOrderTransactionId,
                Type = Type
            };
        }
    }

    internal static class IBUtils
    {
        public static OrderState? ParseOrderState(string state)
        {
            switch (state)
            {
                case "PendingSubmit":
                    // Indicates that you have transmitted the order, 
                    // but have not yet received confirmation that it has been accepted by the order destination. 
                    // NOTE: This order status is not sent by TWS and should be explicitly set by the API developer when an order is submitted.
                    return OrderState.New;

                case "PendingCancel":
                    // Indicates that you have sent a request to cancel the order
                    // but have not yet received cancel confirmation from the order destination.
                    // At this point, your order is not confirmed canceled. 
                    // You may still receive an execution while your cancellation request is pending. 
                    // NOTE: This order status is not sent by TWS and should be explicitly set by the API developer when an order is canceled.
                    return OrderState.Active;

                case "PreSubmitted":
                    // Indicates that a simulated order type has been accepted by the IB system and that this order has yet to be elected. 
                    // The order is held in the IB system until the election criteria are met. 
                    // At that time the order is transmitted to the order destination as specified .
                    return OrderState.New;

                case "Submitted":
                    // Indicates that your order has been accepted at the order destination and is working.
                    return OrderState.Active;

                case "ApiCanceled":
                    // After an order has been submitted and before it has been acknowledged, an API client client can request its cancelation, producing this state.
                    return OrderState.Cancelled;

                case "Cancelled":
                    // Indicates that the balance of your order has been confirmed canceled by the IB system. 
                    // This could occur unexpectedly when IB or the destination has rejected your order.
                    return OrderState.Cancelled;

                case "Filled":
                    // Indicates that the order has been completely filled.
                    return OrderState.Filled;

                case "Inactive":
                    // Indicates that the order has been accepted by the system (simulated orders) or an exchange (native orders)
                    // but that currently the order is inactive due to system, exchange or other issues.
                    return OrderState.Error;

                default:
                    return null;
            }
        }

        public static OrderType? ParseOrderType(string type)
        {
            switch (type)
            {
                case "LMT":
                    return OrderType.Limit;
                case "MKT":
                    return OrderType.Market;
                default:
                    return null;
            }
        }

        public static OrderOperation ParseOrderOperation(string operation)
        {
            switch (operation)
            {
                case "BUY":
                    return OrderOperation.Buy;
                case "SELL":
                case "SSHORT":
                    return OrderOperation.Sell;
                default:
                    return OrderOperation.NoOp;
            }
        }
    }
}

