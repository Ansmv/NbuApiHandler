using CurrencyDataLibrary.ApiCommunication;
using CurrencyDataLibrary.Tests.MockImplementations;

namespace CurrencyDataLibrary.Tests
{
    [TestClass]
    public class JsonFetcherTests
    {
        [TestMethod]
        public async Task FetchJsonFromApiSuccess()
        {
            var mockHttpClient = new MockHttpClientWrapper();
            var mockCacheManager = new MockCacheManager();
            var jsonFetcher = new JsonFetcher(mockHttpClient, mockCacheManager, "https:\\dummy.com");
            string json = await jsonFetcher.FetchJsonFromApi();
            Assert.IsNotNull(json);
            Assert.AreEqual("Mock JSON Response", json);
        }
    }
}