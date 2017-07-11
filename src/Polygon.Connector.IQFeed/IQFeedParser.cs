using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Polygon.Connector.IQFeed
{
   internal static class IQFeedParser
    {
        public static int ParseInt(string strToParse)
        {
            int rVal;
            return int.TryParse(strToParse, out rVal) ? rVal : 0;
        }

        public static uint ParseUint(string strToParse)
        {
            uint rVal;
            return uint.TryParse(strToParse, out rVal) ? rVal : 0U;
        }

        public static decimal ParseDecimal(string strToParse)
        {
            decimal rVal;
            return decimal.TryParse(strToParse, NumberStyles.Any, CultureInfo.InvariantCulture, out rVal) ? rVal : 0M;
        }

        public static long ParseLong(string strToParse)
        {
            long rVal;
            return long.TryParse(strToParse, out rVal) ? rVal : 0L;
        }

        public static DateTime ParseDateTime(string strToParse, string format = @"MM/dd/yyyy")
        {
            //02/01/2013
            DateTime rVal;
            return DateTime.TryParseExact(strToParse, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out rVal) ? rVal : DateTime.MinValue;
        }

        public static DateTime ParseDateTime(string strToParse, params string[] formats)
        {
            //02/01/2013
            DateTime rVal;
            return DateTime.TryParseExact(strToParse, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out rVal) ? rVal : DateTime.MinValue;
        }

        public static IPAddress ParseIpAddressOrDns(string strAddress, AddressFamily family)
        {
            IPAddress ipAddress;
            if (!IPAddress.TryParse(strAddress, out ipAddress))
            {
#if NET45
                ipAddress = Dns.GetHostEntry(strAddress).AddressList.FirstOrDefault(a => a.AddressFamily == family);
#endif
#if NETSTANDARD1_6
                ipAddress = Dns.GetHostEntryAsync(strAddress).Result.AddressList.FirstOrDefault(a => a.AddressFamily == family);
#endif
            }

            return ipAddress;
        }

        #region TimeZone

        /// <summary>
        ///     Временная зона, в которая используется в свечах IQFeed
        /// </summary>
        private static readonly TimeZoneInfo IQFeedTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        /// <summary>
        ///     Перевод из локального времени
        /// </summary>
        public static DateTime ToIQFeedTime(DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, IQFeedTimeZone);
        }

        /// <summary>
        ///     Перевод в локальное время
        /// </summary>
        public static DateTime FromIQFeedTime(DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, IQFeedTimeZone, TimeZoneInfo.Local);
        }

        /// <summary>
        ///     Округлить дату
        /// </summary>
        public static DateTime RoundDateTime(DateTime dateTime, TimeSpan timeSpan)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        #endregion
    }
}

