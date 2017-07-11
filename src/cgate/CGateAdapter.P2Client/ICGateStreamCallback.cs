using CGateAdapter.Messages;

namespace CGateAdapter
{
    internal interface ICGateStreamCallback
    {
        void RegisterStream(CGateStream stream);
        void HandleMessage(CGateStream stream, CGateMessage message);
    }
}