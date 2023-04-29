namespace OnionBaseArchitecture.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Task<DTOs.Token> CreateTokenAsync(string UserId);
    }
}
