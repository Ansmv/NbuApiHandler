namespace CurrencyDataLibrary.ApiCommunication.Abstr
{
    public interface ICacheManager<T>
    {
        Task<T> GetOrAdd(string key, Func<Task<T>> valueFactory);
    }
}
