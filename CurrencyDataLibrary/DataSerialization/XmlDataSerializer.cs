using CurrencyDataLibrary.Models;
using System.Text;
using System.Xml.Serialization;

namespace CurrencyDataLibrary.DataSerialization
{
    public class XmlDataSerializer : IDataSerializer
    {
        public string FileExtension => ".xml";

        public string Serialize(List<CurrencyData> currencyData)
        {
            var serializer = new XmlSerializer(typeof(List<CurrencyData>));
            using var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, currencyData);
            return Encoding.UTF8.GetString(memoryStream.ToArray());

        }
    }
}
