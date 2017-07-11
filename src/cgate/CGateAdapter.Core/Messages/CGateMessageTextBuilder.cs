using System.Text;

namespace CGateAdapter.Messages
{
    internal sealed class CGateMessageTextBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private bool _hasAnyFields;
        private bool _isClosed;

        public CGateMessageTextBuilder(CGateMessage message)
        {
            _builder.Append("CG_MESSAGE {");
            Add(CGateFieldNames.StreamName, message.StreamName?.ToUpperInvariant());
            Add(CGateFieldNames.StreamRegime, message.StreamRegime);
            Add(CGateFieldNames.MessageTypeName, message.MessageTypeName?.ToUpperInvariant());
            Add(CGateFieldNames.UserId, message.UserId);
        }

        public void Add<T>(string member, T value)
        {
            if (!Equals(value, default(T)))
            {
                Add(member, value.ToString());
            }
        }

        public void Add(string member, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!_hasAnyFields)
                {
                    _builder.Append(" ");
                    _hasAnyFields = true;
                }
                else
                {
                    _builder.Append(", ");
                }

                _builder.AppendFormat("{0}={1}", member, value);
            }
        }
      
        public override string ToString()
        {
            if (!_isClosed)
            {
                if (!_hasAnyFields)
                {
                    _builder.Append(" ");
                }

                _builder.Append("}");
                _isClosed = true;
            }
            
            return _builder.ToString();
        }
    }
}