namespace OnionBaseArchitecture.Application.Abstractions.Repositories.EmailAccount
{
    public interface IEmailAccountReadRepository : IReadRepository<Domain.Entities.EmailAccount>
    {
        Task<Domain.Entities.EmailAccount> GetBySystemNameAsync(string SystemName);
    }
}
