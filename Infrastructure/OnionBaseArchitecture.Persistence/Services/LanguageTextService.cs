using OnionBaseArchitecture.Application.Abstractions.Repositories.LanguageText;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Persistence.Services
{
    public class LanguageTextService : ILanguageTextService
    {
        private readonly ILanguageTextReadRepository _languageTextReadRepository;

        public LanguageTextService(ILanguageTextReadRepository languageTextReadRepository)
        {
            _languageTextReadRepository = languageTextReadRepository;
        }

        public async Task<IEnumerable<LanguageText>> GetAllAsync()
        {
            IEnumerable<LanguageText> data = await _languageTextReadRepository.GetAllActiveAsync();
            
            return data;
        }
    }
}
