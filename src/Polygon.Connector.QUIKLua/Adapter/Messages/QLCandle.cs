using System;
using Polygon.Diagnostics;
using Polygon.Connector.QUIKLua.Adapter.Messages;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLCandle)]
    internal class QLCandle : IPrintable
    {
        private string _time;

        public string time
        {
            get { return _time; }
            set
            {
                _time = value;
                Time = DateTime.ParseExact(_time, "yyyy.MM.dd HH:mm", null);
            }
        }

        public DateTime Time { get; set; }
        public decimal o { get; set; }
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal c { get; set; }

        public override string ToString() => Print(PrintOption.Default);

        public string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddField("t", Time);
            fmt.AddField("o", o);
            fmt.AddField("h", h);
            fmt.AddField("l", l);
            fmt.AddField("c", c);
            return fmt.ToString();
        }
    }
}

