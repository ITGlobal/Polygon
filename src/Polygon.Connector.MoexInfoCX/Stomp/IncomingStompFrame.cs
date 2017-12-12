using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Polygon.Connector.MoexInfoCX.Stomp
{
    internal sealed class IncomingStompFrame : IStompFrame
    {
        private readonly Dictionary<string, StringValues> _headers = new Dictionary<string, StringValues>();
        private string _body = "";

        public string Command { get; private set; } = "";

        IDictionary<string, StringValues> IStompFrame.Headers => _headers;
        string IStompFrame.Body => _body;

        public void SetCommand(string command)
        {
            Command = command;
        }

        public void SetHeader(string header, string value)
        {
            if (!_headers.TryGetValue(header, out var values))
            {
                values = value;
            }
            else
            {
                values += value;
            }

            _headers[header] = values;
        }

        public void SetBody(string body)
        {
            _body = body;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Command))
            {
                throw new StompWireFormatException("Message is empty");
            }
        }
    }
}