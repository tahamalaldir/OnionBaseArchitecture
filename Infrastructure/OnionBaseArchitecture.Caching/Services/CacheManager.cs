using OnionBaseArchitecture.Application.Abstractions.Caching;
using OnionBaseArchitecture.Application.Common;
using System.Reflection;

namespace OnionBaseArchitecture.Caching.Services
{
    public class CacheManager : ICacheManager
    {
        private readonly IBaseCacheService _cacheService;
        public CacheConfigs _cacheConfigs;

        public CacheManager(IBaseCacheService cacheService, CacheConfigs cacheConfigs)
        {
            _cacheService = cacheService;
            _cacheConfigs = cacheConfigs;
        }

        public async Task<T> GetSetAsync<T>(MethodBase method, Func<T> action, string additionalCacheKey = null, int timeoutMinute = 10)
        {
            if (!_cacheConfigs.EnableCache) return action();
            try
            {
                var cacheKey = $"{_cacheConfigs.CacheStartKey}:{method.Name}" + (!string.IsNullOrWhiteSpace(additionalCacheKey) ? ":" + additionalCacheKey : "");
                var cached = await _cacheService.GetAsync<T>(cacheKey);
                if (cached == null)
                {
                    var resp = action();
                    await _cacheService.AddAsync(cacheKey, resp, timeoutMinute);
                    return resp;
                }
                else return cached;
            }
            catch (Exception)
            {
                return action();
            }
        }

        public async Task<T> GetSetAsync<T>(string key, Func<T> action, string additionalCacheKey = null, int timeoutMinute = 10)
        {
            if (!_cacheConfigs.EnableCache) return action();
            try
            {
                var cacheKey = $"{_cacheConfigs.CacheStartKey}:{key}" + (!string.IsNullOrWhiteSpace(additionalCacheKey) ? ":" + additionalCacheKey : "");
                var cached = await _cacheService.GetAsync<T>(cacheKey);
                if (cached == null)
                {
                    var resp = action();
                    await _cacheService.AddAsync(cacheKey, resp, timeoutMinute);
                    return resp;
                }
                else return cached;
            }
            catch (Exception)
            {
                return action();
            }
        }

        public async Task<T> GetAsync<T>(string key, string additionalCacheKey = null)
        {
            if (!_cacheConfigs.EnableCache) return default(T);
            try
            {
                var cacheKey = $"{_cacheConfigs.CacheStartKey}:{key}" + (!string.IsNullOrWhiteSpace(additionalCacheKey) ? ":" + additionalCacheKey : "");
                var cached = await _cacheService.GetAsync<T>(cacheKey);
                if (cached == null)
                {
                    return default(T);
                }
                else return cached;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public async Task SetAsync<T>(string key, T data, string additionalCacheKey = "", int timeoutMinute = 10)
        {
            if (_cacheConfigs.EnableCache)
            {
                try
                {
                    var cacheKey = $"{_cacheConfigs.CacheStartKey}:{key}" + (!string.IsNullOrWhiteSpace(additionalCacheKey) ? ":" + additionalCacheKey : "");
                    await _cacheService.AddAsync<T>(cacheKey, data, timeoutMinute);
                }
                catch
                {
                }
            }
        }

        public async Task ClearAsync(MethodBase method, string additionalCacheKey = "")
        {
            if (_cacheConfigs.EnableCache)
            {
                var cacheKey = $"{_cacheConfigs.CacheStartKey}:{method.Name}" + (!string.IsNullOrWhiteSpace(additionalCacheKey) ? ":" + additionalCacheKey : "");
                await _cacheService.RemoveAsync(cacheKey);
            }
        }

        public async Task ClearAsync(string key = "")
        {
            if (_cacheConfigs.EnableCache)
            {
                var cacheKey = $"{_cacheConfigs.CacheStartKey}:{key}";
                await _cacheService.RemoveAsync(cacheKey);
            }
        }
    }
}
