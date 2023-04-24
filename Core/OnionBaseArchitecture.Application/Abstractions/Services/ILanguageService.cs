using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Application.Abstractions.Services
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllAsync();
    }
}
