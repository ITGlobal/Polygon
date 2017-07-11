namespace Polygon.Connector.IQFeed.Level1
{
    internal static class L1Commands
    {
        public static string GetSubscribeCommand(string code)
        {
            return "w" + code + "\r\n";
        }

        public static string GetUnsubscribeCommand(string code)
        {
            return "r" + code + "\r\n";
        }

        public static string GetSelectUpdateFieldsCommand(string[] codes)
        {
            return "S,SELECT UPDATE FIELDS," + string.Join(",", codes) + "\r\n";
        }
    }
}

