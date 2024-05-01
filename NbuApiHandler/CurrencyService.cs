using System;
using System.Threading.Tasks;

namespace CurrencyDataService
{
    public partial class CurrencyService : ServiceBase
    {
        private IConfiguration _config;
        private JsonFetcher _jsonFetcher;
        private JsonToCurrencyDataProcessor _processor;
        private CurrencyAPIClient _currencyAPIClient;

        public CurrencyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(System.IO.Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _config = builder.Build();

            int delayInSeconds = int.Parse(_config["DelayInSeconds"]);
            string dataFormat = _config["DataFormat"];
            string dataStoragePath = _config["DataStoragePath"];

            _jsonFetcher = new JsonFetcher(
               new HttpClientWrapper(),
               new CacheManager<string>(
                   new TimeSpan(0, 10, 0)
               ),
               "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json"
           );
            _processor = new JsonToCurrencyDataProcessor();
            _currencyAPIClient = new CurrencyAPIClient(_jsonFetcher, _processor);

            Task.Run(async () =>
            {
                while (true)
                {
                    var currencyData = await _currencyAPIClient.FetchCurrencyData();
                    DataSaver.SaveToFile(currencyData, dataFormat, dataStoragePath);
                    await Task.Delay(delayInSeconds * 1000);
                }
            });
        }

        protected override void OnStop()
        {
            // Cleanup logic if needed
        }
    }
}
