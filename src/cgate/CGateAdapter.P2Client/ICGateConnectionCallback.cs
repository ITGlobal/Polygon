namespace CGateAdapter
{
    internal interface ICGateConnectionCallback
    {
        void RegisterConnection(CGateConnection connection);
        void ConnectionClosed(CGateConnection connection);
        void ConnectionError(CGateConnection connection);
        void ConnectionActive(CGateConnection connection);
        void ConnectionOpening(CGateConnection connection);
    }
}