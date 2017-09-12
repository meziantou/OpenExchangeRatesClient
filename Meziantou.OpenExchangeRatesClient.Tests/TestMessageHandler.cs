using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meziantou.OpenExchangeRates.Tests
{
    internal class TestMessageHandler : HttpMessageHandler
    {
        public const string Host = "http://test/";

        protected virtual string GetResponseContent(Uri uri)
        {
            var path = uri.PathAndQuery.Substring(1)
                .Replace('?', '_')
                .Replace('&', '_')
                .Replace('/', '_')
                .Replace("_app_id=", "");

            var assembly = typeof(TestMessageHandler).Assembly;
            string resourceName = "Meziantou.OpenExchangeRates.Tests.ApiResponses." + path + ".json";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    var names = assembly.GetManifestResourceNames();
                    throw new Exception($"Resource '{resourceName}' not found. Available resources:\r\n" + string.Join(Environment.NewLine, names));
                }

                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = new StringContent(GetResponseContent(request.RequestUri), Encoding.UTF8, "application/json");

            return Task.FromResult(response);
        }
    }
}
