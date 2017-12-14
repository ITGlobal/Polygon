using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Send : ClientMessage
    {
        public Send(
            string destination,
            string body,
            string contentType = null,
            string receipt = null,
            string transaction = null)
            : base(StompCommands.SEND)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination), $"\"destination\" is required for {StompCommands.SEND} frame");

            SetHeader("destination", destination);
            SetHeader("receipt", receipt);
            SetHeader("transaction", transaction);

            SetBody(body, contentType);
        }
    }
}