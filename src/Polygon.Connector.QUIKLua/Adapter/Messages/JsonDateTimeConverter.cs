using System;
using Polygon.Diagnostics;
using Newtonsoft.Json;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    internal class JsonDateTimeConverter : JsonConverter
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(JsonDateTimeConverter));
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly string format;

        public JsonDateTimeConverter(IDateTimeProvider dateTimeProvider, string format = "h\\:mm\\:ss")
        {
            this.dateTimeProvider = dateTimeProvider;
            this.format = format;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;

            // квик присылает полночь как 24
            if (!string.IsNullOrEmpty(value) && value.StartsWith("24"))
            {
                value = $"00{value.Substring(2)}";
            }

            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return existingValue;
                }

                var now = dateTimeProvider.Now;
                var today = now.Date;
                
                DateTime serverTime;
                if (!DateTime.TryParse(value, out serverTime))
                    throw new FormatException($"Unable to parse time from '{value}'");

                var serverDateTime = today + serverTime.TimeOfDay;
                var diff = now.TimeOfDay - serverTime.TimeOfDay;

                if (diff < TimeSpan.FromHours(-12))
                {
                    return serverDateTime.AddDays(-1);
                }
                if (diff > TimeSpan.FromHours(12))
                {
                    return serverDateTime.AddDays(1);
                }
                return serverDateTime;
            }
            catch (Exception e)
            {
                Logger.Warn().PrintFormat(e, "Unable to parse {0} as datetime with format {1}", value, format);
                return existingValue;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override bool CanWrite => false;
    }
}

