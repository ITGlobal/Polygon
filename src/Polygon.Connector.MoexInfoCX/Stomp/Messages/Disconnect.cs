namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal class Disconnect : ClientMessage
    {
        public Disconnect(string receipt = null)
            : base(StompCommands.DISCONNECT)
        {
            SetHeader("receipt", receipt);
        }
    }
}