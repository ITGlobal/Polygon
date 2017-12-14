namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal static class StompFrameExtensions
    {
        public static string GetHeader(this IStompFrame frame, string name, bool isRequired = false)
        {
            if (!frame.Headers.TryGetValue(name, out var value))
            {
                if (isRequired)
                {
                    throw new StompProtocolException($"\"{name}\" header is required for {frame.Command} frame");
                }

                return null;
            }

            return value;
        }

        public static void GetBody(this IStompFrame frame, out string body, out string contentType)
        {
            contentType = frame.GetHeader("content-type") ?? "application/octet-stream";
            body = frame.Body;
        }

        public static void EnsureMessageHasNoBody(this IStompFrame frame)
        {
            if (!string.IsNullOrEmpty(frame.Body))
            {
                throw new StompProtocolException($"Body is not allowed for {frame.Command} frame");
            }
        }
    }
}