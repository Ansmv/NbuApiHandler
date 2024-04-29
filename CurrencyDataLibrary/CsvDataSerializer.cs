﻿using System.Text;

namespace CurrencyDataLibrary
{
    public class CsvDataSerializer : IDataSerializer
    {
        public CsvDataSerializer()
        {
        }

        public string Serialize(List<CurrencyData> currencyData)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id,CurrencyCode,FullName,Rate,Timestamp");
            foreach (var currency in currencyData)
            {
                sb.AppendLine(ToCsvString(currency));
            }
            return sb.ToString();
        }
        private string ToCsvString(CurrencyData currencyData)
        {
            string[] result = [
                currencyData.Id.ToString(),
                currencyData.CurrencyCode.ToString(),
                currencyData.FullName.ToString(),
                currencyData.Rate.ToString(),
                currencyData.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")
                ];
            return string.Join(",", result);
        }
    }

}
