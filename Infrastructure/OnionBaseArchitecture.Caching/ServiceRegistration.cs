using Microsoft.Extensions.DependencyInjection;
using OnionBaseArchitecture.Application.Abstractions.Caching;
using OnionBaseArchitecture.Caching.Context;
using OnionBaseArchitecture.Caching.Services;

namespace OnionBaseArchitecture.Caching
{
    public static class ServiceRegistration
    {
        public static void AddCachingServices(this IServiceCollection services)
        {
            services.AddSingleton<RedisContext>();
            services.AddSingleton<IBaseCacheService, RedisCache>();
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddScoped<ICacheService, CacheService>();
        }
    }
}
