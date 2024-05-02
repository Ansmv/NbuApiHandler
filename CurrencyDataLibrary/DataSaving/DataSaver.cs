using CurrencyDataLibrary.DataSerialization;
using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataSaving
{
    public class DataSaver : IDataSaver
    {
        private readonly IPathHandler _pathHandler;

        public DataSaver(IPathHandler pathHandler)
        {
            _pathHandler = pathHandler;
        }
        public void SaveToFile(List<CurrencyData> currencyData, string format, string folderPath)
        {
            IDataSerializer serializer = DataSerializerFactory.GetSerializer(format);
            string serializedData = serializer.Serialize(currencyData);
            string fileExtension = serializer.FileExtension;

            string fullPath = _pathHandler.GetFullPath(folderPath, fileExtension);
            EnsureDirectoryExists(folderPath);
            File.WriteAllText(fullPath, serializedData);
        }
        private static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
