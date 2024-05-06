using System.Diagnostics;

namespace CurrencyDataLibrary.ApiCommunication
{
    public class JsonFetcher : IJsonFetcher
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly ICacheManager<string> _cacheManager;
        private readonly string _apiEndpoint;

        public JsonFetcher(IHttpClientWrapper httpClient, ICacheManager<string> cacheManager, string apiEndpoint)
        {
            _httpClient = httpClient;
            _cacheManager = cacheManager;
            _apiEndpoint = apiEndpoint;
        }

        public async Task<string> FetchJsonFromApi()
        {
            return await _cacheManager.GetOrAdd(_apiEndpoint, FetchJsonFromApiAsync);
        }

        private async Task<string> FetchJsonFromApiAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_apiEndpoint);
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
}
