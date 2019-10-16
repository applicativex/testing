using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Accounting.ComponentTests
{
    [TestFixture]
    public abstract class ComponentTestsBase
    {
        private string API_URL = "http://accounting";
        protected HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            var isLocalRun = Environment.GetEnvironmentVariable("RUN_ENV") == null;
            API_URL = isLocalRun ? "http://localhost:5000" : "http://accounting";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(API_URL)
            };
        }

        [TearDown]
        public void Cleanup()
        {
        }

        protected async Task<TResponse> GetAsync<TResponse>(string uri)
        {
            var response = await _httpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<TResponse>(response);
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request)
        {
            var response = await _httpClient.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}