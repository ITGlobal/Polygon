using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Commit : ClientMessage
    {
        public Commit(string transaction, string receipt = null)
            : base(StompCommands.COMMIT)
        {
            if (string.IsNullOrEmpty(transaction))
                throw new ArgumentNullException(nameof(transaction), $"\"transaction\" is required for {StompCommands.COMMIT} frame");

            SetHeader("transaction", transaction);
            SetHeader("receipt", receipt);
        }
    }
}