using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<User> GetByUsernameOrEmailAndPassword(string UsernameOrEmail, string Password);

        Task<bool> UpdateUserRefreshTokenByUserId(string UserId, string RefreshToken);

        Task<Tuple<bool, string>> CreateUserAsync(string Email, string Username, string Name, string Surname, string PhoneNumber, string Password);
    }
}
