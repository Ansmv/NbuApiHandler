namespace CurrencyDataLibrary.ApiCommunication
{
    public interface ICacheManager<T>
    {
        Task<T> GetOrAdd(string key, Func<Task<T>> valueFactory);
    }
}
