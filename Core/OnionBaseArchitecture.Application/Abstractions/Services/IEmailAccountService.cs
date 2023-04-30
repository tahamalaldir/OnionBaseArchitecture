using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface IEmailAccountService
    {
        Task<EmailAccount> GetBySystemName(string SystemName);
    }
}
