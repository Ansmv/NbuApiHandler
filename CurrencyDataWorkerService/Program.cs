using CurrencyDataLibrary.ApiCommunication.Abstr;
using CurrencyDataLibrary.ApiCommunication.Impl;
using CurrencyDataWorkerService;
using CurrencyDataWorkerService.Extensions;
using CurrencyDataWorkerService.Settings;

IConfiguration config = ConfigExtensions.LoadConfiguration(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService();
builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));
builder.Services.Configure<CurrencyApiSettings>(config.GetSection("CurrencyApiSettings"));

builder.Services
    .AddJsonFetcherService()
    .AddTransient<IJsonToCurrencyDataProcessor, JsonToCurrencyDataProcessor>()
    .AddTransient<CurrencyAPIClient>()
    .AddDataSaver();

builder.Services.AddHostedService<CurrencyApiHandler>();

var host = builder.Build();
host.Run();
