using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface ILanguageTextService
    {
        Task<IEnumerable<LanguageText>> GetAllAsync();
    }
}
