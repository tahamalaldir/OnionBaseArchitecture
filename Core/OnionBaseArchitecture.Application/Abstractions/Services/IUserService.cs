using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<User> GetByUsernameOrEmailAndPassword(string UsernameOrEmail, string Password);

        Task<bool> UpdateUserRefreshTokenByUserId(string UserId, string RefreshToken);
    }
}
