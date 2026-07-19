using ShopVerseECommercePlatform.Application;
using ShopVerseECommercePlatform.Persistence;

namespace ShopVerseECommercePlatform.Api
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceService(configuration).AddApplicationService();
            return services;
        }
    }
}
