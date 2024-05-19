using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataSerialization.Abstr
{
    public interface IDataSerializer
    {
        string Serialize(List<CurrencyData> currencyData);
        string GetFileExtension();
    }
}

