using System;

namespace Polygon.Connector.IQFeed
{
    internal sealed class IQFeedHistoryDataException : Exception
    {
        public IQFeedHistoryDataException(string message)
            : base(message)
        { } 
    }
}

