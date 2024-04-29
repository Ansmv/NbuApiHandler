using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataProcessing
{
    public interface ICurrencyDataProcessor
    {
        List<CurrencyData> ProcessJson(string json);
    }
}
