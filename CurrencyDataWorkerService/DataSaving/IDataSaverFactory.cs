using CurrencyDataLibrary.DataSaving.Abstr;

namespace CurrencyDataWorkerService.DataSaving
{
    public interface IDataSaverFactory
    {
        IDataSaver CreateDataSaver(string dataSaverType);
    }
}
