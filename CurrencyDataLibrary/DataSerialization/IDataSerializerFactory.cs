namespace CurrencyDataLibrary.DataSerialization
{
    public interface IDataSerializerFactory
    {
        IDataSerializer GetSerializer(string format);
    }
}