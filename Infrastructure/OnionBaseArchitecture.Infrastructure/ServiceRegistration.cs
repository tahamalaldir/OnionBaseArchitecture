using Microsoft.Extensions.DependencyInjection;
using OnionBaseArchitecture.Application.Abstractions.Token;
using OnionBaseArchitecture.Infrastructure.Token;

namespace OnionBaseArchitecture.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
