using System.Text.Json;
using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataSerialization
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string Serialize(List<CurrencyData> currencyData)
        {
            return JsonSerializer.Serialize(currencyData, BuildSerializerSettings());
        }
        private static JsonSerializerOptions BuildSerializerSettings()
        {
            var settings = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return settings;
        }
    }
}
