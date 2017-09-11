using Newtonsoft.Json;

namespace Meziantou.OpenExchangeRates
{
    public class Usage
    {
        [JsonProperty("requests")]
        public int Requests { get; set; }
        [JsonProperty("requests_quota")]
        public int RequestsQuota { get; set; }
        [JsonProperty("requests_remaining")]
        public int RequestsRemaining { get; set; }
        [JsonProperty("days_elapsed")]
        public int DaysElapsed { get; set; }
        [JsonProperty("days_remaining")]
        public int DaysRemaining { get; set; }
        [JsonProperty("daily_average")]
        public int DailyAverage { get; set; }
    }
}
