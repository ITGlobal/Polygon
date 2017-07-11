using System;
using System.Linq;

namespace CGateAdapter.P2Client.Sample
{
    public sealed class Logger : ICGateLogger
    {
        public bool TraceEnabled = false;
        public bool DebugEnabled = false;
        public bool InfoEnabled = true;
        public bool WarnEnabled = true;
        public bool ErrorEnabled = true;
        

        public void Trace(string message, params object[] args)
        {
            if(!TraceEnabled) return;
            Print(message, args, null, ConsoleColor.Gray);
        }

        public void Debug(string message, params object[] args)
        {
            if(!DebugEnabled) return;
            Print(message, args, null, ConsoleColor.White);
        }

        public void Info(string message, params object[] args)
        {
            if (!InfoEnabled) return;
            Print(message, args, null, ConsoleColor.Cyan);
        }

        public void Warn(string message, params object[] args)
        {
            if (!WarnEnabled) return;
            Print(message, args, null, ConsoleColor.Yellow);
        }

        public void Error(string message, params object[] args)
        {
            if (!ErrorEnabled) return;
            Print(message, args, null, ConsoleColor.Red);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            if (!ErrorEnabled) return;
            Print(message, args, exception, ConsoleColor.Red);
        }

        private static void Print(string message, object[] args, Exception exception, ConsoleColor color)
        {
            var text = (args != null && args.Any()) ? string.Format(message, args) : message;
            if (exception != null)
            {
                text += "\n" + exception;
            }

            Program.Print(text, color);
        }
    }
}