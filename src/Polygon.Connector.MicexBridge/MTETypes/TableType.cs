#region

using System;

#endregion

namespace Polygon.Connector.MicexBridge.MTETypes
{
    public struct TableType
    {
        public string Description;
        public TableFlags Flags;
        public Field[] Input;
        public string Name;
        public Field[] Output;
    }

    [Flags]
    public enum TableFlags
    {
        Updateable = 1,
        ClearOnUpdate = 2
    }
}