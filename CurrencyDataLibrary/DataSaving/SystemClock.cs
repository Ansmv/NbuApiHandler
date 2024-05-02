namespace CurrencyDataLibrary.DataSaving
{
    public class SystemClock : ISystemClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
