using System.Collections.Generic;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    /// <summary>
    /// Список счетов
    /// </summary>
    [ObjectName(QLObjectNames.QLAccountsList)]
    internal class QLAccountsList : QLMessage
    {
        public List<string> accounts;
        
        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddListField(LogFieldNames.Items, accounts);
            return fmt.ToString();
        }
    }
}

