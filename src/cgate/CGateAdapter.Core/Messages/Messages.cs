

using System;  
using System.Diagnostics;  
using System.Text;
using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Типы потоков
    /// </summary>
    [PublicAPI]
	public enum CGateStreamType
    {
        /// <summary>
        ///    Системный псевдопоток
        /// </summary>
        Preudo,

        /// <summary>
        ///    Поток clr
        /// </summary>
        Clr,

        /// <summary>
        ///    Поток forts_messages
        /// </summary>
        FortsMessages,

        /// <summary>
        ///    Поток fut_common
        /// </summary>
        FutCommon,

        /// <summary>
        ///    Поток fut_info
        /// </summary>
        FutInfo,

        /// <summary>
        ///    Поток fut_trades
        /// </summary>
        FutTrades,

        /// <summary>
        ///    Поток fut_trades_heartbeat
        /// </summary>
        FutTradesHeartbeat,

        /// <summary>
        ///    Поток info
        /// </summary>
        Info,

        /// <summary>
        ///    Поток misc_info
        /// </summary>
        MiscInfo,

        /// <summary>
        ///    Поток mm
        /// </summary>
        Mm,

        /// <summary>
        ///    Поток opt_common
        /// </summary>
        OptCommon,

        /// <summary>
        ///    Поток opt_info
        /// </summary>
        OptInfo,

        /// <summary>
        ///    Поток opt_trades
        /// </summary>
        OptTrades,

        /// <summary>
        ///    Поток ordbook
        /// </summary>
        Ordbook,

        /// <summary>
        ///    Поток orderbook
        /// </summary>
        Orderbook,

        /// <summary>
        ///    Поток orders_aggr
        /// </summary>
        OrdersAggr,

        /// <summary>
        ///    Поток ordLog_trades
        /// </summary>
        OrdLogTrades,

        /// <summary>
        ///    Поток part
        /// </summary>
        Part,

        /// <summary>
        ///    Поток pos
        /// </summary>
        Pos,

        /// <summary>
        ///    Поток rates
        /// </summary>
        Rates,

        /// <summary>
        ///    Поток rts_index
        /// </summary>
        RtsIndex,

        /// <summary>
        ///    Поток rts_indexlog
        /// </summary>
        RtsIndexlog,

        /// <summary>
        ///    Поток tnpenalty
        /// </summary>
        Tnpenalty,

        /// <summary>
        ///    Поток vm
        /// </summary>
        Vm,

        /// <summary>
        ///    Поток volat
        /// </summary>
        Volat,
    }

	/// <summary>
	///     Константы с названиями полей для преобразования сообщений в строку
	/// </summary>
	[PublicAPI]
	public static class CGateFieldNames
	{
		/// <summary>
		///     Константа для поля StreamName
		/// </summary>
		public const string StreamName = "str";

		/// <summary>
		///     Константа для поля StreamRegime
		/// </summary>
		public const string StreamRegime = "st";

		/// <summary>
		///     Константа для поля MessageTypeName
		/// </summary>
		public const string MessageTypeName = "mtname";

		/// <summary>
		///     Константа для поля UserId
		/// </summary>
		public const string UserId = "user_id";
		/// <summary>
		///     Константа для поля ReplId
		/// </summary>
		public const string ReplId = "repl_id";
		/// <summary>
		///     Константа для поля ReplRev
		/// </summary>
		public const string ReplRev = "repl_rev";
		/// <summary>
		///     Константа для поля ReplAct
		/// </summary>
		public const string ReplAct = "repl_act";
		/// <summary>
		///     Константа для поля ClientCode
		/// </summary>
		public const string ClientCode = "client_code";
		/// <summary>
		///     Константа для поля Share
		/// </summary>
		public const string Share = "share";
		/// <summary>
		///     Константа для поля AmountBeg
		/// </summary>
		public const string AmountBeg = "amount_beg";
		/// <summary>
		///     Константа для поля Vm
		/// </summary>
		public const string Vm = "vm";
		/// <summary>
		///     Константа для поля Premium
		/// </summary>
		public const string Premium = "premium";
		/// <summary>
		///     Константа для поля Pay
		/// </summary>
		public const string Pay = "pay";
		/// <summary>
		///     Константа для поля FeeFut
		/// </summary>
		public const string FeeFut = "fee_fut";
		/// <summary>
		///     Константа для поля FeeOpt
		/// </summary>
		public const string FeeOpt = "fee_opt";
		/// <summary>
		///     Константа для поля Go
		/// </summary>
		public const string Go = "go";
		/// <summary>
		///     Константа для поля AmountEnd
		/// </summary>
		public const string AmountEnd = "amount_end";
		/// <summary>
		///     Константа для поля Free
		/// </summary>
		public const string Free = "free";
		/// <summary>
		///     Константа для поля ExtReserve
		/// </summary>
		public const string ExtReserve = "ext_reserve";
		/// <summary>
		///     Константа для поля SettlementAccount
		/// </summary>
		public const string SettlementAccount = "settlement_account";
		/// <summary>
		///     Константа для поля Rate
		/// </summary>
		public const string Rate = "rate";
		/// <summary>
		///     Константа для поля Moment
		/// </summary>
		public const string Moment = "moment";
		/// <summary>
		///     Константа для поля Signs
		/// </summary>
		public const string Signs = "signs";
		/// <summary>
		///     Константа для поля SessId
		/// </summary>
		public const string SessId = "sess_id";
		/// <summary>
		///     Константа для поля RateId
		/// </summary>
		public const string RateId = "rate_id";
		/// <summary>
		///     Константа для поля IsinId
		/// </summary>
		public const string IsinId = "isin_id";
		/// <summary>
		///     Константа для поля Isin
		/// </summary>
		public const string Isin = "isin";
		/// <summary>
		///     Константа для поля Account
		/// </summary>
		public const string Account = "account";
		/// <summary>
		///     Константа для поля PosBeg
		/// </summary>
		public const string PosBeg = "pos_beg";
		/// <summary>
		///     Константа для поля PosEnd
		/// </summary>
		public const string PosEnd = "pos_end";
		/// <summary>
		///     Константа для поля Fee
		/// </summary>
		public const string Fee = "fee";
		/// <summary>
		///     Константа для поля AccumGo
		/// </summary>
		public const string AccumGo = "accum_go";
		/// <summary>
		///     Константа для поля FeeEx
		/// </summary>
		public const string FeeEx = "fee_ex";
		/// <summary>
		///     Константа для поля VatEx
		/// </summary>
		public const string VatEx = "vat_ex";
		/// <summary>
		///     Константа для поля FeeCc
		/// </summary>
		public const string FeeCc = "fee_cc";
		/// <summary>
		///     Константа для поля VatCc
		/// </summary>
		public const string VatCc = "vat_cc";
		/// <summary>
		///     Константа для поля DateClr
		/// </summary>
		public const string DateClr = "date_clr";
		/// <summary>
		///     Константа для поля SettlPrice
		/// </summary>
		public const string SettlPrice = "settl_price";
		/// <summary>
		///     Константа для поля Volat
		/// </summary>
		public const string Volat = "volat";
		/// <summary>
		///     Константа для поля TheorPrice
		/// </summary>
		public const string TheorPrice = "theor_price";
		/// <summary>
		///     Константа для поля PledgeName
		/// </summary>
		public const string PledgeName = "pledge_name";
		/// <summary>
		///     Константа для поля Amount
		/// </summary>
		public const string Amount = "amount";
		/// <summary>
		///     Константа для поля AmountBegMoney
		/// </summary>
		public const string AmountBegMoney = "amount_beg_money";
		/// <summary>
		///     Константа для поля PayMoney
		/// </summary>
		public const string PayMoney = "pay_money";
		/// <summary>
		///     Константа для поля AmountMoney
		/// </summary>
		public const string AmountMoney = "amount_money";
		/// <summary>
		///     Константа для поля ComEnsure
		/// </summary>
		public const string ComEnsure = "com_ensure";
		/// <summary>
		///     Константа для поля EventId
		/// </summary>
		public const string EventId = "event_id";
		/// <summary>
		///     Константа для поля EventType
		/// </summary>
		public const string EventType = "event_type";
		/// <summary>
		///     Константа для поля Message
		/// </summary>
		public const string Message = "message";
		/// <summary>
		///     Константа для поля BrokerCode
		/// </summary>
		public const string BrokerCode = "broker_code";
		/// <summary>
		///     Константа для поля Type
		/// </summary>
		public const string Type = "type";
		/// <summary>
		///     Константа для поля Dir
		/// </summary>
		public const string Dir = "dir";
		/// <summary>
		///     Константа для поля Price
		/// </summary>
		public const string Price = "price";
		/// <summary>
		///     Константа для поля Comment
		/// </summary>
		public const string Comment = "comment";
		/// <summary>
		///     Константа для поля BrokerTo
		/// </summary>
		public const string BrokerTo = "broker_to";
		/// <summary>
		///     Константа для поля ExtId
		/// </summary>
		public const string ExtId = "ext_id";
		/// <summary>
		///     Константа для поля Du
		/// </summary>
		public const string Du = "du";
		/// <summary>
		///     Константа для поля DateExp
		/// </summary>
		public const string DateExp = "date_exp";
		/// <summary>
		///     Константа для поля Hedge
		/// </summary>
		public const string Hedge = "hedge";
		/// <summary>
		///     Константа для поля DontCheckMoney
		/// </summary>
		public const string DontCheckMoney = "dont_check_money";
		/// <summary>
		///     Константа для поля LocalStamp
		/// </summary>
		public const string LocalStamp = "local_stamp";
		/// <summary>
		///     Константа для поля MatchRef
		/// </summary>
		public const string MatchRef = "match_ref";
		/// <summary>
		///     Константа для поля Msgid
		/// </summary>
		public const string Msgid = "msgid";
		/// <summary>
		///     Константа для поля Request
		/// </summary>
		public const string Request = "request";
		/// <summary>
		///     Константа для поля Code
		/// </summary>
		public const string Code = "code";
		/// <summary>
		///     Константа для поля OrderId
		/// </summary>
		public const string OrderId = "order_id";
		/// <summary>
		///     Константа для поля Reply
		/// </summary>
		public const string Reply = "reply";
		/// <summary>
		///     Константа для поля RatePrice
		/// </summary>
		public const string RatePrice = "rate_price";
		/// <summary>
		///     Константа для поля Trust
		/// </summary>
		public const string Trust = "trust";
		/// <summary>
		///     Константа для поля TradeMode
		/// </summary>
		public const string TradeMode = "trade_mode";
		/// <summary>
		///     Константа для поля BuySell
		/// </summary>
		public const string BuySell = "buy_sell";
		/// <summary>
		///     Константа для поля NonSystem
		/// </summary>
		public const string NonSystem = "non_system";
		/// <summary>
		///     Константа для поля CodeVcb
		/// </summary>
		public const string CodeVcb = "code_vcb";
		/// <summary>
		///     Константа для поля WorkMode
		/// </summary>
		public const string WorkMode = "work_mode";
		/// <summary>
		///     Константа для поля NumOrders
		/// </summary>
		public const string NumOrders = "num_orders";
		/// <summary>
		///     Константа для поля Regime
		/// </summary>
		public const string Regime = "regime";
		/// <summary>
		///     Константа для поля OrderId1
		/// </summary>
		public const string OrderId1 = "order_id1";
		/// <summary>
		///     Константа для поля Amount1
		/// </summary>
		public const string Amount1 = "amount1";
		/// <summary>
		///     Константа для поля Price1
		/// </summary>
		public const string Price1 = "price1";
		/// <summary>
		///     Константа для поля ExtId1
		/// </summary>
		public const string ExtId1 = "ext_id1";
		/// <summary>
		///     Константа для поля OrderId2
		/// </summary>
		public const string OrderId2 = "order_id2";
		/// <summary>
		///     Константа для поля Amount2
		/// </summary>
		public const string Amount2 = "amount2";
		/// <summary>
		///     Константа для поля Price2
		/// </summary>
		public const string Price2 = "price2";
		/// <summary>
		///     Константа для поля ExtId2
		/// </summary>
		public const string ExtId2 = "ext_id2";
		/// <summary>
		///     Константа для поля CheckLimit
		/// </summary>
		public const string CheckLimit = "check_limit";
		/// <summary>
		///     Константа для поля Mode
		/// </summary>
		public const string Mode = "mode";
		/// <summary>
		///     Константа для поля LimitMoney
		/// </summary>
		public const string LimitMoney = "limit_money";
		/// <summary>
		///     Константа для поля LimitPledge
		/// </summary>
		public const string LimitPledge = "limit_pledge";
		/// <summary>
		///     Константа для поля CoeffLiquidity
		/// </summary>
		public const string CoeffLiquidity = "coeff_liquidity";
		/// <summary>
		///     Константа для поля CoeffGo
		/// </summary>
		public const string CoeffGo = "coeff_go";
		/// <summary>
		///     Константа для поля IsAutoUpdateLimit
		/// </summary>
		public const string IsAutoUpdateLimit = "is_auto_update_limit";
		/// <summary>
		///     Константа для поля NoFutDiscount
		/// </summary>
		public const string NoFutDiscount = "no_fut_discount";
		/// <summary>
		///     Константа для поля State
		/// </summary>
		public const string State = "state";
		/// <summary>
		///     Константа для поля StateMask
		/// </summary>
		public const string StateMask = "state_mask";
		/// <summary>
		///     Константа для поля CodeFrom
		/// </summary>
		public const string CodeFrom = "code_from";
		/// <summary>
		///     Константа для поля CodeTo
		/// </summary>
		public const string CodeTo = "code_to";
		/// <summary>
		///     Константа для поля AmountPledge
		/// </summary>
		public const string AmountPledge = "amount_pledge";
		/// <summary>
		///     Константа для поля NumClr2delivery
		/// </summary>
		public const string NumClr2delivery = "num_clr_2delivery";
		/// <summary>
		///     Константа для поля UseBrokerNumClr2delivery
		/// </summary>
		public const string UseBrokerNumClr2delivery = "use_broker_num_clr_2delivery";
		/// <summary>
		///     Константа для поля ExpWeight
		/// </summary>
		public const string ExpWeight = "exp_weight";
		/// <summary>
		///     Константа для поля UseBrokerExpWeight
		/// </summary>
		public const string UseBrokerExpWeight = "use_broker_exp_weight";
		/// <summary>
		///     Константа для поля DealId1
		/// </summary>
		public const string DealId1 = "deal_id1";
		/// <summary>
		///     Константа для поля DealId2
		/// </summary>
		public const string DealId2 = "deal_id2";
		/// <summary>
		///     Константа для поля SeqNumber
		/// </summary>
		public const string SeqNumber = "seq_number";
		/// <summary>
		///     Константа для поля QueueSize
		/// </summary>
		public const string QueueSize = "queue_size";
		/// <summary>
		///     Константа для поля PenaltyRemain
		/// </summary>
		public const string PenaltyRemain = "penalty_remain";
		/// <summary>
		///     Константа для поля BestSell
		/// </summary>
		public const string BestSell = "best_sell";
		/// <summary>
		///     Константа для поля AmountSell
		/// </summary>
		public const string AmountSell = "amount_sell";
		/// <summary>
		///     Константа для поля BestBuy
		/// </summary>
		public const string BestBuy = "best_buy";
		/// <summary>
		///     Константа для поля AmountBuy
		/// </summary>
		public const string AmountBuy = "amount_buy";
		/// <summary>
		///     Константа для поля Trend
		/// </summary>
		public const string Trend = "trend";
		/// <summary>
		///     Константа для поля DealTime
		/// </summary>
		public const string DealTime = "deal_time";
		/// <summary>
		///     Константа для поля MinPrice
		/// </summary>
		public const string MinPrice = "min_price";
		/// <summary>
		///     Константа для поля MaxPrice
		/// </summary>
		public const string MaxPrice = "max_price";
		/// <summary>
		///     Константа для поля AvrPrice
		/// </summary>
		public const string AvrPrice = "avr_price";
		/// <summary>
		///     Константа для поля OldKotir
		/// </summary>
		public const string OldKotir = "old_kotir";
		/// <summary>
		///     Константа для поля DealCount
		/// </summary>
		public const string DealCount = "deal_count";
		/// <summary>
		///     Константа для поля ContrCount
		/// </summary>
		public const string ContrCount = "contr_count";
		/// <summary>
		///     Константа для поля Capital
		/// </summary>
		public const string Capital = "capital";
		/// <summary>
		///     Константа для поля Pos
		/// </summary>
		public const string Pos = "pos";
		/// <summary>
		///     Константа для поля ModTime
		/// </summary>
		public const string ModTime = "mod_time";
		/// <summary>
		///     Константа для поля CurKotir
		/// </summary>
		public const string CurKotir = "cur_kotir";
		/// <summary>
		///     Константа для поля CurKotirReal
		/// </summary>
		public const string CurKotirReal = "cur_kotir_real";
		/// <summary>
		///     Константа для поля OrdersSellQty
		/// </summary>
		public const string OrdersSellQty = "orders_sell_qty";
		/// <summary>
		///     Константа для поля OrdersSellAmount
		/// </summary>
		public const string OrdersSellAmount = "orders_sell_amount";
		/// <summary>
		///     Константа для поля OrdersBuyQty
		/// </summary>
		public const string OrdersBuyQty = "orders_buy_qty";
		/// <summary>
		///     Константа для поля OrdersBuyAmount
		/// </summary>
		public const string OrdersBuyAmount = "orders_buy_amount";
		/// <summary>
		///     Константа для поля OpenPrice
		/// </summary>
		public const string OpenPrice = "open_price";
		/// <summary>
		///     Константа для поля ClosePrice
		/// </summary>
		public const string ClosePrice = "close_price";
		/// <summary>
		///     Константа для поля LocalTime
		/// </summary>
		public const string LocalTime = "local_time";
		/// <summary>
		///     Константа для поля Date
		/// </summary>
		public const string Date = "date";
		/// <summary>
		///     Константа для поля PosExcl
		/// </summary>
		public const string PosExcl = "pos_excl";
		/// <summary>
		///     Константа для поля PosUnexec
		/// </summary>
		public const string PosUnexec = "pos_unexec";
		/// <summary>
		///     Константа для поля Unexec
		/// </summary>
		public const string Unexec = "unexec";
		/// <summary>
		///     Константа для поля SettlPair
		/// </summary>
		public const string SettlPair = "settl_pair";
		/// <summary>
		///     Константа для поля AssetCode
		/// </summary>
		public const string AssetCode = "asset_code";
		/// <summary>
		///     Константа для поля IssueCode
		/// </summary>
		public const string IssueCode = "issue_code";
		/// <summary>
		///     Константа для поля ObligRur
		/// </summary>
		public const string ObligRur = "oblig_rur";
		/// <summary>
		///     Константа для поля ObligQty
		/// </summary>
		public const string ObligQty = "oblig_qty";
		/// <summary>
		///     Константа для поля FulfilRur
		/// </summary>
		public const string FulfilRur = "fulfil_rur";
		/// <summary>
		///     Константа для поля FulfilQty
		/// </summary>
		public const string FulfilQty = "fulfil_qty";
		/// <summary>
		///     Константа для поля Step
		/// </summary>
		public const string Step = "step";
		/// <summary>
		///     Константа для поля IdGen
		/// </summary>
		public const string IdGen = "id_gen";
		/// <summary>
		///     Константа для поля MomentReject
		/// </summary>
		public const string MomentReject = "moment_reject";
		/// <summary>
		///     Константа для поля IdOrd1
		/// </summary>
		public const string IdOrd1 = "id_ord1";
		/// <summary>
		///     Константа для поля RetCode
		/// </summary>
		public const string RetCode = "ret_code";
		/// <summary>
		///     Константа для поля RetMessage
		/// </summary>
		public const string RetMessage = "ret_message";
		/// <summary>
		///     Константа для поля LoginFrom
		/// </summary>
		public const string LoginFrom = "login_from";
		/// <summary>
		///     Константа для поля VmIntercl
		/// </summary>
		public const string VmIntercl = "vm_intercl";
		/// <summary>
		///     Константа для поля BondId
		/// </summary>
		public const string BondId = "bond_id";
		/// <summary>
		///     Константа для поля SmallName
		/// </summary>
		public const string SmallName = "small_name";
		/// <summary>
		///     Константа для поля ShortIsin
		/// </summary>
		public const string ShortIsin = "short_isin";
		/// <summary>
		///     Константа для поля Name
		/// </summary>
		public const string Name = "name";
		/// <summary>
		///     Константа для поля DateRedempt
		/// </summary>
		public const string DateRedempt = "date_redempt";
		/// <summary>
		///     Константа для поля Nominal
		/// </summary>
		public const string Nominal = "nominal";
		/// <summary>
		///     Константа для поля BondType
		/// </summary>
		public const string BondType = "bond_type";
		/// <summary>
		///     Константа для поля YearBase
		/// </summary>
		public const string YearBase = "year_base";
		/// <summary>
		///     Константа для поля CoeffConversion
		/// </summary>
		public const string CoeffConversion = "coeff_conversion";
		/// <summary>
		///     Константа для поля Nkd
		/// </summary>
		public const string Nkd = "nkd";
		/// <summary>
		///     Константа для поля IsCupon
		/// </summary>
		public const string IsCupon = "is_cupon";
		/// <summary>
		///     Константа для поля FaceValue
		/// </summary>
		public const string FaceValue = "face_value";
		/// <summary>
		///     Константа для поля CouponNominal
		/// </summary>
		public const string CouponNominal = "coupon_nominal";
		/// <summary>
		///     Константа для поля IsNominal
		/// </summary>
		public const string IsNominal = "is_nominal";
		/// <summary>
		///     Константа для поля Id
		/// </summary>
		public const string Id = "id";
		/// <summary>
		///     Константа для поля ExecType
		/// </summary>
		public const string ExecType = "exec_type";
		/// <summary>
		///     Константа для поля Curr
		/// </summary>
		public const string Curr = "curr";
		/// <summary>
		///     Константа для поля ExchPay
		/// </summary>
		public const string ExchPay = "exch_pay";
		/// <summary>
		///     Константа для поля ExchPayScalped
		/// </summary>
		public const string ExchPayScalped = "exch_pay_scalped";
		/// <summary>
		///     Константа для поля ClearPay
		/// </summary>
		public const string ClearPay = "clear_pay";
		/// <summary>
		///     Константа для поля ClearPayScalped
		/// </summary>
		public const string ClearPayScalped = "clear_pay_scalped";
		/// <summary>
		///     Константа для поля SellFee
		/// </summary>
		public const string SellFee = "sell_fee";
		/// <summary>
		///     Константа для поля BuyFee
		/// </summary>
		public const string BuyFee = "buy_fee";
		/// <summary>
		///     Константа для поля TradeScheme
		/// </summary>
		public const string TradeScheme = "trade_scheme";
		/// <summary>
		///     Константа для поля Section
		/// </summary>
		public const string Section = "section";
		/// <summary>
		///     Константа для поля ExchPaySpot
		/// </summary>
		public const string ExchPaySpot = "exch_pay_spot";
		/// <summary>
		///     Константа для поля ExchPaySpotRepo
		/// </summary>
		public const string ExchPaySpotRepo = "exch_pay_spot_repo";
		/// <summary>
		///     Константа для поля Begin
		/// </summary>
		public const string Begin = "begin";
		/// <summary>
		///     Константа для поля End
		/// </summary>
		public const string End = "end";
		/// <summary>
		///     Константа для поля OptSessId
		/// </summary>
		public const string OptSessId = "opt_sess_id";
		/// <summary>
		///     Константа для поля InterClBegin
		/// </summary>
		public const string InterClBegin = "inter_cl_begin";
		/// <summary>
		///     Константа для поля InterClEnd
		/// </summary>
		public const string InterClEnd = "inter_cl_end";
		/// <summary>
		///     Константа для поля InterClState
		/// </summary>
		public const string InterClState = "inter_cl_state";
		/// <summary>
		///     Константа для поля EveOn
		/// </summary>
		public const string EveOn = "eve_on";
		/// <summary>
		///     Константа для поля EveBegin
		/// </summary>
		public const string EveBegin = "eve_begin";
		/// <summary>
		///     Константа для поля EveEnd
		/// </summary>
		public const string EveEnd = "eve_end";
		/// <summary>
		///     Константа для поля MonOn
		/// </summary>
		public const string MonOn = "mon_on";
		/// <summary>
		///     Константа для поля MonBegin
		/// </summary>
		public const string MonBegin = "mon_begin";
		/// <summary>
		///     Константа для поля MonEnd
		/// </summary>
		public const string MonEnd = "mon_end";
		/// <summary>
		///     Константа для поля PosTransferBegin
		/// </summary>
		public const string PosTransferBegin = "pos_transfer_begin";
		/// <summary>
		///     Константа для поля PosTransferEnd
		/// </summary>
		public const string PosTransferEnd = "pos_transfer_end";
		/// <summary>
		///     Константа для поля IsinIdLeg
		/// </summary>
		public const string IsinIdLeg = "isin_id_leg";
		/// <summary>
		///     Константа для поля QtyRatio
		/// </summary>
		public const string QtyRatio = "qty_ratio";
		/// <summary>
		///     Константа для поля InstTerm
		/// </summary>
		public const string InstTerm = "inst_term";
		/// <summary>
		///     Константа для поля IsLimited
		/// </summary>
		public const string IsLimited = "is_limited";
		/// <summary>
		///     Константа для поля LimitUp
		/// </summary>
		public const string LimitUp = "limit_up";
		/// <summary>
		///     Константа для поля LimitDown
		/// </summary>
		public const string LimitDown = "limit_down";
		/// <summary>
		///     Константа для поля BuyDeposit
		/// </summary>
		public const string BuyDeposit = "buy_deposit";
		/// <summary>
		///     Константа для поля SellDeposit
		/// </summary>
		public const string SellDeposit = "sell_deposit";
		/// <summary>
		///     Константа для поля Roundto
		/// </summary>
		public const string Roundto = "roundto";
		/// <summary>
		///     Константа для поля MinStep
		/// </summary>
		public const string MinStep = "min_step";
		/// <summary>
		///     Константа для поля LotVolume
		/// </summary>
		public const string LotVolume = "lot_volume";
		/// <summary>
		///     Константа для поля StepPrice
		/// </summary>
		public const string StepPrice = "step_price";
		/// <summary>
		///     Константа для поля DPg
		/// </summary>
		public const string DPg = "d_pg";
		/// <summary>
		///     Константа для поля IsSpread
		/// </summary>
		public const string IsSpread = "is_spread";
		/// <summary>
		///     Константа для поля Coeff
		/// </summary>
		public const string Coeff = "coeff";
		/// <summary>
		///     Константа для поля DExp
		/// </summary>
		public const string DExp = "d_exp";
		/// <summary>
		///     Константа для поля IsPercent
		/// </summary>
		public const string IsPercent = "is_percent";
		/// <summary>
		///     Константа для поля PercentRate
		/// </summary>
		public const string PercentRate = "percent_rate";
		/// <summary>
		///     Константа для поля LastClQuote
		/// </summary>
		public const string LastClQuote = "last_cl_quote";
		/// <summary>
		///     Константа для поля IsTradeEvening
		/// </summary>
		public const string IsTradeEvening = "is_trade_evening";
		/// <summary>
		///     Константа для поля Ticker
		/// </summary>
		public const string Ticker = "ticker";
		/// <summary>
		///     Константа для поля PriceDir
		/// </summary>
		public const string PriceDir = "price_dir";
		/// <summary>
		///     Константа для поля MultilegType
		/// </summary>
		public const string MultilegType = "multileg_type";
		/// <summary>
		///     Константа для поля LegsQty
		/// </summary>
		public const string LegsQty = "legs_qty";
		/// <summary>
		///     Константа для поля StepPriceClr
		/// </summary>
		public const string StepPriceClr = "step_price_clr";
		/// <summary>
		///     Константа для поля StepPriceInterclr
		/// </summary>
		public const string StepPriceInterclr = "step_price_interclr";
		/// <summary>
		///     Константа для поля StepPriceCurr
		/// </summary>
		public const string StepPriceCurr = "step_price_curr";
		/// <summary>
		///     Константа для поля DStart
		/// </summary>
		public const string DStart = "d_start";
		/// <summary>
		///     Константа для поля PctyieldCoeff
		/// </summary>
		public const string PctyieldCoeff = "pctyield_coeff";
		/// <summary>
		///     Константа для поля PctyieldTotal
		/// </summary>
		public const string PctyieldTotal = "pctyield_total";
		/// <summary>
		///     Константа для поля VolatMin
		/// </summary>
		public const string VolatMin = "volat_min";
		/// <summary>
		///     Константа для поля VolatMax
		/// </summary>
		public const string VolatMax = "volat_max";
		/// <summary>
		///     Константа для поля IsLimitOpt
		/// </summary>
		public const string IsLimitOpt = "is_limit_opt";
		/// <summary>
		///     Константа для поля LimitUpOpt
		/// </summary>
		public const string LimitUpOpt = "limit_up_opt";
		/// <summary>
		///     Константа для поля LimitDownOpt
		/// </summary>
		public const string LimitDownOpt = "limit_down_opt";
		/// <summary>
		///     Константа для поля AdmLim
		/// </summary>
		public const string AdmLim = "adm_lim";
		/// <summary>
		///     Константа для поля AdmLimOffmoney
		/// </summary>
		public const string AdmLimOffmoney = "adm_lim_offmoney";
		/// <summary>
		///     Константа для поля ApplyAdmLimit
		/// </summary>
		public const string ApplyAdmLimit = "apply_adm_limit";
		/// <summary>
		///     Константа для поля ExecName
		/// </summary>
		public const string ExecName = "exec_name";
		/// <summary>
		///     Константа для поля RtsCode
		/// </summary>
		public const string RtsCode = "rts_code";
		/// <summary>
		///     Константа для поля TransferCode
		/// </summary>
		public const string TransferCode = "transfer_code";
		/// <summary>
		///     Константа для поля Status
		/// </summary>
		public const string Status = "status";
		/// <summary>
		///     Константа для поля MsgId
		/// </summary>
		public const string MsgId = "msg_id";
		/// <summary>
		///     Константа для поля LangCode
		/// </summary>
		public const string LangCode = "lang_code";
		/// <summary>
		///     Константа для поля Urgency
		/// </summary>
		public const string Urgency = "urgency";
		/// <summary>
		///     Константа для поля Text
		/// </summary>
		public const string Text = "text";
		/// <summary>
		///     Константа для поля MessageBody
		/// </summary>
		public const string MessageBody = "message_body";
		/// <summary>
		///     Константа для поля MarginType
		/// </summary>
		public const string MarginType = "margin_type";
		/// <summary>
		///     Константа для поля ProhibId
		/// </summary>
		public const string ProhibId = "prohib_id";
		/// <summary>
		///     Константа для поля Initiator
		/// </summary>
		public const string Initiator = "initiator";
		/// <summary>
		///     Константа для поля Priority
		/// </summary>
		public const string Priority = "priority";
		/// <summary>
		///     Константа для поля GroupMask
		/// </summary>
		public const string GroupMask = "group_mask";
		/// <summary>
		///     Константа для поля IsLegacy
		/// </summary>
		public const string IsLegacy = "is_legacy";
		/// <summary>
		///     Константа для поля CurrBase
		/// </summary>
		public const string CurrBase = "curr_base";
		/// <summary>
		///     Константа для поля CurrCoupled
		/// </summary>
		public const string CurrCoupled = "curr_coupled";
		/// <summary>
		///     Константа для поля Radius
		/// </summary>
		public const string Radius = "radius";
		/// <summary>
		///     Константа для поля IdOrd
		/// </summary>
		public const string IdOrd = "id_ord";
		/// <summary>
		///     Константа для поля AmountRest
		/// </summary>
		public const string AmountRest = "amount_rest";
		/// <summary>
		///     Константа для поля IdDeal
		/// </summary>
		public const string IdDeal = "id_deal";
		/// <summary>
		///     Константа для поля Xstatus
		/// </summary>
		public const string Xstatus = "xstatus";
		/// <summary>
		///     Константа для поля Action
		/// </summary>
		public const string Action = "action";
		/// <summary>
		///     Константа для поля DealPrice
		/// </summary>
		public const string DealPrice = "deal_price";
		/// <summary>
		///     Константа для поля BrokerToRts
		/// </summary>
		public const string BrokerToRts = "broker_to_rts";
		/// <summary>
		///     Константа для поля BrokerFromRts
		/// </summary>
		public const string BrokerFromRts = "broker_from_rts";
		/// <summary>
		///     Константа для поля SwapPrice
		/// </summary>
		public const string SwapPrice = "swap_price";
		/// <summary>
		///     Константа для поля IdDealMultileg
		/// </summary>
		public const string IdDealMultileg = "id_deal_multileg";
		/// <summary>
		///     Константа для поля IdRepo
		/// </summary>
		public const string IdRepo = "id_repo";
		/// <summary>
		///     Константа для поля IdOrdBuy
		/// </summary>
		public const string IdOrdBuy = "id_ord_buy";
		/// <summary>
		///     Константа для поля IdOrdSell
		/// </summary>
		public const string IdOrdSell = "id_ord_sell";
		/// <summary>
		///     Константа для поля Nosystem
		/// </summary>
		public const string Nosystem = "nosystem";
		/// <summary>
		///     Константа для поля XstatusBuy
		/// </summary>
		public const string XstatusBuy = "xstatus_buy";
		/// <summary>
		///     Константа для поля XstatusSell
		/// </summary>
		public const string XstatusSell = "xstatus_sell";
		/// <summary>
		///     Константа для поля StatusBuy
		/// </summary>
		public const string StatusBuy = "status_buy";
		/// <summary>
		///     Константа для поля StatusSell
		/// </summary>
		public const string StatusSell = "status_sell";
		/// <summary>
		///     Константа для поля ExtIdBuy
		/// </summary>
		public const string ExtIdBuy = "ext_id_buy";
		/// <summary>
		///     Константа для поля ExtIdSell
		/// </summary>
		public const string ExtIdSell = "ext_id_sell";
		/// <summary>
		///     Константа для поля CodeBuy
		/// </summary>
		public const string CodeBuy = "code_buy";
		/// <summary>
		///     Константа для поля CodeSell
		/// </summary>
		public const string CodeSell = "code_sell";
		/// <summary>
		///     Константа для поля CommentBuy
		/// </summary>
		public const string CommentBuy = "comment_buy";
		/// <summary>
		///     Константа для поля CommentSell
		/// </summary>
		public const string CommentSell = "comment_sell";
		/// <summary>
		///     Константа для поля TrustBuy
		/// </summary>
		public const string TrustBuy = "trust_buy";
		/// <summary>
		///     Константа для поля TrustSell
		/// </summary>
		public const string TrustSell = "trust_sell";
		/// <summary>
		///     Константа для поля HedgeBuy
		/// </summary>
		public const string HedgeBuy = "hedge_buy";
		/// <summary>
		///     Константа для поля HedgeSell
		/// </summary>
		public const string HedgeSell = "hedge_sell";
		/// <summary>
		///     Константа для поля FeeBuy
		/// </summary>
		public const string FeeBuy = "fee_buy";
		/// <summary>
		///     Константа для поля FeeSell
		/// </summary>
		public const string FeeSell = "fee_sell";
		/// <summary>
		///     Константа для поля LoginBuy
		/// </summary>
		public const string LoginBuy = "login_buy";
		/// <summary>
		///     Константа для поля LoginSell
		/// </summary>
		public const string LoginSell = "login_sell";
		/// <summary>
		///     Константа для поля CodeRtsBuy
		/// </summary>
		public const string CodeRtsBuy = "code_rts_buy";
		/// <summary>
		///     Константа для поля CodeRtsSell
		/// </summary>
		public const string CodeRtsSell = "code_rts_sell";
		/// <summary>
		///     Константа для поля IsinIdRd
		/// </summary>
		public const string IsinIdRd = "isin_id_rd";
		/// <summary>
		///     Константа для поля IsinIdRb
		/// </summary>
		public const string IsinIdRb = "isin_id_rb";
		/// <summary>
		///     Константа для поля IsinIdRepo
		/// </summary>
		public const string IsinIdRepo = "isin_id_repo";
		/// <summary>
		///     Константа для поля Duration
		/// </summary>
		public const string Duration = "duration";
		/// <summary>
		///     Константа для поля IdDealRd
		/// </summary>
		public const string IdDealRd = "id_deal_rd";
		/// <summary>
		///     Константа для поля IdDealRb
		/// </summary>
		public const string IdDealRb = "id_deal_rb";
		/// <summary>
		///     Константа для поля BuybackAmount
		/// </summary>
		public const string BuybackAmount = "buyback_amount";
		/// <summary>
		///     Константа для поля ServerTime
		/// </summary>
		public const string ServerTime = "server_time";
		/// <summary>
		///     Константа для поля CodeMcs
		/// </summary>
		public const string CodeMcs = "code_mcs";
		/// <summary>
		///     Константа для поля VolatNum
		/// </summary>
		public const string VolatNum = "volat_num";
		/// <summary>
		///     Константа для поля PointsNum
		/// </summary>
		public const string PointsNum = "points_num";
		/// <summary>
		///     Константа для поля SubriskStep
		/// </summary>
		public const string SubriskStep = "subrisk_step";
		/// <summary>
		///     Константа для поля CurrencyVolat
		/// </summary>
		public const string CurrencyVolat = "currency_volat";
		/// <summary>
		///     Константа для поля IsUsd
		/// </summary>
		public const string IsUsd = "is_usd";
		/// <summary>
		///     Константа для поля UsdRateCurvRadius
		/// </summary>
		public const string UsdRateCurvRadius = "usd_rate_curv_radius";
		/// <summary>
		///     Константа для поля Somc
		/// </summary>
		public const string Somc = "somc";
		/// <summary>
		///     Константа для поля Limit
		/// </summary>
		public const string Limit = "limit";
		/// <summary>
		///     Константа для поля SpreadAspect
		/// </summary>
		public const string SpreadAspect = "spread_aspect";
		/// <summary>
		///     Константа для поля Subrisk
		/// </summary>
		public const string Subrisk = "subrisk";
		/// <summary>
		///     Константа для поля BaseGo
		/// </summary>
		public const string BaseGo = "base_go";
		/// <summary>
		///     Константа для поля ExpDate
		/// </summary>
		public const string ExpDate = "exp_date";
		/// <summary>
		///     Константа для поля SpotSigns
		/// </summary>
		public const string SpotSigns = "spot_signs";
		/// <summary>
		///     Константа для поля SettlPriceReal
		/// </summary>
		public const string SettlPriceReal = "settl_price_real";
		/// <summary>
		///     Константа для поля IsinBase
		/// </summary>
		public const string IsinBase = "isin_base";
		/// <summary>
		///     Константа для поля IsNetPositive
		/// </summary>
		public const string IsNetPositive = "is_net_positive";
		/// <summary>
		///     Константа для поля VolatRange
		/// </summary>
		public const string VolatRange = "volat_range";
		/// <summary>
		///     Константа для поля TSquared
		/// </summary>
		public const string TSquared = "t_squared";
		/// <summary>
		///     Константа для поля MaxAddrisk
		/// </summary>
		public const string MaxAddrisk = "max_addrisk";
		/// <summary>
		///     Константа для поля A
		/// </summary>
		public const string A = "a";
		/// <summary>
		///     Константа для поля B
		/// </summary>
		public const string B = "b";
		/// <summary>
		///     Константа для поля C
		/// </summary>
		public const string C = "c";
		/// <summary>
		///     Константа для поля D
		/// </summary>
		public const string D = "d";
		/// <summary>
		///     Константа для поля E
		/// </summary>
		public const string E = "e";
		/// <summary>
		///     Константа для поля S
		/// </summary>
		public const string S = "s";
		/// <summary>
		///     Константа для поля FutType
		/// </summary>
		public const string FutType = "fut_type";
		/// <summary>
		///     Константа для поля UseNullVolat
		/// </summary>
		public const string UseNullVolat = "use_null_volat";
		/// <summary>
		///     Константа для поля ExpClearingsBf
		/// </summary>
		public const string ExpClearingsBf = "exp_clearings_bf";
		/// <summary>
		///     Константа для поля ExpClearingsCc
		/// </summary>
		public const string ExpClearingsCc = "exp_clearings_cc";
		/// <summary>
		///     Константа для поля Strike
		/// </summary>
		public const string Strike = "strike";
		/// <summary>
		///     Константа для поля OptType
		/// </summary>
		public const string OptType = "opt_type";
		/// <summary>
		///     Константа для поля BaseGoSell
		/// </summary>
		public const string BaseGoSell = "base_go_sell";
		/// <summary>
		///     Константа для поля SynthBaseGo
		/// </summary>
		public const string SynthBaseGo = "synth_base_go";
		/// <summary>
		///     Константа для поля BaseGoBuy
		/// </summary>
		public const string BaseGoBuy = "base_go_buy";
		/// <summary>
		///     Константа для поля LimitSpotSell
		/// </summary>
		public const string LimitSpotSell = "limit_spot_sell";
		/// <summary>
		///     Константа для поля UsedLimitSpotSell
		/// </summary>
		public const string UsedLimitSpotSell = "used_limit_spot_sell";
		/// <summary>
		///     Константа для поля Spread
		/// </summary>
		public const string Spread = "spread";
		/// <summary>
		///     Константа для поля PriceEdgeSell
		/// </summary>
		public const string PriceEdgeSell = "price_edge_sell";
		/// <summary>
		///     Константа для поля AmountSells
		/// </summary>
		public const string AmountSells = "amount_sells";
		/// <summary>
		///     Константа для поля PriceEdgeBuy
		/// </summary>
		public const string PriceEdgeBuy = "price_edge_buy";
		/// <summary>
		///     Константа для поля AmountBuys
		/// </summary>
		public const string AmountBuys = "amount_buys";
		/// <summary>
		///     Константа для поля MmSpread
		/// </summary>
		public const string MmSpread = "mm_spread";
		/// <summary>
		///     Константа для поля MmAmount
		/// </summary>
		public const string MmAmount = "mm_amount";
		/// <summary>
		///     Константа для поля SpreadSign
		/// </summary>
		public const string SpreadSign = "spread_sign";
		/// <summary>
		///     Константа для поля AmountSign
		/// </summary>
		public const string AmountSign = "amount_sign";
		/// <summary>
		///     Константа для поля PercentTime
		/// </summary>
		public const string PercentTime = "percent_time";
		/// <summary>
		///     Константа для поля PeriodStart
		/// </summary>
		public const string PeriodStart = "period_start";
		/// <summary>
		///     Константа для поля PeriodEnd
		/// </summary>
		public const string PeriodEnd = "period_end";
		/// <summary>
		///     Константа для поля ActiveSign
		/// </summary>
		public const string ActiveSign = "active_sign";
		/// <summary>
		///     Константа для поля AgmtId
		/// </summary>
		public const string AgmtId = "agmt_id";
		/// <summary>
		///     Константа для поля FulfilMin
		/// </summary>
		public const string FulfilMin = "fulfil_min";
		/// <summary>
		///     Константа для поля FulfilPartial
		/// </summary>
		public const string FulfilPartial = "fulfil_partial";
		/// <summary>
		///     Константа для поля FulfilTotal
		/// </summary>
		public const string FulfilTotal = "fulfil_total";
		/// <summary>
		///     Константа для поля IsFulfilMin
		/// </summary>
		public const string IsFulfilMin = "is_fulfil_min";
		/// <summary>
		///     Константа для поля IsFulfilPartial
		/// </summary>
		public const string IsFulfilPartial = "is_fulfil_partial";
		/// <summary>
		///     Константа для поля IsFulfilTotal
		/// </summary>
		public const string IsFulfilTotal = "is_fulfil_total";
		/// <summary>
		///     Константа для поля IsRf
		/// </summary>
		public const string IsRf = "is_rf";
		/// <summary>
		///     Константа для поля IdGroup
		/// </summary>
		public const string IdGroup = "id_group";
		/// <summary>
		///     Константа для поля CstrikeOffset
		/// </summary>
		public const string CstrikeOffset = "cstrike_offset";
		/// <summary>
		///     Константа для поля Agreement
		/// </summary>
		public const string Agreement = "agreement";
		/// <summary>
		///     Константа для поля IsFut
		/// </summary>
		public const string IsFut = "is_fut";
		/// <summary>
		///     Константа для поля IsinIsSpec
		/// </summary>
		public const string IsinIsSpec = "isin_is_spec";
		/// <summary>
		///     Константа для поля ExporderId
		/// </summary>
		public const string ExporderId = "exporder_id";
		/// <summary>
		///     Константа для поля AmountApply
		/// </summary>
		public const string AmountApply = "amount_apply";
		/// <summary>
		///     Константа для поля CoeffOut
		/// </summary>
		public const string CoeffOut = "coeff_out";
		/// <summary>
		///     Константа для поля IsSpec
		/// </summary>
		public const string IsSpec = "is_spec";
		/// <summary>
		///     Константа для поля SpecSpread
		/// </summary>
		public const string SpecSpread = "spec_spread";
		/// <summary>
		///     Константа для поля MinVol
		/// </summary>
		public const string MinVol = "min_vol";
		/// <summary>
		///     Константа для поля FutIsinId
		/// </summary>
		public const string FutIsinId = "fut_isin_id";
		/// <summary>
		///     Константа для поля BgoC
		/// </summary>
		public const string BgoC = "bgo_c";
		/// <summary>
		///     Константа для поля BgoNc
		/// </summary>
		public const string BgoNc = "bgo_nc";
		/// <summary>
		///     Константа для поля Europe
		/// </summary>
		public const string Europe = "europe";
		/// <summary>
		///     Константа для поля Put
		/// </summary>
		public const string Put = "put";
		/// <summary>
		///     Константа для поля DExecBeg
		/// </summary>
		public const string DExecBeg = "d_exec_beg";
		/// <summary>
		///     Константа для поля DExecEnd
		/// </summary>
		public const string DExecEnd = "d_exec_end";
		/// <summary>
		///     Константа для поля BgoBuy
		/// </summary>
		public const string BgoBuy = "bgo_buy";
		/// <summary>
		///     Константа для поля BaseIsinId
		/// </summary>
		public const string BaseIsinId = "base_isin_id";
		/// <summary>
		///     Константа для поля InitMoment
		/// </summary>
		public const string InitMoment = "init_moment";
		/// <summary>
		///     Константа для поля InitAmount
		/// </summary>
		public const string InitAmount = "init_amount";
		/// <summary>
		///     Константа для поля InfoId
		/// </summary>
		public const string InfoId = "info_id";
		/// <summary>
		///     Константа для поля LogRev
		/// </summary>
		public const string LogRev = "log_rev";
		/// <summary>
		///     Константа для поля LifeNum
		/// </summary>
		public const string LifeNum = "life_num";
		/// <summary>
		///     Константа для поля Volume
		/// </summary>
		public const string Volume = "volume";
		/// <summary>
		///     Константа для поля MoneyFree
		/// </summary>
		public const string MoneyFree = "money_free";
		/// <summary>
		///     Константа для поля MoneyBlocked
		/// </summary>
		public const string MoneyBlocked = "money_blocked";
		/// <summary>
		///     Константа для поля PledgeFree
		/// </summary>
		public const string PledgeFree = "pledge_free";
		/// <summary>
		///     Константа для поля PledgeBlocked
		/// </summary>
		public const string PledgeBlocked = "pledge_blocked";
		/// <summary>
		///     Константа для поля VmReserve
		/// </summary>
		public const string VmReserve = "vm_reserve";
		/// <summary>
		///     Константа для поля BalanceMoney
		/// </summary>
		public const string BalanceMoney = "balance_money";
		/// <summary>
		///     Константа для поля LimitsSet
		/// </summary>
		public const string LimitsSet = "limits_set";
		/// <summary>
		///     Константа для поля MoneyOld
		/// </summary>
		public const string MoneyOld = "money_old";
		/// <summary>
		///     Константа для поля MoneyAmount
		/// </summary>
		public const string MoneyAmount = "money_amount";
		/// <summary>
		///     Константа для поля PledgeOld
		/// </summary>
		public const string PledgeOld = "pledge_old";
		/// <summary>
		///     Константа для поля PledgeAmount
		/// </summary>
		public const string PledgeAmount = "pledge_amount";
		/// <summary>
		///     Константа для поля MoneyPledgeAmount
		/// </summary>
		public const string MoneyPledgeAmount = "money_pledge_amount";
		/// <summary>
		///     Константа для поля LiquidityRatio
		/// </summary>
		public const string LiquidityRatio = "liquidity_ratio";
		/// <summary>
		///     Константа для поля BuysQty
		/// </summary>
		public const string BuysQty = "buys_qty";
		/// <summary>
		///     Константа для поля SellsQty
		/// </summary>
		public const string SellsQty = "sells_qty";
		/// <summary>
		///     Константа для поля OpenQty
		/// </summary>
		public const string OpenQty = "open_qty";
		/// <summary>
		///     Константа для поля Waprice
		/// </summary>
		public const string Waprice = "waprice";
		/// <summary>
		///     Константа для поля NetVolumeRur
		/// </summary>
		public const string NetVolumeRur = "net_volume_rur";
		/// <summary>
		///     Константа для поля LastDealId
		/// </summary>
		public const string LastDealId = "last_deal_id";
		/// <summary>
		///     Константа для поля Value
		/// </summary>
		public const string Value = "value";
		/// <summary>
		///     Константа для поля PrevCloseValue
		/// </summary>
		public const string PrevCloseValue = "prev_close_value";
		/// <summary>
		///     Константа для поля OpenValue
		/// </summary>
		public const string OpenValue = "open_value";
		/// <summary>
		///     Константа для поля MaxValue
		/// </summary>
		public const string MaxValue = "max_value";
		/// <summary>
		///     Константа для поля MinValue
		/// </summary>
		public const string MinValue = "min_value";
		/// <summary>
		///     Константа для поля UsdRate
		/// </summary>
		public const string UsdRate = "usd_rate";
		/// <summary>
		///     Константа для поля Cap
		/// </summary>
		public const string Cap = "cap";
		/// <summary>
		///     Константа для поля Time
		/// </summary>
		public const string Time = "time";
		/// <summary>
		///     Константа для поля P2login
		/// </summary>
		public const string P2login = "p2login";
		/// <summary>
		///     Константа для поля Points
		/// </summary>
		public const string Points = "points";
		/// <summary>
		///     Константа для поля TnType
		/// </summary>
		public const string TnType = "tn_type";
		/// <summary>
		///     Константа для поля ErrCode
		/// </summary>
		public const string ErrCode = "err_code";
		/// <summary>
		///     Константа для поля Count
		/// </summary>
		public const string Count = "count";
		/// <summary>
		///     Константа для поля VmReal
		/// </summary>
		public const string VmReal = "vm_real";
		/// <summary>
		///     Константа для поля TheorPriceLimit
		/// </summary>
		public const string TheorPriceLimit = "theor_price_limit";
		/// <summary>
		///     Константа для поля UpPrem
		/// </summary>
		public const string UpPrem = "up_prem";
		/// <summary>
		///     Константа для поля DownPrem
		/// </summary>
		public const string DownPrem = "down_prem";
	}
}

namespace CGateAdapter.Messages.Clr
{
    /// <summary>
    ///     Сообщение cgm_money_clearing
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmMoneyClearing : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "money_clearing";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_MoneyClearing;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(101)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(102)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(103)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(104)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле share
        /// </summary>
        [ProtoMember(105)]
        public sbyte Share { get; set; }

        /// <summary>
        ///     Поле amount_beg
        /// </summary>
        [ProtoMember(106)]
        public double AmountBeg { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(107)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле premium
        /// </summary>
        [ProtoMember(108)]
        public double Premium { get; set; }

        /// <summary>
        ///     Поле pay
        /// </summary>
        [ProtoMember(109)]
        public double Pay { get; set; }

        /// <summary>
        ///     Поле fee_fut
        /// </summary>
        [ProtoMember(110)]
        public double FeeFut { get; set; }

        /// <summary>
        ///     Поле fee_opt
        /// </summary>
        [ProtoMember(111)]
        public double FeeOpt { get; set; }

        /// <summary>
        ///     Поле go
        /// </summary>
        [ProtoMember(112)]
        public double Go { get; set; }

        /// <summary>
        ///     Поле amount_end
        /// </summary>
        [ProtoMember(113)]
        public double AmountEnd { get; set; }

        /// <summary>
        ///     Поле free
        /// </summary>
        [ProtoMember(114)]
        public double Free { get; set; }

        /// <summary>
        ///     Поле ext_reserve
        /// </summary>
        [ProtoMember(115)]
        public double ExtReserve { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Share, Share);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBeg, AmountBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Premium, Premium);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pay, Pay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeFut, FeeFut);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeOpt, FeeOpt);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Go, Go);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountEnd, AmountEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Free, Free);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtReserve, ExtReserve);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_money_clearing_sa
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmMoneyClearingSa : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "money_clearing_sa";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_MoneyClearingSa;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(116)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(117)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(118)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле settlement_account
        /// </summary>
        [ProtoMember(119)]
        public string SettlementAccount { get; set; }

        /// <summary>
        ///     Поле share
        /// </summary>
        [ProtoMember(120)]
        public sbyte Share { get; set; }

        /// <summary>
        ///     Поле amount_beg
        /// </summary>
        [ProtoMember(121)]
        public double AmountBeg { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(122)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле premium
        /// </summary>
        [ProtoMember(123)]
        public double Premium { get; set; }

        /// <summary>
        ///     Поле pay
        /// </summary>
        [ProtoMember(124)]
        public double Pay { get; set; }

        /// <summary>
        ///     Поле fee_fut
        /// </summary>
        [ProtoMember(125)]
        public double FeeFut { get; set; }

        /// <summary>
        ///     Поле fee_opt
        /// </summary>
        [ProtoMember(126)]
        public double FeeOpt { get; set; }

        /// <summary>
        ///     Поле go
        /// </summary>
        [ProtoMember(127)]
        public double Go { get; set; }

        /// <summary>
        ///     Поле amount_end
        /// </summary>
        [ProtoMember(128)]
        public double AmountEnd { get; set; }

        /// <summary>
        ///     Поле free
        /// </summary>
        [ProtoMember(129)]
        public double Free { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlementAccount, SettlementAccount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Share, Share);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBeg, AmountBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Premium, Premium);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pay, Pay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeFut, FeeFut);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeOpt, FeeOpt);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Go, Go);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountEnd, AmountEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Free, Free);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_clr_rate
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmClrRate : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "clr_rate";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_ClrRate;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(130)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(131)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(132)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле rate
        /// </summary>
        [ProtoMember(133)]
        public double Rate { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(134)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле signs
        /// </summary>
        [ProtoMember(135)]
        public sbyte Signs { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(136)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле rate_id
        /// </summary>
        [ProtoMember(137)]
        public int RateId { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Rate, Rate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Signs, Signs);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RateId, RateId);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_pos
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutPos : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_pos";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_FutPos;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(138)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(139)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(140)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(141)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(142)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(143)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(144)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле account
        /// </summary>
        [ProtoMember(145)]
        public sbyte Account { get; set; }

        /// <summary>
        ///     Поле pos_beg
        /// </summary>
        [ProtoMember(146)]
        public int PosBeg { get; set; }

        /// <summary>
        ///     Поле pos_end
        /// </summary>
        [ProtoMember(147)]
        public int PosEnd { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(148)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле fee
        /// </summary>
        [ProtoMember(149)]
        public double Fee { get; set; }

        /// <summary>
        ///     Поле accum_go
        /// </summary>
        [ProtoMember(150)]
        public double AccumGo { get; set; }

        /// <summary>
        ///     Поле fee_ex
        /// </summary>
        [ProtoMember(151)]
        public double FeeEx { get; set; }

        /// <summary>
        ///     Поле vat_ex
        /// </summary>
        [ProtoMember(152)]
        public double VatEx { get; set; }

        /// <summary>
        ///     Поле fee_cc
        /// </summary>
        [ProtoMember(153)]
        public double FeeCc { get; set; }

        /// <summary>
        ///     Поле vat_cc
        /// </summary>
        [ProtoMember(154)]
        public double VatCc { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Account, Account);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosBeg, PosBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosEnd, PosEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Fee, Fee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AccumGo, AccumGo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeEx, FeeEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatEx, VatEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeCc, FeeCc);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatCc, VatCc);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_pos
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptPos : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_pos";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_OptPos;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(155)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(156)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(157)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(158)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(159)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(160)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(161)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле account
        /// </summary>
        [ProtoMember(162)]
        public sbyte Account { get; set; }

        /// <summary>
        ///     Поле pos_beg
        /// </summary>
        [ProtoMember(163)]
        public int PosBeg { get; set; }

        /// <summary>
        ///     Поле pos_end
        /// </summary>
        [ProtoMember(164)]
        public int PosEnd { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(165)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле fee
        /// </summary>
        [ProtoMember(166)]
        public double Fee { get; set; }

        /// <summary>
        ///     Поле fee_ex
        /// </summary>
        [ProtoMember(167)]
        public double FeeEx { get; set; }

        /// <summary>
        ///     Поле vat_ex
        /// </summary>
        [ProtoMember(168)]
        public double VatEx { get; set; }

        /// <summary>
        ///     Поле fee_cc
        /// </summary>
        [ProtoMember(169)]
        public double FeeCc { get; set; }

        /// <summary>
        ///     Поле vat_cc
        /// </summary>
        [ProtoMember(170)]
        public double VatCc { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Account, Account);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosBeg, PosBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosEnd, PosEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Fee, Fee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeEx, FeeEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatEx, VatEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeCc, FeeCc);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatCc, VatCc);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_pos_sa
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutPosSa : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_pos_sa";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_FutPosSa;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(171)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(172)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(173)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(174)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(175)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(176)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле settlement_account
        /// </summary>
        [ProtoMember(177)]
        public string SettlementAccount { get; set; }

        /// <summary>
        ///     Поле pos_beg
        /// </summary>
        [ProtoMember(178)]
        public int PosBeg { get; set; }

        /// <summary>
        ///     Поле pos_end
        /// </summary>
        [ProtoMember(179)]
        public int PosEnd { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(180)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле fee
        /// </summary>
        [ProtoMember(181)]
        public double Fee { get; set; }

        /// <summary>
        ///     Поле fee_ex
        /// </summary>
        [ProtoMember(182)]
        public double FeeEx { get; set; }

        /// <summary>
        ///     Поле vat_ex
        /// </summary>
        [ProtoMember(183)]
        public double VatEx { get; set; }

        /// <summary>
        ///     Поле fee_cc
        /// </summary>
        [ProtoMember(184)]
        public double FeeCc { get; set; }

        /// <summary>
        ///     Поле vat_cc
        /// </summary>
        [ProtoMember(185)]
        public double VatCc { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlementAccount, SettlementAccount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosBeg, PosBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosEnd, PosEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Fee, Fee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeEx, FeeEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatEx, VatEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeCc, FeeCc);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatCc, VatCc);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_pos_sa
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptPosSa : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_pos_sa";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_OptPosSa;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(186)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(187)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(188)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(189)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(190)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(191)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле settlement_account
        /// </summary>
        [ProtoMember(192)]
        public string SettlementAccount { get; set; }

        /// <summary>
        ///     Поле pos_beg
        /// </summary>
        [ProtoMember(193)]
        public int PosBeg { get; set; }

        /// <summary>
        ///     Поле pos_end
        /// </summary>
        [ProtoMember(194)]
        public int PosEnd { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(195)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле fee
        /// </summary>
        [ProtoMember(196)]
        public double Fee { get; set; }

        /// <summary>
        ///     Поле fee_ex
        /// </summary>
        [ProtoMember(197)]
        public double FeeEx { get; set; }

        /// <summary>
        ///     Поле vat_ex
        /// </summary>
        [ProtoMember(198)]
        public double VatEx { get; set; }

        /// <summary>
        ///     Поле fee_cc
        /// </summary>
        [ProtoMember(199)]
        public double FeeCc { get; set; }

        /// <summary>
        ///     Поле vat_cc
        /// </summary>
        [ProtoMember(200)]
        public double VatCc { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlementAccount, SettlementAccount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosBeg, PosBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosEnd, PosEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Fee, Fee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeEx, FeeEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatEx, VatEx);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeCc, FeeCc);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VatCc, VatCc);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_sess_settl
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutSessSettl : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_sess_settl";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_FutSessSettl;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(201)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(202)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(203)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(204)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле date_clr
        /// </summary>
        [ProtoMember(205)]
        public DateTime DateClr { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(206)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(207)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле settl_price
        /// </summary>
        [ProtoMember(208)]
        public double SettlPrice { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateClr, DateClr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlPrice, SettlPrice);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_sess_settl
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptSessSettl : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_sess_settl";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_OptSessSettl;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(209)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(210)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(211)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(212)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле date_clr
        /// </summary>
        [ProtoMember(213)]
        public DateTime DateClr { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(214)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(215)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле volat
        /// </summary>
        [ProtoMember(216)]
        public double Volat { get; set; }

        /// <summary>
        ///     Поле theor_price
        /// </summary>
        [ProtoMember(217)]
        public double TheorPrice { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateClr, DateClr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Volat, Volat);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TheorPrice, TheorPrice);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_pledge_details
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmPledgeDetails : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "pledge_details";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_PledgeDetails;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(218)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(219)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(220)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(221)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле pledge_name
        /// </summary>
        [ProtoMember(222)]
        public string PledgeName { get; set; }

        /// <summary>
        ///     Поле amount_beg
        /// </summary>
        [ProtoMember(223)]
        public double AmountBeg { get; set; }

        /// <summary>
        ///     Поле pay
        /// </summary>
        [ProtoMember(224)]
        public double Pay { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(225)]
        public double Amount { get; set; }

        /// <summary>
        ///     Поле rate
        /// </summary>
        [ProtoMember(226)]
        public double Rate { get; set; }

        /// <summary>
        ///     Поле amount_beg_money
        /// </summary>
        [ProtoMember(227)]
        public double AmountBegMoney { get; set; }

        /// <summary>
        ///     Поле pay_money
        /// </summary>
        [ProtoMember(228)]
        public double PayMoney { get; set; }

        /// <summary>
        ///     Поле amount_money
        /// </summary>
        [ProtoMember(229)]
        public double AmountMoney { get; set; }

        /// <summary>
        ///     Поле com_ensure
        /// </summary>
        [ProtoMember(230)]
        public sbyte ComEnsure { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PledgeName, PledgeName);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBeg, AmountBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pay, Pay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Rate, Rate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBegMoney, AmountBegMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PayMoney, PayMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountMoney, AmountMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ComEnsure, ComEnsure);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Clr_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Clr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(231)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(232)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(233)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(234)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(235)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(236)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(237)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.FortsMessages
{
    /// <summary>
    ///     Сообщение cgm_FutAddOrder
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutAddOrder : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutAddOrder";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutAddOrder;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(238)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(239)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(240)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле type
        /// </summary>
        [ProtoMember(241)]
        public int Type { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(242)]
        public int Dir { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(243)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(244)]
        public string Price { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(245)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле broker_to
        /// </summary>
        [ProtoMember(246)]
        public string BrokerTo { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(247)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле du
        /// </summary>
        [ProtoMember(248)]
        public int Du { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(249)]
        public string DateExp { get; set; }

        /// <summary>
        ///     Поле hedge
        /// </summary>
        [ProtoMember(250)]
        public int Hedge { get; set; }

        /// <summary>
        ///     Поле dont_check_money
        /// </summary>
        [ProtoMember(251)]
        public int DontCheckMoney { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(252)]
        public DateTime LocalStamp { get; set; }

        /// <summary>
        ///     Поле match_ref
        /// </summary>
        [ProtoMember(253)]
        public string MatchRef { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 64;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Type, Type);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerTo, BrokerTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Du, Du);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Hedge, Hedge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DontCheckMoney, DontCheckMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MatchRef, MatchRef);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG101
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg101 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG101";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg101;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(254)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(255)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(256)]
        public long OrderId { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 101;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutAddMultiLegOrder
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutAddMultiLegOrder : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutAddMultiLegOrder";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutAddMultiLegOrder;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(257)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(258)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(259)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(260)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле type
        /// </summary>
        [ProtoMember(261)]
        public int Type { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(262)]
        public int Dir { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(263)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(264)]
        public string Price { get; set; }

        /// <summary>
        ///     Поле rate_price
        /// </summary>
        [ProtoMember(265)]
        public string RatePrice { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(266)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле hedge
        /// </summary>
        [ProtoMember(267)]
        public int Hedge { get; set; }

        /// <summary>
        ///     Поле broker_to
        /// </summary>
        [ProtoMember(268)]
        public string BrokerTo { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(269)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле trust
        /// </summary>
        [ProtoMember(270)]
        public int Trust { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(271)]
        public string DateExp { get; set; }

        /// <summary>
        ///     Поле trade_mode
        /// </summary>
        [ProtoMember(272)]
        public int TradeMode { get; set; }

        /// <summary>
        ///     Поле dont_check_money
        /// </summary>
        [ProtoMember(273)]
        public int DontCheckMoney { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(274)]
        public DateTime LocalStamp { get; set; }

        /// <summary>
        ///     Поле match_ref
        /// </summary>
        [ProtoMember(275)]
        public string MatchRef { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 65;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Type, Type);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RatePrice, RatePrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Hedge, Hedge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerTo, BrokerTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Trust, Trust);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TradeMode, TradeMode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DontCheckMoney, DontCheckMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MatchRef, MatchRef);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG129
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg129 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG129";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg129;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(276)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(277)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(278)]
        public long OrderId { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 129;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutDelOrder
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutDelOrder : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutDelOrder";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutDelOrder;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(279)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(280)]
        public long OrderId { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(281)]
        public DateTime LocalStamp { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 37;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG102
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg102 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG102";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg102;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(282)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(283)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(284)]
        public int Amount { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 102;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutDelUserOrders
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutDelUserOrders : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutDelUserOrders";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutDelUserOrders;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(285)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле buy_sell
        /// </summary>
        [ProtoMember(286)]
        public int BuySell { get; set; }

        /// <summary>
        ///     Поле non_system
        /// </summary>
        [ProtoMember(287)]
        public int NonSystem { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(288)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(289)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(290)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле work_mode
        /// </summary>
        [ProtoMember(291)]
        public int WorkMode { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(292)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(293)]
        public DateTime LocalStamp { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 38;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuySell, BuySell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NonSystem, NonSystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.WorkMode, WorkMode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG103
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg103 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG103";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg103;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(294)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(295)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле num_orders
        /// </summary>
        [ProtoMember(296)]
        public int NumOrders { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 103;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NumOrders, NumOrders);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutMoveOrder
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutMoveOrder : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutMoveOrder";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutMoveOrder;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(297)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле regime
        /// </summary>
        [ProtoMember(298)]
        public int Regime { get; set; }

        /// <summary>
        ///     Поле order_id1
        /// </summary>
        [ProtoMember(299)]
        public long OrderId1 { get; set; }

        /// <summary>
        ///     Поле amount1
        /// </summary>
        [ProtoMember(300)]
        public int Amount1 { get; set; }

        /// <summary>
        ///     Поле price1
        /// </summary>
        [ProtoMember(301)]
        public string Price1 { get; set; }

        /// <summary>
        ///     Поле ext_id1
        /// </summary>
        [ProtoMember(302)]
        public int ExtId1 { get; set; }

        /// <summary>
        ///     Поле order_id2
        /// </summary>
        [ProtoMember(303)]
        public long OrderId2 { get; set; }

        /// <summary>
        ///     Поле amount2
        /// </summary>
        [ProtoMember(304)]
        public int Amount2 { get; set; }

        /// <summary>
        ///     Поле price2
        /// </summary>
        [ProtoMember(305)]
        public string Price2 { get; set; }

        /// <summary>
        ///     Поле ext_id2
        /// </summary>
        [ProtoMember(306)]
        public int ExtId2 { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(307)]
        public DateTime LocalStamp { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 39;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Regime, Regime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId1, OrderId1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount1, Amount1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price1, Price1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId1, ExtId1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId2, OrderId2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount2, Amount2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price2, Price2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId2, ExtId2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG105
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg105 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG105";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg105;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(308)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(309)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле order_id1
        /// </summary>
        [ProtoMember(310)]
        public long OrderId1 { get; set; }

        /// <summary>
        ///     Поле order_id2
        /// </summary>
        [ProtoMember(311)]
        public long OrderId2 { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 105;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId1, OrderId1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId2, OrderId2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptAddOrder
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptAddOrder : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptAddOrder";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptAddOrder;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(312)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(313)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(314)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле type
        /// </summary>
        [ProtoMember(315)]
        public int Type { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(316)]
        public int Dir { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(317)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(318)]
        public string Price { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(319)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле broker_to
        /// </summary>
        [ProtoMember(320)]
        public string BrokerTo { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(321)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле du
        /// </summary>
        [ProtoMember(322)]
        public int Du { get; set; }

        /// <summary>
        ///     Поле check_limit
        /// </summary>
        [ProtoMember(323)]
        public int CheckLimit { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(324)]
        public string DateExp { get; set; }

        /// <summary>
        ///     Поле hedge
        /// </summary>
        [ProtoMember(325)]
        public int Hedge { get; set; }

        /// <summary>
        ///     Поле dont_check_money
        /// </summary>
        [ProtoMember(326)]
        public int DontCheckMoney { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(327)]
        public DateTime LocalStamp { get; set; }

        /// <summary>
        ///     Поле match_ref
        /// </summary>
        [ProtoMember(328)]
        public string MatchRef { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 66;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Type, Type);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerTo, BrokerTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Du, Du);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CheckLimit, CheckLimit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Hedge, Hedge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DontCheckMoney, DontCheckMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MatchRef, MatchRef);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG109
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg109 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG109";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg109;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(329)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(330)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(331)]
        public long OrderId { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 109;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptDelOrder
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptDelOrder : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptDelOrder";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptDelOrder;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(332)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(333)]
        public long OrderId { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(334)]
        public DateTime LocalStamp { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 42;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG110
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg110 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG110";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg110;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(335)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(336)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(337)]
        public int Amount { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 110;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptDelUserOrders
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptDelUserOrders : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptDelUserOrders";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptDelUserOrders;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(338)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле buy_sell
        /// </summary>
        [ProtoMember(339)]
        public int BuySell { get; set; }

        /// <summary>
        ///     Поле non_system
        /// </summary>
        [ProtoMember(340)]
        public int NonSystem { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(341)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(342)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(343)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле work_mode
        /// </summary>
        [ProtoMember(344)]
        public int WorkMode { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(345)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(346)]
        public DateTime LocalStamp { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 43;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuySell, BuySell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NonSystem, NonSystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.WorkMode, WorkMode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG111
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg111 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG111";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg111;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(347)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(348)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле num_orders
        /// </summary>
        [ProtoMember(349)]
        public int NumOrders { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 111;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NumOrders, NumOrders);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptMoveOrder
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptMoveOrder : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptMoveOrder";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptMoveOrder;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(350)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле regime
        /// </summary>
        [ProtoMember(351)]
        public int Regime { get; set; }

        /// <summary>
        ///     Поле order_id1
        /// </summary>
        [ProtoMember(352)]
        public long OrderId1 { get; set; }

        /// <summary>
        ///     Поле amount1
        /// </summary>
        [ProtoMember(353)]
        public int Amount1 { get; set; }

        /// <summary>
        ///     Поле price1
        /// </summary>
        [ProtoMember(354)]
        public string Price1 { get; set; }

        /// <summary>
        ///     Поле ext_id1
        /// </summary>
        [ProtoMember(355)]
        public int ExtId1 { get; set; }

        /// <summary>
        ///     Поле check_limit
        /// </summary>
        [ProtoMember(356)]
        public int CheckLimit { get; set; }

        /// <summary>
        ///     Поле order_id2
        /// </summary>
        [ProtoMember(357)]
        public long OrderId2 { get; set; }

        /// <summary>
        ///     Поле amount2
        /// </summary>
        [ProtoMember(358)]
        public int Amount2 { get; set; }

        /// <summary>
        ///     Поле price2
        /// </summary>
        [ProtoMember(359)]
        public string Price2 { get; set; }

        /// <summary>
        ///     Поле ext_id2
        /// </summary>
        [ProtoMember(360)]
        public int ExtId2 { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(361)]
        public DateTime LocalStamp { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 44;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Regime, Regime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId1, OrderId1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount1, Amount1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price1, Price1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId1, ExtId1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CheckLimit, CheckLimit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId2, OrderId2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount2, Amount2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price2, Price2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId2, ExtId2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG113
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg113 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG113";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg113;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(362)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(363)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле order_id1
        /// </summary>
        [ProtoMember(364)]
        public long OrderId1 { get; set; }

        /// <summary>
        ///     Поле order_id2
        /// </summary>
        [ProtoMember(365)]
        public long OrderId2 { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 113;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId1, OrderId1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId2, OrderId2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutChangeClientMoney
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutChangeClientMoney : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutChangeClientMoney";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutChangeClientMoney;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(366)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле mode
        /// </summary>
        [ProtoMember(367)]
        public int Mode { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(368)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле limit_money
        /// </summary>
        [ProtoMember(369)]
        public string LimitMoney { get; set; }

        /// <summary>
        ///     Поле limit_pledge
        /// </summary>
        [ProtoMember(370)]
        public string LimitPledge { get; set; }

        /// <summary>
        ///     Поле coeff_liquidity
        /// </summary>
        [ProtoMember(371)]
        public string CoeffLiquidity { get; set; }

        /// <summary>
        ///     Поле coeff_go
        /// </summary>
        [ProtoMember(372)]
        public string CoeffGo { get; set; }

        /// <summary>
        ///     Поле is_auto_update_limit
        /// </summary>
        [ProtoMember(373)]
        public int IsAutoUpdateLimit { get; set; }

        /// <summary>
        ///     Поле no_fut_discount
        /// </summary>
        [ProtoMember(374)]
        public int NoFutDiscount { get; set; }

        /// <summary>
        ///     Поле check_limit
        /// </summary>
        [ProtoMember(375)]
        public int CheckLimit { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 67;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Mode, Mode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitMoney, LimitMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitPledge, LimitPledge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CoeffLiquidity, CoeffLiquidity);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CoeffGo, CoeffGo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsAutoUpdateLimit, IsAutoUpdateLimit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NoFutDiscount, NoFutDiscount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CheckLimit, CheckLimit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG104
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg104 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG104";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg104;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(376)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(377)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 104;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutChangeBFMoney
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutChangeBfmoney : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutChangeBFMoney";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutChangeBfmoney;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(378)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле mode
        /// </summary>
        [ProtoMember(379)]
        public int Mode { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(380)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле limit_money
        /// </summary>
        [ProtoMember(381)]
        public string LimitMoney { get; set; }

        /// <summary>
        ///     Поле limit_pledge
        /// </summary>
        [ProtoMember(382)]
        public string LimitPledge { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 7;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Mode, Mode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitMoney, LimitMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitPledge, LimitPledge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG107
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg107 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG107";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg107;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(383)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(384)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 107;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptChangeExpiration
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptChangeExpiration : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptChangeExpiration";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptChangeExpiration;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(385)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле mode
        /// </summary>
        [ProtoMember(386)]
        public int Mode { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(387)]
        public int OrderId { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(388)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(389)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(390)]
        public int Amount { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 12;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Mode, Mode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG112
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg112 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG112";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg112;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(391)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(392)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(393)]
        public int OrderId { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 112;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutChangeClientProhibit
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutChangeClientProhibit : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutChangeClientProhibit";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutChangeClientProhibit;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(394)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле mode
        /// </summary>
        [ProtoMember(395)]
        public int Mode { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(396)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(397)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(398)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле state
        /// </summary>
        [ProtoMember(399)]
        public int State { get; set; }

        /// <summary>
        ///     Поле state_mask
        /// </summary>
        [ProtoMember(400)]
        public int StateMask { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 15;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Mode, Mode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.State, State);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StateMask, StateMask);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG115
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg115 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG115";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg115;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(401)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(402)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 115;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptChangeClientProhibit
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptChangeClientProhibit : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptChangeClientProhibit";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptChangeClientProhibit;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(403)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле mode
        /// </summary>
        [ProtoMember(404)]
        public int Mode { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(405)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(406)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(407)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле state
        /// </summary>
        [ProtoMember(408)]
        public int State { get; set; }

        /// <summary>
        ///     Поле state_mask
        /// </summary>
        [ProtoMember(409)]
        public int StateMask { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 17;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Mode, Mode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.State, State);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StateMask, StateMask);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG117
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg117 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG117";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg117;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(410)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(411)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 117;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutExchangeBFMoney
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutExchangeBfmoney : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutExchangeBFMoney";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutExchangeBfmoney;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(412)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле mode
        /// </summary>
        [ProtoMember(413)]
        public int Mode { get; set; }

        /// <summary>
        ///     Поле code_from
        /// </summary>
        [ProtoMember(414)]
        public string CodeFrom { get; set; }

        /// <summary>
        ///     Поле code_to
        /// </summary>
        [ProtoMember(415)]
        public string CodeTo { get; set; }

        /// <summary>
        ///     Поле amount_money
        /// </summary>
        [ProtoMember(416)]
        public string AmountMoney { get; set; }

        /// <summary>
        ///     Поле amount_pledge
        /// </summary>
        [ProtoMember(417)]
        public string AmountPledge { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 35;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Mode, Mode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeFrom, CodeFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeTo, CodeTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountMoney, AmountMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountPledge, AmountPledge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG130
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg130 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG130";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg130;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(418)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(419)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 130;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptRecalcCS
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptRecalcCs : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptRecalcCS";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptRecalcCs;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(420)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(421)]
        public int IsinId { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 45;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG132
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg132 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG132";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg132;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(422)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(423)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 132;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutTransferClientPosition
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutTransferClientPosition : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutTransferClientPosition";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutTransferClientPosition;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(424)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле code_from
        /// </summary>
        [ProtoMember(425)]
        public string CodeFrom { get; set; }

        /// <summary>
        ///     Поле code_to
        /// </summary>
        [ProtoMember(426)]
        public string CodeTo { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(427)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(428)]
        public int Amount { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 61;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeFrom, CodeFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeTo, CodeTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG137
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg137 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG137";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg137;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(429)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(430)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 137;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptTransferClientPosition
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptTransferClientPosition : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptTransferClientPosition";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptTransferClientPosition;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(431)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле code_from
        /// </summary>
        [ProtoMember(432)]
        public string CodeFrom { get; set; }

        /// <summary>
        ///     Поле code_to
        /// </summary>
        [ProtoMember(433)]
        public string CodeTo { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(434)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(435)]
        public int Amount { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 62;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeFrom, CodeFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeTo, CodeTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG138
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg138 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG138";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg138;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(436)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(437)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 138;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_OptChangeRiskParameters
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptChangeRiskParameters : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "OptChangeRiskParameters";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_OptChangeRiskParameters;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(438)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(439)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле num_clr_2delivery
        /// </summary>
        [ProtoMember(440)]
        public int NumClr2delivery { get; set; }

        /// <summary>
        ///     Поле use_broker_num_clr_2delivery
        /// </summary>
        [ProtoMember(441)]
        public sbyte UseBrokerNumClr2delivery { get; set; }

        /// <summary>
        ///     Поле exp_weight
        /// </summary>
        [ProtoMember(442)]
        public string ExpWeight { get; set; }

        /// <summary>
        ///     Поле use_broker_exp_weight
        /// </summary>
        [ProtoMember(443)]
        public sbyte UseBrokerExpWeight { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 69;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NumClr2delivery, NumClr2delivery);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UseBrokerNumClr2delivery, UseBrokerNumClr2delivery);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExpWeight, ExpWeight);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UseBrokerExpWeight, UseBrokerExpWeight);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG140
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg140 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG140";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg140;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(444)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(445)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 140;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FutTransferRisk
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutTransferRisk : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FutTransferRisk";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FutTransferRisk;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(446)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле code_from
        /// </summary>
        [ProtoMember(447)]
        public string CodeFrom { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(448)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(449)]
        public int Amount { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 68;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeFrom, CodeFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG139
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg139 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG139";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg139;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(450)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(451)]
        public string Message { get; set; }

        /// <summary>
        ///     Поле deal_id1
        /// </summary>
        [ProtoMember(452)]
        public long DealId1 { get; set; }

        /// <summary>
        ///     Поле deal_id2
        /// </summary>
        [ProtoMember(453)]
        public long DealId2 { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 139;


        /// <summary>
        ///     Поле reply
        /// </summary>
        [ProtoIgnore]
        public int Reply => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealId1, DealId1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealId2, DealId2);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Reply, Reply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_CODHeartbeat
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmCodheartbeat : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "CODHeartbeat";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_Codheartbeat;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле seq_number
        /// </summary>
        [ProtoMember(454)]
        public int SeqNumber { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 10000;


        /// <summary>
        ///     Поле request
        /// </summary>
        [ProtoIgnore]
        public int Request => 1;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SeqNumber, SeqNumber);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Request, Request);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG99
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg99 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG99";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg99;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле queue_size
        /// </summary>
        [ProtoMember(455)]
        public int QueueSize { get; set; }

        /// <summary>
        ///     Поле penalty_remain
        /// </summary>
        [ProtoMember(456)]
        public int PenaltyRemain { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(457)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 99;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.QueueSize, QueueSize);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PenaltyRemain, PenaltyRemain);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_FORTS_MSG100
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFortsMsg100 : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "FORTS_MSG100";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FortsMessages_FortsMsg100;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FortsMessages;

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(458)]
        public int Code { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(459)]
        public string Message { get; set; }


        /// <summary>
        ///     Поле msgid
        /// </summary>
        [ProtoIgnore]
        public int Msgid => 100;


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Msgid, Msgid);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.FutCommon
{
    /// <summary>
    ///     Сообщение cgm_common
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmCommon : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "common";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutCommon_Common;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutCommon;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(460)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(461)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(462)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(463)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(464)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле best_sell
        /// </summary>
        [ProtoMember(465)]
        public double BestSell { get; set; }

        /// <summary>
        ///     Поле amount_sell
        /// </summary>
        [ProtoMember(466)]
        public int AmountSell { get; set; }

        /// <summary>
        ///     Поле best_buy
        /// </summary>
        [ProtoMember(467)]
        public double BestBuy { get; set; }

        /// <summary>
        ///     Поле amount_buy
        /// </summary>
        [ProtoMember(468)]
        public int AmountBuy { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(469)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле trend
        /// </summary>
        [ProtoMember(470)]
        public double Trend { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(471)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле deal_time
        /// </summary>
        [ProtoMember(472)]
        public DateTime DealTime { get; set; }

        /// <summary>
        ///     Поле min_price
        /// </summary>
        [ProtoMember(473)]
        public double MinPrice { get; set; }

        /// <summary>
        ///     Поле max_price
        /// </summary>
        [ProtoMember(474)]
        public double MaxPrice { get; set; }

        /// <summary>
        ///     Поле avr_price
        /// </summary>
        [ProtoMember(475)]
        public double AvrPrice { get; set; }

        /// <summary>
        ///     Поле old_kotir
        /// </summary>
        [ProtoMember(476)]
        public double OldKotir { get; set; }

        /// <summary>
        ///     Поле deal_count
        /// </summary>
        [ProtoMember(477)]
        public int DealCount { get; set; }

        /// <summary>
        ///     Поле contr_count
        /// </summary>
        [ProtoMember(478)]
        public int ContrCount { get; set; }

        /// <summary>
        ///     Поле capital
        /// </summary>
        [ProtoMember(479)]
        public double Capital { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(480)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле mod_time
        /// </summary>
        [ProtoMember(481)]
        public DateTime ModTime { get; set; }

        /// <summary>
        ///     Поле cur_kotir
        /// </summary>
        [ProtoMember(482)]
        public double CurKotir { get; set; }

        /// <summary>
        ///     Поле cur_kotir_real
        /// </summary>
        [ProtoMember(483)]
        public double CurKotirReal { get; set; }

        /// <summary>
        ///     Поле orders_sell_qty
        /// </summary>
        [ProtoMember(484)]
        public int OrdersSellQty { get; set; }

        /// <summary>
        ///     Поле orders_sell_amount
        /// </summary>
        [ProtoMember(485)]
        public int OrdersSellAmount { get; set; }

        /// <summary>
        ///     Поле orders_buy_qty
        /// </summary>
        [ProtoMember(486)]
        public int OrdersBuyQty { get; set; }

        /// <summary>
        ///     Поле orders_buy_amount
        /// </summary>
        [ProtoMember(487)]
        public int OrdersBuyAmount { get; set; }

        /// <summary>
        ///     Поле open_price
        /// </summary>
        [ProtoMember(488)]
        public double OpenPrice { get; set; }

        /// <summary>
        ///     Поле close_price
        /// </summary>
        [ProtoMember(489)]
        public double ClosePrice { get; set; }

        /// <summary>
        ///     Поле local_time
        /// </summary>
        [ProtoMember(490)]
        public DateTime LocalTime { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BestSell, BestSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountSell, AmountSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BestBuy, BestBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBuy, AmountBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Trend, Trend);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealTime, DealTime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinPrice, MinPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MaxPrice, MaxPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AvrPrice, AvrPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OldKotir, OldKotir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealCount, DealCount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ContrCount, ContrCount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Capital, Capital);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ModTime, ModTime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CurKotir, CurKotir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CurKotirReal, CurKotirReal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersSellQty, OrdersSellQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersSellAmount, OrdersSellAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersBuyQty, OrdersBuyQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersBuyAmount, OrdersBuyAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OpenPrice, OpenPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClosePrice, ClosePrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalTime, LocalTime);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.FutInfo
{
    /// <summary>
    ///     Сообщение cgm_delivery_report
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmDeliveryReport : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "delivery_report";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_DeliveryReport;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(491)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(492)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(493)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле date
        /// </summary>
        [ProtoMember(494)]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(495)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле type
        /// </summary>
        [ProtoMember(496)]
        public string Type { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(497)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(498)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле pos_excl
        /// </summary>
        [ProtoMember(499)]
        public int PosExcl { get; set; }

        /// <summary>
        ///     Поле pos_unexec
        /// </summary>
        [ProtoMember(500)]
        public int PosUnexec { get; set; }

        /// <summary>
        ///     Поле unexec
        /// </summary>
        [ProtoMember(501)]
        public sbyte Unexec { get; set; }

        /// <summary>
        ///     Поле settl_pair
        /// </summary>
        [ProtoMember(502)]
        public string SettlPair { get; set; }

        /// <summary>
        ///     Поле asset_code
        /// </summary>
        [ProtoMember(503)]
        public string AssetCode { get; set; }

        /// <summary>
        ///     Поле issue_code
        /// </summary>
        [ProtoMember(504)]
        public string IssueCode { get; set; }

        /// <summary>
        ///     Поле oblig_rur
        /// </summary>
        [ProtoMember(505)]
        public double ObligRur { get; set; }

        /// <summary>
        ///     Поле oblig_qty
        /// </summary>
        [ProtoMember(506)]
        public long ObligQty { get; set; }

        /// <summary>
        ///     Поле fulfil_rur
        /// </summary>
        [ProtoMember(507)]
        public double FulfilRur { get; set; }

        /// <summary>
        ///     Поле fulfil_qty
        /// </summary>
        [ProtoMember(508)]
        public long FulfilQty { get; set; }

        /// <summary>
        ///     Поле step
        /// </summary>
        [ProtoMember(509)]
        public int Step { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(510)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле id_gen
        /// </summary>
        [ProtoMember(511)]
        public int IdGen { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Date, Date);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Type, Type);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosExcl, PosExcl);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosUnexec, PosUnexec);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Unexec, Unexec);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlPair, SettlPair);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AssetCode, AssetCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IssueCode, IssueCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ObligRur, ObligRur);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ObligQty, ObligQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilRur, FulfilRur);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilQty, FulfilQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Step, Step);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdGen, IdGen);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_rejected_orders
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutRejectedOrders : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_rejected_orders";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutRejectedOrders;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(512)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(513)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(514)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(515)]
        public long OrderId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(516)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(517)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(518)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле moment_reject
        /// </summary>
        [ProtoMember(519)]
        public DateTime MomentReject { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(520)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(521)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(522)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(523)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(524)]
        public DateTime DateExp { get; set; }

        /// <summary>
        ///     Поле id_ord1
        /// </summary>
        [ProtoMember(525)]
        public long IdOrd1 { get; set; }

        /// <summary>
        ///     Поле ret_code
        /// </summary>
        [ProtoMember(526)]
        public int RetCode { get; set; }

        /// <summary>
        ///     Поле ret_message
        /// </summary>
        [ProtoMember(527)]
        public string RetMessage { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(528)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле login_from
        /// </summary>
        [ProtoMember(529)]
        public string LoginFrom { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(530)]
        public int ExtId { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MomentReject, MomentReject);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd1, IdOrd1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RetCode, RetCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RetMessage, RetMessage);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginFrom, LoginFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_intercl_info
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutInterclInfo : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_intercl_info";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutInterclInfo;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(531)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(532)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(533)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(534)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(535)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле vm_intercl
        /// </summary>
        [ProtoMember(536)]
        public double VmIntercl { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmIntercl, VmIntercl);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_bond_registry
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutBondRegistry : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_bond_registry";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutBondRegistry;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(537)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(538)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(539)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле bond_id
        /// </summary>
        [ProtoMember(540)]
        public int BondId { get; set; }

        /// <summary>
        ///     Поле small_name
        /// </summary>
        [ProtoMember(541)]
        public string SmallName { get; set; }

        /// <summary>
        ///     Поле short_isin
        /// </summary>
        [ProtoMember(542)]
        public string ShortIsin { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(543)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле date_redempt
        /// </summary>
        [ProtoMember(544)]
        public DateTime DateRedempt { get; set; }

        /// <summary>
        ///     Поле nominal
        /// </summary>
        [ProtoMember(545)]
        public double Nominal { get; set; }

        /// <summary>
        ///     Поле bond_type
        /// </summary>
        [ProtoMember(546)]
        public sbyte BondType { get; set; }

        /// <summary>
        ///     Поле year_base
        /// </summary>
        [ProtoMember(547)]
        public short YearBase { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BondId, BondId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SmallName, SmallName);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ShortIsin, ShortIsin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateRedempt, DateRedempt);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nominal, Nominal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BondType, BondType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.YearBase, YearBase);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_bond_isin
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutBondIsin : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_bond_isin";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutBondIsin;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(548)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(549)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(550)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(551)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле bond_id
        /// </summary>
        [ProtoMember(552)]
        public int BondId { get; set; }

        /// <summary>
        ///     Поле coeff_conversion
        /// </summary>
        [ProtoMember(553)]
        public double CoeffConversion { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BondId, BondId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CoeffConversion, CoeffConversion);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_bond_nkd
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutBondNkd : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_bond_nkd";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutBondNkd;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(554)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(555)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(556)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле bond_id
        /// </summary>
        [ProtoMember(557)]
        public int BondId { get; set; }

        /// <summary>
        ///     Поле date
        /// </summary>
        [ProtoMember(558)]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Поле nkd
        /// </summary>
        [ProtoMember(559)]
        public double Nkd { get; set; }

        /// <summary>
        ///     Поле is_cupon
        /// </summary>
        [ProtoMember(560)]
        public sbyte IsCupon { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BondId, BondId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Date, Date);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nkd, Nkd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsCupon, IsCupon);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_bond_nominal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutBondNominal : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_bond_nominal";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutBondNominal;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(561)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(562)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(563)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле bond_id
        /// </summary>
        [ProtoMember(564)]
        public int BondId { get; set; }

        /// <summary>
        ///     Поле date
        /// </summary>
        [ProtoMember(565)]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Поле nominal
        /// </summary>
        [ProtoMember(566)]
        public double Nominal { get; set; }

        /// <summary>
        ///     Поле face_value
        /// </summary>
        [ProtoMember(567)]
        public double FaceValue { get; set; }

        /// <summary>
        ///     Поле coupon_nominal
        /// </summary>
        [ProtoMember(568)]
        public double CouponNominal { get; set; }

        /// <summary>
        ///     Поле is_nominal
        /// </summary>
        [ProtoMember(569)]
        public sbyte IsNominal { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BondId, BondId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Date, Date);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nominal, Nominal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FaceValue, FaceValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CouponNominal, CouponNominal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsNominal, IsNominal);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_usd_online
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmUsdOnline : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "usd_online";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_UsdOnline;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(570)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(571)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(572)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id
        /// </summary>
        [ProtoMember(573)]
        public long Id { get; set; }

        /// <summary>
        ///     Поле rate
        /// </summary>
        [ProtoMember(574)]
        public double Rate { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(575)]
        public DateTime Moment { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Id, Id);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Rate, Rate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_vcb
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutVcb : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_vcb";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutVcb;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(576)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(577)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(578)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(579)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(580)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле exec_type
        /// </summary>
        [ProtoMember(581)]
        public string ExecType { get; set; }

        /// <summary>
        ///     Поле curr
        /// </summary>
        [ProtoMember(582)]
        public string Curr { get; set; }

        /// <summary>
        ///     Поле exch_pay
        /// </summary>
        [ProtoMember(583)]
        public double ExchPay { get; set; }

        /// <summary>
        ///     Поле exch_pay_scalped
        /// </summary>
        [ProtoMember(584)]
        public sbyte ExchPayScalped { get; set; }

        /// <summary>
        ///     Поле clear_pay
        /// </summary>
        [ProtoMember(585)]
        public double ClearPay { get; set; }

        /// <summary>
        ///     Поле clear_pay_scalped
        /// </summary>
        [ProtoMember(586)]
        public sbyte ClearPayScalped { get; set; }

        /// <summary>
        ///     Поле sell_fee
        /// </summary>
        [ProtoMember(587)]
        public double SellFee { get; set; }

        /// <summary>
        ///     Поле buy_fee
        /// </summary>
        [ProtoMember(588)]
        public double BuyFee { get; set; }

        /// <summary>
        ///     Поле trade_scheme
        /// </summary>
        [ProtoMember(589)]
        public string TradeScheme { get; set; }

        /// <summary>
        ///     Поле section
        /// </summary>
        [ProtoMember(590)]
        public string Section { get; set; }

        /// <summary>
        ///     Поле exch_pay_spot
        /// </summary>
        [ProtoMember(591)]
        public double ExchPaySpot { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(592)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле exch_pay_spot_repo
        /// </summary>
        [ProtoMember(593)]
        public double ExchPaySpotRepo { get; set; }

        /// <summary>
        ///     Поле rate_id
        /// </summary>
        [ProtoMember(594)]
        public int RateId { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExecType, ExecType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Curr, Curr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPay, ExchPay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPayScalped, ExchPayScalped);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClearPay, ClearPay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClearPayScalped, ClearPayScalped);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SellFee, SellFee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuyFee, BuyFee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TradeScheme, TradeScheme);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Section, Section);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPaySpot, ExchPaySpot);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPaySpotRepo, ExchPaySpotRepo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RateId, RateId);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_session
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSession : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "session";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_Session;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(595)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(596)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(597)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(598)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле begin
        /// </summary>
        [ProtoMember(599)]
        public DateTime Begin { get; set; }

        /// <summary>
        ///     Поле end
        /// </summary>
        [ProtoMember(600)]
        public DateTime End { get; set; }

        /// <summary>
        ///     Поле state
        /// </summary>
        [ProtoMember(601)]
        public int State { get; set; }

        /// <summary>
        ///     Поле opt_sess_id
        /// </summary>
        [ProtoMember(602)]
        public int OptSessId { get; set; }

        /// <summary>
        ///     Поле inter_cl_begin
        /// </summary>
        [ProtoMember(603)]
        public DateTime InterClBegin { get; set; }

        /// <summary>
        ///     Поле inter_cl_end
        /// </summary>
        [ProtoMember(604)]
        public DateTime InterClEnd { get; set; }

        /// <summary>
        ///     Поле inter_cl_state
        /// </summary>
        [ProtoMember(605)]
        public int InterClState { get; set; }

        /// <summary>
        ///     Поле eve_on
        /// </summary>
        [ProtoMember(606)]
        public sbyte EveOn { get; set; }

        /// <summary>
        ///     Поле eve_begin
        /// </summary>
        [ProtoMember(607)]
        public DateTime EveBegin { get; set; }

        /// <summary>
        ///     Поле eve_end
        /// </summary>
        [ProtoMember(608)]
        public DateTime EveEnd { get; set; }

        /// <summary>
        ///     Поле mon_on
        /// </summary>
        [ProtoMember(609)]
        public sbyte MonOn { get; set; }

        /// <summary>
        ///     Поле mon_begin
        /// </summary>
        [ProtoMember(610)]
        public DateTime MonBegin { get; set; }

        /// <summary>
        ///     Поле mon_end
        /// </summary>
        [ProtoMember(611)]
        public DateTime MonEnd { get; set; }

        /// <summary>
        ///     Поле pos_transfer_begin
        /// </summary>
        [ProtoMember(612)]
        public DateTime PosTransferBegin { get; set; }

        /// <summary>
        ///     Поле pos_transfer_end
        /// </summary>
        [ProtoMember(613)]
        public DateTime PosTransferEnd { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Begin, Begin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.End, End);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.State, State);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OptSessId, OptSessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InterClBegin, InterClBegin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InterClEnd, InterClEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InterClState, InterClState);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EveOn, EveOn);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EveBegin, EveBegin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EveEnd, EveEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MonOn, MonOn);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MonBegin, MonBegin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MonEnd, MonEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosTransferBegin, PosTransferBegin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PosTransferEnd, PosTransferEnd);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_multileg_dict
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmMultilegDict : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "multileg_dict";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_MultilegDict;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(614)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(615)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(616)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(617)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(618)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле isin_id_leg
        /// </summary>
        [ProtoMember(619)]
        public int IsinIdLeg { get; set; }

        /// <summary>
        ///     Поле qty_ratio
        /// </summary>
        [ProtoMember(620)]
        public int QtyRatio { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIdLeg, IsinIdLeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.QtyRatio, QtyRatio);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_sess_contents
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutSessContents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_sess_contents";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutSessContents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(621)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(622)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(623)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(624)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(625)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле short_isin
        /// </summary>
        [ProtoMember(626)]
        public string ShortIsin { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(627)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(628)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле inst_term
        /// </summary>
        [ProtoMember(629)]
        public int InstTerm { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(630)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле is_limited
        /// </summary>
        [ProtoMember(631)]
        public sbyte IsLimited { get; set; }

        /// <summary>
        ///     Поле limit_up
        /// </summary>
        [ProtoMember(632)]
        public double LimitUp { get; set; }

        /// <summary>
        ///     Поле limit_down
        /// </summary>
        [ProtoMember(633)]
        public double LimitDown { get; set; }

        /// <summary>
        ///     Поле old_kotir
        /// </summary>
        [ProtoMember(634)]
        public double OldKotir { get; set; }

        /// <summary>
        ///     Поле buy_deposit
        /// </summary>
        [ProtoMember(635)]
        public double BuyDeposit { get; set; }

        /// <summary>
        ///     Поле sell_deposit
        /// </summary>
        [ProtoMember(636)]
        public double SellDeposit { get; set; }

        /// <summary>
        ///     Поле roundto
        /// </summary>
        [ProtoMember(637)]
        public int Roundto { get; set; }

        /// <summary>
        ///     Поле min_step
        /// </summary>
        [ProtoMember(638)]
        public double MinStep { get; set; }

        /// <summary>
        ///     Поле lot_volume
        /// </summary>
        [ProtoMember(639)]
        public int LotVolume { get; set; }

        /// <summary>
        ///     Поле step_price
        /// </summary>
        [ProtoMember(640)]
        public double StepPrice { get; set; }

        /// <summary>
        ///     Поле d_pg
        /// </summary>
        [ProtoMember(641)]
        public DateTime DPg { get; set; }

        /// <summary>
        ///     Поле is_spread
        /// </summary>
        [ProtoMember(642)]
        public sbyte IsSpread { get; set; }

        /// <summary>
        ///     Поле coeff
        /// </summary>
        [ProtoMember(643)]
        public double Coeff { get; set; }

        /// <summary>
        ///     Поле d_exp
        /// </summary>
        [ProtoMember(644)]
        public DateTime DExp { get; set; }

        /// <summary>
        ///     Поле is_percent
        /// </summary>
        [ProtoMember(645)]
        public sbyte IsPercent { get; set; }

        /// <summary>
        ///     Поле percent_rate
        /// </summary>
        [ProtoMember(646)]
        public double PercentRate { get; set; }

        /// <summary>
        ///     Поле last_cl_quote
        /// </summary>
        [ProtoMember(647)]
        public double LastClQuote { get; set; }

        /// <summary>
        ///     Поле signs
        /// </summary>
        [ProtoMember(648)]
        public int Signs { get; set; }

        /// <summary>
        ///     Поле is_trade_evening
        /// </summary>
        [ProtoMember(649)]
        public sbyte IsTradeEvening { get; set; }

        /// <summary>
        ///     Поле ticker
        /// </summary>
        [ProtoMember(650)]
        public int Ticker { get; set; }

        /// <summary>
        ///     Поле state
        /// </summary>
        [ProtoMember(651)]
        public int State { get; set; }

        /// <summary>
        ///     Поле price_dir
        /// </summary>
        [ProtoMember(652)]
        public sbyte PriceDir { get; set; }

        /// <summary>
        ///     Поле multileg_type
        /// </summary>
        [ProtoMember(653)]
        public int MultilegType { get; set; }

        /// <summary>
        ///     Поле legs_qty
        /// </summary>
        [ProtoMember(654)]
        public int LegsQty { get; set; }

        /// <summary>
        ///     Поле step_price_clr
        /// </summary>
        [ProtoMember(655)]
        public double StepPriceClr { get; set; }

        /// <summary>
        ///     Поле step_price_interclr
        /// </summary>
        [ProtoMember(656)]
        public double StepPriceInterclr { get; set; }

        /// <summary>
        ///     Поле step_price_curr
        /// </summary>
        [ProtoMember(657)]
        public double StepPriceCurr { get; set; }

        /// <summary>
        ///     Поле d_start
        /// </summary>
        [ProtoMember(658)]
        public DateTime DStart { get; set; }

        /// <summary>
        ///     Поле exch_pay
        /// </summary>
        [ProtoMember(659)]
        public double ExchPay { get; set; }

        /// <summary>
        ///     Поле pctyield_coeff
        /// </summary>
        [ProtoMember(660)]
        public double PctyieldCoeff { get; set; }

        /// <summary>
        ///     Поле pctyield_total
        /// </summary>
        [ProtoMember(661)]
        public double PctyieldTotal { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ShortIsin, ShortIsin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InstTerm, InstTerm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsLimited, IsLimited);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitUp, LimitUp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitDown, LimitDown);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OldKotir, OldKotir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuyDeposit, BuyDeposit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SellDeposit, SellDeposit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Roundto, Roundto);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinStep, MinStep);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LotVolume, LotVolume);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPrice, StepPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DPg, DPg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsSpread, IsSpread);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Coeff, Coeff);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DExp, DExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsPercent, IsPercent);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PercentRate, PercentRate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LastClQuote, LastClQuote);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Signs, Signs);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsTradeEvening, IsTradeEvening);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Ticker, Ticker);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.State, State);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PriceDir, PriceDir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MultilegType, MultilegType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LegsQty, LegsQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPriceClr, StepPriceClr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPriceInterclr, StepPriceInterclr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPriceCurr, StepPriceCurr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DStart, DStart);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPay, ExchPay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PctyieldCoeff, PctyieldCoeff);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PctyieldTotal, PctyieldTotal);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_instruments
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutInstruments : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_instruments";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutInstruments;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(662)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(663)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(664)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(665)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле short_isin
        /// </summary>
        [ProtoMember(666)]
        public string ShortIsin { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(667)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(668)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле inst_term
        /// </summary>
        [ProtoMember(669)]
        public int InstTerm { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(670)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле is_limited
        /// </summary>
        [ProtoMember(671)]
        public sbyte IsLimited { get; set; }

        /// <summary>
        ///     Поле old_kotir
        /// </summary>
        [ProtoMember(672)]
        public double OldKotir { get; set; }

        /// <summary>
        ///     Поле roundto
        /// </summary>
        [ProtoMember(673)]
        public int Roundto { get; set; }

        /// <summary>
        ///     Поле min_step
        /// </summary>
        [ProtoMember(674)]
        public double MinStep { get; set; }

        /// <summary>
        ///     Поле lot_volume
        /// </summary>
        [ProtoMember(675)]
        public int LotVolume { get; set; }

        /// <summary>
        ///     Поле step_price
        /// </summary>
        [ProtoMember(676)]
        public double StepPrice { get; set; }

        /// <summary>
        ///     Поле d_pg
        /// </summary>
        [ProtoMember(677)]
        public DateTime DPg { get; set; }

        /// <summary>
        ///     Поле is_spread
        /// </summary>
        [ProtoMember(678)]
        public sbyte IsSpread { get; set; }

        /// <summary>
        ///     Поле coeff
        /// </summary>
        [ProtoMember(679)]
        public double Coeff { get; set; }

        /// <summary>
        ///     Поле d_exp
        /// </summary>
        [ProtoMember(680)]
        public DateTime DExp { get; set; }

        /// <summary>
        ///     Поле is_percent
        /// </summary>
        [ProtoMember(681)]
        public sbyte IsPercent { get; set; }

        /// <summary>
        ///     Поле percent_rate
        /// </summary>
        [ProtoMember(682)]
        public double PercentRate { get; set; }

        /// <summary>
        ///     Поле last_cl_quote
        /// </summary>
        [ProtoMember(683)]
        public double LastClQuote { get; set; }

        /// <summary>
        ///     Поле signs
        /// </summary>
        [ProtoMember(684)]
        public int Signs { get; set; }

        /// <summary>
        ///     Поле volat_min
        /// </summary>
        [ProtoMember(685)]
        public double VolatMin { get; set; }

        /// <summary>
        ///     Поле volat_max
        /// </summary>
        [ProtoMember(686)]
        public double VolatMax { get; set; }

        /// <summary>
        ///     Поле price_dir
        /// </summary>
        [ProtoMember(687)]
        public sbyte PriceDir { get; set; }

        /// <summary>
        ///     Поле multileg_type
        /// </summary>
        [ProtoMember(688)]
        public int MultilegType { get; set; }

        /// <summary>
        ///     Поле legs_qty
        /// </summary>
        [ProtoMember(689)]
        public int LegsQty { get; set; }

        /// <summary>
        ///     Поле step_price_clr
        /// </summary>
        [ProtoMember(690)]
        public double StepPriceClr { get; set; }

        /// <summary>
        ///     Поле step_price_interclr
        /// </summary>
        [ProtoMember(691)]
        public double StepPriceInterclr { get; set; }

        /// <summary>
        ///     Поле step_price_curr
        /// </summary>
        [ProtoMember(692)]
        public double StepPriceCurr { get; set; }

        /// <summary>
        ///     Поле d_start
        /// </summary>
        [ProtoMember(693)]
        public DateTime DStart { get; set; }

        /// <summary>
        ///     Поле is_limit_opt
        /// </summary>
        [ProtoMember(694)]
        public sbyte IsLimitOpt { get; set; }

        /// <summary>
        ///     Поле limit_up_opt
        /// </summary>
        [ProtoMember(695)]
        public double LimitUpOpt { get; set; }

        /// <summary>
        ///     Поле limit_down_opt
        /// </summary>
        [ProtoMember(696)]
        public double LimitDownOpt { get; set; }

        /// <summary>
        ///     Поле adm_lim
        /// </summary>
        [ProtoMember(697)]
        public double AdmLim { get; set; }

        /// <summary>
        ///     Поле adm_lim_offmoney
        /// </summary>
        [ProtoMember(698)]
        public double AdmLimOffmoney { get; set; }

        /// <summary>
        ///     Поле apply_adm_limit
        /// </summary>
        [ProtoMember(699)]
        public sbyte ApplyAdmLimit { get; set; }

        /// <summary>
        ///     Поле pctyield_coeff
        /// </summary>
        [ProtoMember(700)]
        public double PctyieldCoeff { get; set; }

        /// <summary>
        ///     Поле pctyield_total
        /// </summary>
        [ProtoMember(701)]
        public double PctyieldTotal { get; set; }

        /// <summary>
        ///     Поле exec_name
        /// </summary>
        [ProtoMember(702)]
        public string ExecName { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ShortIsin, ShortIsin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InstTerm, InstTerm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsLimited, IsLimited);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OldKotir, OldKotir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Roundto, Roundto);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinStep, MinStep);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LotVolume, LotVolume);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPrice, StepPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DPg, DPg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsSpread, IsSpread);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Coeff, Coeff);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DExp, DExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsPercent, IsPercent);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PercentRate, PercentRate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LastClQuote, LastClQuote);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Signs, Signs);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VolatMin, VolatMin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VolatMax, VolatMax);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PriceDir, PriceDir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MultilegType, MultilegType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LegsQty, LegsQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPriceClr, StepPriceClr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPriceInterclr, StepPriceInterclr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPriceCurr, StepPriceCurr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DStart, DStart);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsLimitOpt, IsLimitOpt);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitUpOpt, LimitUpOpt);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitDownOpt, LimitDownOpt);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AdmLim, AdmLim);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AdmLimOffmoney, AdmLimOffmoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ApplyAdmLimit, ApplyAdmLimit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PctyieldCoeff, PctyieldCoeff);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PctyieldTotal, PctyieldTotal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExecName, ExecName);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_diler
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmDiler : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "diler";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_Diler;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(703)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(704)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(705)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(706)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(707)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле rts_code
        /// </summary>
        [ProtoMember(708)]
        public string RtsCode { get; set; }

        /// <summary>
        ///     Поле transfer_code
        /// </summary>
        [ProtoMember(709)]
        public string TransferCode { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(710)]
        public int Status { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RtsCode, RtsCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TransferCode, TransferCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_investr
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmInvestr : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "investr";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_Investr;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(711)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(712)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(713)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(714)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(715)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(716)]
        public int Status { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_sess_settl
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutSessSettl : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_sess_settl";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutSessSettl;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(717)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(718)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(719)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(720)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле date_clr
        /// </summary>
        [ProtoMember(721)]
        public DateTime DateClr { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(722)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(723)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле settl_price
        /// </summary>
        [ProtoMember(724)]
        public double SettlPrice { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateClr, DateClr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlPrice, SettlPrice);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_messages
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysMessages : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_messages";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_SysMessages;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(725)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(726)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(727)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле msg_id
        /// </summary>
        [ProtoMember(728)]
        public int MsgId { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(729)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле lang_code
        /// </summary>
        [ProtoMember(730)]
        public string LangCode { get; set; }

        /// <summary>
        ///     Поле urgency
        /// </summary>
        [ProtoMember(731)]
        public sbyte Urgency { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(732)]
        public sbyte Status { get; set; }

        /// <summary>
        ///     Поле text
        /// </summary>
        [ProtoMember(733)]
        public string Text { get; set; }

        /// <summary>
        ///     Поле message_body
        /// </summary>
        [ProtoMember(734)]
        public string MessageBody { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MsgId, MsgId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LangCode, LangCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Urgency, Urgency);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Text, Text);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MessageBody, MessageBody);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_settlement_account
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutSettlementAccount : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_settlement_account";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutSettlementAccount;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(735)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(736)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(737)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(738)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле type
        /// </summary>
        [ProtoMember(739)]
        public sbyte Type { get; set; }

        /// <summary>
        ///     Поле settlement_account
        /// </summary>
        [ProtoMember(740)]
        public string SettlementAccount { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Type, Type);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlementAccount, SettlementAccount);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_margin_type
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutMarginType : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_margin_type";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_FutMarginType;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(741)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(742)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(743)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле code
        /// </summary>
        [ProtoMember(744)]
        public string Code { get; set; }

        /// <summary>
        ///     Поле type
        /// </summary>
        [ProtoMember(745)]
        public sbyte Type { get; set; }

        /// <summary>
        ///     Поле margin_type
        /// </summary>
        [ProtoMember(746)]
        public sbyte MarginType { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Code, Code);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Type, Type);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MarginType, MarginType);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_prohibition
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmProhibition : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "prohibition";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_Prohibition;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(747)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(748)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(749)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле prohib_id
        /// </summary>
        [ProtoMember(750)]
        public int ProhibId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(751)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле initiator
        /// </summary>
        [ProtoMember(752)]
        public int Initiator { get; set; }

        /// <summary>
        ///     Поле section
        /// </summary>
        [ProtoMember(753)]
        public string Section { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(754)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(755)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле priority
        /// </summary>
        [ProtoMember(756)]
        public int Priority { get; set; }

        /// <summary>
        ///     Поле group_mask
        /// </summary>
        [ProtoMember(757)]
        public long GroupMask { get; set; }

        /// <summary>
        ///     Поле type
        /// </summary>
        [ProtoMember(758)]
        public int Type { get; set; }

        /// <summary>
        ///     Поле is_legacy
        /// </summary>
        [ProtoMember(759)]
        public int IsLegacy { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ProhibId, ProhibId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Initiator, Initiator);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Section, Section);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Priority, Priority);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.GroupMask, GroupMask);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Type, Type);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsLegacy, IsLegacy);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_rates
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmRates : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "rates";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_Rates;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(760)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(761)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(762)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле rate_id
        /// </summary>
        [ProtoMember(763)]
        public int RateId { get; set; }

        /// <summary>
        ///     Поле curr_base
        /// </summary>
        [ProtoMember(764)]
        public string CurrBase { get; set; }

        /// <summary>
        ///     Поле curr_coupled
        /// </summary>
        [ProtoMember(765)]
        public string CurrCoupled { get; set; }

        /// <summary>
        ///     Поле radius
        /// </summary>
        [ProtoMember(766)]
        public double Radius { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RateId, RateId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CurrBase, CurrBase);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CurrCoupled, CurrCoupled);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Radius, Radius);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutInfo_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(767)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(768)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(769)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(770)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(771)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(772)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(773)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.FutTrades
{
    /// <summary>
    ///     Сообщение cgm_orders_log
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOrdersLog : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "orders_log";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_OrdersLog;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(774)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(775)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(776)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id_ord
        /// </summary>
        [ProtoMember(777)]
        public long IdOrd { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(778)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(779)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(780)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле amount_rest
        /// </summary>
        [ProtoMember(781)]
        public int AmountRest { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(782)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле xstatus
        /// </summary>
        [ProtoMember(783)]
        public long Xstatus { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(784)]
        public int Status { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(785)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(786)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(787)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле action
        /// </summary>
        [ProtoMember(788)]
        public sbyte Action { get; set; }

        /// <summary>
        ///     Поле deal_price
        /// </summary>
        [ProtoMember(789)]
        public double DealPrice { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(790)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле login_from
        /// </summary>
        [ProtoMember(791)]
        public string LoginFrom { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(792)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле hedge
        /// </summary>
        [ProtoMember(793)]
        public sbyte Hedge { get; set; }

        /// <summary>
        ///     Поле trust
        /// </summary>
        [ProtoMember(794)]
        public sbyte Trust { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(795)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле broker_to
        /// </summary>
        [ProtoMember(796)]
        public string BrokerTo { get; set; }

        /// <summary>
        ///     Поле broker_to_rts
        /// </summary>
        [ProtoMember(797)]
        public string BrokerToRts { get; set; }

        /// <summary>
        ///     Поле broker_from_rts
        /// </summary>
        [ProtoMember(798)]
        public string BrokerFromRts { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(799)]
        public DateTime DateExp { get; set; }

        /// <summary>
        ///     Поле id_ord1
        /// </summary>
        [ProtoMember(800)]
        public long IdOrd1 { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(801)]
        public DateTime LocalStamp { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd, IdOrd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountRest, AmountRest);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Xstatus, Xstatus);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Action, Action);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealPrice, DealPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginFrom, LoginFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Hedge, Hedge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Trust, Trust);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerTo, BrokerTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerToRts, BrokerToRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerFromRts, BrokerFromRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd1, IdOrd1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_multileg_orders_log
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmMultilegOrdersLog : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "multileg_orders_log";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_MultilegOrdersLog;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(802)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(803)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(804)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id_ord
        /// </summary>
        [ProtoMember(805)]
        public long IdOrd { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(806)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(807)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(808)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле amount_rest
        /// </summary>
        [ProtoMember(809)]
        public int AmountRest { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(810)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле xstatus
        /// </summary>
        [ProtoMember(811)]
        public long Xstatus { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(812)]
        public int Status { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(813)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(814)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(815)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле action
        /// </summary>
        [ProtoMember(816)]
        public sbyte Action { get; set; }

        /// <summary>
        ///     Поле deal_price
        /// </summary>
        [ProtoMember(817)]
        public double DealPrice { get; set; }

        /// <summary>
        ///     Поле rate_price
        /// </summary>
        [ProtoMember(818)]
        public double RatePrice { get; set; }

        /// <summary>
        ///     Поле swap_price
        /// </summary>
        [ProtoMember(819)]
        public double SwapPrice { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(820)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле login_from
        /// </summary>
        [ProtoMember(821)]
        public string LoginFrom { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(822)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле hedge
        /// </summary>
        [ProtoMember(823)]
        public sbyte Hedge { get; set; }

        /// <summary>
        ///     Поле trust
        /// </summary>
        [ProtoMember(824)]
        public sbyte Trust { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(825)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле broker_to
        /// </summary>
        [ProtoMember(826)]
        public string BrokerTo { get; set; }

        /// <summary>
        ///     Поле broker_to_rts
        /// </summary>
        [ProtoMember(827)]
        public string BrokerToRts { get; set; }

        /// <summary>
        ///     Поле broker_from_rts
        /// </summary>
        [ProtoMember(828)]
        public string BrokerFromRts { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(829)]
        public DateTime DateExp { get; set; }

        /// <summary>
        ///     Поле id_ord1
        /// </summary>
        [ProtoMember(830)]
        public long IdOrd1 { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(831)]
        public DateTime LocalStamp { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd, IdOrd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountRest, AmountRest);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Xstatus, Xstatus);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Action, Action);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealPrice, DealPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RatePrice, RatePrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SwapPrice, SwapPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginFrom, LoginFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Hedge, Hedge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Trust, Trust);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerTo, BrokerTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerToRts, BrokerToRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerFromRts, BrokerFromRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd1, IdOrd1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_deal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmDeal : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "deal";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_Deal;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(832)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(833)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(834)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(835)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(836)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(837)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле id_deal_multileg
        /// </summary>
        [ProtoMember(838)]
        public long IdDealMultileg { get; set; }

        /// <summary>
        ///     Поле id_repo
        /// </summary>
        [ProtoMember(839)]
        public long IdRepo { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(840)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(841)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле id_ord_buy
        /// </summary>
        [ProtoMember(842)]
        public long IdOrdBuy { get; set; }

        /// <summary>
        ///     Поле id_ord_sell
        /// </summary>
        [ProtoMember(843)]
        public long IdOrdSell { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(844)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(845)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле nosystem
        /// </summary>
        [ProtoMember(846)]
        public sbyte Nosystem { get; set; }

        /// <summary>
        ///     Поле xstatus_buy
        /// </summary>
        [ProtoMember(847)]
        public long XstatusBuy { get; set; }

        /// <summary>
        ///     Поле xstatus_sell
        /// </summary>
        [ProtoMember(848)]
        public long XstatusSell { get; set; }

        /// <summary>
        ///     Поле status_buy
        /// </summary>
        [ProtoMember(849)]
        public int StatusBuy { get; set; }

        /// <summary>
        ///     Поле status_sell
        /// </summary>
        [ProtoMember(850)]
        public int StatusSell { get; set; }

        /// <summary>
        ///     Поле ext_id_buy
        /// </summary>
        [ProtoMember(851)]
        public int ExtIdBuy { get; set; }

        /// <summary>
        ///     Поле ext_id_sell
        /// </summary>
        [ProtoMember(852)]
        public int ExtIdSell { get; set; }

        /// <summary>
        ///     Поле code_buy
        /// </summary>
        [ProtoMember(853)]
        public string CodeBuy { get; set; }

        /// <summary>
        ///     Поле code_sell
        /// </summary>
        [ProtoMember(854)]
        public string CodeSell { get; set; }

        /// <summary>
        ///     Поле comment_buy
        /// </summary>
        [ProtoMember(855)]
        public string CommentBuy { get; set; }

        /// <summary>
        ///     Поле comment_sell
        /// </summary>
        [ProtoMember(856)]
        public string CommentSell { get; set; }

        /// <summary>
        ///     Поле trust_buy
        /// </summary>
        [ProtoMember(857)]
        public sbyte TrustBuy { get; set; }

        /// <summary>
        ///     Поле trust_sell
        /// </summary>
        [ProtoMember(858)]
        public sbyte TrustSell { get; set; }

        /// <summary>
        ///     Поле hedge_buy
        /// </summary>
        [ProtoMember(859)]
        public sbyte HedgeBuy { get; set; }

        /// <summary>
        ///     Поле hedge_sell
        /// </summary>
        [ProtoMember(860)]
        public sbyte HedgeSell { get; set; }

        /// <summary>
        ///     Поле fee_buy
        /// </summary>
        [ProtoMember(861)]
        public double FeeBuy { get; set; }

        /// <summary>
        ///     Поле fee_sell
        /// </summary>
        [ProtoMember(862)]
        public double FeeSell { get; set; }

        /// <summary>
        ///     Поле login_buy
        /// </summary>
        [ProtoMember(863)]
        public string LoginBuy { get; set; }

        /// <summary>
        ///     Поле login_sell
        /// </summary>
        [ProtoMember(864)]
        public string LoginSell { get; set; }

        /// <summary>
        ///     Поле code_rts_buy
        /// </summary>
        [ProtoMember(865)]
        public string CodeRtsBuy { get; set; }

        /// <summary>
        ///     Поле code_rts_sell
        /// </summary>
        [ProtoMember(866)]
        public string CodeRtsSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealMultileg, IdDealMultileg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdRepo, IdRepo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdBuy, IdOrdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdSell, IdOrdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nosystem, Nosystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusBuy, XstatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusSell, XstatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusBuy, StatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusSell, StatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdBuy, ExtIdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdSell, ExtIdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeBuy, CodeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeSell, CodeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentBuy, CommentBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentSell, CommentSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustBuy, TrustBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustSell, TrustSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeBuy, HedgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeSell, HedgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeBuy, FeeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeSell, FeeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginBuy, LoginBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginSell, LoginSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsBuy, CodeRtsBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsSell, CodeRtsSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_multileg_deal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmMultilegDeal : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "multileg_deal";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_MultilegDeal;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(867)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(868)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(869)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(870)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(871)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле isin_id_rd
        /// </summary>
        [ProtoMember(872)]
        public int IsinIdRd { get; set; }

        /// <summary>
        ///     Поле isin_id_rb
        /// </summary>
        [ProtoMember(873)]
        public int IsinIdRb { get; set; }

        /// <summary>
        ///     Поле isin_id_repo
        /// </summary>
        [ProtoMember(874)]
        public int IsinIdRepo { get; set; }

        /// <summary>
        ///     Поле duration
        /// </summary>
        [ProtoMember(875)]
        public int Duration { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(876)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле id_deal_rd
        /// </summary>
        [ProtoMember(877)]
        public long IdDealRd { get; set; }

        /// <summary>
        ///     Поле id_deal_rb
        /// </summary>
        [ProtoMember(878)]
        public long IdDealRb { get; set; }

        /// <summary>
        ///     Поле id_ord_buy
        /// </summary>
        [ProtoMember(879)]
        public long IdOrdBuy { get; set; }

        /// <summary>
        ///     Поле id_ord_sell
        /// </summary>
        [ProtoMember(880)]
        public long IdOrdSell { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(881)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(882)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле rate_price
        /// </summary>
        [ProtoMember(883)]
        public double RatePrice { get; set; }

        /// <summary>
        ///     Поле swap_price
        /// </summary>
        [ProtoMember(884)]
        public double SwapPrice { get; set; }

        /// <summary>
        ///     Поле buyback_amount
        /// </summary>
        [ProtoMember(885)]
        public double BuybackAmount { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(886)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле nosystem
        /// </summary>
        [ProtoMember(887)]
        public sbyte Nosystem { get; set; }

        /// <summary>
        ///     Поле xstatus_buy
        /// </summary>
        [ProtoMember(888)]
        public long XstatusBuy { get; set; }

        /// <summary>
        ///     Поле xstatus_sell
        /// </summary>
        [ProtoMember(889)]
        public long XstatusSell { get; set; }

        /// <summary>
        ///     Поле status_buy
        /// </summary>
        [ProtoMember(890)]
        public int StatusBuy { get; set; }

        /// <summary>
        ///     Поле status_sell
        /// </summary>
        [ProtoMember(891)]
        public int StatusSell { get; set; }

        /// <summary>
        ///     Поле ext_id_buy
        /// </summary>
        [ProtoMember(892)]
        public int ExtIdBuy { get; set; }

        /// <summary>
        ///     Поле ext_id_sell
        /// </summary>
        [ProtoMember(893)]
        public int ExtIdSell { get; set; }

        /// <summary>
        ///     Поле code_buy
        /// </summary>
        [ProtoMember(894)]
        public string CodeBuy { get; set; }

        /// <summary>
        ///     Поле code_sell
        /// </summary>
        [ProtoMember(895)]
        public string CodeSell { get; set; }

        /// <summary>
        ///     Поле comment_buy
        /// </summary>
        [ProtoMember(896)]
        public string CommentBuy { get; set; }

        /// <summary>
        ///     Поле comment_sell
        /// </summary>
        [ProtoMember(897)]
        public string CommentSell { get; set; }

        /// <summary>
        ///     Поле trust_buy
        /// </summary>
        [ProtoMember(898)]
        public sbyte TrustBuy { get; set; }

        /// <summary>
        ///     Поле trust_sell
        /// </summary>
        [ProtoMember(899)]
        public sbyte TrustSell { get; set; }

        /// <summary>
        ///     Поле hedge_buy
        /// </summary>
        [ProtoMember(900)]
        public sbyte HedgeBuy { get; set; }

        /// <summary>
        ///     Поле hedge_sell
        /// </summary>
        [ProtoMember(901)]
        public sbyte HedgeSell { get; set; }

        /// <summary>
        ///     Поле login_buy
        /// </summary>
        [ProtoMember(902)]
        public string LoginBuy { get; set; }

        /// <summary>
        ///     Поле login_sell
        /// </summary>
        [ProtoMember(903)]
        public string LoginSell { get; set; }

        /// <summary>
        ///     Поле code_rts_buy
        /// </summary>
        [ProtoMember(904)]
        public string CodeRtsBuy { get; set; }

        /// <summary>
        ///     Поле code_rts_sell
        /// </summary>
        [ProtoMember(905)]
        public string CodeRtsSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIdRd, IsinIdRd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIdRb, IsinIdRb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIdRepo, IsinIdRepo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Duration, Duration);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealRd, IdDealRd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealRb, IdDealRb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdBuy, IdOrdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdSell, IdOrdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RatePrice, RatePrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SwapPrice, SwapPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuybackAmount, BuybackAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nosystem, Nosystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusBuy, XstatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusSell, XstatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusBuy, StatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusSell, StatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdBuy, ExtIdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdSell, ExtIdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeBuy, CodeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeSell, CodeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentBuy, CommentBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentSell, CommentSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustBuy, TrustBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustSell, TrustSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeBuy, HedgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeSell, HedgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginBuy, LoginBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginSell, LoginSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsBuy, CodeRtsBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsSell, CodeRtsSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_heartbeat
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmHeartbeat : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "heartbeat";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_Heartbeat;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(906)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(907)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(908)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле server_time
        /// </summary>
        [ProtoMember(909)]
        public DateTime ServerTime { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ServerTime, ServerTime);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(910)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(911)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(912)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(913)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(914)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(915)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(916)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_user_deal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmUserDeal : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "user_deal";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_UserDeal;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(917)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(918)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(919)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(920)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(921)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(922)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле id_deal_multileg
        /// </summary>
        [ProtoMember(923)]
        public long IdDealMultileg { get; set; }

        /// <summary>
        ///     Поле id_repo
        /// </summary>
        [ProtoMember(924)]
        public long IdRepo { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(925)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(926)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле id_ord_buy
        /// </summary>
        [ProtoMember(927)]
        public long IdOrdBuy { get; set; }

        /// <summary>
        ///     Поле id_ord_sell
        /// </summary>
        [ProtoMember(928)]
        public long IdOrdSell { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(929)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(930)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле nosystem
        /// </summary>
        [ProtoMember(931)]
        public sbyte Nosystem { get; set; }

        /// <summary>
        ///     Поле xstatus_buy
        /// </summary>
        [ProtoMember(932)]
        public long XstatusBuy { get; set; }

        /// <summary>
        ///     Поле xstatus_sell
        /// </summary>
        [ProtoMember(933)]
        public long XstatusSell { get; set; }

        /// <summary>
        ///     Поле status_buy
        /// </summary>
        [ProtoMember(934)]
        public int StatusBuy { get; set; }

        /// <summary>
        ///     Поле status_sell
        /// </summary>
        [ProtoMember(935)]
        public int StatusSell { get; set; }

        /// <summary>
        ///     Поле ext_id_buy
        /// </summary>
        [ProtoMember(936)]
        public int ExtIdBuy { get; set; }

        /// <summary>
        ///     Поле ext_id_sell
        /// </summary>
        [ProtoMember(937)]
        public int ExtIdSell { get; set; }

        /// <summary>
        ///     Поле code_buy
        /// </summary>
        [ProtoMember(938)]
        public string CodeBuy { get; set; }

        /// <summary>
        ///     Поле code_sell
        /// </summary>
        [ProtoMember(939)]
        public string CodeSell { get; set; }

        /// <summary>
        ///     Поле comment_buy
        /// </summary>
        [ProtoMember(940)]
        public string CommentBuy { get; set; }

        /// <summary>
        ///     Поле comment_sell
        /// </summary>
        [ProtoMember(941)]
        public string CommentSell { get; set; }

        /// <summary>
        ///     Поле trust_buy
        /// </summary>
        [ProtoMember(942)]
        public sbyte TrustBuy { get; set; }

        /// <summary>
        ///     Поле trust_sell
        /// </summary>
        [ProtoMember(943)]
        public sbyte TrustSell { get; set; }

        /// <summary>
        ///     Поле hedge_buy
        /// </summary>
        [ProtoMember(944)]
        public sbyte HedgeBuy { get; set; }

        /// <summary>
        ///     Поле hedge_sell
        /// </summary>
        [ProtoMember(945)]
        public sbyte HedgeSell { get; set; }

        /// <summary>
        ///     Поле fee_buy
        /// </summary>
        [ProtoMember(946)]
        public double FeeBuy { get; set; }

        /// <summary>
        ///     Поле fee_sell
        /// </summary>
        [ProtoMember(947)]
        public double FeeSell { get; set; }

        /// <summary>
        ///     Поле login_buy
        /// </summary>
        [ProtoMember(948)]
        public string LoginBuy { get; set; }

        /// <summary>
        ///     Поле login_sell
        /// </summary>
        [ProtoMember(949)]
        public string LoginSell { get; set; }

        /// <summary>
        ///     Поле code_rts_buy
        /// </summary>
        [ProtoMember(950)]
        public string CodeRtsBuy { get; set; }

        /// <summary>
        ///     Поле code_rts_sell
        /// </summary>
        [ProtoMember(951)]
        public string CodeRtsSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealMultileg, IdDealMultileg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdRepo, IdRepo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdBuy, IdOrdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdSell, IdOrdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nosystem, Nosystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusBuy, XstatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusSell, XstatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusBuy, StatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusSell, StatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdBuy, ExtIdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdSell, ExtIdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeBuy, CodeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeSell, CodeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentBuy, CommentBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentSell, CommentSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustBuy, TrustBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustSell, TrustSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeBuy, HedgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeSell, HedgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeBuy, FeeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeSell, FeeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginBuy, LoginBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginSell, LoginSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsBuy, CodeRtsBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsSell, CodeRtsSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_user_multileg_deal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmUserMultilegDeal : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "user_multileg_deal";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTrades_UserMultilegDeal;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(952)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(953)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(954)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(955)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(956)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(957)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле isin_id_rd
        /// </summary>
        [ProtoMember(958)]
        public int IsinIdRd { get; set; }

        /// <summary>
        ///     Поле isin_id_rb
        /// </summary>
        [ProtoMember(959)]
        public int IsinIdRb { get; set; }

        /// <summary>
        ///     Поле isin_id_repo
        /// </summary>
        [ProtoMember(960)]
        public int IsinIdRepo { get; set; }

        /// <summary>
        ///     Поле duration
        /// </summary>
        [ProtoMember(961)]
        public int Duration { get; set; }

        /// <summary>
        ///     Поле id_deal_rd
        /// </summary>
        [ProtoMember(962)]
        public long IdDealRd { get; set; }

        /// <summary>
        ///     Поле id_deal_rb
        /// </summary>
        [ProtoMember(963)]
        public long IdDealRb { get; set; }

        /// <summary>
        ///     Поле id_ord_buy
        /// </summary>
        [ProtoMember(964)]
        public long IdOrdBuy { get; set; }

        /// <summary>
        ///     Поле id_ord_sell
        /// </summary>
        [ProtoMember(965)]
        public long IdOrdSell { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(966)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(967)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле rate_price
        /// </summary>
        [ProtoMember(968)]
        public double RatePrice { get; set; }

        /// <summary>
        ///     Поле swap_price
        /// </summary>
        [ProtoMember(969)]
        public double SwapPrice { get; set; }

        /// <summary>
        ///     Поле buyback_amount
        /// </summary>
        [ProtoMember(970)]
        public double BuybackAmount { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(971)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле nosystem
        /// </summary>
        [ProtoMember(972)]
        public sbyte Nosystem { get; set; }

        /// <summary>
        ///     Поле xstatus_buy
        /// </summary>
        [ProtoMember(973)]
        public long XstatusBuy { get; set; }

        /// <summary>
        ///     Поле xstatus_sell
        /// </summary>
        [ProtoMember(974)]
        public long XstatusSell { get; set; }

        /// <summary>
        ///     Поле status_buy
        /// </summary>
        [ProtoMember(975)]
        public int StatusBuy { get; set; }

        /// <summary>
        ///     Поле status_sell
        /// </summary>
        [ProtoMember(976)]
        public int StatusSell { get; set; }

        /// <summary>
        ///     Поле ext_id_buy
        /// </summary>
        [ProtoMember(977)]
        public int ExtIdBuy { get; set; }

        /// <summary>
        ///     Поле ext_id_sell
        /// </summary>
        [ProtoMember(978)]
        public int ExtIdSell { get; set; }

        /// <summary>
        ///     Поле code_buy
        /// </summary>
        [ProtoMember(979)]
        public string CodeBuy { get; set; }

        /// <summary>
        ///     Поле code_sell
        /// </summary>
        [ProtoMember(980)]
        public string CodeSell { get; set; }

        /// <summary>
        ///     Поле comment_buy
        /// </summary>
        [ProtoMember(981)]
        public string CommentBuy { get; set; }

        /// <summary>
        ///     Поле comment_sell
        /// </summary>
        [ProtoMember(982)]
        public string CommentSell { get; set; }

        /// <summary>
        ///     Поле trust_buy
        /// </summary>
        [ProtoMember(983)]
        public sbyte TrustBuy { get; set; }

        /// <summary>
        ///     Поле trust_sell
        /// </summary>
        [ProtoMember(984)]
        public sbyte TrustSell { get; set; }

        /// <summary>
        ///     Поле hedge_buy
        /// </summary>
        [ProtoMember(985)]
        public sbyte HedgeBuy { get; set; }

        /// <summary>
        ///     Поле hedge_sell
        /// </summary>
        [ProtoMember(986)]
        public sbyte HedgeSell { get; set; }

        /// <summary>
        ///     Поле login_buy
        /// </summary>
        [ProtoMember(987)]
        public string LoginBuy { get; set; }

        /// <summary>
        ///     Поле login_sell
        /// </summary>
        [ProtoMember(988)]
        public string LoginSell { get; set; }

        /// <summary>
        ///     Поле code_rts_buy
        /// </summary>
        [ProtoMember(989)]
        public string CodeRtsBuy { get; set; }

        /// <summary>
        ///     Поле code_rts_sell
        /// </summary>
        [ProtoMember(990)]
        public string CodeRtsSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIdRd, IsinIdRd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIdRb, IsinIdRb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIdRepo, IsinIdRepo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Duration, Duration);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealRd, IdDealRd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealRb, IdDealRb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdBuy, IdOrdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdSell, IdOrdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RatePrice, RatePrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SwapPrice, SwapPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuybackAmount, BuybackAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nosystem, Nosystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusBuy, XstatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusSell, XstatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusBuy, StatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusSell, StatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdBuy, ExtIdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdSell, ExtIdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeBuy, CodeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeSell, CodeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentBuy, CommentBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentSell, CommentSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustBuy, TrustBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustSell, TrustSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeBuy, HedgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeSell, HedgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginBuy, LoginBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginSell, LoginSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsBuy, CodeRtsBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsSell, CodeRtsSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.FutTradesHeartbeat
{
    /// <summary>
    ///     Сообщение cgm_heartbeat
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmHeartbeat : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "heartbeat";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.FutTradesHeartbeat_Heartbeat;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.FutTradesHeartbeat;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(991)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(992)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(993)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле server_time
        /// </summary>
        [ProtoMember(994)]
        public DateTime ServerTime { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ServerTime, ServerTime);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Info
{
    /// <summary>
    ///     Сообщение cgm_base_contracts_params
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmBaseContractsParams : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "base_contracts_params";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Info_BaseContractsParams;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Info;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(995)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(996)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(997)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(998)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле code_mcs
        /// </summary>
        [ProtoMember(999)]
        public string CodeMcs { get; set; }

        /// <summary>
        ///     Поле volat_num
        /// </summary>
        [ProtoMember(1000)]
        public sbyte VolatNum { get; set; }

        /// <summary>
        ///     Поле points_num
        /// </summary>
        [ProtoMember(1001)]
        public sbyte PointsNum { get; set; }

        /// <summary>
        ///     Поле subrisk_step
        /// </summary>
        [ProtoMember(1002)]
        public double SubriskStep { get; set; }

        /// <summary>
        ///     Поле is_percent
        /// </summary>
        [ProtoMember(1003)]
        public sbyte IsPercent { get; set; }

        /// <summary>
        ///     Поле percent_rate
        /// </summary>
        [ProtoMember(1004)]
        public double PercentRate { get; set; }

        /// <summary>
        ///     Поле currency_volat
        /// </summary>
        [ProtoMember(1005)]
        public double CurrencyVolat { get; set; }

        /// <summary>
        ///     Поле is_usd
        /// </summary>
        [ProtoMember(1006)]
        public sbyte IsUsd { get; set; }

        /// <summary>
        ///     Поле usd_rate_curv_radius
        /// </summary>
        [ProtoMember(1007)]
        public double UsdRateCurvRadius { get; set; }

        /// <summary>
        ///     Поле somc
        /// </summary>
        [ProtoMember(1008)]
        public double Somc { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeMcs, CodeMcs);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VolatNum, VolatNum);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PointsNum, PointsNum);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SubriskStep, SubriskStep);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsPercent, IsPercent);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PercentRate, PercentRate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CurrencyVolat, CurrencyVolat);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsUsd, IsUsd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UsdRateCurvRadius, UsdRateCurvRadius);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Somc, Somc);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_futures_params
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFuturesParams : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "futures_params";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Info_FuturesParams;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Info;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1009)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1010)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1011)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(1012)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1013)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(1014)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле limit
        /// </summary>
        [ProtoMember(1015)]
        public double Limit { get; set; }

        /// <summary>
        ///     Поле settl_price
        /// </summary>
        [ProtoMember(1016)]
        public double SettlPrice { get; set; }

        /// <summary>
        ///     Поле spread_aspect
        /// </summary>
        [ProtoMember(1017)]
        public sbyte SpreadAspect { get; set; }

        /// <summary>
        ///     Поле subrisk
        /// </summary>
        [ProtoMember(1018)]
        public sbyte Subrisk { get; set; }

        /// <summary>
        ///     Поле step_price
        /// </summary>
        [ProtoMember(1019)]
        public double StepPrice { get; set; }

        /// <summary>
        ///     Поле base_go
        /// </summary>
        [ProtoMember(1020)]
        public double BaseGo { get; set; }

        /// <summary>
        ///     Поле exp_date
        /// </summary>
        [ProtoMember(1021)]
        public DateTime ExpDate { get; set; }

        /// <summary>
        ///     Поле spot_signs
        /// </summary>
        [ProtoMember(1022)]
        public sbyte SpotSigns { get; set; }

        /// <summary>
        ///     Поле settl_price_real
        /// </summary>
        [ProtoMember(1023)]
        public double SettlPriceReal { get; set; }

        /// <summary>
        ///     Поле min_step
        /// </summary>
        [ProtoMember(1024)]
        public double MinStep { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Limit, Limit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlPrice, SettlPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SpreadAspect, SpreadAspect);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Subrisk, Subrisk);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPrice, StepPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BaseGo, BaseGo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExpDate, ExpDate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SpotSigns, SpotSigns);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlPriceReal, SettlPriceReal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinStep, MinStep);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_virtual_futures_params
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmVirtualFuturesParams : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "virtual_futures_params";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Info_VirtualFuturesParams;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Info;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1025)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1026)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1027)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(1028)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле isin_base
        /// </summary>
        [ProtoMember(1029)]
        public string IsinBase { get; set; }

        /// <summary>
        ///     Поле is_net_positive
        /// </summary>
        [ProtoMember(1030)]
        public sbyte IsNetPositive { get; set; }

        /// <summary>
        ///     Поле volat_range
        /// </summary>
        [ProtoMember(1031)]
        public double VolatRange { get; set; }

        /// <summary>
        ///     Поле t_squared
        /// </summary>
        [ProtoMember(1032)]
        public double TSquared { get; set; }

        /// <summary>
        ///     Поле max_addrisk
        /// </summary>
        [ProtoMember(1033)]
        public double MaxAddrisk { get; set; }

        /// <summary>
        ///     Поле a
        /// </summary>
        [ProtoMember(1034)]
        public double A { get; set; }

        /// <summary>
        ///     Поле b
        /// </summary>
        [ProtoMember(1035)]
        public double B { get; set; }

        /// <summary>
        ///     Поле c
        /// </summary>
        [ProtoMember(1036)]
        public double C { get; set; }

        /// <summary>
        ///     Поле d
        /// </summary>
        [ProtoMember(1037)]
        public double D { get; set; }

        /// <summary>
        ///     Поле e
        /// </summary>
        [ProtoMember(1038)]
        public double E { get; set; }

        /// <summary>
        ///     Поле s
        /// </summary>
        [ProtoMember(1039)]
        public double S { get; set; }

        /// <summary>
        ///     Поле exp_date
        /// </summary>
        [ProtoMember(1040)]
        public DateTime ExpDate { get; set; }

        /// <summary>
        ///     Поле fut_type
        /// </summary>
        [ProtoMember(1041)]
        public sbyte FutType { get; set; }

        /// <summary>
        ///     Поле use_null_volat
        /// </summary>
        [ProtoMember(1042)]
        public sbyte UseNullVolat { get; set; }

        /// <summary>
        ///     Поле exp_clearings_bf
        /// </summary>
        [ProtoMember(1043)]
        public int ExpClearingsBf { get; set; }

        /// <summary>
        ///     Поле exp_clearings_cc
        /// </summary>
        [ProtoMember(1044)]
        public int ExpClearingsCc { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinBase, IsinBase);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsNetPositive, IsNetPositive);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VolatRange, VolatRange);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TSquared, TSquared);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MaxAddrisk, MaxAddrisk);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.A, A);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.B, B);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.C, C);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.D, D);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.E, E);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.S, S);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExpDate, ExpDate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FutType, FutType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UseNullVolat, UseNullVolat);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExpClearingsBf, ExpClearingsBf);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExpClearingsCc, ExpClearingsCc);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_options_params
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptionsParams : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "options_params";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Info_OptionsParams;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Info;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1045)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1046)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1047)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(1048)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1049)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле isin_base
        /// </summary>
        [ProtoMember(1050)]
        public string IsinBase { get; set; }

        /// <summary>
        ///     Поле strike
        /// </summary>
        [ProtoMember(1051)]
        public double Strike { get; set; }

        /// <summary>
        ///     Поле opt_type
        /// </summary>
        [ProtoMember(1052)]
        public sbyte OptType { get; set; }

        /// <summary>
        ///     Поле settl_price
        /// </summary>
        [ProtoMember(1053)]
        public double SettlPrice { get; set; }

        /// <summary>
        ///     Поле base_go_sell
        /// </summary>
        [ProtoMember(1054)]
        public double BaseGoSell { get; set; }

        /// <summary>
        ///     Поле synth_base_go
        /// </summary>
        [ProtoMember(1055)]
        public double SynthBaseGo { get; set; }

        /// <summary>
        ///     Поле base_go_buy
        /// </summary>
        [ProtoMember(1056)]
        public double BaseGoBuy { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinBase, IsinBase);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Strike, Strike);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OptType, OptType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlPrice, SettlPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BaseGoSell, BaseGoSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SynthBaseGo, SynthBaseGo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BaseGoBuy, BaseGoBuy);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_broker_params
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmBrokerParams : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "broker_params";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Info_BrokerParams;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Info;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1057)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1058)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1059)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле broker_code
        /// </summary>
        [ProtoMember(1060)]
        public string BrokerCode { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(1061)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле limit_spot_sell
        /// </summary>
        [ProtoMember(1062)]
        public int LimitSpotSell { get; set; }

        /// <summary>
        ///     Поле used_limit_spot_sell
        /// </summary>
        [ProtoMember(1063)]
        public int UsedLimitSpotSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerCode, BrokerCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitSpotSell, LimitSpotSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UsedLimitSpotSell, UsedLimitSpotSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_client_params
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmClientParams : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "client_params";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Info_ClientParams;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Info;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1064)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1065)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1066)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1067)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(1068)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле coeff_go
        /// </summary>
        [ProtoMember(1069)]
        public double CoeffGo { get; set; }

        /// <summary>
        ///     Поле limit_spot_sell
        /// </summary>
        [ProtoMember(1070)]
        public int LimitSpotSell { get; set; }

        /// <summary>
        ///     Поле used_limit_spot_sell
        /// </summary>
        [ProtoMember(1071)]
        public int UsedLimitSpotSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CoeffGo, CoeffGo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitSpotSell, LimitSpotSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UsedLimitSpotSell, UsedLimitSpotSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Info_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Info;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1072)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1073)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1074)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(1075)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1076)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(1077)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(1078)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.MiscInfo
{
    /// <summary>
    ///     Сообщение cgm_volat_coeff
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmVolatCoeff : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "volat_coeff";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.MiscInfo_VolatCoeff;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.MiscInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1079)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1080)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1081)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1082)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле a
        /// </summary>
        [ProtoMember(1083)]
        public double A { get; set; }

        /// <summary>
        ///     Поле b
        /// </summary>
        [ProtoMember(1084)]
        public double B { get; set; }

        /// <summary>
        ///     Поле c
        /// </summary>
        [ProtoMember(1085)]
        public double C { get; set; }

        /// <summary>
        ///     Поле d
        /// </summary>
        [ProtoMember(1086)]
        public double D { get; set; }

        /// <summary>
        ///     Поле e
        /// </summary>
        [ProtoMember(1087)]
        public double E { get; set; }

        /// <summary>
        ///     Поле s
        /// </summary>
        [ProtoMember(1088)]
        public double S { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.A, A);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.B, B);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.C, C);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.D, D);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.E, E);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.S, S);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Mm
{
    /// <summary>
    ///     Сообщение cgm_fut_MM_info
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutMmInfo : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_MM_info";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Mm_FutMmInfo;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Mm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1089)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1090)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1091)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1092)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1093)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле spread
        /// </summary>
        [ProtoMember(1094)]
        public double Spread { get; set; }

        /// <summary>
        ///     Поле price_edge_sell
        /// </summary>
        [ProtoMember(1095)]
        public double PriceEdgeSell { get; set; }

        /// <summary>
        ///     Поле amount_sells
        /// </summary>
        [ProtoMember(1096)]
        public int AmountSells { get; set; }

        /// <summary>
        ///     Поле price_edge_buy
        /// </summary>
        [ProtoMember(1097)]
        public double PriceEdgeBuy { get; set; }

        /// <summary>
        ///     Поле amount_buys
        /// </summary>
        [ProtoMember(1098)]
        public int AmountBuys { get; set; }

        /// <summary>
        ///     Поле mm_spread
        /// </summary>
        [ProtoMember(1099)]
        public double MmSpread { get; set; }

        /// <summary>
        ///     Поле mm_amount
        /// </summary>
        [ProtoMember(1100)]
        public int MmAmount { get; set; }

        /// <summary>
        ///     Поле spread_sign
        /// </summary>
        [ProtoMember(1101)]
        public sbyte SpreadSign { get; set; }

        /// <summary>
        ///     Поле amount_sign
        /// </summary>
        [ProtoMember(1102)]
        public sbyte AmountSign { get; set; }

        /// <summary>
        ///     Поле percent_time
        /// </summary>
        [ProtoMember(1103)]
        public double PercentTime { get; set; }

        /// <summary>
        ///     Поле period_start
        /// </summary>
        [ProtoMember(1104)]
        public DateTime PeriodStart { get; set; }

        /// <summary>
        ///     Поле period_end
        /// </summary>
        [ProtoMember(1105)]
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1106)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле active_sign
        /// </summary>
        [ProtoMember(1107)]
        public int ActiveSign { get; set; }

        /// <summary>
        ///     Поле agmt_id
        /// </summary>
        [ProtoMember(1108)]
        public int AgmtId { get; set; }

        /// <summary>
        ///     Поле fulfil_min
        /// </summary>
        [ProtoMember(1109)]
        public double FulfilMin { get; set; }

        /// <summary>
        ///     Поле fulfil_partial
        /// </summary>
        [ProtoMember(1110)]
        public double FulfilPartial { get; set; }

        /// <summary>
        ///     Поле fulfil_total
        /// </summary>
        [ProtoMember(1111)]
        public double FulfilTotal { get; set; }

        /// <summary>
        ///     Поле is_fulfil_min
        /// </summary>
        [ProtoMember(1112)]
        public sbyte IsFulfilMin { get; set; }

        /// <summary>
        ///     Поле is_fulfil_partial
        /// </summary>
        [ProtoMember(1113)]
        public sbyte IsFulfilPartial { get; set; }

        /// <summary>
        ///     Поле is_fulfil_total
        /// </summary>
        [ProtoMember(1114)]
        public sbyte IsFulfilTotal { get; set; }

        /// <summary>
        ///     Поле is_rf
        /// </summary>
        [ProtoMember(1115)]
        public sbyte IsRf { get; set; }

        /// <summary>
        ///     Поле id_group
        /// </summary>
        [ProtoMember(1116)]
        public int IdGroup { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Spread, Spread);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PriceEdgeSell, PriceEdgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountSells, AmountSells);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PriceEdgeBuy, PriceEdgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBuys, AmountBuys);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MmSpread, MmSpread);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MmAmount, MmAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SpreadSign, SpreadSign);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountSign, AmountSign);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PercentTime, PercentTime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PeriodStart, PeriodStart);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PeriodEnd, PeriodEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ActiveSign, ActiveSign);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AgmtId, AgmtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilMin, FulfilMin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilPartial, FulfilPartial);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilTotal, FulfilTotal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsFulfilMin, IsFulfilMin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsFulfilPartial, IsFulfilPartial);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsFulfilTotal, IsFulfilTotal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsRf, IsRf);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdGroup, IdGroup);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_MM_info
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptMmInfo : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_MM_info";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Mm_OptMmInfo;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Mm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1117)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1118)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1119)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1120)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1121)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле spread
        /// </summary>
        [ProtoMember(1122)]
        public double Spread { get; set; }

        /// <summary>
        ///     Поле price_edge_sell
        /// </summary>
        [ProtoMember(1123)]
        public double PriceEdgeSell { get; set; }

        /// <summary>
        ///     Поле amount_sells
        /// </summary>
        [ProtoMember(1124)]
        public int AmountSells { get; set; }

        /// <summary>
        ///     Поле price_edge_buy
        /// </summary>
        [ProtoMember(1125)]
        public double PriceEdgeBuy { get; set; }

        /// <summary>
        ///     Поле amount_buys
        /// </summary>
        [ProtoMember(1126)]
        public int AmountBuys { get; set; }

        /// <summary>
        ///     Поле mm_spread
        /// </summary>
        [ProtoMember(1127)]
        public double MmSpread { get; set; }

        /// <summary>
        ///     Поле mm_amount
        /// </summary>
        [ProtoMember(1128)]
        public int MmAmount { get; set; }

        /// <summary>
        ///     Поле spread_sign
        /// </summary>
        [ProtoMember(1129)]
        public sbyte SpreadSign { get; set; }

        /// <summary>
        ///     Поле amount_sign
        /// </summary>
        [ProtoMember(1130)]
        public sbyte AmountSign { get; set; }

        /// <summary>
        ///     Поле percent_time
        /// </summary>
        [ProtoMember(1131)]
        public double PercentTime { get; set; }

        /// <summary>
        ///     Поле period_start
        /// </summary>
        [ProtoMember(1132)]
        public DateTime PeriodStart { get; set; }

        /// <summary>
        ///     Поле period_end
        /// </summary>
        [ProtoMember(1133)]
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1134)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле cstrike_offset
        /// </summary>
        [ProtoMember(1135)]
        public double CstrikeOffset { get; set; }

        /// <summary>
        ///     Поле active_sign
        /// </summary>
        [ProtoMember(1136)]
        public int ActiveSign { get; set; }

        /// <summary>
        ///     Поле agmt_id
        /// </summary>
        [ProtoMember(1137)]
        public int AgmtId { get; set; }

        /// <summary>
        ///     Поле fulfil_min
        /// </summary>
        [ProtoMember(1138)]
        public double FulfilMin { get; set; }

        /// <summary>
        ///     Поле fulfil_partial
        /// </summary>
        [ProtoMember(1139)]
        public double FulfilPartial { get; set; }

        /// <summary>
        ///     Поле fulfil_total
        /// </summary>
        [ProtoMember(1140)]
        public double FulfilTotal { get; set; }

        /// <summary>
        ///     Поле is_fulfil_min
        /// </summary>
        [ProtoMember(1141)]
        public sbyte IsFulfilMin { get; set; }

        /// <summary>
        ///     Поле is_fulfil_partial
        /// </summary>
        [ProtoMember(1142)]
        public sbyte IsFulfilPartial { get; set; }

        /// <summary>
        ///     Поле is_fulfil_total
        /// </summary>
        [ProtoMember(1143)]
        public sbyte IsFulfilTotal { get; set; }

        /// <summary>
        ///     Поле is_rf
        /// </summary>
        [ProtoMember(1144)]
        public sbyte IsRf { get; set; }

        /// <summary>
        ///     Поле id_group
        /// </summary>
        [ProtoMember(1145)]
        public int IdGroup { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Spread, Spread);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PriceEdgeSell, PriceEdgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountSells, AmountSells);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PriceEdgeBuy, PriceEdgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBuys, AmountBuys);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MmSpread, MmSpread);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MmAmount, MmAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SpreadSign, SpreadSign);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountSign, AmountSign);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PercentTime, PercentTime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PeriodStart, PeriodStart);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PeriodEnd, PeriodEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CstrikeOffset, CstrikeOffset);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ActiveSign, ActiveSign);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AgmtId, AgmtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilMin, FulfilMin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilPartial, FulfilPartial);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FulfilTotal, FulfilTotal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsFulfilMin, IsFulfilMin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsFulfilPartial, IsFulfilPartial);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsFulfilTotal, IsFulfilTotal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsRf, IsRf);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdGroup, IdGroup);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_cs_mm_rule
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmCsMmRule : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "cs_mm_rule";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Mm_CsMmRule;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Mm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1146)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1147)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1148)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1149)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1150)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1151)]
        public int IsinId { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_mm_agreement_filter
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmMmAgreementFilter : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "mm_agreement_filter";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Mm_MmAgreementFilter;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Mm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1152)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1153)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1154)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле agmt_id
        /// </summary>
        [ProtoMember(1155)]
        public int AgmtId { get; set; }

        /// <summary>
        ///     Поле agreement
        /// </summary>
        [ProtoMember(1156)]
        public string Agreement { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1157)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле is_fut
        /// </summary>
        [ProtoMember(1158)]
        public sbyte IsFut { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AgmtId, AgmtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Agreement, Agreement);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsFut, IsFut);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.OptCommon
{
    /// <summary>
    ///     Сообщение cgm_common
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmCommon : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "common";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptCommon_Common;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptCommon;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1159)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1160)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1161)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1162)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1163)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле best_sell
        /// </summary>
        [ProtoMember(1164)]
        public double BestSell { get; set; }

        /// <summary>
        ///     Поле amount_sell
        /// </summary>
        [ProtoMember(1165)]
        public int AmountSell { get; set; }

        /// <summary>
        ///     Поле best_buy
        /// </summary>
        [ProtoMember(1166)]
        public double BestBuy { get; set; }

        /// <summary>
        ///     Поле amount_buy
        /// </summary>
        [ProtoMember(1167)]
        public int AmountBuy { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1168)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле trend
        /// </summary>
        [ProtoMember(1169)]
        public double Trend { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1170)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле deal_time
        /// </summary>
        [ProtoMember(1171)]
        public DateTime DealTime { get; set; }

        /// <summary>
        ///     Поле min_price
        /// </summary>
        [ProtoMember(1172)]
        public double MinPrice { get; set; }

        /// <summary>
        ///     Поле max_price
        /// </summary>
        [ProtoMember(1173)]
        public double MaxPrice { get; set; }

        /// <summary>
        ///     Поле avr_price
        /// </summary>
        [ProtoMember(1174)]
        public double AvrPrice { get; set; }

        /// <summary>
        ///     Поле old_kotir
        /// </summary>
        [ProtoMember(1175)]
        public double OldKotir { get; set; }

        /// <summary>
        ///     Поле deal_count
        /// </summary>
        [ProtoMember(1176)]
        public int DealCount { get; set; }

        /// <summary>
        ///     Поле contr_count
        /// </summary>
        [ProtoMember(1177)]
        public int ContrCount { get; set; }

        /// <summary>
        ///     Поле capital
        /// </summary>
        [ProtoMember(1178)]
        public double Capital { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(1179)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле mod_time
        /// </summary>
        [ProtoMember(1180)]
        public DateTime ModTime { get; set; }

        /// <summary>
        ///     Поле isin_is_spec
        /// </summary>
        [ProtoMember(1181)]
        public sbyte IsinIsSpec { get; set; }

        /// <summary>
        ///     Поле orders_sell_qty
        /// </summary>
        [ProtoMember(1182)]
        public int OrdersSellQty { get; set; }

        /// <summary>
        ///     Поле orders_sell_amount
        /// </summary>
        [ProtoMember(1183)]
        public int OrdersSellAmount { get; set; }

        /// <summary>
        ///     Поле orders_buy_qty
        /// </summary>
        [ProtoMember(1184)]
        public int OrdersBuyQty { get; set; }

        /// <summary>
        ///     Поле orders_buy_amount
        /// </summary>
        [ProtoMember(1185)]
        public int OrdersBuyAmount { get; set; }

        /// <summary>
        ///     Поле open_price
        /// </summary>
        [ProtoMember(1186)]
        public double OpenPrice { get; set; }

        /// <summary>
        ///     Поле close_price
        /// </summary>
        [ProtoMember(1187)]
        public double ClosePrice { get; set; }

        /// <summary>
        ///     Поле local_time
        /// </summary>
        [ProtoMember(1188)]
        public DateTime LocalTime { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BestSell, BestSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountSell, AmountSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BestBuy, BestBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountBuy, AmountBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Trend, Trend);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealTime, DealTime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinPrice, MinPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MaxPrice, MaxPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AvrPrice, AvrPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OldKotir, OldKotir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealCount, DealCount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ContrCount, ContrCount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Capital, Capital);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ModTime, ModTime);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinIsSpec, IsinIsSpec);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersSellQty, OrdersSellQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersSellAmount, OrdersSellAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersBuyQty, OrdersBuyQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrdersBuyAmount, OrdersBuyAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OpenPrice, OpenPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClosePrice, ClosePrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalTime, LocalTime);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.OptInfo
{
    /// <summary>
    ///     Сообщение cgm_opt_rejected_orders
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptRejectedOrders : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_rejected_orders";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptInfo_OptRejectedOrders;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1189)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1190)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1191)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле order_id
        /// </summary>
        [ProtoMember(1192)]
        public long OrderId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1193)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1194)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1195)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле moment_reject
        /// </summary>
        [ProtoMember(1196)]
        public DateTime MomentReject { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1197)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(1198)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1199)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1200)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(1201)]
        public DateTime DateExp { get; set; }

        /// <summary>
        ///     Поле id_ord1
        /// </summary>
        [ProtoMember(1202)]
        public long IdOrd1 { get; set; }

        /// <summary>
        ///     Поле ret_code
        /// </summary>
        [ProtoMember(1203)]
        public int RetCode { get; set; }

        /// <summary>
        ///     Поле ret_message
        /// </summary>
        [ProtoMember(1204)]
        public string RetMessage { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(1205)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле login_from
        /// </summary>
        [ProtoMember(1206)]
        public string LoginFrom { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(1207)]
        public int ExtId { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OrderId, OrderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MomentReject, MomentReject);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd1, IdOrd1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RetCode, RetCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RetMessage, RetMessage);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginFrom, LoginFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_intercl_info
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptInterclInfo : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_intercl_info";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptInfo_OptInterclInfo;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1208)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1209)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1210)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1211)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1212)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле vm_intercl
        /// </summary>
        [ProtoMember(1213)]
        public double VmIntercl { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmIntercl, VmIntercl);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_exp_orders
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptExpOrders : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_exp_orders";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptInfo_OptExpOrders;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1214)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1215)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1216)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле exporder_id
        /// </summary>
        [ProtoMember(1217)]
        public long ExporderId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1218)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1219)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1220)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1221)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле date
        /// </summary>
        [ProtoMember(1222)]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Поле amount_apply
        /// </summary>
        [ProtoMember(1223)]
        public int AmountApply { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExporderId, ExporderId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Date, Date);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountApply, AmountApply);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_vcb
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptVcb : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_vcb";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptInfo_OptVcb;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1224)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1225)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1226)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(1227)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(1228)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле exec_type
        /// </summary>
        [ProtoMember(1229)]
        public string ExecType { get; set; }

        /// <summary>
        ///     Поле curr
        /// </summary>
        [ProtoMember(1230)]
        public string Curr { get; set; }

        /// <summary>
        ///     Поле exch_pay
        /// </summary>
        [ProtoMember(1231)]
        public double ExchPay { get; set; }

        /// <summary>
        ///     Поле exch_pay_scalped
        /// </summary>
        [ProtoMember(1232)]
        public sbyte ExchPayScalped { get; set; }

        /// <summary>
        ///     Поле clear_pay
        /// </summary>
        [ProtoMember(1233)]
        public double ClearPay { get; set; }

        /// <summary>
        ///     Поле clear_pay_scalped
        /// </summary>
        [ProtoMember(1234)]
        public sbyte ClearPayScalped { get; set; }

        /// <summary>
        ///     Поле sell_fee
        /// </summary>
        [ProtoMember(1235)]
        public double SellFee { get; set; }

        /// <summary>
        ///     Поле buy_fee
        /// </summary>
        [ProtoMember(1236)]
        public double BuyFee { get; set; }

        /// <summary>
        ///     Поле trade_scheme
        /// </summary>
        [ProtoMember(1237)]
        public string TradeScheme { get; set; }

        /// <summary>
        ///     Поле coeff_out
        /// </summary>
        [ProtoMember(1238)]
        public double CoeffOut { get; set; }

        /// <summary>
        ///     Поле is_spec
        /// </summary>
        [ProtoMember(1239)]
        public sbyte IsSpec { get; set; }

        /// <summary>
        ///     Поле spec_spread
        /// </summary>
        [ProtoMember(1240)]
        public double SpecSpread { get; set; }

        /// <summary>
        ///     Поле min_vol
        /// </summary>
        [ProtoMember(1241)]
        public int MinVol { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1242)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле rate_id
        /// </summary>
        [ProtoMember(1243)]
        public int RateId { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExecType, ExecType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Curr, Curr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPay, ExchPay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPayScalped, ExchPayScalped);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClearPay, ClearPay);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClearPayScalped, ClearPayScalped);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SellFee, SellFee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuyFee, BuyFee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TradeScheme, TradeScheme);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CoeffOut, CoeffOut);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsSpec, IsSpec);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SpecSpread, SpecSpread);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinVol, MinVol);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RateId, RateId);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_sess_contents
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptSessContents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_sess_contents";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptInfo_OptSessContents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1244)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1245)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1246)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1247)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1248)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(1249)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле short_isin
        /// </summary>
        [ProtoMember(1250)]
        public string ShortIsin { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(1251)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле code_vcb
        /// </summary>
        [ProtoMember(1252)]
        public string CodeVcb { get; set; }

        /// <summary>
        ///     Поле fut_isin_id
        /// </summary>
        [ProtoMember(1253)]
        public int FutIsinId { get; set; }

        /// <summary>
        ///     Поле is_limited
        /// </summary>
        [ProtoMember(1254)]
        public sbyte IsLimited { get; set; }

        /// <summary>
        ///     Поле limit_up
        /// </summary>
        [ProtoMember(1255)]
        public double LimitUp { get; set; }

        /// <summary>
        ///     Поле limit_down
        /// </summary>
        [ProtoMember(1256)]
        public double LimitDown { get; set; }

        /// <summary>
        ///     Поле old_kotir
        /// </summary>
        [ProtoMember(1257)]
        public double OldKotir { get; set; }

        /// <summary>
        ///     Поле bgo_c
        /// </summary>
        [ProtoMember(1258)]
        public double BgoC { get; set; }

        /// <summary>
        ///     Поле bgo_nc
        /// </summary>
        [ProtoMember(1259)]
        public double BgoNc { get; set; }

        /// <summary>
        ///     Поле europe
        /// </summary>
        [ProtoMember(1260)]
        public sbyte Europe { get; set; }

        /// <summary>
        ///     Поле put
        /// </summary>
        [ProtoMember(1261)]
        public sbyte Put { get; set; }

        /// <summary>
        ///     Поле strike
        /// </summary>
        [ProtoMember(1262)]
        public double Strike { get; set; }

        /// <summary>
        ///     Поле roundto
        /// </summary>
        [ProtoMember(1263)]
        public int Roundto { get; set; }

        /// <summary>
        ///     Поле min_step
        /// </summary>
        [ProtoMember(1264)]
        public double MinStep { get; set; }

        /// <summary>
        ///     Поле lot_volume
        /// </summary>
        [ProtoMember(1265)]
        public int LotVolume { get; set; }

        /// <summary>
        ///     Поле step_price
        /// </summary>
        [ProtoMember(1266)]
        public double StepPrice { get; set; }

        /// <summary>
        ///     Поле d_pg
        /// </summary>
        [ProtoMember(1267)]
        public DateTime DPg { get; set; }

        /// <summary>
        ///     Поле d_exec_beg
        /// </summary>
        [ProtoMember(1268)]
        public DateTime DExecBeg { get; set; }

        /// <summary>
        ///     Поле d_exec_end
        /// </summary>
        [ProtoMember(1269)]
        public DateTime DExecEnd { get; set; }

        /// <summary>
        ///     Поле signs
        /// </summary>
        [ProtoMember(1270)]
        public int Signs { get; set; }

        /// <summary>
        ///     Поле last_cl_quote
        /// </summary>
        [ProtoMember(1271)]
        public double LastClQuote { get; set; }

        /// <summary>
        ///     Поле bgo_buy
        /// </summary>
        [ProtoMember(1272)]
        public double BgoBuy { get; set; }

        /// <summary>
        ///     Поле base_isin_id
        /// </summary>
        [ProtoMember(1273)]
        public int BaseIsinId { get; set; }

        /// <summary>
        ///     Поле d_start
        /// </summary>
        [ProtoMember(1274)]
        public DateTime DStart { get; set; }

        /// <summary>
        ///     Поле exch_pay
        /// </summary>
        [ProtoMember(1275)]
        public double ExchPay { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ShortIsin, ShortIsin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeVcb, CodeVcb);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FutIsinId, FutIsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsLimited, IsLimited);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitUp, LimitUp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitDown, LimitDown);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OldKotir, OldKotir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BgoC, BgoC);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BgoNc, BgoNc);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Europe, Europe);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Put, Put);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Strike, Strike);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Roundto, Roundto);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinStep, MinStep);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LotVolume, LotVolume);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StepPrice, StepPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DPg, DPg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DExecBeg, DExecBeg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DExecEnd, DExecEnd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Signs, Signs);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LastClQuote, LastClQuote);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BgoBuy, BgoBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BaseIsinId, BaseIsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DStart, DStart);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExchPay, ExchPay);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_sess_settl
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptSessSettl : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_sess_settl";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptInfo_OptSessSettl;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1276)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1277)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1278)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1279)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле date_clr
        /// </summary>
        [ProtoMember(1280)]
        public DateTime DateClr { get; set; }

        /// <summary>
        ///     Поле isin
        /// </summary>
        [ProtoMember(1281)]
        public string Isin { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1282)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле volat
        /// </summary>
        [ProtoMember(1283)]
        public double Volat { get; set; }

        /// <summary>
        ///     Поле theor_price
        /// </summary>
        [ProtoMember(1284)]
        public double TheorPrice { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateClr, DateClr);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Isin, Isin);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Volat, Volat);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TheorPrice, TheorPrice);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptInfo_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptInfo;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1285)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1286)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1287)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(1288)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1289)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(1290)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(1291)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.OptTrades
{
    /// <summary>
    ///     Сообщение cgm_orders_log
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOrdersLog : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "orders_log";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptTrades_OrdersLog;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1292)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1293)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1294)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id_ord
        /// </summary>
        [ProtoMember(1295)]
        public long IdOrd { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1296)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1297)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1298)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле amount_rest
        /// </summary>
        [ProtoMember(1299)]
        public int AmountRest { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(1300)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле xstatus
        /// </summary>
        [ProtoMember(1301)]
        public long Xstatus { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(1302)]
        public int Status { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1303)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1304)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(1305)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле action
        /// </summary>
        [ProtoMember(1306)]
        public sbyte Action { get; set; }

        /// <summary>
        ///     Поле deal_price
        /// </summary>
        [ProtoMember(1307)]
        public double DealPrice { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1308)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле login_from
        /// </summary>
        [ProtoMember(1309)]
        public string LoginFrom { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(1310)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле hedge
        /// </summary>
        [ProtoMember(1311)]
        public sbyte Hedge { get; set; }

        /// <summary>
        ///     Поле trust
        /// </summary>
        [ProtoMember(1312)]
        public sbyte Trust { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(1313)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле broker_to
        /// </summary>
        [ProtoMember(1314)]
        public string BrokerTo { get; set; }

        /// <summary>
        ///     Поле broker_to_rts
        /// </summary>
        [ProtoMember(1315)]
        public string BrokerToRts { get; set; }

        /// <summary>
        ///     Поле broker_from_rts
        /// </summary>
        [ProtoMember(1316)]
        public string BrokerFromRts { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(1317)]
        public DateTime DateExp { get; set; }

        /// <summary>
        ///     Поле id_ord1
        /// </summary>
        [ProtoMember(1318)]
        public long IdOrd1 { get; set; }

        /// <summary>
        ///     Поле local_stamp
        /// </summary>
        [ProtoMember(1319)]
        public DateTime LocalStamp { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd, IdOrd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountRest, AmountRest);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Xstatus, Xstatus);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Action, Action);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealPrice, DealPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginFrom, LoginFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Hedge, Hedge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Trust, Trust);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerTo, BrokerTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerToRts, BrokerToRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerFromRts, BrokerFromRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd1, IdOrd1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LocalStamp, LocalStamp);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_deal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmDeal : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "deal";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptTrades_Deal;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1320)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1321)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1322)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1323)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1324)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(1325)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле id_deal_multileg
        /// </summary>
        [ProtoMember(1326)]
        public long IdDealMultileg { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(1327)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1328)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле id_ord_buy
        /// </summary>
        [ProtoMember(1329)]
        public long IdOrdBuy { get; set; }

        /// <summary>
        ///     Поле id_ord_sell
        /// </summary>
        [ProtoMember(1330)]
        public long IdOrdSell { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1331)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1332)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле nosystem
        /// </summary>
        [ProtoMember(1333)]
        public sbyte Nosystem { get; set; }

        /// <summary>
        ///     Поле xstatus_buy
        /// </summary>
        [ProtoMember(1334)]
        public long XstatusBuy { get; set; }

        /// <summary>
        ///     Поле xstatus_sell
        /// </summary>
        [ProtoMember(1335)]
        public long XstatusSell { get; set; }

        /// <summary>
        ///     Поле status_buy
        /// </summary>
        [ProtoMember(1336)]
        public int StatusBuy { get; set; }

        /// <summary>
        ///     Поле status_sell
        /// </summary>
        [ProtoMember(1337)]
        public int StatusSell { get; set; }

        /// <summary>
        ///     Поле ext_id_buy
        /// </summary>
        [ProtoMember(1338)]
        public int ExtIdBuy { get; set; }

        /// <summary>
        ///     Поле ext_id_sell
        /// </summary>
        [ProtoMember(1339)]
        public int ExtIdSell { get; set; }

        /// <summary>
        ///     Поле code_buy
        /// </summary>
        [ProtoMember(1340)]
        public string CodeBuy { get; set; }

        /// <summary>
        ///     Поле code_sell
        /// </summary>
        [ProtoMember(1341)]
        public string CodeSell { get; set; }

        /// <summary>
        ///     Поле comment_buy
        /// </summary>
        [ProtoMember(1342)]
        public string CommentBuy { get; set; }

        /// <summary>
        ///     Поле comment_sell
        /// </summary>
        [ProtoMember(1343)]
        public string CommentSell { get; set; }

        /// <summary>
        ///     Поле trust_buy
        /// </summary>
        [ProtoMember(1344)]
        public sbyte TrustBuy { get; set; }

        /// <summary>
        ///     Поле trust_sell
        /// </summary>
        [ProtoMember(1345)]
        public sbyte TrustSell { get; set; }

        /// <summary>
        ///     Поле hedge_buy
        /// </summary>
        [ProtoMember(1346)]
        public sbyte HedgeBuy { get; set; }

        /// <summary>
        ///     Поле hedge_sell
        /// </summary>
        [ProtoMember(1347)]
        public sbyte HedgeSell { get; set; }

        /// <summary>
        ///     Поле fee_buy
        /// </summary>
        [ProtoMember(1348)]
        public double FeeBuy { get; set; }

        /// <summary>
        ///     Поле fee_sell
        /// </summary>
        [ProtoMember(1349)]
        public double FeeSell { get; set; }

        /// <summary>
        ///     Поле login_buy
        /// </summary>
        [ProtoMember(1350)]
        public string LoginBuy { get; set; }

        /// <summary>
        ///     Поле login_sell
        /// </summary>
        [ProtoMember(1351)]
        public string LoginSell { get; set; }

        /// <summary>
        ///     Поле code_rts_buy
        /// </summary>
        [ProtoMember(1352)]
        public string CodeRtsBuy { get; set; }

        /// <summary>
        ///     Поле code_rts_sell
        /// </summary>
        [ProtoMember(1353)]
        public string CodeRtsSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealMultileg, IdDealMultileg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdBuy, IdOrdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdSell, IdOrdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nosystem, Nosystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusBuy, XstatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusSell, XstatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusBuy, StatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusSell, StatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdBuy, ExtIdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdSell, ExtIdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeBuy, CodeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeSell, CodeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentBuy, CommentBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentSell, CommentSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustBuy, TrustBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustSell, TrustSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeBuy, HedgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeSell, HedgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeBuy, FeeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeSell, FeeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginBuy, LoginBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginSell, LoginSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsBuy, CodeRtsBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsSell, CodeRtsSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_heartbeat
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmHeartbeat : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "heartbeat";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptTrades_Heartbeat;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1354)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1355)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1356)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле server_time
        /// </summary>
        [ProtoMember(1357)]
        public DateTime ServerTime { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ServerTime, ServerTime);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptTrades_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1358)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1359)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1360)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(1361)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1362)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(1363)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(1364)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_user_deal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmUserDeal : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "user_deal";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OptTrades_UserDeal;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OptTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1365)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1366)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1367)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1368)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1369)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(1370)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле id_deal_multileg
        /// </summary>
        [ProtoMember(1371)]
        public long IdDealMultileg { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(1372)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1373)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле id_ord_buy
        /// </summary>
        [ProtoMember(1374)]
        public long IdOrdBuy { get; set; }

        /// <summary>
        ///     Поле id_ord_sell
        /// </summary>
        [ProtoMember(1375)]
        public long IdOrdSell { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1376)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1377)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле nosystem
        /// </summary>
        [ProtoMember(1378)]
        public sbyte Nosystem { get; set; }

        /// <summary>
        ///     Поле xstatus_buy
        /// </summary>
        [ProtoMember(1379)]
        public long XstatusBuy { get; set; }

        /// <summary>
        ///     Поле xstatus_sell
        /// </summary>
        [ProtoMember(1380)]
        public long XstatusSell { get; set; }

        /// <summary>
        ///     Поле status_buy
        /// </summary>
        [ProtoMember(1381)]
        public int StatusBuy { get; set; }

        /// <summary>
        ///     Поле status_sell
        /// </summary>
        [ProtoMember(1382)]
        public int StatusSell { get; set; }

        /// <summary>
        ///     Поле ext_id_buy
        /// </summary>
        [ProtoMember(1383)]
        public int ExtIdBuy { get; set; }

        /// <summary>
        ///     Поле ext_id_sell
        /// </summary>
        [ProtoMember(1384)]
        public int ExtIdSell { get; set; }

        /// <summary>
        ///     Поле code_buy
        /// </summary>
        [ProtoMember(1385)]
        public string CodeBuy { get; set; }

        /// <summary>
        ///     Поле code_sell
        /// </summary>
        [ProtoMember(1386)]
        public string CodeSell { get; set; }

        /// <summary>
        ///     Поле comment_buy
        /// </summary>
        [ProtoMember(1387)]
        public string CommentBuy { get; set; }

        /// <summary>
        ///     Поле comment_sell
        /// </summary>
        [ProtoMember(1388)]
        public string CommentSell { get; set; }

        /// <summary>
        ///     Поле trust_buy
        /// </summary>
        [ProtoMember(1389)]
        public sbyte TrustBuy { get; set; }

        /// <summary>
        ///     Поле trust_sell
        /// </summary>
        [ProtoMember(1390)]
        public sbyte TrustSell { get; set; }

        /// <summary>
        ///     Поле hedge_buy
        /// </summary>
        [ProtoMember(1391)]
        public sbyte HedgeBuy { get; set; }

        /// <summary>
        ///     Поле hedge_sell
        /// </summary>
        [ProtoMember(1392)]
        public sbyte HedgeSell { get; set; }

        /// <summary>
        ///     Поле fee_buy
        /// </summary>
        [ProtoMember(1393)]
        public double FeeBuy { get; set; }

        /// <summary>
        ///     Поле fee_sell
        /// </summary>
        [ProtoMember(1394)]
        public double FeeSell { get; set; }

        /// <summary>
        ///     Поле login_buy
        /// </summary>
        [ProtoMember(1395)]
        public string LoginBuy { get; set; }

        /// <summary>
        ///     Поле login_sell
        /// </summary>
        [ProtoMember(1396)]
        public string LoginSell { get; set; }

        /// <summary>
        ///     Поле code_rts_buy
        /// </summary>
        [ProtoMember(1397)]
        public string CodeRtsBuy { get; set; }

        /// <summary>
        ///     Поле code_rts_sell
        /// </summary>
        [ProtoMember(1398)]
        public string CodeRtsSell { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDealMultileg, IdDealMultileg);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdBuy, IdOrdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrdSell, IdOrdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Nosystem, Nosystem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusBuy, XstatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.XstatusSell, XstatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusBuy, StatusBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.StatusSell, StatusSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdBuy, ExtIdBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtIdSell, ExtIdSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeBuy, CodeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeSell, CodeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentBuy, CommentBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CommentSell, CommentSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustBuy, TrustBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TrustSell, TrustSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeBuy, HedgeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.HedgeSell, HedgeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeBuy, FeeBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.FeeSell, FeeSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginBuy, LoginBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginSell, LoginSell);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsBuy, CodeRtsBuy);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CodeRtsSell, CodeRtsSell);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Ordbook
{
    /// <summary>
    ///     Сообщение cgm_orders
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOrders : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "orders";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Ordbook_Orders;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Ordbook;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1399)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1400)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1401)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id_ord
        /// </summary>
        [ProtoMember(1402)]
        public long IdOrd { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1403)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1404)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле xstatus
        /// </summary>
        [ProtoMember(1405)]
        public long Xstatus { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(1406)]
        public int Status { get; set; }

        /// <summary>
        ///     Поле action
        /// </summary>
        [ProtoMember(1407)]
        public sbyte Action { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1408)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(1409)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1410)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1411)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле amount_rest
        /// </summary>
        [ProtoMember(1412)]
        public int AmountRest { get; set; }

        /// <summary>
        ///     Поле init_moment
        /// </summary>
        [ProtoMember(1413)]
        public DateTime InitMoment { get; set; }

        /// <summary>
        ///     Поле init_amount
        /// </summary>
        [ProtoMember(1414)]
        public int InitAmount { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd, IdOrd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Xstatus, Xstatus);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Action, Action);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountRest, AmountRest);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InitMoment, InitMoment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InitAmount, InitAmount);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_info
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmInfo : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "info";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Ordbook_Info;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Ordbook;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1415)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1416)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1417)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле infoID
        /// </summary>
        [ProtoMember(1418)]
        public long InfoId { get; set; }

        /// <summary>
        ///     Поле logRev
        /// </summary>
        [ProtoMember(1419)]
        public long LogRev { get; set; }

        /// <summary>
        ///     Поле lifeNum
        /// </summary>
        [ProtoMember(1420)]
        public int LifeNum { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1421)]
        public DateTime Moment { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InfoId, InfoId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LogRev, LogRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LifeNum, LifeNum);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Orderbook
{
    /// <summary>
    ///     Сообщение cgm_orders
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOrders : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "orders";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Orderbook_Orders;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Orderbook;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1422)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1423)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1424)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id_ord
        /// </summary>
        [ProtoMember(1425)]
        public long IdOrd { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1426)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1427)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1428)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле xstatus
        /// </summary>
        [ProtoMember(1429)]
        public long Xstatus { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(1430)]
        public int Status { get; set; }

        /// <summary>
        ///     Поле action
        /// </summary>
        [ProtoMember(1431)]
        public sbyte Action { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1432)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(1433)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1434)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1435)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле amount_rest
        /// </summary>
        [ProtoMember(1436)]
        public int AmountRest { get; set; }

        /// <summary>
        ///     Поле comment
        /// </summary>
        [ProtoMember(1437)]
        public string Comment { get; set; }

        /// <summary>
        ///     Поле hedge
        /// </summary>
        [ProtoMember(1438)]
        public sbyte Hedge { get; set; }

        /// <summary>
        ///     Поле trust
        /// </summary>
        [ProtoMember(1439)]
        public sbyte Trust { get; set; }

        /// <summary>
        ///     Поле ext_id
        /// </summary>
        [ProtoMember(1440)]
        public int ExtId { get; set; }

        /// <summary>
        ///     Поле login_from
        /// </summary>
        [ProtoMember(1441)]
        public string LoginFrom { get; set; }

        /// <summary>
        ///     Поле broker_to
        /// </summary>
        [ProtoMember(1442)]
        public string BrokerTo { get; set; }

        /// <summary>
        ///     Поле broker_to_rts
        /// </summary>
        [ProtoMember(1443)]
        public string BrokerToRts { get; set; }

        /// <summary>
        ///     Поле date_exp
        /// </summary>
        [ProtoMember(1444)]
        public DateTime DateExp { get; set; }

        /// <summary>
        ///     Поле id_ord1
        /// </summary>
        [ProtoMember(1445)]
        public long IdOrd1 { get; set; }

        /// <summary>
        ///     Поле broker_from_rts
        /// </summary>
        [ProtoMember(1446)]
        public string BrokerFromRts { get; set; }

        /// <summary>
        ///     Поле init_moment
        /// </summary>
        [ProtoMember(1447)]
        public DateTime InitMoment { get; set; }

        /// <summary>
        ///     Поле init_amount
        /// </summary>
        [ProtoMember(1448)]
        public int InitAmount { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd, IdOrd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Xstatus, Xstatus);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Action, Action);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountRest, AmountRest);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Comment, Comment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Hedge, Hedge);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Trust, Trust);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ExtId, ExtId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LoginFrom, LoginFrom);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerTo, BrokerTo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerToRts, BrokerToRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DateExp, DateExp);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd1, IdOrd1);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BrokerFromRts, BrokerFromRts);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InitMoment, InitMoment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InitAmount, InitAmount);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_info
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmInfo : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "info";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Orderbook_Info;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Orderbook;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1449)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1450)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1451)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле infoID
        /// </summary>
        [ProtoMember(1452)]
        public long InfoId { get; set; }

        /// <summary>
        ///     Поле logRev
        /// </summary>
        [ProtoMember(1453)]
        public long LogRev { get; set; }

        /// <summary>
        ///     Поле lifeNum
        /// </summary>
        [ProtoMember(1454)]
        public int LifeNum { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1455)]
        public DateTime Moment { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.InfoId, InfoId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LogRev, LogRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LifeNum, LifeNum);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.OrdersAggr
{
    /// <summary>
    ///     Сообщение cgm_orders_aggr
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOrdersAggr : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "orders_aggr";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OrdersAggr_OrdersAggr;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OrdersAggr;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1456)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1457)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1458)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1459)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1460)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле volume
        /// </summary>
        [ProtoMember(1461)]
        public long Volume { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1462)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(1463)]
        public sbyte Dir { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Volume, Volume);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.OrdLogTrades
{
    /// <summary>
    ///     Сообщение cgm_orders_log
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOrdersLog : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "orders_log";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OrdLogTrades_OrdersLog;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OrdLogTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1464)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1465)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1466)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id_ord
        /// </summary>
        [ProtoMember(1467)]
        public long IdOrd { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1468)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1469)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1470)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле amount_rest
        /// </summary>
        [ProtoMember(1471)]
        public int AmountRest { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(1472)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле xstatus
        /// </summary>
        [ProtoMember(1473)]
        public long Xstatus { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(1474)]
        public int Status { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1475)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1476)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(1477)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле action
        /// </summary>
        [ProtoMember(1478)]
        public sbyte Action { get; set; }

        /// <summary>
        ///     Поле deal_price
        /// </summary>
        [ProtoMember(1479)]
        public double DealPrice { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd, IdOrd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountRest, AmountRest);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Xstatus, Xstatus);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Action, Action);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealPrice, DealPrice);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_multileg_orders_log
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmMultilegOrdersLog : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "multileg_orders_log";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OrdLogTrades_MultilegOrdersLog;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OrdLogTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1480)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1481)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1482)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле id_ord
        /// </summary>
        [ProtoMember(1483)]
        public long IdOrd { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1484)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1485)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(1486)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле amount_rest
        /// </summary>
        [ProtoMember(1487)]
        public int AmountRest { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(1488)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле xstatus
        /// </summary>
        [ProtoMember(1489)]
        public long Xstatus { get; set; }

        /// <summary>
        ///     Поле status
        /// </summary>
        [ProtoMember(1490)]
        public int Status { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(1491)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1492)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле dir
        /// </summary>
        [ProtoMember(1493)]
        public sbyte Dir { get; set; }

        /// <summary>
        ///     Поле action
        /// </summary>
        [ProtoMember(1494)]
        public sbyte Action { get; set; }

        /// <summary>
        ///     Поле deal_price
        /// </summary>
        [ProtoMember(1495)]
        public double DealPrice { get; set; }

        /// <summary>
        ///     Поле rate_price
        /// </summary>
        [ProtoMember(1496)]
        public double RatePrice { get; set; }

        /// <summary>
        ///     Поле swap_price
        /// </summary>
        [ProtoMember(1497)]
        public double SwapPrice { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdOrd, IdOrd);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Amount, Amount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.AmountRest, AmountRest);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IdDeal, IdDeal);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Xstatus, Xstatus);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Status, Status);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Price, Price);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Dir, Dir);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Action, Action);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DealPrice, DealPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RatePrice, RatePrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SwapPrice, SwapPrice);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_heartbeat
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmHeartbeat : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "heartbeat";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OrdLogTrades_Heartbeat;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OrdLogTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1498)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1499)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1500)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле server_time
        /// </summary>
        [ProtoMember(1501)]
        public DateTime ServerTime { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ServerTime, ServerTime);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.OrdLogTrades_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.OrdLogTrades;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1502)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1503)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1504)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(1505)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1506)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(1507)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(1508)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Part
{
    /// <summary>
    ///     Сообщение cgm_part
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmPart : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "part";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Part_Part;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Part;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1509)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1510)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1511)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1512)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле money_free
        /// </summary>
        [ProtoMember(1513)]
        public double MoneyFree { get; set; }

        /// <summary>
        ///     Поле money_blocked
        /// </summary>
        [ProtoMember(1514)]
        public double MoneyBlocked { get; set; }

        /// <summary>
        ///     Поле pledge_free
        /// </summary>
        [ProtoMember(1515)]
        public double PledgeFree { get; set; }

        /// <summary>
        ///     Поле pledge_blocked
        /// </summary>
        [ProtoMember(1516)]
        public double PledgeBlocked { get; set; }

        /// <summary>
        ///     Поле vm_reserve
        /// </summary>
        [ProtoMember(1517)]
        public double VmReserve { get; set; }

        /// <summary>
        ///     Поле fee
        /// </summary>
        [ProtoMember(1518)]
        public double Fee { get; set; }

        /// <summary>
        ///     Поле balance_money
        /// </summary>
        [ProtoMember(1519)]
        public double BalanceMoney { get; set; }

        /// <summary>
        ///     Поле coeff_go
        /// </summary>
        [ProtoMember(1520)]
        public double CoeffGo { get; set; }

        /// <summary>
        ///     Поле coeff_liquidity
        /// </summary>
        [ProtoMember(1521)]
        public double CoeffLiquidity { get; set; }

        /// <summary>
        ///     Поле limits_set
        /// </summary>
        [ProtoMember(1522)]
        public sbyte LimitsSet { get; set; }

        /// <summary>
        ///     Поле money_old
        /// </summary>
        [ProtoMember(1523)]
        public double MoneyOld { get; set; }

        /// <summary>
        ///     Поле money_amount
        /// </summary>
        [ProtoMember(1524)]
        public double MoneyAmount { get; set; }

        /// <summary>
        ///     Поле pledge_old
        /// </summary>
        [ProtoMember(1525)]
        public double PledgeOld { get; set; }

        /// <summary>
        ///     Поле pledge_amount
        /// </summary>
        [ProtoMember(1526)]
        public double PledgeAmount { get; set; }

        /// <summary>
        ///     Поле money_pledge_amount
        /// </summary>
        [ProtoMember(1527)]
        public double MoneyPledgeAmount { get; set; }

        /// <summary>
        ///     Поле vm_intercl
        /// </summary>
        [ProtoMember(1528)]
        public double VmIntercl { get; set; }

        /// <summary>
        ///     Поле is_auto_update_limit
        /// </summary>
        [ProtoMember(1529)]
        public sbyte IsAutoUpdateLimit { get; set; }

        /// <summary>
        ///     Поле no_fut_discount
        /// </summary>
        [ProtoMember(1530)]
        public sbyte NoFutDiscount { get; set; }

        /// <summary>
        ///     Поле num_clr_2delivery
        /// </summary>
        [ProtoMember(1531)]
        public int NumClr2delivery { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyFree, MoneyFree);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyBlocked, MoneyBlocked);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PledgeFree, PledgeFree);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PledgeBlocked, PledgeBlocked);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmReserve, VmReserve);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Fee, Fee);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BalanceMoney, BalanceMoney);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CoeffGo, CoeffGo);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.CoeffLiquidity, CoeffLiquidity);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LimitsSet, LimitsSet);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyOld, MoneyOld);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyAmount, MoneyAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PledgeOld, PledgeOld);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PledgeAmount, PledgeAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyPledgeAmount, MoneyPledgeAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmIntercl, VmIntercl);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsAutoUpdateLimit, IsAutoUpdateLimit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NoFutDiscount, NoFutDiscount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NumClr2delivery, NumClr2delivery);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_part_sa
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmPartSa : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "part_sa";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Part_PartSa;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Part;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1532)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1533)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1534)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле settlement_account
        /// </summary>
        [ProtoMember(1535)]
        public string SettlementAccount { get; set; }

        /// <summary>
        ///     Поле money_amount
        /// </summary>
        [ProtoMember(1536)]
        public double MoneyAmount { get; set; }

        /// <summary>
        ///     Поле money_free
        /// </summary>
        [ProtoMember(1537)]
        public double MoneyFree { get; set; }

        /// <summary>
        ///     Поле pledge_amount
        /// </summary>
        [ProtoMember(1538)]
        public double PledgeAmount { get; set; }

        /// <summary>
        ///     Поле money_pledge_amount
        /// </summary>
        [ProtoMember(1539)]
        public double MoneyPledgeAmount { get; set; }

        /// <summary>
        ///     Поле liquidity_ratio
        /// </summary>
        [ProtoMember(1540)]
        public double LiquidityRatio { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlementAccount, SettlementAccount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyAmount, MoneyAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyFree, MoneyFree);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PledgeAmount, PledgeAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MoneyPledgeAmount, MoneyPledgeAmount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LiquidityRatio, LiquidityRatio);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Part_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Part;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1541)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1542)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1543)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(1544)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1545)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(1546)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(1547)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Pos
{
    /// <summary>
    ///     Сообщение cgm_position
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmPosition : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "position";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Pos_Position;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Pos;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1548)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1549)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1550)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1551)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1552)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле pos
        /// </summary>
        [ProtoMember(1553)]
        public int Pos { get; set; }

        /// <summary>
        ///     Поле buys_qty
        /// </summary>
        [ProtoMember(1554)]
        public int BuysQty { get; set; }

        /// <summary>
        ///     Поле sells_qty
        /// </summary>
        [ProtoMember(1555)]
        public int SellsQty { get; set; }

        /// <summary>
        ///     Поле open_qty
        /// </summary>
        [ProtoMember(1556)]
        public int OpenQty { get; set; }

        /// <summary>
        ///     Поле waprice
        /// </summary>
        [ProtoMember(1557)]
        public double Waprice { get; set; }

        /// <summary>
        ///     Поле net_volume_rur
        /// </summary>
        [ProtoMember(1558)]
        public double NetVolumeRur { get; set; }

        /// <summary>
        ///     Поле last_deal_id
        /// </summary>
        [ProtoMember(1559)]
        public long LastDealId { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Pos, Pos);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.BuysQty, BuysQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SellsQty, SellsQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OpenQty, OpenQty);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Waprice, Waprice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.NetVolumeRur, NetVolumeRur);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.LastDealId, LastDealId);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_sys_events
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmSysEvents : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "sys_events";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Pos_SysEvents;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Pos;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1560)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1561)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1562)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле event_id
        /// </summary>
        [ProtoMember(1563)]
        public long EventId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1564)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле event_type
        /// </summary>
        [ProtoMember(1565)]
        public int EventType { get; set; }

        /// <summary>
        ///     Поле message
        /// </summary>
        [ProtoMember(1566)]
        public string Message { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventId, EventId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.EventType, EventType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Message, Message);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Rates
{
    /// <summary>
    ///     Сообщение cgm_curr_online
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmCurrOnline : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "curr_online";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Rates_CurrOnline;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Rates;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1567)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1568)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1569)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле rate_id
        /// </summary>
        [ProtoMember(1570)]
        public int RateId { get; set; }

        /// <summary>
        ///     Поле value
        /// </summary>
        [ProtoMember(1571)]
        public double Value { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1572)]
        public DateTime Moment { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.RateId, RateId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Value, Value);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.RtsIndex
{
    /// <summary>
    ///     Сообщение cgm_rts_index
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmRtsIndex : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "rts_index";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.RtsIndex_RtsIndex;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.RtsIndex;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1573)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1574)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1575)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(1576)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1577)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле value
        /// </summary>
        [ProtoMember(1578)]
        public double Value { get; set; }

        /// <summary>
        ///     Поле prev_close_value
        /// </summary>
        [ProtoMember(1579)]
        public double PrevCloseValue { get; set; }

        /// <summary>
        ///     Поле open_value
        /// </summary>
        [ProtoMember(1580)]
        public double OpenValue { get; set; }

        /// <summary>
        ///     Поле max_value
        /// </summary>
        [ProtoMember(1581)]
        public double MaxValue { get; set; }

        /// <summary>
        ///     Поле min_value
        /// </summary>
        [ProtoMember(1582)]
        public double MinValue { get; set; }

        /// <summary>
        ///     Поле usd_rate
        /// </summary>
        [ProtoMember(1583)]
        public double UsdRate { get; set; }

        /// <summary>
        ///     Поле cap
        /// </summary>
        [ProtoMember(1584)]
        public double Cap { get; set; }

        /// <summary>
        ///     Поле volume
        /// </summary>
        [ProtoMember(1585)]
        public double Volume { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Value, Value);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PrevCloseValue, PrevCloseValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OpenValue, OpenValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MaxValue, MaxValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinValue, MinValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UsdRate, UsdRate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Cap, Cap);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Volume, Volume);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.RtsIndexlog
{
    /// <summary>
    ///     Сообщение cgm_rts_index_log
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmRtsIndexLog : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "rts_index_log";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.RtsIndexlog_RtsIndexLog;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.RtsIndexlog;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1586)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1587)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1588)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле name
        /// </summary>
        [ProtoMember(1589)]
        public string Name { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(1590)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле value
        /// </summary>
        [ProtoMember(1591)]
        public double Value { get; set; }

        /// <summary>
        ///     Поле prev_close_value
        /// </summary>
        [ProtoMember(1592)]
        public double PrevCloseValue { get; set; }

        /// <summary>
        ///     Поле open_value
        /// </summary>
        [ProtoMember(1593)]
        public double OpenValue { get; set; }

        /// <summary>
        ///     Поле max_value
        /// </summary>
        [ProtoMember(1594)]
        public double MaxValue { get; set; }

        /// <summary>
        ///     Поле min_value
        /// </summary>
        [ProtoMember(1595)]
        public double MinValue { get; set; }

        /// <summary>
        ///     Поле usd_rate
        /// </summary>
        [ProtoMember(1596)]
        public double UsdRate { get; set; }

        /// <summary>
        ///     Поле cap
        /// </summary>
        [ProtoMember(1597)]
        public double Cap { get; set; }

        /// <summary>
        ///     Поле volume
        /// </summary>
        [ProtoMember(1598)]
        public double Volume { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Name, Name);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Moment, Moment);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Value, Value);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.PrevCloseValue, PrevCloseValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.OpenValue, OpenValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MaxValue, MaxValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.MinValue, MinValue);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UsdRate, UsdRate);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Cap, Cap);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Volume, Volume);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Tnpenalty
{
    /// <summary>
    ///     Сообщение cgm_fee_all
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFeeAll : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fee_all";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Tnpenalty_FeeAll;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Tnpenalty;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1599)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1600)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1601)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле time
        /// </summary>
        [ProtoMember(1602)]
        public long Time { get; set; }

        /// <summary>
        ///     Поле p2login
        /// </summary>
        [ProtoMember(1603)]
        public string P2login { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1604)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле points
        /// </summary>
        [ProtoMember(1605)]
        public int Points { get; set; }

        /// <summary>
        ///     Поле fee
        /// </summary>
        [ProtoMember(1606)]
        public double Fee { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Time, Time);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.P2login, P2login);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Points, Points);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Fee, Fee);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fee_tn
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFeeTn : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fee_tn";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Tnpenalty_FeeTn;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Tnpenalty;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1607)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1608)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1609)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле time
        /// </summary>
        [ProtoMember(1610)]
        public long Time { get; set; }

        /// <summary>
        ///     Поле p2login
        /// </summary>
        [ProtoMember(1611)]
        public string P2login { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1612)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле tn_type
        /// </summary>
        [ProtoMember(1613)]
        public int TnType { get; set; }

        /// <summary>
        ///     Поле err_code
        /// </summary>
        [ProtoMember(1614)]
        public int ErrCode { get; set; }

        /// <summary>
        ///     Поле count
        /// </summary>
        [ProtoMember(1615)]
        public int Count { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Time, Time);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.P2login, P2login);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TnType, TnType);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ErrCode, ErrCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Count, Count);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Vm
{
    /// <summary>
    ///     Сообщение cgm_fut_vm
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutVm : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_vm";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Vm_FutVm;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Vm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1616)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1617)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1618)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1619)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1620)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1621)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(1622)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле vm_real
        /// </summary>
        [ProtoMember(1623)]
        public double VmReal { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmReal, VmReal);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_vm
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptVm : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_vm";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Vm_OptVm;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Vm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1624)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1625)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1626)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1627)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1628)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле client_code
        /// </summary>
        [ProtoMember(1629)]
        public string ClientCode { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(1630)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле vm_real
        /// </summary>
        [ProtoMember(1631)]
        public double VmReal { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ClientCode, ClientCode);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmReal, VmReal);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_fut_vm_sa
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmFutVmSa : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "fut_vm_sa";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Vm_FutVmSa;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Vm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1632)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1633)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1634)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1635)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1636)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле settlement_account
        /// </summary>
        [ProtoMember(1637)]
        public string SettlementAccount { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(1638)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле vm_real
        /// </summary>
        [ProtoMember(1639)]
        public double VmReal { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlementAccount, SettlementAccount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmReal, VmReal);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

    /// <summary>
    ///     Сообщение cgm_opt_vm_sa
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmOptVmSa : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "opt_vm_sa";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Vm_OptVmSa;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Vm;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1640)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1641)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1642)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1643)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1644)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле settlement_account
        /// </summary>
        [ProtoMember(1645)]
        public string SettlementAccount { get; set; }

        /// <summary>
        ///     Поле vm
        /// </summary>
        [ProtoMember(1646)]
        public double Vm { get; set; }

        /// <summary>
        ///     Поле vm_real
        /// </summary>
        [ProtoMember(1647)]
        public double VmReal { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SettlementAccount, SettlementAccount);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Vm, Vm);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.VmReal, VmReal);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages.Volat
{
    /// <summary>
    ///     Сообщение cgm_volat
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CgmVolat : CGateMessage
    {
        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "volat";
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.Volat_Volat;
		
        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Volat;

        /// <summary>
        ///     Поле replID
        /// </summary>
        [ProtoMember(1648)]
        public long ReplId { get; set; }

        /// <summary>
        ///     Поле replRev
        /// </summary>
        [ProtoMember(1649)]
        public long ReplRev { get; set; }

        /// <summary>
        ///     Поле replAct
        /// </summary>
        [ProtoMember(1650)]
        public long ReplAct { get; set; }

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(1651)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле sess_id
        /// </summary>
        [ProtoMember(1652)]
        public int SessId { get; set; }

        /// <summary>
        ///     Поле volat
        /// </summary>
        [ProtoMember(1653)]
        public double Volat { get; set; }

        /// <summary>
        ///     Поле theor_price
        /// </summary>
        [ProtoMember(1654)]
        public double TheorPrice { get; set; }

        /// <summary>
        ///     Поле theor_price_limit
        /// </summary>
        [ProtoMember(1655)]
        public double TheorPriceLimit { get; set; }

        /// <summary>
        ///     Поле up_prem
        /// </summary>
        [ProtoMember(1656)]
        public double UpPrem { get; set; }

        /// <summary>
        ///     Поле down_prem
        /// </summary>
        [ProtoMember(1657)]
        public double DownPrem { get; set; }


        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplId, ReplId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplRev, ReplRev);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.ReplAct, ReplAct);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.IsinId, IsinId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.SessId, SessId);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.Volat, Volat);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TheorPrice, TheorPrice);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.TheorPriceLimit, TheorPriceLimit);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.UpPrem, UpPrem);
            builder.Add(global::CGateAdapter.Messages.CGateFieldNames.DownPrem, DownPrem);
            
            return builder.ToString();
        }
		
        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);
    }

}

namespace CGateAdapter.Messages
{
    /// <summary>
    ///		Типы сообщений. Состоит из имени потока (ini файла, с которым открывается поток) и имени типа сообщения через подчёркивание.
    ///		С помощью этого енама можно фильтровать на клиенте (в обработчике события) только те сообщения, которые интересуют.
    /// </summary>
    [PublicAPI]
	public enum CGateMessageType
    {
        /// <summary>
        ///		Системное сообщение StreamStateChange
        /// </summary>
        StreamStateChange,

        /// <summary>
        ///		Системное сообщение CGateOrder
        /// </summary>
        CGateOrder,

        /// <summary>
        ///		Системное сообщение CGateDelOrderReply
        /// </summary>
        CGateDelOrderReply,

        /// <summary>
        ///		Системное сообщение CGateDeal
        /// </summary>
        CGateDeal,

        /// <summary>
        ///		Системное сообщение CGateDataEnd
        /// </summary>
        CGateDataEnd,

        /// <summary>
        ///		Системное сообщение CGateDataBegin
        /// </summary>
        CGateDataBegin,

        /// <summary>
        ///		Системное сообщение CGateAddOrderReply
        /// </summary>
        CGateAddOrderReply,

        /// <summary>
        ///		Системное сообщение CGConnectionStateChange
        /// </summary>
        CGConnectionStateChange,

        /// <summary>
        ///		Системное сообщение CGateClearTableMessage
        /// </summary>
        CGateClearTableMessage,

        /// <summary>
        ///     Сообщение money_clearing из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "money_clearing")]
        Clr_MoneyClearing,

        /// <summary>
        ///     Сообщение money_clearing_sa из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "money_clearing_sa")]
        Clr_MoneyClearingSa,

        /// <summary>
        ///     Сообщение clr_rate из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "clr_rate")]
        Clr_ClrRate,

        /// <summary>
        ///     Сообщение fut_pos из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "fut_pos")]
        Clr_FutPos,

        /// <summary>
        ///     Сообщение opt_pos из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "opt_pos")]
        Clr_OptPos,

        /// <summary>
        ///     Сообщение fut_pos_sa из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "fut_pos_sa")]
        Clr_FutPosSa,

        /// <summary>
        ///     Сообщение opt_pos_sa из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "opt_pos_sa")]
        Clr_OptPosSa,

        /// <summary>
        ///     Сообщение fut_sess_settl из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "fut_sess_settl")]
        Clr_FutSessSettl,

        /// <summary>
        ///     Сообщение opt_sess_settl из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "opt_sess_settl")]
        Clr_OptSessSettl,

        /// <summary>
        ///     Сообщение pledge_details из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "pledge_details")]
        Clr_PledgeDetails,

        /// <summary>
        ///     Сообщение sys_events из потока clr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("clr", "sys_events")]
        Clr_SysEvents,

        /// <summary>
        ///     Сообщение FutAddOrder из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutAddOrder")]
        FortsMessages_FutAddOrder,

        /// <summary>
        ///     Сообщение FORTS_MSG101 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG101")]
        FortsMessages_FortsMsg101,

        /// <summary>
        ///     Сообщение FutAddMultiLegOrder из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutAddMultiLegOrder")]
        FortsMessages_FutAddMultiLegOrder,

        /// <summary>
        ///     Сообщение FORTS_MSG129 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG129")]
        FortsMessages_FortsMsg129,

        /// <summary>
        ///     Сообщение FutDelOrder из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutDelOrder")]
        FortsMessages_FutDelOrder,

        /// <summary>
        ///     Сообщение FORTS_MSG102 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG102")]
        FortsMessages_FortsMsg102,

        /// <summary>
        ///     Сообщение FutDelUserOrders из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutDelUserOrders")]
        FortsMessages_FutDelUserOrders,

        /// <summary>
        ///     Сообщение FORTS_MSG103 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG103")]
        FortsMessages_FortsMsg103,

        /// <summary>
        ///     Сообщение FutMoveOrder из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutMoveOrder")]
        FortsMessages_FutMoveOrder,

        /// <summary>
        ///     Сообщение FORTS_MSG105 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG105")]
        FortsMessages_FortsMsg105,

        /// <summary>
        ///     Сообщение OptAddOrder из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptAddOrder")]
        FortsMessages_OptAddOrder,

        /// <summary>
        ///     Сообщение FORTS_MSG109 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG109")]
        FortsMessages_FortsMsg109,

        /// <summary>
        ///     Сообщение OptDelOrder из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptDelOrder")]
        FortsMessages_OptDelOrder,

        /// <summary>
        ///     Сообщение FORTS_MSG110 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG110")]
        FortsMessages_FortsMsg110,

        /// <summary>
        ///     Сообщение OptDelUserOrders из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptDelUserOrders")]
        FortsMessages_OptDelUserOrders,

        /// <summary>
        ///     Сообщение FORTS_MSG111 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG111")]
        FortsMessages_FortsMsg111,

        /// <summary>
        ///     Сообщение OptMoveOrder из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptMoveOrder")]
        FortsMessages_OptMoveOrder,

        /// <summary>
        ///     Сообщение FORTS_MSG113 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG113")]
        FortsMessages_FortsMsg113,

        /// <summary>
        ///     Сообщение FutChangeClientMoney из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutChangeClientMoney")]
        FortsMessages_FutChangeClientMoney,

        /// <summary>
        ///     Сообщение FORTS_MSG104 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG104")]
        FortsMessages_FortsMsg104,

        /// <summary>
        ///     Сообщение FutChangeBFMoney из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutChangeBFMoney")]
        FortsMessages_FutChangeBfmoney,

        /// <summary>
        ///     Сообщение FORTS_MSG107 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG107")]
        FortsMessages_FortsMsg107,

        /// <summary>
        ///     Сообщение OptChangeExpiration из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptChangeExpiration")]
        FortsMessages_OptChangeExpiration,

        /// <summary>
        ///     Сообщение FORTS_MSG112 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG112")]
        FortsMessages_FortsMsg112,

        /// <summary>
        ///     Сообщение FutChangeClientProhibit из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutChangeClientProhibit")]
        FortsMessages_FutChangeClientProhibit,

        /// <summary>
        ///     Сообщение FORTS_MSG115 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG115")]
        FortsMessages_FortsMsg115,

        /// <summary>
        ///     Сообщение OptChangeClientProhibit из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptChangeClientProhibit")]
        FortsMessages_OptChangeClientProhibit,

        /// <summary>
        ///     Сообщение FORTS_MSG117 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG117")]
        FortsMessages_FortsMsg117,

        /// <summary>
        ///     Сообщение FutExchangeBFMoney из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutExchangeBFMoney")]
        FortsMessages_FutExchangeBfmoney,

        /// <summary>
        ///     Сообщение FORTS_MSG130 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG130")]
        FortsMessages_FortsMsg130,

        /// <summary>
        ///     Сообщение OptRecalcCS из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptRecalcCS")]
        FortsMessages_OptRecalcCs,

        /// <summary>
        ///     Сообщение FORTS_MSG132 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG132")]
        FortsMessages_FortsMsg132,

        /// <summary>
        ///     Сообщение FutTransferClientPosition из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutTransferClientPosition")]
        FortsMessages_FutTransferClientPosition,

        /// <summary>
        ///     Сообщение FORTS_MSG137 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG137")]
        FortsMessages_FortsMsg137,

        /// <summary>
        ///     Сообщение OptTransferClientPosition из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptTransferClientPosition")]
        FortsMessages_OptTransferClientPosition,

        /// <summary>
        ///     Сообщение FORTS_MSG138 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG138")]
        FortsMessages_FortsMsg138,

        /// <summary>
        ///     Сообщение OptChangeRiskParameters из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "OptChangeRiskParameters")]
        FortsMessages_OptChangeRiskParameters,

        /// <summary>
        ///     Сообщение FORTS_MSG140 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG140")]
        FortsMessages_FortsMsg140,

        /// <summary>
        ///     Сообщение FutTransferRisk из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FutTransferRisk")]
        FortsMessages_FutTransferRisk,

        /// <summary>
        ///     Сообщение FORTS_MSG139 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG139")]
        FortsMessages_FortsMsg139,

        /// <summary>
        ///     Сообщение CODHeartbeat из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "CODHeartbeat")]
        FortsMessages_Codheartbeat,

        /// <summary>
        ///     Сообщение FORTS_MSG99 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG99")]
        FortsMessages_FortsMsg99,

        /// <summary>
        ///     Сообщение FORTS_MSG100 из потока forts_messages
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("forts_messages", "FORTS_MSG100")]
        FortsMessages_FortsMsg100,

        /// <summary>
        ///     Сообщение common из потока fut_common
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_common", "common")]
        FutCommon_Common,

        /// <summary>
        ///     Сообщение delivery_report из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "delivery_report")]
        FutInfo_DeliveryReport,

        /// <summary>
        ///     Сообщение fut_rejected_orders из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_rejected_orders")]
        FutInfo_FutRejectedOrders,

        /// <summary>
        ///     Сообщение fut_intercl_info из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_intercl_info")]
        FutInfo_FutInterclInfo,

        /// <summary>
        ///     Сообщение fut_bond_registry из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_bond_registry")]
        FutInfo_FutBondRegistry,

        /// <summary>
        ///     Сообщение fut_bond_isin из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_bond_isin")]
        FutInfo_FutBondIsin,

        /// <summary>
        ///     Сообщение fut_bond_nkd из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_bond_nkd")]
        FutInfo_FutBondNkd,

        /// <summary>
        ///     Сообщение fut_bond_nominal из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_bond_nominal")]
        FutInfo_FutBondNominal,

        /// <summary>
        ///     Сообщение usd_online из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "usd_online")]
        FutInfo_UsdOnline,

        /// <summary>
        ///     Сообщение fut_vcb из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_vcb")]
        FutInfo_FutVcb,

        /// <summary>
        ///     Сообщение session из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "session")]
        FutInfo_Session,

        /// <summary>
        ///     Сообщение multileg_dict из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "multileg_dict")]
        FutInfo_MultilegDict,

        /// <summary>
        ///     Сообщение fut_sess_contents из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_sess_contents")]
        FutInfo_FutSessContents,

        /// <summary>
        ///     Сообщение fut_instruments из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_instruments")]
        FutInfo_FutInstruments,

        /// <summary>
        ///     Сообщение diler из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "diler")]
        FutInfo_Diler,

        /// <summary>
        ///     Сообщение investr из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "investr")]
        FutInfo_Investr,

        /// <summary>
        ///     Сообщение fut_sess_settl из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_sess_settl")]
        FutInfo_FutSessSettl,

        /// <summary>
        ///     Сообщение sys_messages из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "sys_messages")]
        FutInfo_SysMessages,

        /// <summary>
        ///     Сообщение fut_settlement_account из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_settlement_account")]
        FutInfo_FutSettlementAccount,

        /// <summary>
        ///     Сообщение fut_margin_type из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "fut_margin_type")]
        FutInfo_FutMarginType,

        /// <summary>
        ///     Сообщение prohibition из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "prohibition")]
        FutInfo_Prohibition,

        /// <summary>
        ///     Сообщение rates из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "rates")]
        FutInfo_Rates,

        /// <summary>
        ///     Сообщение sys_events из потока fut_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_info", "sys_events")]
        FutInfo_SysEvents,

        /// <summary>
        ///     Сообщение orders_log из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "orders_log")]
        FutTrades_OrdersLog,

        /// <summary>
        ///     Сообщение multileg_orders_log из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "multileg_orders_log")]
        FutTrades_MultilegOrdersLog,

        /// <summary>
        ///     Сообщение deal из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "deal")]
        FutTrades_Deal,

        /// <summary>
        ///     Сообщение multileg_deal из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "multileg_deal")]
        FutTrades_MultilegDeal,

        /// <summary>
        ///     Сообщение heartbeat из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "heartbeat")]
        FutTrades_Heartbeat,

        /// <summary>
        ///     Сообщение sys_events из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "sys_events")]
        FutTrades_SysEvents,

        /// <summary>
        ///     Сообщение user_deal из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "user_deal")]
        FutTrades_UserDeal,

        /// <summary>
        ///     Сообщение user_multileg_deal из потока fut_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades", "user_multileg_deal")]
        FutTrades_UserMultilegDeal,

        /// <summary>
        ///     Сообщение heartbeat из потока fut_trades_heartbeat
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("fut_trades_heartbeat", "heartbeat")]
        FutTradesHeartbeat_Heartbeat,

        /// <summary>
        ///     Сообщение base_contracts_params из потока info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("info", "base_contracts_params")]
        Info_BaseContractsParams,

        /// <summary>
        ///     Сообщение futures_params из потока info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("info", "futures_params")]
        Info_FuturesParams,

        /// <summary>
        ///     Сообщение virtual_futures_params из потока info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("info", "virtual_futures_params")]
        Info_VirtualFuturesParams,

        /// <summary>
        ///     Сообщение options_params из потока info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("info", "options_params")]
        Info_OptionsParams,

        /// <summary>
        ///     Сообщение broker_params из потока info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("info", "broker_params")]
        Info_BrokerParams,

        /// <summary>
        ///     Сообщение client_params из потока info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("info", "client_params")]
        Info_ClientParams,

        /// <summary>
        ///     Сообщение sys_events из потока info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("info", "sys_events")]
        Info_SysEvents,

        /// <summary>
        ///     Сообщение volat_coeff из потока misc_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("misc_info", "volat_coeff")]
        MiscInfo_VolatCoeff,

        /// <summary>
        ///     Сообщение fut_MM_info из потока mm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("mm", "fut_MM_info")]
        Mm_FutMmInfo,

        /// <summary>
        ///     Сообщение opt_MM_info из потока mm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("mm", "opt_MM_info")]
        Mm_OptMmInfo,

        /// <summary>
        ///     Сообщение cs_mm_rule из потока mm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("mm", "cs_mm_rule")]
        Mm_CsMmRule,

        /// <summary>
        ///     Сообщение mm_agreement_filter из потока mm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("mm", "mm_agreement_filter")]
        Mm_MmAgreementFilter,

        /// <summary>
        ///     Сообщение common из потока opt_common
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_common", "common")]
        OptCommon_Common,

        /// <summary>
        ///     Сообщение opt_rejected_orders из потока opt_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_info", "opt_rejected_orders")]
        OptInfo_OptRejectedOrders,

        /// <summary>
        ///     Сообщение opt_intercl_info из потока opt_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_info", "opt_intercl_info")]
        OptInfo_OptInterclInfo,

        /// <summary>
        ///     Сообщение opt_exp_orders из потока opt_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_info", "opt_exp_orders")]
        OptInfo_OptExpOrders,

        /// <summary>
        ///     Сообщение opt_vcb из потока opt_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_info", "opt_vcb")]
        OptInfo_OptVcb,

        /// <summary>
        ///     Сообщение opt_sess_contents из потока opt_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_info", "opt_sess_contents")]
        OptInfo_OptSessContents,

        /// <summary>
        ///     Сообщение opt_sess_settl из потока opt_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_info", "opt_sess_settl")]
        OptInfo_OptSessSettl,

        /// <summary>
        ///     Сообщение sys_events из потока opt_info
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_info", "sys_events")]
        OptInfo_SysEvents,

        /// <summary>
        ///     Сообщение orders_log из потока opt_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_trades", "orders_log")]
        OptTrades_OrdersLog,

        /// <summary>
        ///     Сообщение deal из потока opt_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_trades", "deal")]
        OptTrades_Deal,

        /// <summary>
        ///     Сообщение heartbeat из потока opt_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_trades", "heartbeat")]
        OptTrades_Heartbeat,

        /// <summary>
        ///     Сообщение sys_events из потока opt_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_trades", "sys_events")]
        OptTrades_SysEvents,

        /// <summary>
        ///     Сообщение user_deal из потока opt_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("opt_trades", "user_deal")]
        OptTrades_UserDeal,

        /// <summary>
        ///     Сообщение orders из потока ordbook
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("ordbook", "orders")]
        Ordbook_Orders,

        /// <summary>
        ///     Сообщение info из потока ordbook
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("ordbook", "info")]
        Ordbook_Info,

        /// <summary>
        ///     Сообщение orders из потока orderbook
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("orderbook", "orders")]
        Orderbook_Orders,

        /// <summary>
        ///     Сообщение info из потока orderbook
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("orderbook", "info")]
        Orderbook_Info,

        /// <summary>
        ///     Сообщение orders_aggr из потока orders_aggr
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("orders_aggr", "orders_aggr")]
        OrdersAggr_OrdersAggr,

        /// <summary>
        ///     Сообщение orders_log из потока ordLog_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("ordLog_trades", "orders_log")]
        OrdLogTrades_OrdersLog,

        /// <summary>
        ///     Сообщение multileg_orders_log из потока ordLog_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("ordLog_trades", "multileg_orders_log")]
        OrdLogTrades_MultilegOrdersLog,

        /// <summary>
        ///     Сообщение heartbeat из потока ordLog_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("ordLog_trades", "heartbeat")]
        OrdLogTrades_Heartbeat,

        /// <summary>
        ///     Сообщение sys_events из потока ordLog_trades
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("ordLog_trades", "sys_events")]
        OrdLogTrades_SysEvents,

        /// <summary>
        ///     Сообщение part из потока part
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("part", "part")]
        Part_Part,

        /// <summary>
        ///     Сообщение part_sa из потока part
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("part", "part_sa")]
        Part_PartSa,

        /// <summary>
        ///     Сообщение sys_events из потока part
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("part", "sys_events")]
        Part_SysEvents,

        /// <summary>
        ///     Сообщение position из потока pos
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("pos", "position")]
        Pos_Position,

        /// <summary>
        ///     Сообщение sys_events из потока pos
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("pos", "sys_events")]
        Pos_SysEvents,

        /// <summary>
        ///     Сообщение curr_online из потока rates
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("rates", "curr_online")]
        Rates_CurrOnline,

        /// <summary>
        ///     Сообщение rts_index из потока rts_index
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("rts_index", "rts_index")]
        RtsIndex_RtsIndex,

        /// <summary>
        ///     Сообщение rts_index_log из потока rts_indexlog
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("rts_indexlog", "rts_index_log")]
        RtsIndexlog_RtsIndexLog,

        /// <summary>
        ///     Сообщение fee_all из потока tnpenalty
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("tnpenalty", "fee_all")]
        Tnpenalty_FeeAll,

        /// <summary>
        ///     Сообщение fee_tn из потока tnpenalty
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("tnpenalty", "fee_tn")]
        Tnpenalty_FeeTn,

        /// <summary>
        ///     Сообщение fut_vm из потока vm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("vm", "fut_vm")]
        Vm_FutVm,

        /// <summary>
        ///     Сообщение opt_vm из потока vm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("vm", "opt_vm")]
        Vm_OptVm,

        /// <summary>
        ///     Сообщение fut_vm_sa из потока vm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("vm", "fut_vm_sa")]
        Vm_FutVmSa,

        /// <summary>
        ///     Сообщение opt_vm_sa из потока vm
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("vm", "opt_vm_sa")]
        Vm_OptVmSa,

        /// <summary>
        ///     Сообщение volat из потока volat
        /// </summary>
        // ReSharper disable once InconsistentNaming
        [CGateMessageType("volat", "volat")]
        Volat_Volat,

    }
    
    /// <summary>
    ///     Посетитель для иерархии классов <see cref="CGateMessage" />
    /// </summary>
    [PublicAPI]
	public interface ICGateMessageVisitor
    {
        /// <summary>
        ///     Обработать сообщение типа <see cref="StreamStateChange"/>
        /// </summary>
        void Handle([NotNull] StreamStateChange message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateOrder"/>
        /// </summary>
        void Handle([NotNull] CGateOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDelOrderReply"/>
        /// </summary>
        void Handle([NotNull] CGateDelOrderReply message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDeal"/>
        /// </summary>
        void Handle([NotNull] CGateDeal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDataEnd"/>
        /// </summary>
        void Handle([NotNull] CGateDataEnd message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDataBegin"/>
        /// </summary>
        void Handle([NotNull] CGateDataBegin message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateAddOrderReply"/>
        /// </summary>
        void Handle([NotNull] CGateAddOrderReply message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGConnectionStateChange"/>
        /// </summary>
        void Handle([NotNull] CGConnectionStateChange message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateClearTableMessage"/>
        /// </summary>
        void Handle([NotNull] CGateClearTableMessage message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmMoneyClearing"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmMoneyClearing message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmMoneyClearingSa"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmMoneyClearingSa message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmClrRate"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmClrRate message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmFutPos"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmFutPos message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmOptPos"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmOptPos message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmFutPosSa"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmFutPosSa message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmOptPosSa"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmOptPosSa message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmFutSessSettl"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmFutSessSettl message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmOptSessSettl"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmOptSessSettl message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmPledgeDetails"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmPledgeDetails message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] Clr.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutAddOrder"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutAddOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg101"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg101 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutAddMultiLegOrder"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutAddMultiLegOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg129"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg129 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutDelOrder"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutDelOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg102"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg102 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutDelUserOrders"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutDelUserOrders message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg103"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg103 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutMoveOrder"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutMoveOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg105"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg105 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptAddOrder"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptAddOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg109"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg109 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptDelOrder"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptDelOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg110"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg110 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptDelUserOrders"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptDelUserOrders message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg111"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg111 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptMoveOrder"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptMoveOrder message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg113"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg113 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutChangeClientMoney"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutChangeClientMoney message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg104"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg104 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutChangeBfmoney"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutChangeBfmoney message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg107"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg107 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptChangeExpiration"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptChangeExpiration message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg112"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg112 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutChangeClientProhibit"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutChangeClientProhibit message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg115"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg115 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptChangeClientProhibit"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptChangeClientProhibit message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg117"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg117 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutExchangeBfmoney"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutExchangeBfmoney message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg130"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg130 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptRecalcCs"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptRecalcCs message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg132"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg132 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutTransferClientPosition"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutTransferClientPosition message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg137"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg137 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptTransferClientPosition"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptTransferClientPosition message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg138"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg138 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptChangeRiskParameters"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmOptChangeRiskParameters message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg140"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg140 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutTransferRisk"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFutTransferRisk message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg139"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg139 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmCodheartbeat"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmCodheartbeat message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg99"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg99 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg100"/>
        /// </summary>
        void Handle([NotNull] FortsMessages.CgmFortsMsg100 message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutCommon.CgmCommon"/>
        /// </summary>
        void Handle([NotNull] FutCommon.CgmCommon message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmDeliveryReport"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmDeliveryReport message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutRejectedOrders"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutRejectedOrders message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutInterclInfo"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutInterclInfo message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondRegistry"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutBondRegistry message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondIsin"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutBondIsin message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondNkd"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutBondNkd message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondNominal"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutBondNominal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmUsdOnline"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmUsdOnline message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutVcb"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutVcb message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmSession"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmSession message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmMultilegDict"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmMultilegDict message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutSessContents"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutSessContents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutInstruments"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutInstruments message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmDiler"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmDiler message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmInvestr"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmInvestr message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutSessSettl"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutSessSettl message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmSysMessages"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmSysMessages message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutSettlementAccount"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutSettlementAccount message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutMarginType"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmFutMarginType message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmProhibition"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmProhibition message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmRates"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmRates message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] FutInfo.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmOrdersLog"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmOrdersLog message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmMultilegOrdersLog"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmMultilegOrdersLog message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmDeal"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmDeal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmMultilegDeal"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmMultilegDeal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmHeartbeat"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmHeartbeat message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmUserDeal"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmUserDeal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmUserMultilegDeal"/>
        /// </summary>
        void Handle([NotNull] FutTrades.CgmUserMultilegDeal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTradesHeartbeat.CgmHeartbeat"/>
        /// </summary>
        void Handle([NotNull] FutTradesHeartbeat.CgmHeartbeat message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmBaseContractsParams"/>
        /// </summary>
        void Handle([NotNull] Info.CgmBaseContractsParams message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmFuturesParams"/>
        /// </summary>
        void Handle([NotNull] Info.CgmFuturesParams message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmVirtualFuturesParams"/>
        /// </summary>
        void Handle([NotNull] Info.CgmVirtualFuturesParams message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmOptionsParams"/>
        /// </summary>
        void Handle([NotNull] Info.CgmOptionsParams message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmBrokerParams"/>
        /// </summary>
        void Handle([NotNull] Info.CgmBrokerParams message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmClientParams"/>
        /// </summary>
        void Handle([NotNull] Info.CgmClientParams message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] Info.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="MiscInfo.CgmVolatCoeff"/>
        /// </summary>
        void Handle([NotNull] MiscInfo.CgmVolatCoeff message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmFutMmInfo"/>
        /// </summary>
        void Handle([NotNull] Mm.CgmFutMmInfo message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmOptMmInfo"/>
        /// </summary>
        void Handle([NotNull] Mm.CgmOptMmInfo message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmCsMmRule"/>
        /// </summary>
        void Handle([NotNull] Mm.CgmCsMmRule message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmMmAgreementFilter"/>
        /// </summary>
        void Handle([NotNull] Mm.CgmMmAgreementFilter message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptCommon.CgmCommon"/>
        /// </summary>
        void Handle([NotNull] OptCommon.CgmCommon message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptRejectedOrders"/>
        /// </summary>
        void Handle([NotNull] OptInfo.CgmOptRejectedOrders message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptInterclInfo"/>
        /// </summary>
        void Handle([NotNull] OptInfo.CgmOptInterclInfo message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptExpOrders"/>
        /// </summary>
        void Handle([NotNull] OptInfo.CgmOptExpOrders message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptVcb"/>
        /// </summary>
        void Handle([NotNull] OptInfo.CgmOptVcb message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptSessContents"/>
        /// </summary>
        void Handle([NotNull] OptInfo.CgmOptSessContents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptSessSettl"/>
        /// </summary>
        void Handle([NotNull] OptInfo.CgmOptSessSettl message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] OptInfo.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmOrdersLog"/>
        /// </summary>
        void Handle([NotNull] OptTrades.CgmOrdersLog message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmDeal"/>
        /// </summary>
        void Handle([NotNull] OptTrades.CgmDeal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmHeartbeat"/>
        /// </summary>
        void Handle([NotNull] OptTrades.CgmHeartbeat message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] OptTrades.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmUserDeal"/>
        /// </summary>
        void Handle([NotNull] OptTrades.CgmUserDeal message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Ordbook.CgmOrders"/>
        /// </summary>
        void Handle([NotNull] Ordbook.CgmOrders message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Ordbook.CgmInfo"/>
        /// </summary>
        void Handle([NotNull] Ordbook.CgmInfo message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Orderbook.CgmOrders"/>
        /// </summary>
        void Handle([NotNull] Orderbook.CgmOrders message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Orderbook.CgmInfo"/>
        /// </summary>
        void Handle([NotNull] Orderbook.CgmInfo message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdersAggr.CgmOrdersAggr"/>
        /// </summary>
        void Handle([NotNull] OrdersAggr.CgmOrdersAggr message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmOrdersLog"/>
        /// </summary>
        void Handle([NotNull] OrdLogTrades.CgmOrdersLog message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmMultilegOrdersLog"/>
        /// </summary>
        void Handle([NotNull] OrdLogTrades.CgmMultilegOrdersLog message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmHeartbeat"/>
        /// </summary>
        void Handle([NotNull] OrdLogTrades.CgmHeartbeat message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] OrdLogTrades.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Part.CgmPart"/>
        /// </summary>
        void Handle([NotNull] Part.CgmPart message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Part.CgmPartSa"/>
        /// </summary>
        void Handle([NotNull] Part.CgmPartSa message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Part.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] Part.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Pos.CgmPosition"/>
        /// </summary>
        void Handle([NotNull] Pos.CgmPosition message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Pos.CgmSysEvents"/>
        /// </summary>
        void Handle([NotNull] Pos.CgmSysEvents message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Rates.CgmCurrOnline"/>
        /// </summary>
        void Handle([NotNull] Rates.CgmCurrOnline message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="RtsIndex.CgmRtsIndex"/>
        /// </summary>
        void Handle([NotNull] RtsIndex.CgmRtsIndex message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="RtsIndexlog.CgmRtsIndexLog"/>
        /// </summary>
        void Handle([NotNull] RtsIndexlog.CgmRtsIndexLog message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Tnpenalty.CgmFeeAll"/>
        /// </summary>
        void Handle([NotNull] Tnpenalty.CgmFeeAll message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Tnpenalty.CgmFeeTn"/>
        /// </summary>
        void Handle([NotNull] Tnpenalty.CgmFeeTn message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmFutVm"/>
        /// </summary>
        void Handle([NotNull] Vm.CgmFutVm message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmOptVm"/>
        /// </summary>
        void Handle([NotNull] Vm.CgmOptVm message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmFutVmSa"/>
        /// </summary>
        void Handle([NotNull] Vm.CgmFutVmSa message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmOptVmSa"/>
        /// </summary>
        void Handle([NotNull] Vm.CgmOptVmSa message);

        /// <summary>
        ///     Обработать сообщение типа <see cref="Volat.CgmVolat"/>
        /// </summary>
        void Handle([NotNull] Volat.CgmVolat message);

    }

    /// <summary>
    ///     Посетитель для иерархии классов <see cref="CGateMessage" />
    /// </summary>
    [PublicAPI]
	public abstract class CGateMessageVisitor : ICGateMessageVisitor
    {
        /// <summary>
        ///     Обработчик сообщений по умолчанию
        /// </summary>
        public virtual void HandleDefault(CGateMessage message) { }

        /// <summary>
        ///     Обработать сообщение типа <see cref="StreamStateChange"/>
        /// </summary>
        public virtual void Handle(StreamStateChange message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateOrder"/>
        /// </summary>
        public virtual void Handle(CGateOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDelOrderReply"/>
        /// </summary>
        public virtual void Handle(CGateDelOrderReply message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDeal"/>
        /// </summary>
        public virtual void Handle(CGateDeal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDataEnd"/>
        /// </summary>
        public virtual void Handle(CGateDataEnd message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateDataBegin"/>
        /// </summary>
        public virtual void Handle(CGateDataBegin message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateAddOrderReply"/>
        /// </summary>
        public virtual void Handle(CGateAddOrderReply message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGConnectionStateChange"/>
        /// </summary>
        public virtual void Handle(CGConnectionStateChange message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="CGateClearTableMessage"/>
        /// </summary>
        public virtual void Handle(CGateClearTableMessage message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmMoneyClearing"/>
        /// </summary>
        public virtual void Handle(Clr.CgmMoneyClearing message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmMoneyClearingSa"/>
        /// </summary>
        public virtual void Handle(Clr.CgmMoneyClearingSa message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmClrRate"/>
        /// </summary>
        public virtual void Handle(Clr.CgmClrRate message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmFutPos"/>
        /// </summary>
        public virtual void Handle(Clr.CgmFutPos message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmOptPos"/>
        /// </summary>
        public virtual void Handle(Clr.CgmOptPos message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmFutPosSa"/>
        /// </summary>
        public virtual void Handle(Clr.CgmFutPosSa message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmOptPosSa"/>
        /// </summary>
        public virtual void Handle(Clr.CgmOptPosSa message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmFutSessSettl"/>
        /// </summary>
        public virtual void Handle(Clr.CgmFutSessSettl message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmOptSessSettl"/>
        /// </summary>
        public virtual void Handle(Clr.CgmOptSessSettl message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmPledgeDetails"/>
        /// </summary>
        public virtual void Handle(Clr.CgmPledgeDetails message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Clr.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(Clr.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutAddOrder"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutAddOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg101"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg101 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutAddMultiLegOrder"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutAddMultiLegOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg129"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg129 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutDelOrder"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutDelOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg102"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg102 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutDelUserOrders"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutDelUserOrders message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg103"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg103 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutMoveOrder"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutMoveOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg105"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg105 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptAddOrder"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptAddOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg109"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg109 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptDelOrder"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptDelOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg110"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg110 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptDelUserOrders"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptDelUserOrders message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg111"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg111 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptMoveOrder"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptMoveOrder message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg113"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg113 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutChangeClientMoney"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutChangeClientMoney message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg104"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg104 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutChangeBfmoney"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutChangeBfmoney message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg107"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg107 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptChangeExpiration"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptChangeExpiration message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg112"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg112 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutChangeClientProhibit"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutChangeClientProhibit message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg115"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg115 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptChangeClientProhibit"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptChangeClientProhibit message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg117"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg117 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutExchangeBfmoney"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutExchangeBfmoney message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg130"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg130 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptRecalcCs"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptRecalcCs message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg132"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg132 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutTransferClientPosition"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutTransferClientPosition message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg137"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg137 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptTransferClientPosition"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptTransferClientPosition message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg138"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg138 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmOptChangeRiskParameters"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmOptChangeRiskParameters message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg140"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg140 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFutTransferRisk"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFutTransferRisk message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg139"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg139 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmCodheartbeat"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmCodheartbeat message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg99"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg99 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FortsMessages.CgmFortsMsg100"/>
        /// </summary>
        public virtual void Handle(FortsMessages.CgmFortsMsg100 message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutCommon.CgmCommon"/>
        /// </summary>
        public virtual void Handle(FutCommon.CgmCommon message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmDeliveryReport"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmDeliveryReport message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutRejectedOrders"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutRejectedOrders message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutInterclInfo"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutInterclInfo message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondRegistry"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutBondRegistry message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondIsin"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutBondIsin message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondNkd"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutBondNkd message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutBondNominal"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutBondNominal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmUsdOnline"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmUsdOnline message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutVcb"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutVcb message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmSession"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmSession message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmMultilegDict"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmMultilegDict message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutSessContents"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutSessContents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutInstruments"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutInstruments message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmDiler"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmDiler message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmInvestr"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmInvestr message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutSessSettl"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutSessSettl message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmSysMessages"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmSysMessages message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutSettlementAccount"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutSettlementAccount message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmFutMarginType"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmFutMarginType message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmProhibition"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmProhibition message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmRates"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmRates message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutInfo.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(FutInfo.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmOrdersLog"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmOrdersLog message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmMultilegOrdersLog"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmMultilegOrdersLog message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmDeal"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmDeal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmMultilegDeal"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmMultilegDeal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmHeartbeat"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmHeartbeat message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmUserDeal"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmUserDeal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTrades.CgmUserMultilegDeal"/>
        /// </summary>
        public virtual void Handle(FutTrades.CgmUserMultilegDeal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="FutTradesHeartbeat.CgmHeartbeat"/>
        /// </summary>
        public virtual void Handle(FutTradesHeartbeat.CgmHeartbeat message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmBaseContractsParams"/>
        /// </summary>
        public virtual void Handle(Info.CgmBaseContractsParams message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmFuturesParams"/>
        /// </summary>
        public virtual void Handle(Info.CgmFuturesParams message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmVirtualFuturesParams"/>
        /// </summary>
        public virtual void Handle(Info.CgmVirtualFuturesParams message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmOptionsParams"/>
        /// </summary>
        public virtual void Handle(Info.CgmOptionsParams message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmBrokerParams"/>
        /// </summary>
        public virtual void Handle(Info.CgmBrokerParams message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmClientParams"/>
        /// </summary>
        public virtual void Handle(Info.CgmClientParams message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Info.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(Info.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="MiscInfo.CgmVolatCoeff"/>
        /// </summary>
        public virtual void Handle(MiscInfo.CgmVolatCoeff message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmFutMmInfo"/>
        /// </summary>
        public virtual void Handle(Mm.CgmFutMmInfo message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmOptMmInfo"/>
        /// </summary>
        public virtual void Handle(Mm.CgmOptMmInfo message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmCsMmRule"/>
        /// </summary>
        public virtual void Handle(Mm.CgmCsMmRule message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Mm.CgmMmAgreementFilter"/>
        /// </summary>
        public virtual void Handle(Mm.CgmMmAgreementFilter message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptCommon.CgmCommon"/>
        /// </summary>
        public virtual void Handle(OptCommon.CgmCommon message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptRejectedOrders"/>
        /// </summary>
        public virtual void Handle(OptInfo.CgmOptRejectedOrders message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptInterclInfo"/>
        /// </summary>
        public virtual void Handle(OptInfo.CgmOptInterclInfo message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptExpOrders"/>
        /// </summary>
        public virtual void Handle(OptInfo.CgmOptExpOrders message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptVcb"/>
        /// </summary>
        public virtual void Handle(OptInfo.CgmOptVcb message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptSessContents"/>
        /// </summary>
        public virtual void Handle(OptInfo.CgmOptSessContents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmOptSessSettl"/>
        /// </summary>
        public virtual void Handle(OptInfo.CgmOptSessSettl message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptInfo.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(OptInfo.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmOrdersLog"/>
        /// </summary>
        public virtual void Handle(OptTrades.CgmOrdersLog message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmDeal"/>
        /// </summary>
        public virtual void Handle(OptTrades.CgmDeal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmHeartbeat"/>
        /// </summary>
        public virtual void Handle(OptTrades.CgmHeartbeat message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(OptTrades.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OptTrades.CgmUserDeal"/>
        /// </summary>
        public virtual void Handle(OptTrades.CgmUserDeal message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Ordbook.CgmOrders"/>
        /// </summary>
        public virtual void Handle(Ordbook.CgmOrders message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Ordbook.CgmInfo"/>
        /// </summary>
        public virtual void Handle(Ordbook.CgmInfo message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Orderbook.CgmOrders"/>
        /// </summary>
        public virtual void Handle(Orderbook.CgmOrders message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Orderbook.CgmInfo"/>
        /// </summary>
        public virtual void Handle(Orderbook.CgmInfo message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdersAggr.CgmOrdersAggr"/>
        /// </summary>
        public virtual void Handle(OrdersAggr.CgmOrdersAggr message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmOrdersLog"/>
        /// </summary>
        public virtual void Handle(OrdLogTrades.CgmOrdersLog message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmMultilegOrdersLog"/>
        /// </summary>
        public virtual void Handle(OrdLogTrades.CgmMultilegOrdersLog message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmHeartbeat"/>
        /// </summary>
        public virtual void Handle(OrdLogTrades.CgmHeartbeat message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="OrdLogTrades.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(OrdLogTrades.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Part.CgmPart"/>
        /// </summary>
        public virtual void Handle(Part.CgmPart message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Part.CgmPartSa"/>
        /// </summary>
        public virtual void Handle(Part.CgmPartSa message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Part.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(Part.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Pos.CgmPosition"/>
        /// </summary>
        public virtual void Handle(Pos.CgmPosition message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Pos.CgmSysEvents"/>
        /// </summary>
        public virtual void Handle(Pos.CgmSysEvents message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Rates.CgmCurrOnline"/>
        /// </summary>
        public virtual void Handle(Rates.CgmCurrOnline message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="RtsIndex.CgmRtsIndex"/>
        /// </summary>
        public virtual void Handle(RtsIndex.CgmRtsIndex message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="RtsIndexlog.CgmRtsIndexLog"/>
        /// </summary>
        public virtual void Handle(RtsIndexlog.CgmRtsIndexLog message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Tnpenalty.CgmFeeAll"/>
        /// </summary>
        public virtual void Handle(Tnpenalty.CgmFeeAll message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Tnpenalty.CgmFeeTn"/>
        /// </summary>
        public virtual void Handle(Tnpenalty.CgmFeeTn message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmFutVm"/>
        /// </summary>
        public virtual void Handle(Vm.CgmFutVm message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmOptVm"/>
        /// </summary>
        public virtual void Handle(Vm.CgmOptVm message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmFutVmSa"/>
        /// </summary>
        public virtual void Handle(Vm.CgmFutVmSa message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Vm.CgmOptVmSa"/>
        /// </summary>
        public virtual void Handle(Vm.CgmOptVmSa message)
        { 
            HandleDefault(message);
        }

        /// <summary>
        ///     Обработать сообщение типа <see cref="Volat.CgmVolat"/>
        /// </summary>
        public virtual void Handle(Volat.CgmVolat message)
        { 
            HandleDefault(message);
        }

    }
}

namespace CGateAdapter.Messages
{
    // Системные сообщения
    [ProtoInclude(1001, typeof(CGateAdapter.Messages.StreamStateChange))]
    [ProtoInclude(1002, typeof(CGateAdapter.Messages.CGateOrder))]
    [ProtoInclude(1003, typeof(CGateAdapter.Messages.CGateDelOrderReply))]
    [ProtoInclude(1004, typeof(CGateAdapter.Messages.CGateDeal))]
    [ProtoInclude(1005, typeof(CGateAdapter.Messages.CGateDataEnd))]
    [ProtoInclude(1006, typeof(CGateAdapter.Messages.CGateDataBegin))]
    [ProtoInclude(1007, typeof(CGateAdapter.Messages.CGateAddOrderReply))]
    [ProtoInclude(1008, typeof(CGateAdapter.Messages.CGConnectionStateChange))]
    [ProtoInclude(1009, typeof(CGateAdapter.Messages.CGateClearTableMessage))]
    // Сообщения CGate
    [ProtoInclude(1101, typeof(CGateAdapter.Messages.Clr.CgmMoneyClearing))]
    [ProtoInclude(1102, typeof(CGateAdapter.Messages.Clr.CgmMoneyClearingSa))]
    [ProtoInclude(1103, typeof(CGateAdapter.Messages.Clr.CgmClrRate))]
    [ProtoInclude(1104, typeof(CGateAdapter.Messages.Clr.CgmFutPos))]
    [ProtoInclude(1105, typeof(CGateAdapter.Messages.Clr.CgmOptPos))]
    [ProtoInclude(1106, typeof(CGateAdapter.Messages.Clr.CgmFutPosSa))]
    [ProtoInclude(1107, typeof(CGateAdapter.Messages.Clr.CgmOptPosSa))]
    [ProtoInclude(1108, typeof(CGateAdapter.Messages.Clr.CgmFutSessSettl))]
    [ProtoInclude(1109, typeof(CGateAdapter.Messages.Clr.CgmOptSessSettl))]
    [ProtoInclude(1110, typeof(CGateAdapter.Messages.Clr.CgmPledgeDetails))]
    [ProtoInclude(1111, typeof(CGateAdapter.Messages.Clr.CgmSysEvents))]
    [ProtoInclude(1112, typeof(CGateAdapter.Messages.FortsMessages.CgmFutAddOrder))]
    [ProtoInclude(1113, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg101))]
    [ProtoInclude(1114, typeof(CGateAdapter.Messages.FortsMessages.CgmFutAddMultiLegOrder))]
    [ProtoInclude(1115, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg129))]
    [ProtoInclude(1116, typeof(CGateAdapter.Messages.FortsMessages.CgmFutDelOrder))]
    [ProtoInclude(1117, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg102))]
    [ProtoInclude(1118, typeof(CGateAdapter.Messages.FortsMessages.CgmFutDelUserOrders))]
    [ProtoInclude(1119, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg103))]
    [ProtoInclude(1120, typeof(CGateAdapter.Messages.FortsMessages.CgmFutMoveOrder))]
    [ProtoInclude(1121, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg105))]
    [ProtoInclude(1122, typeof(CGateAdapter.Messages.FortsMessages.CgmOptAddOrder))]
    [ProtoInclude(1123, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg109))]
    [ProtoInclude(1124, typeof(CGateAdapter.Messages.FortsMessages.CgmOptDelOrder))]
    [ProtoInclude(1125, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg110))]
    [ProtoInclude(1126, typeof(CGateAdapter.Messages.FortsMessages.CgmOptDelUserOrders))]
    [ProtoInclude(1127, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg111))]
    [ProtoInclude(1128, typeof(CGateAdapter.Messages.FortsMessages.CgmOptMoveOrder))]
    [ProtoInclude(1129, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg113))]
    [ProtoInclude(1130, typeof(CGateAdapter.Messages.FortsMessages.CgmFutChangeClientMoney))]
    [ProtoInclude(1131, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg104))]
    [ProtoInclude(1132, typeof(CGateAdapter.Messages.FortsMessages.CgmFutChangeBfmoney))]
    [ProtoInclude(1133, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg107))]
    [ProtoInclude(1134, typeof(CGateAdapter.Messages.FortsMessages.CgmOptChangeExpiration))]
    [ProtoInclude(1135, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg112))]
    [ProtoInclude(1136, typeof(CGateAdapter.Messages.FortsMessages.CgmFutChangeClientProhibit))]
    [ProtoInclude(1137, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg115))]
    [ProtoInclude(1138, typeof(CGateAdapter.Messages.FortsMessages.CgmOptChangeClientProhibit))]
    [ProtoInclude(1139, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg117))]
    [ProtoInclude(1140, typeof(CGateAdapter.Messages.FortsMessages.CgmFutExchangeBfmoney))]
    [ProtoInclude(1141, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg130))]
    [ProtoInclude(1142, typeof(CGateAdapter.Messages.FortsMessages.CgmOptRecalcCs))]
    [ProtoInclude(1143, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg132))]
    [ProtoInclude(1144, typeof(CGateAdapter.Messages.FortsMessages.CgmFutTransferClientPosition))]
    [ProtoInclude(1145, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg137))]
    [ProtoInclude(1146, typeof(CGateAdapter.Messages.FortsMessages.CgmOptTransferClientPosition))]
    [ProtoInclude(1147, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg138))]
    [ProtoInclude(1148, typeof(CGateAdapter.Messages.FortsMessages.CgmOptChangeRiskParameters))]
    [ProtoInclude(1149, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg140))]
    [ProtoInclude(1150, typeof(CGateAdapter.Messages.FortsMessages.CgmFutTransferRisk))]
    [ProtoInclude(1151, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg139))]
    [ProtoInclude(1152, typeof(CGateAdapter.Messages.FortsMessages.CgmCodheartbeat))]
    [ProtoInclude(1153, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg99))]
    [ProtoInclude(1154, typeof(CGateAdapter.Messages.FortsMessages.CgmFortsMsg100))]
    [ProtoInclude(1155, typeof(CGateAdapter.Messages.FutCommon.CgmCommon))]
    [ProtoInclude(1156, typeof(CGateAdapter.Messages.FutInfo.CgmDeliveryReport))]
    [ProtoInclude(1157, typeof(CGateAdapter.Messages.FutInfo.CgmFutRejectedOrders))]
    [ProtoInclude(1158, typeof(CGateAdapter.Messages.FutInfo.CgmFutInterclInfo))]
    [ProtoInclude(1159, typeof(CGateAdapter.Messages.FutInfo.CgmFutBondRegistry))]
    [ProtoInclude(1160, typeof(CGateAdapter.Messages.FutInfo.CgmFutBondIsin))]
    [ProtoInclude(1161, typeof(CGateAdapter.Messages.FutInfo.CgmFutBondNkd))]
    [ProtoInclude(1162, typeof(CGateAdapter.Messages.FutInfo.CgmFutBondNominal))]
    [ProtoInclude(1163, typeof(CGateAdapter.Messages.FutInfo.CgmUsdOnline))]
    [ProtoInclude(1164, typeof(CGateAdapter.Messages.FutInfo.CgmFutVcb))]
    [ProtoInclude(1165, typeof(CGateAdapter.Messages.FutInfo.CgmSession))]
    [ProtoInclude(1166, typeof(CGateAdapter.Messages.FutInfo.CgmMultilegDict))]
    [ProtoInclude(1167, typeof(CGateAdapter.Messages.FutInfo.CgmFutSessContents))]
    [ProtoInclude(1168, typeof(CGateAdapter.Messages.FutInfo.CgmFutInstruments))]
    [ProtoInclude(1169, typeof(CGateAdapter.Messages.FutInfo.CgmDiler))]
    [ProtoInclude(1170, typeof(CGateAdapter.Messages.FutInfo.CgmInvestr))]
    [ProtoInclude(1171, typeof(CGateAdapter.Messages.FutInfo.CgmFutSessSettl))]
    [ProtoInclude(1172, typeof(CGateAdapter.Messages.FutInfo.CgmSysMessages))]
    [ProtoInclude(1173, typeof(CGateAdapter.Messages.FutInfo.CgmFutSettlementAccount))]
    [ProtoInclude(1174, typeof(CGateAdapter.Messages.FutInfo.CgmFutMarginType))]
    [ProtoInclude(1175, typeof(CGateAdapter.Messages.FutInfo.CgmProhibition))]
    [ProtoInclude(1176, typeof(CGateAdapter.Messages.FutInfo.CgmRates))]
    [ProtoInclude(1177, typeof(CGateAdapter.Messages.FutInfo.CgmSysEvents))]
    [ProtoInclude(1178, typeof(CGateAdapter.Messages.FutTrades.CgmOrdersLog))]
    [ProtoInclude(1179, typeof(CGateAdapter.Messages.FutTrades.CgmMultilegOrdersLog))]
    [ProtoInclude(1180, typeof(CGateAdapter.Messages.FutTrades.CgmDeal))]
    [ProtoInclude(1181, typeof(CGateAdapter.Messages.FutTrades.CgmMultilegDeal))]
    [ProtoInclude(1182, typeof(CGateAdapter.Messages.FutTrades.CgmHeartbeat))]
    [ProtoInclude(1183, typeof(CGateAdapter.Messages.FutTrades.CgmSysEvents))]
    [ProtoInclude(1184, typeof(CGateAdapter.Messages.FutTrades.CgmUserDeal))]
    [ProtoInclude(1185, typeof(CGateAdapter.Messages.FutTrades.CgmUserMultilegDeal))]
    [ProtoInclude(1186, typeof(CGateAdapter.Messages.FutTradesHeartbeat.CgmHeartbeat))]
    [ProtoInclude(1187, typeof(CGateAdapter.Messages.Info.CgmBaseContractsParams))]
    [ProtoInclude(1188, typeof(CGateAdapter.Messages.Info.CgmFuturesParams))]
    [ProtoInclude(1189, typeof(CGateAdapter.Messages.Info.CgmVirtualFuturesParams))]
    [ProtoInclude(1190, typeof(CGateAdapter.Messages.Info.CgmOptionsParams))]
    [ProtoInclude(1191, typeof(CGateAdapter.Messages.Info.CgmBrokerParams))]
    [ProtoInclude(1192, typeof(CGateAdapter.Messages.Info.CgmClientParams))]
    [ProtoInclude(1193, typeof(CGateAdapter.Messages.Info.CgmSysEvents))]
    [ProtoInclude(1194, typeof(CGateAdapter.Messages.MiscInfo.CgmVolatCoeff))]
    [ProtoInclude(1195, typeof(CGateAdapter.Messages.Mm.CgmFutMmInfo))]
    [ProtoInclude(1196, typeof(CGateAdapter.Messages.Mm.CgmOptMmInfo))]
    [ProtoInclude(1197, typeof(CGateAdapter.Messages.Mm.CgmCsMmRule))]
    [ProtoInclude(1198, typeof(CGateAdapter.Messages.Mm.CgmMmAgreementFilter))]
    [ProtoInclude(1199, typeof(CGateAdapter.Messages.OptCommon.CgmCommon))]
    [ProtoInclude(1200, typeof(CGateAdapter.Messages.OptInfo.CgmOptRejectedOrders))]
    [ProtoInclude(1201, typeof(CGateAdapter.Messages.OptInfo.CgmOptInterclInfo))]
    [ProtoInclude(1202, typeof(CGateAdapter.Messages.OptInfo.CgmOptExpOrders))]
    [ProtoInclude(1203, typeof(CGateAdapter.Messages.OptInfo.CgmOptVcb))]
    [ProtoInclude(1204, typeof(CGateAdapter.Messages.OptInfo.CgmOptSessContents))]
    [ProtoInclude(1205, typeof(CGateAdapter.Messages.OptInfo.CgmOptSessSettl))]
    [ProtoInclude(1206, typeof(CGateAdapter.Messages.OptInfo.CgmSysEvents))]
    [ProtoInclude(1207, typeof(CGateAdapter.Messages.OptTrades.CgmOrdersLog))]
    [ProtoInclude(1208, typeof(CGateAdapter.Messages.OptTrades.CgmDeal))]
    [ProtoInclude(1209, typeof(CGateAdapter.Messages.OptTrades.CgmHeartbeat))]
    [ProtoInclude(1210, typeof(CGateAdapter.Messages.OptTrades.CgmSysEvents))]
    [ProtoInclude(1211, typeof(CGateAdapter.Messages.OptTrades.CgmUserDeal))]
    [ProtoInclude(1212, typeof(CGateAdapter.Messages.Ordbook.CgmOrders))]
    [ProtoInclude(1213, typeof(CGateAdapter.Messages.Ordbook.CgmInfo))]
    [ProtoInclude(1214, typeof(CGateAdapter.Messages.Orderbook.CgmOrders))]
    [ProtoInclude(1215, typeof(CGateAdapter.Messages.Orderbook.CgmInfo))]
    [ProtoInclude(1216, typeof(CGateAdapter.Messages.OrdersAggr.CgmOrdersAggr))]
    [ProtoInclude(1217, typeof(CGateAdapter.Messages.OrdLogTrades.CgmOrdersLog))]
    [ProtoInclude(1218, typeof(CGateAdapter.Messages.OrdLogTrades.CgmMultilegOrdersLog))]
    [ProtoInclude(1219, typeof(CGateAdapter.Messages.OrdLogTrades.CgmHeartbeat))]
    [ProtoInclude(1220, typeof(CGateAdapter.Messages.OrdLogTrades.CgmSysEvents))]
    [ProtoInclude(1221, typeof(CGateAdapter.Messages.Part.CgmPart))]
    [ProtoInclude(1222, typeof(CGateAdapter.Messages.Part.CgmPartSa))]
    [ProtoInclude(1223, typeof(CGateAdapter.Messages.Part.CgmSysEvents))]
    [ProtoInclude(1224, typeof(CGateAdapter.Messages.Pos.CgmPosition))]
    [ProtoInclude(1225, typeof(CGateAdapter.Messages.Pos.CgmSysEvents))]
    [ProtoInclude(1226, typeof(CGateAdapter.Messages.Rates.CgmCurrOnline))]
    [ProtoInclude(1227, typeof(CGateAdapter.Messages.RtsIndex.CgmRtsIndex))]
    [ProtoInclude(1228, typeof(CGateAdapter.Messages.RtsIndexlog.CgmRtsIndexLog))]
    [ProtoInclude(1229, typeof(CGateAdapter.Messages.Tnpenalty.CgmFeeAll))]
    [ProtoInclude(1230, typeof(CGateAdapter.Messages.Tnpenalty.CgmFeeTn))]
    [ProtoInclude(1231, typeof(CGateAdapter.Messages.Vm.CgmFutVm))]
    [ProtoInclude(1232, typeof(CGateAdapter.Messages.Vm.CgmOptVm))]
    [ProtoInclude(1233, typeof(CGateAdapter.Messages.Vm.CgmFutVmSa))]
    [ProtoInclude(1234, typeof(CGateAdapter.Messages.Vm.CgmOptVmSa))]
    [ProtoInclude(1235, typeof(CGateAdapter.Messages.Volat.CgmVolat))]
    partial class CGateMessage { }
}

