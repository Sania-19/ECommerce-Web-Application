using Microsoft.Extensions.DependencyInjection;
using ShopVerseECommercePlatform.Application.Abstraction.IAppEncryption;
using ShopVerseECommercePlatform.Application.Abstraction.IContextService;
using ShopVerseECommercePlatform.Application.Abstraction.IJWTProvider;
using ShopVerseECommercePlatform.Application.Abstraction.IStorageService;
using ShopVerseECommercePlatform.Infrastructure.StorageService;

namespace ShopVerseECommercePlatform.Infrastructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string webRootPath ,bool isDevelopment)
        {
            services.AddScoped<IAppEncryption,ShopVerseECommercePlatform.Infrastructure.AppEncryption.AppEncryption>();
            services.AddScoped<IJWTProvider,ShopVerseECommercePlatform.Infrastructure.JWTProvider.JWTProvider>();
            services.AddScoped<IContextService,ShopVerseECommercePlatform.Infrastructure.ContextService.ContextService>();
            services.AddSingleton<IStorageService>(new LocalStorageService(webRootPath));

            return services;
        }
    }
}
