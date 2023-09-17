using WeatherAPP.API.Infrastructure.Configurations;
using WeatherAPP.API.Infrastructure.Configurations.OpenWeatherConfiguration;

namespace WeatherAPP.API;
public class Program
{
    public static void Main(string[] args)
    {
        var app = ConfigureApplication(args);
        RunApplication(app);
    }

    private static WebApplication ConfigureApplication(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        builder.Services
            .AddPrincipal(builder.Configuration, builder.Environment)
            .AddInputValidators(builder.Configuration, builder.Environment)
            .AddRepositories(builder.Configuration, builder.Environment)
            .AddOpenWeatherService(builder.Configuration)
            .AddServices(builder.Configuration, builder.Environment)
            .AddCustomMvc()
            .AddAuth(builder.Configuration);

        builder.Services
            .AddMemoryCache();

        var application = builder.Build();

        return application;
    }

    private static void RunApplication(WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application
                .UseCors(opts =>
                {
                    opts.AllowAnyOrigin();
                    opts.AllowAnyHeader();
                    opts.AllowAnyMethod();
                });
        }

        application
            .UseExceptionHandler("/Error")
            .UseHsts();

        application
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuth()
            .UseCustomMvc();

        application.Run();
    }
}