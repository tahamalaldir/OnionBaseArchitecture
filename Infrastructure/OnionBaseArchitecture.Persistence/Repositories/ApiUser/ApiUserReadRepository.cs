using Dapper;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories.ApiUser;
using System.Data;

namespace OnionBaseArchitecture.Persistence.Repositories.ApiUser
{
    public class ApiUserReadRepository : ReadRepository<Domain.Entities.ApiUser>, IApiUserReadRepository
    {
        public ApiUserReadRepository(IConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public async Task<Domain.Entities.ApiUser> GetByUsernameAndPasswordAsync(string Username, string Password)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Username", Username);
            parameters.Add("Password", Password);

            return await QeryFirstAsync(@"SELECT * FROM [dbo].[ApiUser] WHERE IsDeleted = 0 AND IsActive = 1 AND Username = @Username AND Password = @Password", parameters, CommandType.Text);
        }
    }
}
