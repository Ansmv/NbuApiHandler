namespace CurrencyDataLibrary
{
    public interface IDataSerializerFactory
    {
        IDataSerializer GetSerializer(string format);
    }
}