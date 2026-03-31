using OctoBridge.Application;
using OctoBridge.Domain.Config;
using OctoBridge.Infrastructure;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Api.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(AppConstants.AppSettings));

        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
