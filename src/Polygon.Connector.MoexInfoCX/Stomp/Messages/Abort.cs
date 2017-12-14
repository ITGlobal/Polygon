using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Abort : ClientMessage
    {
        public Abort(string transaction, string receipt = null)
            : base(StompCommands.ABORT)
        {
            if (string.IsNullOrEmpty(transaction))
                throw new ArgumentNullException(nameof(transaction), $"\"transaction\" is required for {StompCommands.ABORT} frame");

            SetHeader("transaction", transaction);
            SetHeader("receipt", receipt);
        }
    }
}