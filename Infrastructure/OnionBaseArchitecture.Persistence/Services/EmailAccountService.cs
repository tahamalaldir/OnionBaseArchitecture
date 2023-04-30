using OnionBaseArchitecture.Application.Abstractions.Repositories.EmailAccount;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Persistence.Services
{
    public class EmailAccountService : IEmailAccountService
    {
        private readonly IEmailAccountReadRepository _repository;

        public EmailAccountService(IEmailAccountReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailAccount> GetBySystemName(string SystemName)
        {
            try
            {
                return await _repository.GetBySystemNameAsync(SystemName);
            }
            catch (Exception)
            { }

            return null;
        }
    }
}
