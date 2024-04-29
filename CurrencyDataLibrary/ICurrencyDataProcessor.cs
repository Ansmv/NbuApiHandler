namespace CurrencyDataLibrary
{
    public interface ICurrencyDataProcessor
    {
        List<CurrencyData> ProcessJson(string json);
    }
}
