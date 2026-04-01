using OctoBridge.Application;
using OctoBridge.Domain.Config;
using OctoBridge.Infrastructure;

namespace OctoBridge.Api.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(ConfigurationKeys.AppSettings));

        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
