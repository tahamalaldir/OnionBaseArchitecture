namespace OnionBaseArchitecture.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<string> FacebookLoginAsync(string AuthToken, int AccessTokenLifeTime);
        
        Task<string> GoogleLoginAsync(string IdToken, int AccessTokenLifeTime);
    }
}
