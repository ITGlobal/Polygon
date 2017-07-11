using System.Collections.Generic;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    /// <summary>
    /// Конверт с несколькими сообщеняими
    /// </summary>
    [ObjectName(QLObjectNames.QLEnvelope)]
    internal class QLEnvelope : IPrintable
    {
        /// <summary>
        /// Порядковый номер в течение сессии работы 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Количество сообщений
        /// </summary>
        public int count { get; set; }

        public List<QLMessage> body { get; set; }

        public override string ToString() => Print(PrintOption.Default);

        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField(LogFieldNames.Id, id);
            fmt.AddListField(LogFieldNames.Items, body);
            return fmt.ToString();
        }
    }
}

