using Microsoft.Extensions.DependencyInjection;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Application.Abstractions.Token;
using OnionBaseArchitecture.Infrastructure.Services;
using OnionBaseArchitecture.Infrastructure.Services.Token;

namespace OnionBaseArchitecture.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
        }
    }
}
