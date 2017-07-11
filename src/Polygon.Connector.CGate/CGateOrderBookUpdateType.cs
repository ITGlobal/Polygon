using Polygon.Diagnostics;

namespace Polygon.Connector.CGate
{
    internal enum CGateOrderBookUpdateType
    {
        [EnumMemberName("ROW_UPDATE")]
        RowUpdate,

        [EnumMemberName("STREAM_DATA_END")]
        StreamDataEnd,

        [EnumMemberName("CLEAR_TABLE")]
        ClearTable,
    }
}

