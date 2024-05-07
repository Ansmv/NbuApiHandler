using CurrencyDataLibrary.DataSerialization;
using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataSaving
{
    public class NewFileDataSaver : IDataSaver
    {
        private readonly IPathHandler _pathHandler;

        public NewFileDataSaver(IPathHandler pathHandler)
        {
            _pathHandler = pathHandler;
        }

        public void SaveToFile(List<CurrencyData> currencyData, string format, string folderPath)
        {
            IDataSerializer serializer = DataSerializerFactory.GetSerializer(format);
            string serializedData = serializer.Serialize(currencyData);
            string fileExtension = serializer.GetFileExtension();

            string fullPath = _pathHandler.GetFullPath(folderPath, fileExtension);
            folderPath = Path.GetDirectoryName(fullPath);
            _pathHandler.EnsureDirectoryExists(folderPath);
            File.WriteAllText(fullPath, serializedData);
        }
    }
}
