using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Polygon.Connector.IQFeed
{
    /// <summary>
    ///     Тип ценной бумаги
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [PublicAPI]
    public enum SecurityType
    {
        EQUITY = 1,
        IEOPTION,
        MUTUAL,
        MONEY,
        BONDS,
        INDEX,
        MKTSTATS,
        FUTURE,
        FOPTION,
        SPREAD,
        SPOT,
        FORWARD,
        CALC,
        STRIP,
        SSFUTURE,
        FOREX,
        MKTDEPTH,
        PRECMTL,
        RACKS,
        RFSPOT,
        ICSPREAD,
        STRATSPREAD,
        TREASURIES,
        SWAPS,
        MKTRPT,
        SNL_NG,
        SNL_ELEC,
        NP_CAPACITY,
        NP_FLOW,
        NP_POWER,
        COMM3,
        JACOBSEN
    }
}

