namespace Polygon.Connector.IQFeed
{
    internal sealed class IQMessageArgs
    {
        private readonly string message;
        private readonly string requestId;

        public IQMessageArgs(string message, string requestId = "")
        {
            this.message = message;
            this.requestId = requestId;
        }

        public string Message { get { return message; } }
        public string RequestId { get { return requestId; } }
    }
}

