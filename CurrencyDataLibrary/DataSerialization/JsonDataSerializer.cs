﻿using CurrencyDataLibrary.Models;
using System.Text.Json;

namespace CurrencyDataLibrary.DataSerialization
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string FileExtension => ".json";

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
