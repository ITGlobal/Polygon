using System.Collections.Generic;
using Polygon.Diagnostics;
using Newtonsoft.Json;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    [ObjectName(QLObjectNames.QLInstrumentsList)]
    internal class QLInstrumentsList : QLMessage
    {
        private string _futures;
        public string futures
        {
            get { return _futures; }
            set
            {
                _futures = value;
                var codes = _futures.Split(',');
                FuturesCodes = new List<string>(codes);
            } 
        }

        [JsonIgnore]
        public List<string> FuturesCodes { get; set; }

        private string _options;
        public string options 
        {
            get { return _options; }
            set
            {
                _options = value;
                var codes = _options.Split(',');
                OptionsCodes = new List<string>(codes);
            } 
        }

        [JsonIgnore]
        public List<string> OptionsCodes { get; set; }

        public override string Print(PrintOption option)
        {
            var fmt = ObjectLogFormatter.Create(this, option);
            fmt.AddListField(LogFieldNames.Futures, FuturesCodes);
            fmt.AddListField(LogFieldNames.Options, OptionsCodes);
            return fmt.ToString();
        }
    }
}

