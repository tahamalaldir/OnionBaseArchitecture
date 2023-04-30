namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface IMailService
    {
        Task<Tuple<bool, string>> SendResetPasswordMailAsync(string Email);
    }
}
