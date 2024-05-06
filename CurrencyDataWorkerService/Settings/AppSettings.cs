namespace CurrencyDataWorkerService.Settings
{
    public class AppSettings
    {
        public int DelayInSeconds { get; set; }
        public required string DataFormat { get; set; }
        public required string DataStoragePath { get; set; }
        public bool CreateNewFile { get; set; }
    }
}
