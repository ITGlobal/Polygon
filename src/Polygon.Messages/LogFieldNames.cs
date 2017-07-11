namespace Polygon.Messages
{
    /// <summary>
    ///     В этом классе определяются константы для записи свойств различных объектов в лог
    /// </summary>
    internal static class LogFieldNames
    {
        public const string Id = "id";
        public const string Type = "type";
        public const string ExchangeId = "ex_id";
        public const string ExchangeOrderId = "ord_ex_id";
        public const string Time = "time";
        public const string Instrument = "instrument";
        public const string Price = "p";
        public const string Quantity = "q";
        public const string Operation = "op";
        public const string ExecutionCondition = "exec_cond";
        public const string TheorPrice = "theor_p";
        public const string LotSize = "lot_size";
        public const string Vola = "vola";
        public const string Message = "msg";
        public const string StackTrace = "stack_trace";
        public const string Order = "order";
        public const string OpenInterest = "open_int";
        public const string Feeds = "feeds";
        public const string Routers = "routers";
        public const string BestBidPrice = "bb_p";
        public const string BestBidQuantity = "bb_q";
        public const string BestOfferPrice = "bo_p";
        public const string BestOfferQuantity = "bo_q";
        public const string TopPriceLimit = "top_limit";
        public const string BottomPriceLimit = "btm_limit";
        public const string DecimalPlaces = "dec_places";
        public const string LastPrice = "last_p";
        public const string PriceStep = "p_step";
        public const string PriceStepValue = "p_step_val";
        public const string Settlement = "settl";
        public const string PreviousSettlement = "prev_settl";
        public const string VolaTranslatedByFeed = "is_vola_translated";
        public const string SessionEndTime = "session_end_time";
        public const string TransactionId = "tr_id";
        public const string Account = "account";
        public const string ClientCode = "client";
        public const string IsManual = "manual";
        public const string Mode = "mode";
        public const string Comment = "comment";
        public const string IsMarketMakerOrder = "is_mm_order";
        public const string Items = "items";
        public const string ActiveQuantity = "active_q";
        public const string FilledQuantity = "filled_q";
        public const string State = "state";
        public const string IsTrading = "is_trading";
        public const string StartTime = "start";
        public const string EndTime = "end";
        public const string EveningStartTime = "evening_start";
        public const string EveningEndTime = "evening_end";
        public const string Success = "ok";
    }
}
