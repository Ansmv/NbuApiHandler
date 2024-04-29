namespace CurrencyDataLibrary.DataSerialization
{
    public class DataSerializerFactory
    {
        public static IDataSerializer GetSerializer(string format)
        {
            return format switch
            {
                "scv" => new CsvDataSerializer(),
                "json" => new JsonDataSerializer(),
                "xml" => new XmlDataSerializer(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
