using System.Text;

namespace Polygon.Connector.IQFeed
{
    internal static class CommandFormatter
    {
        public static string GetConnectCommand(SocketConnectionType type)
        {
            var sb = new StringBuilder();

            if (type == SocketConnectionType.Level1)
            {
                sb.AppendLine("S,CONNECT");
            }

            sb.AppendLine("S,SET PROTOCOL,5.1");

            return sb.ToString();
        }

        public static string GetDisconnectCommand()
        {
            return "S,DISCONNECT\r\n";
        }
    }
}

