using System.Diagnostics;
using System.Runtime.Caching;

namespace CurrencyDataLibrary.ApiCommunication
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
    public interface ICacheManager<T>
    {
        Task<T> GetOrAdd(string key, Func<Task<T>> valueFactory);
    }

    public class JsonFetcher : IJsonFetcher
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly ICacheManager<string> _cacheManager;
        private const string ApiEndpoint = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
        public JsonFetcher(IHttpClientWrapper httpClient, ICacheManager<string> cacheManager)
        {
            _httpClient = httpClient;
            _cacheManager = cacheManager;
        }
        public async Task<string> FetchJsonFromApi()
        {
            return await _cacheManager.GetOrAdd(ApiEndpoint, FetchJsonFromApiAsync);
        }

        private async Task<string> FetchJsonFromApiAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Trace.TraceError($"Failed to fetch currency data. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri);
        }
    }
    public class CacheManager<T> : ICacheManager<T>
    {
        private readonly ObjectCache cache;
        private readonly TimeSpan cacheDuration;

        public CacheManager(TimeSpan cacheDuration)
        {
            cache = MemoryCache.Default;
            this.cacheDuration = cacheDuration;
        }

        public async Task<T> GetOrAdd(string key, Func<Task<T>> valueFactory)
        {
            var cachedValue = (T)cache.Get(key);
            if (cachedValue != null)
            {
                return cachedValue;
            }

            var value = await valueFactory();

            if (value != null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(cacheDuration);
                cache.Set(key, value, policy);
            }
            return value;
        }
    }
}
