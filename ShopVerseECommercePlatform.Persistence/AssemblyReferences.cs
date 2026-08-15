using ECommerce.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Persistence.Repository;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Persistence.Data;
using ShopVerseECommercePlatform.Persistence.Repository;

namespace ShopVerseECommercePlatform.Persistence
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddScoped<IAddressRepository,AddressRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IProductDetailsRepository,ProductDetailsRepository>();
            services.AddScoped<ICartRepository,CartRepository>();
            services.AddScoped<ICartItemsRepository,CartItemsRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IAppFilesRepository,AppFilesRepository>();
            services.AddDbContext<ShopVerseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ShopVerseDbContext")));
            return services;
        }
    }
}
