using CurrencyDataLibrary.ApiCommunication.Abstr;
using System.Runtime.Caching;

namespace CurrencyDataLibrary.ApiCommunication.Impl
{
    public class CacheManager<T> : ICacheManager<T>
    {
        private readonly ObjectCache cache;
        private readonly TimeSpan cacheDuration;

        public CacheManager(TimeSpan cacheDuration)
        {
            cache = MemoryCache.Default;
            this.cacheDuration = cacheDuration;
        }

        public async Task<T> GetOrAdd(string key, Func<Task<T>> valueFactory)
        {
            var cachedValue = (T)cache.Get(key);
            if (cachedValue != null)
            {
                return cachedValue;
            }

            var value = await valueFactory();

            if (value != null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(cacheDuration);
                cache.Set(key, value, policy);
            }
            return value;
        }
    }
}
