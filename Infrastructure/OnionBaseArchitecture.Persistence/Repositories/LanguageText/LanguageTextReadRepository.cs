using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories.LanguageText;

namespace OnionBaseArchitecture.Persistence.Repositories.LanguageText
{
    public class LanguageTextReadRepository : ReadRepository<Domain.Entities.LanguageText>, ILanguageTextReadRepository
    {
        public LanguageTextReadRepository(IConnectionManager connectionManager) : base(connectionManager)
        {
        }
    }
}
