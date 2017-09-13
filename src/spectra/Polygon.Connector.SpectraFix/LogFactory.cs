using Polygon.Diagnostics;
using QuickFix;

namespace Polygon.Connector.SpectraFix
{
    internal sealed class LogFactory : QuickFix.ILogFactory
    {
        private sealed class Log : QuickFix.ILog
        {
            private readonly string _sessionId;
            private static readonly Diagnostics.ILog _Log = LogManager.GetLogger("Polygon.Connector.SpectraFix");

            public Log(SessionID sessionId)
            {
                _sessionId = sessionId.ToString();
            }

            public void OnIncoming(string msg)
            {
            }

            public void OnOutgoing(string msg) { }

            public void OnEvent(string s) => _Log.Debug().PrintFormat("[{0}] {1}", _sessionId.Preformatted(), s.Preformatted());

            public void Clear() { }

            public void Dispose() { }
        }

        public QuickFix.ILog Create(SessionID sessionId) => new Log(sessionId);
    }
}