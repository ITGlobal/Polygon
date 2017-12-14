namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal sealed class Receipt : ServerMessage
    {
        internal Receipt(IStompFrame frame)
        {
            ReceiptId = frame.GetHeader("receipt-id", isRequired: true);

            frame.EnsureMessageHasNoBody();
        }

        public string ReceiptId { get; }
    }
}