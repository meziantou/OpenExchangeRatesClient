using Newtonsoft.Json;

namespace Meziantou.OpenExchangeRates
{
    public class Features
    {
        [JsonProperty("base")]
        public bool Base { get; set; }
        [JsonProperty("symbols")]
        public bool Symbols { get; set; }
        [JsonProperty("experimental")]
        public bool Experimental { get; set; }
        [JsonProperty("time-series")]
        public bool TimeSeries { get; set; }
        [JsonProperty("convert")]
        public bool Convert { get; set; }
    }
}
