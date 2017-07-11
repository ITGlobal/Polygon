using Polygon.Diagnostics;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLPosition)]
    internal class QLPosition : QLMessage
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public string firmid { get; set; }

        /// <summary>
        /// Торговый счет
        /// </summary>
        public string trdaccid { get; set; }

        /// <summary>
        /// Код фьючерсного контракта
        /// </summary>
        public string sec_code { get; set; }

        /// <summary>
        /// Входящие чистые позиции
        /// </summary>
        public int startnet { get; set; }	

        /// <summary>
        /// Текущие чистые позиции
        /// </summary>
        public int totalnet { get; set; }	

        /// <summary>
        /// Вариационная маржа
        /// </summary>
        public decimal varmargin { get; set; }
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.FirmId, firmid);
            fmt.AddField(LogFieldNames.AccountId, trdaccid);
            fmt.AddField(LogFieldNames.SecCode, sec_code);
            fmt.AddField(LogFieldNames.StartNet, startnet);
            fmt.AddField(LogFieldNames.TotalNet, totalnet);
            fmt.AddField(LogFieldNames.VarMargin, varmargin);
            return fmt.ToString();
        }
    }
}

