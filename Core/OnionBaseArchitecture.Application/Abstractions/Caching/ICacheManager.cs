using System.Reflection;

namespace OnionBaseArchitecture.Application.Abstractions.Caching
{
    public interface ICacheManager
    {
        Task<T> GetSetAsync<T>(MethodBase method, Func<T> action, string additionalCacheKey = null, int timeoutMinute = 10);

        Task<T> GetSetAsync<T>(string key, Func<T> action, string additionalCacheKey = null, int timeoutMinute = 10);

        Task<T> GetAsync<T>(string key, string additionalCacheKey = null);

        Task SetAsync<T>(string key, T data, string additionalCacheKey = "", int timeoutMinute = 10);

        Task ClearAsync(MethodBase method, string additionalCacheKey = "");

        Task ClearAsync(string key = "");
    }
}
