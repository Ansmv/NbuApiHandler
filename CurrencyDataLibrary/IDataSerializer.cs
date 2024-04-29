namespace CurrencyDataLibrary
{
    public interface IDataSerializer
    {
        string Serialize(List<CurrencyData> currencyData);
    }
}
