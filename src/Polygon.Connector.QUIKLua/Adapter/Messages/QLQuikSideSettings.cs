using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLInitBegin)]
    internal class QLQuikSideSettings: QLMessage
    {
        public override QLMessageType message_type => QLMessageType.QuikSideSettings;

        public QLQuikSideSettings(bool receiveMarketdata)
        {
            this.receiveMarketdata = receiveMarketdata;
        }

        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            return fmt.ToString();
        }

        /// <summary>
        /// Если true, то LUA будет отправлять маркетдату
        /// </summary>
        public bool receiveMarketdata { get; set; }
    }
}
