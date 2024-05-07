using CurrencyDataLibrary.ApiCommunication;
using CurrencyDataLibrary.DataSaving;
using CurrencyDataWorkerService.DataSaving;
using CurrencyDataWorkerService.Settings;
using Microsoft.Extensions.Options;

namespace CurrencyDataWorkerService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const int CacheDurationMinutes = 10;

        public static IServiceCollection AddJsonFetcherService(this IServiceCollection services)
        {

            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
            services.AddSingleton<ICacheManager<string>>(provider =>
            {
                return new CacheManager<string>(TimeSpan.FromMinutes(CacheDurationMinutes));
            });
            services.AddTransient<IJsonFetcher, JsonFetcher>(provider =>
            {
                var httpClientWrapper = provider.GetRequiredService<IHttpClientWrapper>();
                var cacheManager = provider.GetRequiredService<ICacheManager<string>>();
                var currencyApiSettings = provider.GetRequiredService<IOptionsMonitor<CurrencyApiSettings>>().CurrentValue;

                return new JsonFetcher(
                    httpClientWrapper,
                    cacheManager,
                    currencyApiSettings.ApiUrl
                );
            });
            return services;
        }

        public static IServiceCollection AddDataSaver(this IServiceCollection services)
        {
            services.AddSingleton<ISystemClock, SystemClock>()
                .AddSingleton<IFileNameGenerator, FileNameGenerator>()
                .AddSingleton<IPathHandler, PathHandler>()
                .AddSingleton<IDataSaverFactory, DataSaverFactory>();
            return services;
        }
    }
}
