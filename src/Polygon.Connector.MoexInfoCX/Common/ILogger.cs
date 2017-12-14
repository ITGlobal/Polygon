using System;

namespace Polygon.Connector.MoexInfoCX.Common
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception exception = null);
        void Debug(string message);
        void ServerFrame(string message);
        void ClientFrame(string message);
    }
}
