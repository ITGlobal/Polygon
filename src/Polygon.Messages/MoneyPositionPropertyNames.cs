using JetBrains.Annotations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Polygon.Messages
{
    /// <summary>
    ///     Константы с именами параметров <see cref="MoneyPosition"/>
    /// </summary>
    [PublicAPI]
    public static class MoneyPositionPropertyNames
    {
        #region General

        public const string CurrentPurePosition = "CURRENT_PURE_POSITION";
        public const string AccumulatedProfit = "ACCUMULATED_PROFIT";
        public const string OpenLimit = "OPEN_LIMIT";
        public const string PlannedPurePosition = "PLANNED_PURE_POSITION";
        public const string VariationMargin = "VARIATION_MARGIN";
        public const string Commission = "COMMISSION";

        public const string ProfitLoss = "PROFIT_LOSS";
        public const string RealizedProfitLoss = "REALIZED_PROFIT_LOSS";
        public const string UnrealizedProfitLoss = "UNREALIZED_PROFIT_LOSS"; 

        #endregion

        #region CQG-specific

        public const string AccountBalance = "ACCOUNT_BALANCE";
        public const string OteAndMvo = "OTE_AND_MVO";
        public const string OteAndMvoAndPl = "OTE_AND_MVO_AND_PL";
        public const string PreviousDayBalance = "PREVIOUS_DAY_BALANCE";
        public const string CollateralOnDeposit = "COLLATERAL_ON_DEPOSIT";
        public const string NetLiquidityValue = "NET_LIQUIDITY_VALUE";
        public const string MarketValueOfOptions = "MARKET_VALUE_OF_OPTIONS";

        #endregion

        #region CQGC-specific

        public const string Ote = "OTE";
        public const string Mvo = "MVO";
        public const string PurchasingPower = "PURCHASING_POWER";
        public const string TotalMargin = "TOTAL_MARGIN";
        public const string Currency = "CURRENCY";
        
        #endregion

        #region IB-specific

        public const string TotalCashBalance = "TOTAL_CASH_BALANCE";
        public const string FullInitMarginReq = "FULL_INIT_MARGIN_REQ";
        public const string FullMaintMarginReq = "FULL_MAINT_MARGIN_REQ";

        #endregion
    }
}

