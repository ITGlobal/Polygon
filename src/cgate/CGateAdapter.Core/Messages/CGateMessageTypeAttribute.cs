using System;

namespace CGateAdapter.Messages
{
    internal sealed class CGateMessageTypeAttribute : Attribute
    {
        public CGateMessageTypeAttribute(string streamName, string tableName)
        {
            StreamName = streamName;
            TableName = tableName;
        }

        public string StreamName { get; }

        public string TableName { get; }
    }
}