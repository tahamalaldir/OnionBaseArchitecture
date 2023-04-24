using Microsoft.Extensions.DependencyInjection;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories;
using OnionBaseArchitecture.Application.Abstractions.Repositories.Language;
using OnionBaseArchitecture.Application.Abstractions.Repositories.LanguageText;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Application.Common;
using OnionBaseArchitecture.Persistence.Repositories;
using OnionBaseArchitecture.Persistence.Repositories.Language;
using OnionBaseArchitecture.Persistence.Repositories.LanguageText;
using OnionBaseArchitecture.Persistence.Services;

namespace OnionBaseArchitecture.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IConnectionManager, MainConnectionManager>();

            services.AddScoped<ILanguageTextReadRepository, LanguageTextReadRepository>();
            services.AddScoped<ILanguageReadRepository, LanguageReadRepository>();

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILanguageTextService, LanguageTextService>();
        }
    }
}
