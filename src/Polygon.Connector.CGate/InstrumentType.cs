using Polygon.Diagnostics;

namespace Polygon.Connector.CGate
{
    internal enum InstrumentType
    {
        [EnumMemberName("UNDEF")]
        Undef,
        [EnumMemberName("FUT")]
        Futures,
        [EnumMemberName("OPT")]
        Option
    }
}

