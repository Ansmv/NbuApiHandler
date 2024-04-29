namespace CurrencyDataLibrary
{
    public class DataSerializerFactory
    {
        public IDataSerializer GetSerializer(string format)
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
