namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface IApiUserService
    {
        Task<bool> CheckApiUser(string Username, string Password);
    }
}
