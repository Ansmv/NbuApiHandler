using CurrencyDataLibrary;
using CurrencyDataLibrary.ApiCommunication;
using CurrencyDataLibrary.Models;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();
int delayInSeconds = int.Parse(config["DelayInSeconds"]);
string dataFormat = config["DataFormat"];
string dataStoragePath = config["DataStoragePath"];

JsonFetcher jsonFetcher = new(
           new HttpClientWrapper(),
           new CacheManager<string>(
               new TimeSpan(0, 10, 0)
           ),
           "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json"
       );
JsonToCurrencyDataProcessor processor = new();
CurrencyAPIClient currencyAPIClient = new(jsonFetcher, processor);

while (true)
{
    List<CurrencyData> currencyData = await currencyAPIClient.FetchCurrencyData();
    DataSaver.SaveToFile(currencyData, dataFormat, dataStoragePath);
    await Task.Delay(delayInSeconds * 1000);
}

