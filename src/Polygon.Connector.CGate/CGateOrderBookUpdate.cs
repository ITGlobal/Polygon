using CGateAdapter.Messages;
using CGateAdapter.Messages.OrdersAggr;
using Polygon.Messages;

namespace Polygon.Connector.CGate
{
    internal class CGateOrderBookUpdate
    {
        /// <summary>
        /// Тип обновления
        /// </summary>
        public CGateOrderBookUpdateType Type { get; }

        public int IsinId { get; }

        public long ReplId { get; }

        public long ReplAct { get; }

        public string InstrumentCode { get; }

        public decimal Price { get; }

        public long Quantity { get; }

        public OrderOperation Operation { get; }

        public long ReplRev { get; }

        /// <summary>
        /// Создаёт объект на основе 
        /// </summary>
        public CGateOrderBookUpdate(CgmOrdersAggr rowUpdate, string instrumentCode)
        {
            IsinId = rowUpdate.IsinId;
            InstrumentCode = instrumentCode;
            Type = CGateOrderBookUpdateType.RowUpdate;
            ReplId = rowUpdate.ReplId;
            ReplAct = rowUpdate.ReplAct;
            Price = (decimal) rowUpdate.Price;
            Quantity = rowUpdate.Volume;
            ReplRev = rowUpdate.ReplRev;
            Operation = rowUpdate.Dir == 1 ? OrderOperation.Buy : OrderOperation.Sell;
        }

        public CGateOrderBookUpdate(CGateOrderBookUpdateType type)
        {
            Type = type;
        }

        public CGateOrderBookUpdate(CGateClearTableMessage message)
        {
            Type = CGateOrderBookUpdateType.ClearTable;
            ReplRev = message.TableRev;
        }
        
    }
}

