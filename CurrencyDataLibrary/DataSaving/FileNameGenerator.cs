namespace CurrencyDataLibrary.DataSaving
{
    public class FileNameGenerator : IFileNameGenerator
    {
        private readonly ISystemClock _clock;
        public FileNameGenerator(ISystemClock clock)
        {
            _clock = clock;
        }

        public string GenerateFileName(string fileExtension)
        {
            return $"{_clock.UtcNow:yyyy-MM-dd_HH-mm-ss}_{GetFileDescription(fileExtension)}{fileExtension}";
        }
        private static string GetFileDescription(string format)
        {
            return format switch
            {
                ".csv" => "currency_data",
                ".json" => "currency_data",
                ".xml" => "currency_data",
                _ => throw new NotImplementedException()
            };
        }
    }
}
