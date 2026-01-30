using Microsoft.Extensions.DependencyInjection;

namespace ARMS.Application.Caching
{
    public static class CachingServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
            return services;
        }
    }
}
