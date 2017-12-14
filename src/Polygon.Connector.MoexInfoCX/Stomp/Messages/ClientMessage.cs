using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Polygon.Connector.MoexInfoCX.Stomp.Messages
{
    internal abstract class ClientMessage : IStompFrame
    {
        private readonly string _command;
        private readonly Dictionary<string, StringValues> _headers = new Dictionary<string, StringValues>();
        private string _body = "";

        protected ClientMessage(string command)
        {
            _command = command;
        }

        string IStompFrame.Command => _command;
        IDictionary<string, StringValues> IStompFrame.Headers => _headers;
        string IStompFrame.Body => _body;
        
        protected void SetHeader(string header, string value, bool force = false)
        {
            if (value == null)
            {
                return;
            }

            if (!_headers.TryGetValue(header, out var values) || force)
            {
                values = value;
            }
            else
            {
                values += value;
            }

            _headers[header] = values;
        }

        protected void SetBody(string body, string contentType)
        {
            _body = body;
            SetHeader("content-length", _body.Length.ToString(), force: true);
            SetHeader("content-type", contentType, force: true);
        }
    }
}