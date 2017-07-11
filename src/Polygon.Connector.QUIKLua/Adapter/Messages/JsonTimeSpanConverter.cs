using System;
using System.Globalization;
using Polygon.Diagnostics;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Polygon.Connector.QUIKLua.Adapter.Messages
{
    internal class JsonTimeSpanConverter : JsonConverter
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(JsonTimeSpanConverter));

        [UsedImplicitly]
        public JsonTimeSpanConverter()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            try
            {
                DateTime dateTime;

                if (DateTime.TryParse(value, out dateTime))
                    return dateTime.TimeOfDay;

                return existingValue;
            }
            catch (Exception e)
            {
                Logger.Warn().PrintFormat(e, $"Unable to parse {value} as time");
                return existingValue;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }

        public override bool CanWrite => false;
    }
}

