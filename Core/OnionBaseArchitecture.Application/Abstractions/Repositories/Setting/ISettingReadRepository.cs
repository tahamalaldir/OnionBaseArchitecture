namespace OnionBaseArchitecture.Application.Abstractions.Repositories.Setting
{
    public interface ISettingReadRepository : IReadRepository<Domain.Entities.Setting>
    {
        Task<Domain.Entities.Setting> GetBySystemNameAsync(string SystemName);
    }
}
