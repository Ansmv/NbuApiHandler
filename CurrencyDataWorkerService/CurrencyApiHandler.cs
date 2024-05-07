using CurrencyDataLibrary.ApiCommunication;
using CurrencyDataLibrary.DataSaving;
using CurrencyDataLibrary.Models;
using CurrencyDataWorkerService.DataSaving;
using CurrencyDataWorkerService.Settings;
using Microsoft.Extensions.Options;

namespace CurrencyDataWorkerService
{
    public class CurrencyApiHandler : BackgroundService
    {
        private readonly ILogger<CurrencyApiHandler> _logger;
        private CurrencyAPIClient _currencyAPIClient;
        private int _delayInSeconds;
        private string? _dataFormat;
        private string? _dataStoragePath;
        private string? _dataSaverType;
        private readonly IOptionsMonitor<AppSettings> _appSettingsMonitor;
        private readonly IDataSaverFactory _dataSaverFactory;
        private IDataSaver _dataSaver;

        public CurrencyApiHandler(
            ILogger<CurrencyApiHandler> logger,
            CurrencyAPIClient currencyAPIClient,
            IOptionsMonitor<AppSettings> appSettingsMonitor,
            IDataSaverFactory dataSaverFactory)
        {
            _logger = logger;
            _currencyAPIClient = currencyAPIClient;
            _appSettingsMonitor = appSettingsMonitor;
            _dataSaverFactory = dataSaverFactory;
            UpdateConfig();
            _appSettingsMonitor.OnChange(_ => UpdateConfig());
        }

        private void UpdateConfig()
        {
            var appSettings = _appSettingsMonitor.CurrentValue;
            _delayInSeconds = Math.Max(5, appSettings.DelayInSeconds);
            _dataFormat = appSettings.DataFormat
                ?? throw new ArgumentNullException(nameof(appSettings.DataFormat));
            _dataStoragePath = appSettings.DataStoragePath
                ?? throw new ArgumentNullException(nameof(appSettings.DataStoragePath));
            _dataSaverType = appSettings.DataSaverType
                ?? throw new ArgumentNullException(nameof(appSettings.DataSaverType));
            _dataSaver = _dataSaverFactory.CreateDataSaver(_dataSaverType);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Fetching currency data...");
                    List<CurrencyData> currencyData = await _currencyAPIClient.FetchCurrencyData();
                    _logger.LogInformation("Currency data fetched successfully.");

                    _logger.LogInformation("Saving currency data to file...");
                    _dataSaver.SaveToFile(currencyData, _dataFormat, _dataStoragePath);
                    _logger.LogInformation("Currency data saved to file successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching or saving currency data.");
                }

                await Task.Delay(_delayInSeconds * 1000, stoppingToken);
            }
        }
    }
}
