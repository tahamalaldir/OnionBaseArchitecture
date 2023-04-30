namespace OnionBaseArchitecture.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<Tuple<bool, string, DTOs.Token>> LoginAsync(string UsernameOrEmail, string Password);

        Task<string> RefreshTokenLoginAsync(string RefreshToken);

        Task<Tuple<bool, string, string>> AuthenticateDeviceAsync(string Username, string Password, string Language);

    }
}
