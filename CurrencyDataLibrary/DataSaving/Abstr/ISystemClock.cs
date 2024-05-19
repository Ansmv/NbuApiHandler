namespace CurrencyDataLibrary.DataSaving.Abstr
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
