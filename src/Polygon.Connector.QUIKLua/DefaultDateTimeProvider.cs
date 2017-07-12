using System;

namespace Polygon.Connector.QUIKLua
{
    internal sealed class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
        public DateTime Today=> DateTime.Today;
    }
}