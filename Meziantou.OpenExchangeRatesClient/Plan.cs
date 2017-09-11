using Newtonsoft.Json;

namespace Meziantou.OpenExchangeRates
{
    public class Plan
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("quota")]
        public string Quota { get; set; }
        [JsonProperty("update_frequency")]
        public string UpdateFrequency { get; set; }
        [JsonProperty("features")]
        public Features Features { get; set; }
    }
}
