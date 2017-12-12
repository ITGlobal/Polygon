using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Begin : ClientMessage
    {
        public Begin(string transaction, string receipt = null)
            : base(StompCommands.BEGIN)
        {
            if (string.IsNullOrEmpty(transaction))
                throw new ArgumentNullException(nameof(transaction), $"\"transaction\" is required for {StompCommands.BEGIN} frame");

            SetHeader("transaction", transaction);
            SetHeader("receipt", receipt);
        }
    }
}