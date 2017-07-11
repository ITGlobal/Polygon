using System;
using System.Globalization;
using Polygon.Diagnostics;
using Polygon.Connector.QUIKLua.Adapter.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    internal abstract class QLMessage : IPrintable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(QLMessage));

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual QLMessageType message_type { get; set; }

        protected DateTime ParseDateTime(string value, DateTime defaultValue, string format = "H:mm:ss")
        {
            try
            {
                return string.IsNullOrEmpty(value)
                    ? defaultValue
                    : DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Logger.Warn().PrintFormat(e, "Unable to parse {0} as datetime with format {1}", value, format);
                return defaultValue;
            }
        }

        protected TimeSpan ParseTimeSpan(string value, TimeSpan defaultValue, string format = "h\\:mm\\:ss")
        {
            try
            {
                return string.IsNullOrEmpty(value)
                    ? defaultValue
                    : TimeSpan.ParseExact(value, format, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Logger.Warn().PrintFormat(e, "Unable to parse {0} as time with format {1}", value, format);
                return defaultValue;
            }
        }

        public override string ToString() => Print(PrintOption.Default);

        public abstract string Print(PrintOption option);
    }
}

