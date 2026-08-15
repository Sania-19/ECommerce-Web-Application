using Microsoft.Extensions.DependencyInjection;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.Services;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.Services;

namespace ShopVerseECommercePlatform.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IAddressService,AddressService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IProductDetailsService,ProductDetailsService>();
            services.AddScoped<ICartService,CartService>();
            return services;
        }
    }
}
