using System;
using System.Linq;
using Polygon.Connector.CQGContinuum.WebAPI;
using Polygon.Messages;

namespace Polygon.Connector.CQGContinuum
{
    internal static class ConvertionHelper
    {
        public static OrderType GetOrderType(OrderStatus message)
        {
            if (message.order == null)
            {
                return OrderType.Limit;
            }

            switch ((WebAPI.Order.OrderType)message.order.order_type)
            {
                case WebAPI.Order.OrderType.MKT:
                case WebAPI.Order.OrderType.STP:
                    return OrderType.Market;
                case WebAPI.Order.OrderType.LMT:
                case WebAPI.Order.OrderType.STL:
                    return OrderType.Limit;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static OrderState GetOrderState(OrderStatus message)
        {
            switch ((OrderStatus.Status)message.status)
            {
                case OrderStatus.Status.IN_TRANSIT:
                    return OrderState.New;
                case OrderStatus.Status.REJECTED:
                    return OrderState.Error;
                case OrderStatus.Status.WORKING:
                    return message.fill_qty > 0 ? OrderState.PartiallyFilled : OrderState.Active;
                case OrderStatus.Status.EXPIRED:
                    return OrderState.Cancelled;
                case OrderStatus.Status.IN_CANCEL:
                    return OrderState.Active;
                case OrderStatus.Status.IN_MODIFY:
                    return OrderState.Active;
                case OrderStatus.Status.CANCELLED:
                    return OrderState.Cancelled;
                case OrderStatus.Status.FILLED:
                    return OrderState.Filled;
                case OrderStatus.Status.SUSPENDED:
                    return OrderState.New;
                case OrderStatus.Status.DISCONNECTED:
                    return OrderState.Error;
                case OrderStatus.Status.ACTIVEAT:
                    return OrderState.Active;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetComment(OrderStatus message)
        {
            if (message.order == null)
            {
                return null;
            }

            var attr = message.order.user_attribute.FirstOrDefault(_ => _.name == CQGCRouter.CommentAttributeName);
            return attr != null ? attr.value : null;
        }

        public static OrderOperation GetOrderOperation(WebAPI.Order.Side side)
        {
            switch (side)
            {
                case WebAPI.Order.Side.BUY:
                    return OrderOperation.Buy;
                case WebAPI.Order.Side.SELL:
                    return OrderOperation.Sell;
                default:
                    throw new ArgumentOutOfRangeException("side", side, null);
            }
        }

        public static WebAPI.Order.Side GetSide(OrderOperation operation)
        {
            switch (operation)
            {
                case OrderOperation.Buy:
                    return WebAPI.Order.Side.BUY;
                case OrderOperation.Sell:
                    return WebAPI.Order.Side.SELL;
                default:
                    throw new ArgumentOutOfRangeException("operation", operation, null);
            }
        }

        public static WebAPI.Order.OrderType GetOrderType(OrderType type)
        {
            switch (type)
            {
                case OrderType.Limit:
                    return WebAPI.Order.OrderType.LMT;
                case OrderType.Market:
                    return WebAPI.Order.OrderType.MKT;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        public static WebAPI.Order.Duration GetDuration(OrderExecutionCondition condition)
        {
            switch (condition)
            {
                case OrderExecutionCondition.PutInQueue:
                    return WebAPI.Order.Duration.GTC;
                case OrderExecutionCondition.FillOrKill:
                    return WebAPI.Order.Duration.FOK;
                case OrderExecutionCondition.KillBalance:
                    return WebAPI.Order.Duration.FAK; // NOTE не факт, что это правильно
                default:
                    throw new ArgumentOutOfRangeException("condition", condition, null);
            }
        }
    }
}

