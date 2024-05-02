using CurrencyDataLibrary.ApiCommunication;

namespace CurrencyDataLibrary.Tests
{
    [TestClass]
    public class JsonFetcherTests
    {
        [TestMethod]
        public async Task FetchJsonFromApi_Success()
        {
            var mockHttpClient = new MockHttpClientWrapper();
            var mockCacheManager = new MockCacheManager();
            var jsonFetcher = new JsonFetcher(mockHttpClient, mockCacheManager, "https:\\dummy.com");
            string json = await jsonFetcher.FetchJsonFromApi();
            Assert.IsNotNull(json);
            Assert.AreEqual("Mock JSON Response", json);
        }
    }

    internal class MockCacheManager : ICacheManager<string>
    {
        public Dictionary<string, string> Cache { get; set; } = [];
        public Task<string> GetOrAdd(string key, Func<Task<string>> valueFactory)
        {
            if (Cache.ContainsKey(key))
            {
                return Task.FromResult(Cache[key]);
            }
            else
            {
                string value = valueFactory().Result;
                Cache[key] = value;
                return Task.FromResult(value);
            }
        }
    }

    internal class MockHttpClientWrapper : IHttpClientWrapper
    {
        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("Mock JSON Response")
            };
            return Task.FromResult(response);
        }
    }
}