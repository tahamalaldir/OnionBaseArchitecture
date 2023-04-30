using Dapper;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories.EmailAccount;
using System.Data;

namespace OnionBaseArchitecture.Persistence.Repositories.EmailAccount
{
    public class EmailAccountReadRepository : ReadRepository<Domain.Entities.EmailAccount>, IEmailAccountReadRepository
    {
        public EmailAccountReadRepository(IConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public async Task<Domain.Entities.EmailAccount> GetBySystemNameAsync(string SystemName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("SystemName", SystemName);

            return await QeryFirstAsync(@"SELECT * FROM [dbo].[EmailAccount] WHERE IsDeleted = 0 AND IsActive = 1 AND SystemName = @SystemName", parameters, CommandType.Text);
        }
    }
}
