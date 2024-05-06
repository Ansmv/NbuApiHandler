using CurrencyDataLibrary.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CurrencyDataLibrary.ApiCommunication
{
    public class JsonToCurrencyDataProcessor : IJsonToCurrencyDataProcessor
    {
        private const string IdPropertyName = "r030";
        private const string CurrencyCodePropertyName = "cc";
        private const string FullNamePropertyName = "txt";
        private const string RatePropertyName = "rate";
        private const string ExchangeDatePropertyName = "exchangedate";

        public List<CurrencyData> ProcessJson(string json)
        {
            var currencyDataList = new List<CurrencyData>();
            try
            {
                var jsonArray = JsonDocument.Parse(json).RootElement;
                foreach (var jsonCurrency in jsonArray.EnumerateArray())
                {
                    var currencyData = new CurrencyData
                    {
                        Id = jsonCurrency.GetProperty(IdPropertyName).GetInt32(),
                        CurrencyCode = jsonCurrency.GetProperty(CurrencyCodePropertyName).GetString(),
                        FullName = jsonCurrency.GetProperty(FullNamePropertyName).GetString(),
                        Rate = (decimal)jsonCurrency.GetProperty(RatePropertyName).GetDouble(),
                        Timestamp = DateTime.Parse(jsonCurrency.GetProperty(ExchangeDatePropertyName).GetString())
                    };
                    currencyDataList.Add(currencyData);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"An error occurred: {ex.Message}");
            }
            return currencyDataList;
        }
    }
}
