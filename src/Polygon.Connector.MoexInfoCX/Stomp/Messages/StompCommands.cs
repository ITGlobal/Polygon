namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal static class StompCommands
    {
        #region Client commands

        public const string STOMP = "STOMP";
        public const string CONNECT = "CONNECT";
        public const string SEND = "SEND";
        public const string SUBSCRIBE = "SUBSCRIBE";
        public const string UNSUBSCRIBE = "UNSUBSCRIBE";
        public const string ACK = "ACK";
        public const string NACK = "NACK";
        public const string BEGIN = "BEGIN";
        public const string COMMIT = "COMMIT";
        public const string ABORT = "ABORT";
        public const string DISCONNECT = "DISCONNECT";
        
        #endregion

        #region Server commands

        public const string CONNECTED = "CONNECTED";
        public const string ERROR = "ERROR";
        public const string RECEIPT = "RECEIPT";
        public const string MESSAGE = "MESSAGE";
        
        #endregion
    }
}