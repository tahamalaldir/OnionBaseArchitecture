namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface ISettingService
    {
        Task<string> GetSettingBySystemNameAsync(string SystemName);
    }
}
