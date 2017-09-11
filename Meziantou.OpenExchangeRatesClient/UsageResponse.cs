using Newtonsoft.Json;

namespace Meziantou.OpenExchangeRates
{
    internal class UsageResponse
    {
        [JsonProperty("data")]
        public UserData Data { get; set; }
    }
}
