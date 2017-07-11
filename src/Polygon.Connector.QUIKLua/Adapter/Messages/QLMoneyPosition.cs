using Polygon.Diagnostics;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLMoneyPosition)]
    internal class QLMoneyPosition : QLMessage
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public string firmid	{get;set;} //	

        /// <summary>
        /// Торговый счет
        /// </summary>
        public string trdaccid { get; set; } //	

        /// <summary>
        /// Лимит открытых позиций
        /// </summary>
        public decimal cbplimit { get; set; } //	

        /// <summary>
        /// Текущие чистые позиции
        /// </summary>
        public decimal cbplused { get; set; } //	

        /// <summary>
        /// Плановые чистые позиции
        /// </summary>
        public decimal cbplplanned { get; set; } //	

        /// <summary>
        /// Вариационная маржа
        /// </summary>
        public decimal varmargin { get; set; } //	

        /// <summary>
        /// Биржевые сборы
        /// </summary>
        public decimal ts_comission	{get;set;} //	

        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.FirmId, firmid);
            fmt.AddField(LogFieldNames.AccountId, trdaccid);
            fmt.AddField(LogFieldNames.CbpLimit, cbplimit);
            fmt.AddField(LogFieldNames.CbpLUsed, cbplused);
            fmt.AddField(LogFieldNames.CbpLPlanned, cbplplanned);
            fmt.AddField(LogFieldNames.VarMargin, varmargin);
            fmt.AddField(LogFieldNames.TsComission, ts_comission);
            return fmt.ToString();
        }
    }
}

