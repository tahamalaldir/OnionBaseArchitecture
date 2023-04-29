using Dapper;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories.Setting;
using System.Data;

namespace OnionBaseArchitecture.Persistence.Repositories.Setting
{
    public class SettingReadRepository : ReadRepository<Domain.Entities.Setting>, ISettingReadRepository
    {
        public SettingReadRepository(IConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public async Task<Domain.Entities.Setting> GetBySystemNameAsync(string SystemName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("SystemName", SystemName);

            return await QeryFirstAsync("SELECT * FROM [dbo].[Setting] WHERE IsDeleted = 0 AND IsActive = 1 AND SystemName = @SystemName", parameters, CommandType.Text);
        }
    }
}
