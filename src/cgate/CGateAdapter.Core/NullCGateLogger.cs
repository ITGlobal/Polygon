using System;

namespace CGateAdapter
{
    internal sealed class NullCGateLogger : ICGateLogger
    {
        public static ICGateLogger Instance { get; } = new NullCGateLogger();

        private NullCGateLogger() { }

        public void Trace(string message, params object[] args) { }
        public void Debug(string message, params object[] args) { }
        public void Info(string message, params object[] args) { }
        public void Warn(string message, params object[] args) { }
        public void Error(string message, params object[] args) { }
        public void Error(Exception exception, string message, params object[] args) { }
    }
}