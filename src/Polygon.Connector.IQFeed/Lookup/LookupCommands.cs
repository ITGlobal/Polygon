namespace Polygon.Connector.IQFeed.Lookup
{
    internal static class LookupCommands
    {
        public static string RequestSecurityTypes()
        {
            return "SST\r\n";
        }

        public static string LookupSymbol(string code, int? type = null, string requestId = null)
        {
            requestId = requestId ?? string.Empty;

            if (type == null)
            {
                return string.Format("SBF,s,{0},,,{1}\r\n", code, requestId);
            }

            return string.Format("SBF,s,{0},t,{1},{2}\r\n", code, type.Value, requestId);
        }
    }
}

