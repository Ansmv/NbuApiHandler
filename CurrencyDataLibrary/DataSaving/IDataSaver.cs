using CurrencyDataLibrary.Models;

namespace CurrencyDataLibrary.DataSaving
{
    public interface IDataSaver
    {
        public void SaveToFile(List<CurrencyData> currencyData, string format, string folderPath);
    }
}
