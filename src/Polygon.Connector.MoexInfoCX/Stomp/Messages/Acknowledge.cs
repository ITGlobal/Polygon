using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Acknowledge : ClientMessage
    {
        public Acknowledge(string id, string transaction = null, string receipt = null)
            : base(StompCommands.ACK)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id), $"\"id\" is required for {StompCommands.ACK} frame");

            SetHeader("id", id);
            SetHeader("transaction", transaction);
            SetHeader("receipt", receipt);
        }
    }
}