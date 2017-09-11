using System;
using Newtonsoft.Json;

namespace Meziantou.OpenExchangeRates
{
    internal class UnixTimestampDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = GetValue(reader.Value);
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private long GetValue(object value)
        {
            if (value == null)
                return 0;

            if (value is int i)
                return i;

            if (value is long l)
                return l;

            return 0;
        }
    }
}
