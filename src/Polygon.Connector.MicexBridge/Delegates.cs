#region

#endregion

using Polygon.Messages;

namespace Polygon.Connector.MicexBridge
{
    public delegate void OrderAddedDelegate(Order order);

    public delegate void OrderDeletedDelegate(object id);
}