using Microsoft.EntityFrameworkCore;
using WeatherAPP.Data;
using WeatherAPP.Data.Entities;
using WeatherAPP.Repository;
using WeatherAPP.Repository.Users;

namespace WeatherAPP.API.Infrastructure.Configurations
{
    public static class RepositoryConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfigurationRoot configuration, IWebHostEnvironment environment)
        {
            services.AddDbContextPool<DatabaseContext>(opts =>
            {
                opts.UseMySQL(configuration.GetConnectionString("DefaultDatabase"), sqlopts =>
                {
                    sqlopts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    sqlopts.MigrationsAssembly(typeof(EntityBase).Assembly.FullName);
                });
            });

            var types = typeof(UserRepository).Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType != null && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition().IsAssignableTo(typeof(RepositoryBase<>)))
                .ToList();

            types.ForEach(x => x.GetInterfaces().ToList().ForEach(i => services.AddTransient(i, x)));

            return services;
        }
    }
}
