using WeatherAPP.Application.Services.Authentication;
using WeatherAPP.Data;
using WeatherAPP.Infrastructure.Context;
using WeatherAPP.Infrastructure.Mediating;
using WeatherAPP.Infrastructure.Services;
using WeatherAPP.Infrastructure.Validation;

namespace WeatherAPP.API.Infrastructure.Configurations
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddInputValidators(this IServiceCollection services, IConfigurationRoot configuration, IWebHostEnvironment environment)
        {
            var types = typeof(Login).Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType != null && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(InputValidator<>)))
                .ToList();

            types.ForEach(x => services.AddTransient(x.BaseType!, x));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfigurationRoot configuration, IWebHostEnvironment environment)
        {
            services.AddTransient<ServiceContext>(container =>
            {
                var principal = container.GetRequiredService<IUserPrincipal>();
                var mediator = container.GetRequiredService<IServiceMediator>();
                var dbContext = container.GetRequiredService<DatabaseContext>();

                return new ServiceContext(principal, mediator, dbContext);
            });
            services.AddTransient<IServiceMediator, ServiceMediator>();

            var types = typeof(Login).Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType != null && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(ServiceBase<,>)))
                .ToList();

            types.ForEach(x => services.AddTransient(x.BaseType!, x));

            return services;
        }
    }
}
