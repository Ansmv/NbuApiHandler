namespace CurrencyDataLibrary.DataSaving
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
