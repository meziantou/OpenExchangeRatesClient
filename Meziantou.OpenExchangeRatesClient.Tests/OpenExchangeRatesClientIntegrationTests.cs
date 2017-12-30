using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meziantou.OpenExchangeRates.Tests
{
    [TestClass]
    public class OpenExchangeRatesClientIntegrationTests
    {
        private const string AppId = "";

        private OpenExchangeRatesClient _openExchangeRatesClient;

        [TestInitialize]
        public void Initialize()
        {
            if (string.IsNullOrEmpty(AppId))
            {
                Assert.Inconclusive("AppId is not set");
            }

            _openExchangeRatesClient = new OpenExchangeRatesClient();
            _openExchangeRatesClient.AppId = AppId;
        }

        [TestCleanup]
        public void CleanUp()
        {
            _openExchangeRatesClient?.Dispose();
        }

        [TestMethod]
        public async Task OpenExchangeRatesClient_Latest()
        {
            var response = await _openExchangeRatesClient.GetExchangeRatesAsync();
            Assert.AreEqual("USD", response.Base);
            Assert.IsTrue(response.Rates["AFN"] > 0);
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
            Assert.IsTrue(response.Rates["AED"] > 0);
        }

        [TestMethod]
        public async Task OpenExchangeRatesClient_Currencies()
        {
            var response = (await _openExchangeRatesClient.GetCurrenciesAsync()).ToList();
            var codes = response.Select(c => c.Code).ToList();
            CollectionAssert.Contains(codes, "EUR");
            CollectionAssert.Contains(codes, "USD");
        }

        [TestMethod]
        public async Task OpenExchangeRatesClient_Usage()
        {
            var response = await _openExchangeRatesClient.GetUsageAsync();
            Assert.IsTrue(response.Usage.RequestsRemaining >= 0);
        }
    }
}
