using System;
using CGateAdapter.Messages;
using CGateAdapter.Messages.FortsMessages;
using CGateAdapter.Messages.FutTrades;

namespace CGateAdapter.P2Client.Sample
{
    class CGateMessageHandler : CGateMessageVisitor
    {
        public Action<string, ConsoleColor> Print;

        public override void Handle(StreamStateChange message)
        {
            Print("< " + message, ConsoleColor.Green);
        }

        public override void Handle(CgmFortsMsg101 message)
        {
            Print("ADD_REPL: " + message, ConsoleColor.Yellow);
        }
        
        public override void Handle(CgmFortsMsg109 message)
        {
            Print("ADD_REPL: " + message, ConsoleColor.Yellow);
        }

        public override void Handle(CgmOrdersLog message)
        {
            Print("ORD_LOG: " + message, ConsoleColor.Magenta);
        }

        public override void Handle(CGateOrder message)
        {
            Print("ORD: " + message, ConsoleColor.Magenta);
        }
    }
}

