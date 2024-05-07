using CurrencyDataLibrary.DataSerialization;
using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataSaving
{
    public class UpdateExistingFileDataSaver : IDataSaver
    {
        private readonly IPathHandler _pathHandler;

        public UpdateExistingFileDataSaver(IPathHandler pathHandler)
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
            DeletePreviousFiles(folderPath);
            File.WriteAllText(fullPath, serializedData);
        }

        private static void DeletePreviousFiles(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
