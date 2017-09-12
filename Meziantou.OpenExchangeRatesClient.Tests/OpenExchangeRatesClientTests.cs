using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meziantou.OpenExchangeRates.Tests
{
    [TestClass]
    public class OpenExchangeRatesClientTests
    {
        private TestMessageHandler _handler;
        private HttpClient _httpClient;
        private OpenExchangeRatesClient _openExchangeRatesClient;

        [TestInitialize]
        public void Initialize()
        {
            _handler = new TestMessageHandler();
            _httpClient = new HttpClient(_handler);
            _httpClient.BaseAddress = new Uri(TestMessageHandler.Host);
            _openExchangeRatesClient = new OpenExchangeRatesClient(_httpClient);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _openExchangeRatesClient?.Dispose();
            _httpClient?.Dispose();
            _handler?.Dispose();
        }

        [TestMethod]
        public async Task OpenExchangeRatesClient_Latest()
        {
            var response = await _openExchangeRatesClient.GetExchangeRatesAsync();
            Assert.AreEqual("USD", response.Base);
            Assert.AreEqual(66.809999m, response.Rates["AFN"]);
        }

        [TestMethod]
        public async Task OpenExchangeRatesClient_Historical()
        {
            var date = new DateTime(2001, 02, 16);
            var response = await _openExchangeRatesClient.GetExchangeRatesAsync(date);

            Assert.AreEqual(2001, response.Timestamp.Year);
            Assert.AreEqual(2, response.Timestamp.Month);
            Assert.AreEqual(16, response.Timestamp.Day);
            Assert.AreEqual("USD", response.Base);
            Assert.AreEqual(3.672538m, response.Rates["AED"]);
        }

        [TestMethod]
        public async Task OpenExchangeRatesClient_Currencies()
        {
            var response = await _openExchangeRatesClient.GetCurrenciesAsync();
            Assert.AreEqual(3, response.Count);

            var collection = new List<Currency>()
            {
                new Currency("AED", "United Arab Emirates Dirham"),
                new Currency("AFN", "Afghan Afghani"),
                new Currency("ALL", "Albanian Lek")
            };

            CollectionAssert.AreEquivalent(collection, response.ToList());
        }

        [TestMethod]
        public async Task OpenExchangeRatesClient_Usage()
        {
            var response = await _openExchangeRatesClient.GetUsageAsync();
            Assert.AreEqual(45476, response.Usage.RequestsRemaining);
            Assert.AreEqual("Enterprise", response.Plan.Name);
        }
    }
}
