namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal sealed class Message : ServerMessage
    {
        internal Message(IStompFrame frame)
        {
            Destination = frame.GetHeader("destination", isRequired: true);
            Subscription = frame.GetHeader("subscription", isRequired: true);
            MessageId = frame.GetHeader("message-id", isRequired: true);
            Ask = frame.GetHeader("ask", isRequired: false);

            frame.GetBody(out var body, out var contentType);
            Body = body;
            ContentType = contentType;
        }

        public string Destination { get; }
        public string Subscription { get; }
        public string MessageId { get; }
        public string Ask { get; }
        
        public string Body { get; }
        public string ContentType { get; }
    }
}