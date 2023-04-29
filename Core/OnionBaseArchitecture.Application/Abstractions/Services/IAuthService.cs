using OnionBaseArchitecture.Application.Abstractions.Services.Authentications;

namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentication, IInternalAuthentication
    {
        Task PasswordResetAsnyc(string Email);
        
        Task<bool> VerifyResetTokenAsync(string ResetToken, string UserId);
    }
}
