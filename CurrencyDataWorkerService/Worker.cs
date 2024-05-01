using CurrencyDataLibrary;
using CurrencyDataLibrary.ApiCommunication;
using CurrencyDataLibrary.Models;
using Microsoft.Extensions.Options;

namespace CurrencyDataWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private CurrencyAPIClient _currencyAPIClient;
        private int _delayInSeconds;
        private string _dataFormat;
        private string _dataStoragePath;
        private readonly IOptionsMonitor<AppSettings> _appSettingsMonitor;
        public Worker(ILogger<Worker> logger, CurrencyAPIClient currencyAPIClient, IOptionsMonitor<AppSettings> appSettingsMonitor)
        {
            _logger = logger;
            _currencyAPIClient = currencyAPIClient;
            _appSettingsMonitor = appSettingsMonitor;
            UpdateConfig();
            _appSettingsMonitor.OnChange(_ => UpdateConfig());
        }
        private void UpdateConfig()
        {
            var appSettings = _appSettingsMonitor.CurrentValue;
            _delayInSeconds = appSettings.DelayInSeconds;
            _dataFormat = appSettings.DataFormat;
            _dataStoragePath = appSettings.DataStoragePath;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<CurrencyData> currencyData = await _currencyAPIClient.FetchCurrencyData();
                DataSaver.SaveToFile(currencyData, _dataFormat, _dataStoragePath);
                _logger.LogInformation($"Yes, something is definitely working! {DateTimeOffset.Now} {currencyData[0].FullName}");
                await Task.Delay(_delayInSeconds * 1000, stoppingToken);
            }
        }
    }
}
