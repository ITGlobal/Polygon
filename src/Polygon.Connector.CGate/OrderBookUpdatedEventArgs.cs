using System;
using System.Collections.Generic;
using Polygon.Messages;

namespace Polygon.Connector.CGate
{
    internal class OrderBookUpdatedEventArgs : EventArgs
    {
        public List<OrderBook> Books { get; set; }
    }
}

