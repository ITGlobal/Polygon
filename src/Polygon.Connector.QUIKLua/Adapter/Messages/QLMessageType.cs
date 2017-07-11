namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    internal enum QLMessageType
    {
        InitBegin,
        InitEnd,
        QuikSideSettings,
        InstrumentParams,
        InstrumentsList,
        Heartbeat,
        AccountsList,
        EnvAck,
        CandlesRequest,
        CandlesResponse,
        CandlesSubscription,
        CandlesUpdate,
        InstrumentParamsSubscriptionRequest,
        InstrumentParamsUnsubscriptionRequest,
        OrderBookSubscriptionRequest,
        OrderBookUnsubscriptionRequest,
        OrderBook,
        Transaction,
        TransactionReply,
        OrderStateChange,
        Position,
        MoneyPosition,
        Fill
    }
}

