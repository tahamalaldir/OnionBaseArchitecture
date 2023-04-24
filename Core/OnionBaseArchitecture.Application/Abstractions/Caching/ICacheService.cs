namespace OnionBaseArchitecture.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        Task<bool> ClearCache();

        Task<bool> SetCache();

        Task<string> StringControl(string Name);
    }
}
