using OnionBaseArchitecture.Application.Abstractions.Repositories.Setting;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Persistence.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingReadRepository _repository;

        public SettingService(ISettingReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetSettingBySystemNameAsync(string SystemName)
        {
            Setting setting = await _repository.GetBySystemNameAsync(SystemName);
            if (setting != null)
                return setting.Value;

            return string.Empty;
        }
    }
}
