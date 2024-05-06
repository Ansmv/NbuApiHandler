using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataSerialization
{
    public interface IDataSerializer
    {
        string Serialize(List<CurrencyData> currencyData);
        string GetFileExtension();
    }
}

