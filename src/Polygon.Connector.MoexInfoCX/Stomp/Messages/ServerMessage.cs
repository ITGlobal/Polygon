namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal abstract class ServerMessage
    {
        internal static ServerMessage Read(IStompFrame frame)
        {
            switch (frame.Command)
            {
                case StompCommands.CONNECTED:
                    return new Connected(frame);
                case StompCommands.RECEIPT:
                    return new Receipt(frame);
                case StompCommands.MESSAGE:
                    return new Message(frame);
                case StompCommands.ERROR:
                    return new Error(frame);

                default:
                    throw new StompProtocolException($"Unknwon server frame: {frame.Command}");
            }
        }
    }
}