namespace Polygon.Connector.MicexBridge.MTETypes
{
    public enum ErrorCode
    {
        OK = 0,
        CONFIG = -1,
        SRVUNAVAIL = -2,
        LOGERROR = -3,
        INVALIDCONNECT = -4,
        NOTCONNECTED = -5,
        WRITE = -6,
        READ = -7,
        TSMR = -8,
        NOMEMORY = -9,
        ZLIB = -10,
        PKTINPROGRESS = -11,
        PKTNOTSTARTED = -12,
        LOGON = -13,
        FATALERROR = LOGON, // not used because of cabalistic signification
        INVALIDHANDLE = -14,
        DSROFF = -15,
        ERRUNKNOWN = -16,
        BADPTR = -17,
        WRONGPARAM = BADPTR,
        TRANSREJECTED = -18,
        REJECTION = TRANSREJECTED,
        TOOSLOWCONNECT = -19,
        TEUNAVAIL = TOOSLOWCONNECT,
        CRYPTO_ERROR = -20
    }
}