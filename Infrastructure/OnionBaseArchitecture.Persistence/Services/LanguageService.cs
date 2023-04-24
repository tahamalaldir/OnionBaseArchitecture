using OnionBaseArchitecture.Application.Abstractions.Repositories.Language;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Persistence.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageReadRepository _languageReadRepository;

        public LanguageService(ILanguageReadRepository languageReadRepository)
        {
            _languageReadRepository = languageReadRepository;
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            IEnumerable<Language> data = await _languageReadRepository.GetAllActiveAsync();

            return data;
        }
    }
}
