using System;
using Polygon.Diagnostics;

namespace Polygon.Connector.MoexInfoCX.Common
{
    class StompLogger : ILogger
    {
        private readonly ILog _log;

        public StompLogger(ILog log)
        {
            _log = log;
        }

        public void Info(string message)
        {
            _log?.Info().Print(message);
        }

        public void Error(string message, Exception exception = null)
        {
            _log?.Error().Print(message);
        }

        public void Debug(string message)
        {
            _log?.Debug().Print(message);
        }

        public void ServerFrame(string message)
        {
            _log?.Info().Print("ServerFrame " + message);
        }

        public void ClientFrame(string message)
        {
            _log?.Info().Print("ClientFrame " + message);
        }
    }
}
