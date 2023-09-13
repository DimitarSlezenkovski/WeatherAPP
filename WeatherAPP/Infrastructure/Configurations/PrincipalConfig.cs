using Microsoft.AspNetCore.Identity;
using ScottBrady91.AspNetCore.Identity;
using WeatherAPP.Data.Entities.Users;
using WeatherAPP.Infrastructure.Context;

namespace WeatherAPP.API.Infrastructure.Configurations
{
    public static class PrincipalConfig
    {
        public static IServiceCollection AddPrincipal(this IServiceCollection services, IConfigurationRoot configuration, IWebHostEnvironment environment)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>();

            services.AddTransient<IUserPrincipal>(collection =>
            {
                var contextAccessor = collection.GetRequiredService<IHttpContextAccessor>();
                var user = contextAccessor.HttpContext?.User;

                if (user != null && user.Identity?.IsAuthenticated == true)
                {
                    return new UserPrincipal(user);
                }
                else
                {
                    return new UserPrincipal();
                }
            });

            return services;
        }
    }
}
