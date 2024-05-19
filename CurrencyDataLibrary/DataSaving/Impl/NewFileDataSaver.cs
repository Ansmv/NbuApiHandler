using CurrencyDataLibrary.DataSaving.Abstr;
using CurrencyDataLibrary.DataSerialization.Abstr;
using CurrencyDataLibrary.DataSerialization.Impl;
using CurrencyDataLibrary.Models;
using System.Text;

namespace CurrencyDataLibrary.DataSaving.Impl
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
            File.WriteAllText(fullPath, serializedData, Encoding.Unicode);
        }
    }
}
