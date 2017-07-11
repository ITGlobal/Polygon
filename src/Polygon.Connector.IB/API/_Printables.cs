using Polygon.Connector;
using Polygon.Diagnostics;

namespace IBApi
{
    [ObjectName("IB_ORDER")]
    partial class Order : IPrintable
    {
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Account, Account);
            fmt.AddField(LogFieldNames.Type, OrderType);
            fmt.AddField(LogFieldNames.Action, Action);
            fmt.AddField(LogFieldNames.Price, LmtPrice);
            fmt.AddField(LogFieldNames.Quantity, TotalQuantity);
            return fmt.ToString();
        }
        
        public override string ToString() => Print(PrintOption.Default);
    }
    
    [ObjectName("IB_CONTRACT")]
    partial class Contract : IPrintable
    {
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Id, conId);
            fmt.AddField(LogFieldNames.Symbol, LocalSymbol);
            fmt.AddField(LogFieldNames.Exchange, Exchange);
            return fmt.ToString();
        }

        public override string ToString() => Print(PrintOption.Default);
    }
    
    [ObjectName("IB_EXEC_FILTER")]
    partial class ExecutionFilter : IPrintable
    {
        /// <summary>
        ///     Вывести объект в лог
        /// </summary>
        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Account, AcctCode);
            return fmt.ToString();
        }

        public override string ToString() => Print(PrintOption.Default);
    }
}

