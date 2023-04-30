using Microsoft.Extensions.DependencyInjection;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories;
using OnionBaseArchitecture.Application.Abstractions.Repositories.ApiUser;
using OnionBaseArchitecture.Application.Abstractions.Repositories.EmailAccount;
using OnionBaseArchitecture.Application.Abstractions.Repositories.Language;
using OnionBaseArchitecture.Application.Abstractions.Repositories.LanguageText;
using OnionBaseArchitecture.Application.Abstractions.Repositories.Setting;
using OnionBaseArchitecture.Application.Abstractions.Repositories.User;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Application.Common;
using OnionBaseArchitecture.Persistence.Repositories;
using OnionBaseArchitecture.Persistence.Repositories.ApiUser;
using OnionBaseArchitecture.Persistence.Repositories.EmailAccount;
using OnionBaseArchitecture.Persistence.Repositories.Language;
using OnionBaseArchitecture.Persistence.Repositories.LanguageText;
using OnionBaseArchitecture.Persistence.Repositories.Setting;
using OnionBaseArchitecture.Persistence.Repositories.User;
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

            // Language
            services.AddScoped<ILanguageReadRepository, LanguageReadRepository>();

            // LanguageText
            services.AddScoped<ILanguageTextReadRepository, LanguageTextReadRepository>();

            // User
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            // Setting
            services.AddScoped<ISettingReadRepository, SettingReadRepository>();
            
            // ApiUser
            services.AddScoped<IApiUserReadRepository, ApiUserReadRepository>();
            
            // EmailAccount
            services.AddScoped<IEmailAccountReadRepository, EmailAccountReadRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILanguageTextService, LanguageTextService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IApiUserService, ApiUserService>();
            services.AddScoped<IEmailAccountService, EmailAccountService>();
        }
    }
}
