using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShopVerseECommercePlatform.Application;
using ShopVerseECommercePlatform.Infrastructure;
using ShopVerseECommercePlatform.Infrastructure.JWTProvider;
using ShopVerseECommercePlatform.Persistence;
using System.Text;
using System.Xml;

namespace ShopVerseECommercePlatform.Api
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddPersistenceService(configuration);
            services.AddApplicationService();
            services.AddHttpContextAccessor();
            services.AddInfrastructureServices(environment.WebRootPath, environment.IsDevelopment());

            //VALIDATING TOKEN
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\": \"You are not authorized to access this resource. Please login again.\"}");
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\": \"You don't have permission to perform this action.\"}");
                    }
                };

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    RoleClaimType = UserClaims.UserRole

                };
            });


            //CORS POLICY
            services.AddCors(options =>
            {
                options.AddPolicy("ECommercePolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            return services;
        }
    }
}
