using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopVerseECommercePlatform.Persistence.Data;

namespace ShopVerseECommercePlatform.Persistence
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ShopVerseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ShopVerseDbContext")));
            return services;
        }
    }
}
