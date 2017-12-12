namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal interface IWireFormat
    {
        string WriteFrame(IStompFrame frame);
        IStompFrame ReadFrame(string rawMessage);
    }
}