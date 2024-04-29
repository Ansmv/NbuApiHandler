using System.Diagnostics;

namespace CurrencyDataLibrary
{
    public class CurrencyAPIClient
    {
        private readonly IJsonFetcher _jsonFetcher;
        private readonly ICurrencyDataProcessor _currencyDataProcessor;
        public CurrencyAPIClient(IJsonFetcher jsonFetcher, ICurrencyDataProcessor currencyDataProcessor)
        {
            _jsonFetcher = jsonFetcher ?? throw new ArgumentNullException(nameof(jsonFetcher));
            _currencyDataProcessor = currencyDataProcessor ?? throw new ArgumentNullException(nameof(currencyDataProcessor));
        }
        public async Task<List<CurrencyData>> FetchCurrencyData()
        {
            try
            {
                var json = await _jsonFetcher.FetchJsonFromApi();
                if (json == null)
                {
                    Trace.TraceError("Failed to fetch JSON data from the API.");
                    return null;
                }
                var currencyDataList = _currencyDataProcessor.ProcessJson(json);
                return currencyDataList;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
