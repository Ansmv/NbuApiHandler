using CurrencyDataLibrary.DataSaving;

namespace CurrencyDataWorkerService.DataSaving
{
    public interface IDataSaverFactory
    {
        IDataSaver CreateDataSaver(string dataSaverType);
    }
}
