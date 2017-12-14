namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal sealed class Error : ServerMessage
    {
        internal Error(IStompFrame frame)
        {
            ReceiptId = frame.GetHeader("receipt-id", isRequired: false);
            Message = frame.GetHeader("message", isRequired: false);

            // frame.EnsureMessageHasNoBody();
        }

        public string ReceiptId { get; }
        public string Message { get; }
    }
}
