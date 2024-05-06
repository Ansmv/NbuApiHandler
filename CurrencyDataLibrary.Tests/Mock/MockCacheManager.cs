using CurrencyDataLibrary.ApiCommunication;

namespace CurrencyDataLibrary.Tests.MockImplementations
{
    public class MockCacheManager : ICacheManager<string>
    {
        public Dictionary<string, string> Cache { get; set; } = [];
        public Task<string> GetOrAdd(string key, Func<Task<string>> valueFactory)
        {
            if (Cache.ContainsKey(key))
            {
                return Task.FromResult(Cache[key]);
            }
            else
            {
                string value = valueFactory().Result;
                Cache[key] = value;
                return Task.FromResult(value);
            }
        }
    }
}
