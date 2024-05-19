using CurrencyDataLibrary.DataSaving.Abstr;

namespace CurrencyDataLibrary.DataSaving.Impl
{
    public class SystemClock : ISystemClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
