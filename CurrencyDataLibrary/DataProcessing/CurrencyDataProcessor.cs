using CurrencyDataLibrary.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CurrencyDataLibrary.DataProcessing
{
    public class CurrencyDataProcessor : ICurrencyDataProcessor
    {
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
                        Id = jsonCurrency.GetProperty("r030").GetInt32(),
                        CurrencyCode = jsonCurrency.GetProperty("cc").GetString(),
                        FullName = jsonCurrency.GetProperty("txt").GetString(),
                        Rate = (decimal)jsonCurrency.GetProperty("rate").GetDouble(),
                        Timestamp = DateTime.Parse(jsonCurrency.GetProperty("exchangedate").GetString())
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
