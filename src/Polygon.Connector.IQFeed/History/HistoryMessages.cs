using System;
using System.Diagnostics.CodeAnalysis;

namespace Polygon.Connector.IQFeed.History
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class HistoryMessages
    {
        public const string ERROR = "E,";
        public const string ENDMSG = "!ENDMSG";
        public const string E = "E,";

        /// <summary>
        ///     Retrieves interval data between [BeginDate BeginTime] and [EndDate EndTime] for the specified [Symbol].
        /// </summary>
        public static string HIT(string code, int interval, DateTime begin, DateTime end, string requestID)
        {
            // HIT,[Symbol],[Interval],[BeginDate BeginTime],[EndDate EndTime],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend],[IntervalType]<CR><LF> 
            return $"HIT,{code},{interval},{begin:yyyyMMdd HHmmss},{end:yyyyMMdd HHmmss},,,,1,{requestID},2500,s\r\n";
        }

        /// <summary>
        ///     Retrieves Daily data between [BeginDate] and [EndDate] for the specified [Symbol].
        /// </summary>
        public static string HDT(string code, DateTime begin, DateTime end, string requestID)
        {
            // HDT,[Symbol],[BeginDate],[EndDate],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            return $"HDT,{code},{begin:yyyyMMdd},{end:yyyyMMdd},,1,{requestID},2500\r\n";
        }

        /// <summary>
        ///     Retrieves up to [MaxDatapoints] datapoints of composite weekly datapoints for the specified [Symbol].
        /// </summary>
        public static string HWX(string code, DateTime begin, DateTime end, string requestID)
        {
            // HWX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            var dataPoints = (int)Math.Ceiling((DateTime.Today - begin).TotalDays / 7f);
            return $"HWX,{code},{dataPoints},1,{requestID},2500\r\n";
        }

        /// <summary>
        ///     Retrieves up to [MaxDatapoints] datapoints of composite monthly datapoints for the specified [Symbol].
        /// </summary>
        public static string HMX(string code, DateTime begin, DateTime end, string requestID)
        {
            // HMX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            var dataPoints = (int)Math.Ceiling((DateTime.Today - begin).TotalDays / 30f);
            return $"HMX,{code},{dataPoints},1,{requestID},2500\r\n";
        }
    }
}

