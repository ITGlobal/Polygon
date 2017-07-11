using System;
using System.Collections.Concurrent;

namespace Polygon.Connector.IQFeed.Lookup
{
    internal sealed class LookupSecurityTypeIndex : ConcurrentDictionary<SecurityType, int>
    {
        private const int ExpectedCount = (int) SecurityType.FOREX;

        public bool UpdateFromSecurityTypeMsg(IQMessageArgs args)
        {
            var fields = args.Message.Split(',');
            if (fields.Length >= 3)
            {
                int id;
                if (!int.TryParse(fields[0], out id))
                {
                    return false;
                }

                SecurityType type;
                if (!Enum.TryParse(fields[1], true, out type))
                {
                    return false;
                }

                if (!TryAdd(type, id))
                {
                    return false;
                }

                return Count >= ExpectedCount;
            }

            return false;
        }
    }
}

