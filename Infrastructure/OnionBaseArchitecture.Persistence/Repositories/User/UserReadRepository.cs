using Dapper;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories.User;
using System.Data;

namespace OnionBaseArchitecture.Persistence.Repositories.User
{
    public class UserReadRepository : ReadRepository<Domain.Entities.User>, IUserReadRepository
    {
        public UserReadRepository(IConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public async Task<Domain.Entities.User> GetUsernameOrEmail(string Username, string Email)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Username", Username);
            parameters.Add("Email", Email);

            return await QeryFirstAsync("SELECT [Id], [Username], [Email], [Name], [Surname], [PhoneNumber], [PasswordExpireDate], [Token], [TokenExpireDate], [EMailApproved], [EMailToken],[EMailTokenExpireDate],[PhoneApproved], [PhoneToken], [PhoneTokenExpireDate], [IsApproved], [CreatedDate], [IsActive], [IsDeleted], [CreatedByUserId] FROM [dbo].[Users] WHERE (Username = @Username OR Email = @Email) AND IsDeleted = 0", parameters, CommandType.Text);
        }

        public async Task<Domain.Entities.User> GetUsernameOrEmailAndPassword(string UsernameOrEmail, string Password)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Username", UsernameOrEmail);
            parameters.Add("Password", Password);

            return await QeryFirstAsync("SELECT [Id], [Username], [Email], [Name], [Surname], [PhoneNumber], [PasswordExpireDate], [Token], [TokenExpireDate], [EMailApproved], [EMailToken],[EMailTokenExpireDate],[PhoneApproved], [PhoneToken], [PhoneTokenExpireDate], [IsApproved], [CreatedDate], [IsActive], [IsDeleted], [CreatedByUserId] FROM [dbo].[Users] WHERE (Email = @Username OR Username = @Username) AND Password = @Password AND IsDeleted = 0", parameters, CommandType.Text);
        }
    }
}
