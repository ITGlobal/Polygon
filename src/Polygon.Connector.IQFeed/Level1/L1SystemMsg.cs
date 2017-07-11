using System.Diagnostics.CodeAnalysis;

namespace Polygon.Connector.IQFeed.Level1
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class L1SystemMsg
    {
        public const string SERVER_CONNECTED = "SERVER CONNECTED";
        public const string SERVER_DISCONNECTED = "SERVER DISCONNECTED";
        public const string SYMBOL_LIMIT_REACHED = "SYMBOL LIMIT REACHED";
        public const string CURRENT_UPDATE_FIELDNAMES = "CURRENT UPDATE FIELDNAMES,";
    }
}

