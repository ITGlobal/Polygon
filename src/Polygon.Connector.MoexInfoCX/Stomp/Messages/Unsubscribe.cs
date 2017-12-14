using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Unsubscribe : ClientMessage
    {
        public Unsubscribe(string id, string receipt = null)
            : base(StompCommands.UNSUBSCRIBE)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id), $"\"destination\" is required for {StompCommands.UNSUBSCRIBE} frame");

            SetHeader("id", id);
            SetHeader("receipt", receipt);
        }
    }
}