using CurrencyDataLibrary.DataSaving;

namespace CurrencyDataWorkerService.DataSaving
{
    public class DataSaverFactory : IDataSaverFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DataSaverFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataSaver CreateDataSaver(string dataSaverType)
        {
            var pathHandler = _serviceProvider.GetService<IPathHandler>();

            return dataSaverType.ToLower() switch
            {
                "multiplefiles" => new NewFileDataSaver(pathHandler),
                "singlefile" => new UpdateExistingFileDataSaver(pathHandler),
                _ => throw new NotImplementedException()
            };
        }
    }
}
