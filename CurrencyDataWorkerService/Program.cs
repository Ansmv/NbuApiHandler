using CurrencyDataLibrary.ApiCommunication;
using CurrencyDataWorkerService;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
var configBuilder = new ConfigurationBuilder();
configBuilder.SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration config = configBuilder.Build();
builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));
builder.Services.Configure<CurrencyApiSettings>(config.GetSection("CurrencyApiSettings"));
builder.Services.AddTransient<IJsonFetcher, JsonFetcher>((provider) =>
{
    var currencyApiSettings = provider.GetRequiredService<IOptionsMonitor<CurrencyApiSettings>>().CurrentValue;
    return new(
               new HttpClientWrapper(),
               new CacheManager<string>(
                   new TimeSpan(0, 10, 0)
               ),
               currencyApiSettings.ApiUrl
           );
});
builder.Services.AddTransient<IJsonToCurrencyDataProcessor, JsonToCurrencyDataProcessor>();
builder.Services.AddTransient<CurrencyAPIClient>();

var host = builder.Build();
host.Run();
