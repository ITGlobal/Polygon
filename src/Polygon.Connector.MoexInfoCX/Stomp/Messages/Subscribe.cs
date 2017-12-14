using System;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Subscribe : ClientMessage
    {
        public Subscribe(
            string destination,
            string id,
            string selector,
            AcknowledgmentMode? ask = null,
            string receipt = null)
            : base(StompCommands.SUBSCRIBE)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination), $"\"destination\" is required for {StompCommands.SUBSCRIBE} frame");
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id), $"\"id\" is required for {StompCommands.SUBSCRIBE} frame");

            SetHeader("destination", destination);
            SetHeader("id", id);
            SetHeader("selector", selector);
            SetHeader("receipt", receipt);

            switch (ask)
            {
                case AcknowledgmentMode.Auto:
                    SetHeader("ask", "auto");
                    break;
                case AcknowledgmentMode.Client:
                    SetHeader("ask", "client");
                    break;
                case AcknowledgmentMode.Individual:
                    SetHeader("ask", "client-individual");
                    break;
            }
        }
    }
}