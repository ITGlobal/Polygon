namespace Polygon.Connector.MicexBridge.MTETypes
{
    public struct EnumType
    {
        public string[] Constants;
        public string Description;
        public EnumKind Kind;
        public string Name;
        public int Size;
    }

    public enum EnumKind
    {
        Check = 0,
        Group = 1,
        Combo = 2
    }
}