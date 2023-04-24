using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories.Language;

namespace OnionBaseArchitecture.Persistence.Repositories.Language
{
    public class LanguageReadRepository : ReadRepository<Domain.Entities.Language>, ILanguageReadRepository
    {
        public LanguageReadRepository(IConnectionManager connectionManager) : base(connectionManager)
        {
        }
    }
}
