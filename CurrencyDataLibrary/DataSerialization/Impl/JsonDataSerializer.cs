using CurrencyDataLibrary.DataSerialization.Abstr;
using CurrencyDataLibrary.Models;
using System.Text.Json;

namespace CurrencyDataLibrary.DataSerialization.Impl
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

        public string GetFileExtension()
        {
            return ".json";
        }
    }
}
