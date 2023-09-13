using System.Text.Json;

namespace WeatherAPP.API.Infrastructure.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services
                .AddControllersWithViews();

            services
                .AddMvcCore(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            return services;
        }

        public static IApplicationBuilder UseCustomMvc(this IApplicationBuilder app)
        {
            return app.UseEndpoints(opts =>
            {
                opts.MapDefaultControllerRoute();
            });
        }
    }
}
