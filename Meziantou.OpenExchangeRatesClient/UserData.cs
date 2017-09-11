using Newtonsoft.Json;

namespace Meziantou.OpenExchangeRates
{
    public class UserData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("plan")]
        public Plan Plan { get; set; }
        [JsonProperty("usage")]
        public Usage Usage { get; set; }
    }
}
