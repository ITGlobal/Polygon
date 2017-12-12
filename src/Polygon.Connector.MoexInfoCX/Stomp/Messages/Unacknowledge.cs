using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Unacknowledge : ClientMessage
    {
        public Unacknowledge(string id, string transaction = null, string receipt = null)
            : base(StompCommands.NACK)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id), $"\"id\" is required for {StompCommands.NACK} frame");

            SetHeader("id", id);
            SetHeader("transaction", transaction);
            SetHeader("receipt", receipt);
        }
    }
}