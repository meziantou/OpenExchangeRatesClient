using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Meziantou.OpenExchangeRates
{
    public class ExchangeRatesResponse
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }
        [JsonProperty("licence")]
        public string License { get; set; }
        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixTimestampDateTimeConverter))]
        public DateTime Timestamp { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("rates")]
        public IDictionary<string, decimal> Rates { get; set; }
    }
}
