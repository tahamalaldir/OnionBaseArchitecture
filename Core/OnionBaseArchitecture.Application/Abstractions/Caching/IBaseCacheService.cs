namespace OnionBaseArchitecture.Application.Abstractions.Caching
{
    public interface IBaseCacheService
    {
        Task RemoveAsync(string key);

        Task AddAsync<T>(string key, T obj, int timeOutMinute = 60);

        Task<T> GetAsync<T>(string key);

        Task<bool> KeyExistsAsync(string key);

        List<string> GetKeys(string pattern);
    }
}
