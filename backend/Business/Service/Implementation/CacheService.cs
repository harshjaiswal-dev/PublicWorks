using Business.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Business.Service.Implementation
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrAddAsync<T>(string cacheKey, Func<Task<T>> fetchFunction, TimeSpan? absoluteExpiration = null)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (!_cache.TryGetValue(cacheKey, out T cachedValue))
            {
                Console.WriteLine($"[CacheService] Cache miss for key '{cacheKey}' — fetching from DB...");

                // Fetch data from DB (using provided function)
                cachedValue = await fetchFunction();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(absoluteExpiration ?? TimeSpan.FromMinutes(30))
                    .SetPriority(CacheItemPriority.High);

                _cache.Set(cacheKey, cachedValue, cacheOptions);
            }
            else
            {
                Console.WriteLine($"[CacheService] Cache hit for key '{cacheKey}' — returning from memory...");
            }

            stopwatch.Stop();
            Console.WriteLine($"[CacheService] Time taken: {stopwatch.ElapsedMilliseconds} ms");

            return cachedValue;
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
            Console.WriteLine($"[CacheService] Cache key '{cacheKey}' removed manually.");
        }
    }
}
