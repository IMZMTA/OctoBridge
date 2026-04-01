using OctoBridge.Domain.Config;
using OctoBridge.Domain.Constants.Messages;

namespace OctoBridge.Api.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(ConfigurationKeys.AppSettings).Get<AppSettings>() ?? throw new InvalidOperationException(ErrorMessages.MissingAppSettings);

        services.AddCors(options =>
        {
            options.AddPolicy(ConfigurationKeys.DefaultCorsPolicy, policy =>
            {
                policy.WithOrigins(settings.AllowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
