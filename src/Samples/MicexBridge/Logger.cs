using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Diagnostics;

namespace MicexBridge
{
    internal sealed class Logger : ILogListener
    {
        public void Write(ref LogEvent e)
        {
            Console.WriteLine($"{e.Level}\t{e.MethodName}\t{e.Message}");
        }
    }
}
