using System.Diagnostics;

namespace CurrencyDataLibrary
{
    public class JsonFetcher : IJsonFetcher
    {
        private readonly HttpClient _httpClient;
        private const string ApiEndpoint = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
        public JsonFetcher()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> FetchJsonFromApi()
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
}
