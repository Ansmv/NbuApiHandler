using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.ApiCommunication.Abstr
{
    public interface IJsonToCurrencyDataProcessor
    {
        List<CurrencyData> ProcessJson(string json);
    }
}
