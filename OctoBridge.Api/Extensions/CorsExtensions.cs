using OctoBridge.Domain.Config;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Api.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(AppConstants.AppSettings).Get<AppSettings>() ?? throw new InvalidOperationException(Messages.MissingAppSetting);

        services.AddCors(options =>
        {
            options.AddPolicy(AppConstants.DefaultCorsPolicy, policy =>
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
