using CurrencyDataLibrary.DataSerialization.Abstr;

namespace CurrencyDataLibrary.DataSerialization.Impl
{
    public class DataSerializerFactory
    {
        public static IDataSerializer GetSerializer(string format)
        {
            return format switch
            {
                "csv" => new CsvDataSerializer(),
                "json" => new JsonDataSerializer(),
                "xml" => new XmlDataSerializer(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
