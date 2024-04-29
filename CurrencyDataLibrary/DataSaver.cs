using CurrencyDataLibrary.DataSerialization;
using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary
{
    public static class DataSaver
    {
        public static void SaveToFile(List<CurrencyData> currencyData, string format, string filePath)
        {
            IDataSerializer serializer = DataSerializerFactory.GetSerializer(format);
            string serializedData = serializer.Serialize(currencyData);
            string fileExtension = serializer.FileExtension;
            string fileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_{GetFileDescription(format)}";
            string fullFileName = $"{fileName}{fileExtension}";
            string fullPath = Path.Combine(filePath, fullFileName);
            File.WriteAllText(fullPath, serializedData);
        }

        private static object GetFileDescription(string format)
        {
            return format switch
            {
                "csv" => "currency_data",
                "json" => "currency_data",
                "xml" => "currency_data",
                _ => throw new NotImplementedException()
            };
        }
    }
}
