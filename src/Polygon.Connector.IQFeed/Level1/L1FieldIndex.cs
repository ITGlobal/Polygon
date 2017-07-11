using System.Collections.Concurrent;

namespace Polygon.Connector.IQFeed.Level1
{
    internal sealed class L1FieldIndex : ConcurrentDictionary<string, int>
    {
        public void UpdateFromL1SystemMsg(IQMessageArgs args)
        {
            var fields = args.Message.Split(',');
            for (var i = 1; i < fields.Length; i++)
            {
                this[fields[i]] = i - 1;
            }
        }

        public bool TryGetField(string[] fields, string name, out string value)
        {
            int index;
            if (!TryGetValue(name, out index))
            {
                value = null;
                return false;
            }

            if (index < 0 || index >= fields.Length)
            {
                value = null;
                return false;
            }

            value = fields[index];
            return true;
        }
    }
}

