using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Meziantou.OpenExchangeRates
{
    public class OpenExchangeRatesClient : IDisposable
    {
        private const string ApiBaseUrl = "https://openexchangerates.org/api/";
        private readonly HttpClient _client;
        private readonly bool _clientOwned;

        public string AppId { get; set; }

        public OpenExchangeRatesClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ApiBaseUrl);
            _clientOwned = true;
        }

        public OpenExchangeRatesClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _clientOwned = false;
        }

        public void Dispose()
        {
            if (_clientOwned)
            {
                _client?.Dispose();
            }
        }

        /// <summary>
        /// Get a JSON list of all currency symbols available from the Open Exchange Rates API, along with their full names, for use in your integration.
        /// </summary>
        /// <seealso cref="https://oxr.readme.io/docs/currencies-json"/>
        /// <param name="showAlternative">Extend returned values with alternative, black market and digital currency rates</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Currency>> GetCurrenciesAsync(bool showAlternative = false)
        {
            var str = await _client.GetStringAsync($"currencies.json?prettyprint=0&show_alternative={ToBoolean(showAlternative)}").ConfigureAwait(false);
            if (str == null)
                return null;

            var currencies = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            if (currencies == null)
                return null;

            return currencies.Select(kvp => new Currency(kvp.Key, kvp.Value)).ToList();
        }

        /// <summary>
        /// Get the latest exchange rates available from the Open Exchange Rates API.
        /// </summary>
        /// <seealso cref="https://oxr.readme.io/docs/latest-json"/>
        /// <param name="showAlternative">Extend returned values with alternative, black market and digital currency rates</param>
        /// <param name="base">Change base currency (3-letter code, default: USD)</param>
        /// <param name="symbols">Limit results to specific currencies (comma-separated list of 3-letter codes)</param>
        /// <returns></returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesAsync(bool showAlternative = false, string @base = null, string symbols = null)
        {
            var url = $"latest.json?app_id={AppId}&prettyprint=0&show_alternative={ToBoolean(showAlternative)}";
            if (@base != null)
            {
                url += "&base=" + @base;
            }
            if (symbols != null)
            {
                url += "&symbols=" + symbols;
            }

            var str = await _client.GetStringAsync(url).ConfigureAwait(false);
            if (str == null)
                return null;

            return JsonConvert.DeserializeObject<ExchangeRatesResponse>(str);
        }

        /// <summary>
        /// Get historical exchange rates for any date available from the Open Exchange Rates API.
        /// </summary>
        /// <seealso cref="https://oxr.readme.io/docs/historical-json"/>
        /// <param name="date"></param>
        /// <param name="showAlternative">Extend returned values with alternative, black market and digital currency rates</param>
        /// <returns></returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesAsync(DateTime date, bool showAlternative = false, string @base = null, string symbols = null)
        {
            string url = $"historical/{date:yyyy-MM-dd}.json?app_id={AppId}&prettyprint=0&show_alternative={ToBoolean(showAlternative)}";
            if (@base != null)
            {
                url += "&base=" + @base;
            }
            if (symbols != null)
            {
                url += "&symbols=" + symbols;
            }

            var str = await _client.GetStringAsync(url).ConfigureAwait(false);
            if (str == null)
                return null;

            return JsonConvert.DeserializeObject<ExchangeRatesResponse>(str);
        }

        /// <summary>
        /// Get historical exchange rates for any date available from the Open Exchange Rates API.
        /// </summary>
        /// <seealso cref="https://oxr.readme.io/docs/historical-json"/>
        /// <param name="date"></param>
        /// <param name="showAlternative">Extend returned values with alternative, black market and digital currency rates</param>
        /// <returns></returns>
        public async Task<Stream> DownloadExchangeRatesAsync(DateTime date, bool showAlternative = false, string @base = null, string symbols = null)
        {
            string url = $"historical/{date:yyyy-MM-dd}.json?app_id={AppId}&prettyprint=0&show_alternative={ToBoolean(showAlternative)}";
            if (@base != null)
            {
                url += "&base=" + @base;
            }
            if (symbols != null)
            {
                url += "&symbols=" + symbols;
            }

            var stream = await _client.GetStreamAsync(url).ConfigureAwait(false);
            if (stream == null)
                return null;

            return stream;
        }

        /// <summary>
        /// Get basic plan information and usage statistics for an Open Exchange Rates App ID
        /// </summary>
        /// <returns></returns>
        public async Task<UserData> GetUsageAsync()
        {
            var str = await _client.GetStringAsync($"usage.json?app_id={AppId}&prettyprint=0").ConfigureAwait(false);
            if (str == null)
                return null;

            var info = JsonConvert.DeserializeObject<UsageResponse>(str);
            if (info == null)
                return null;

            return info.Data;
        }

        private static string ToBoolean(bool value)
        {
            return value ? "1" : "0";
        }
    }
}
