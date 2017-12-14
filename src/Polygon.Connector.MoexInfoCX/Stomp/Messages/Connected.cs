namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal sealed class Connected : ServerMessage
    {
        internal Connected(IStompFrame frame)
        {
            Version = frame.GetHeader("version", isRequired: true);
            Session = frame.GetHeader("session", isRequired: false);
            Server = frame.GetHeader("server", isRequired: false);
            Heartbeat = frame.GetHeader("heart-beat", isRequired: false);
            frame.EnsureMessageHasNoBody();
        }

        public string Version { get; }
        public string Session { get; }
        public string Server { get; }
        public string Heartbeat { get; }
    }
}