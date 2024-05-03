namespace CurrencyDataWorkerService.Extensions
{
    public static class ConfigExtensions
    {
        public static IConfiguration LoadConfiguration(string basePath, string fileName) =>
            new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(fileName, optional: false, reloadOnChange: true)
                .Build();
    }
}
