namespace OnionBaseArchitecture.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<Tuple<bool, string, DTOs.Token>> LoginAsync(string UsernameOrEmail, string Password);

        Task<string> RefreshTokenLoginAsync(string RefreshToken);
    }
}
