using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.ApiCommunication
{
    public interface IJsonToCurrencyDataProcessor
    {
        List<CurrencyData> ProcessJson(string json);
    }
}
