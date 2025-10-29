using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface ICacheService
    {
        Task<T> GetOrAddAsync<T>(string cacheKey, Func<Task<T>> fetchFunction, TimeSpan? absoluteExpiration = null);
        void Remove(string cacheKey);
    }
}