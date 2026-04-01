namespace OctoBridge.Infrastructure;

using OctoBridge.Domain.Config;
using OctoBridge.Domain.Constants;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OctoBridge.Infrastructure.Extensions;
using OctoBridge.Infrastructure.Interfaces;
using OctoBridge.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using OctoBridge.Infrastructure.Services.Security;
using OctoBridge.Infrastructure.Clients.OAuthClient;
using OctoBridge.Infrastructure.Clients.GitHubClient;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.UserContext;
using OctoBridge.Infrastructure.Services.OAuthService;
using OctoBridge.Infrastructure.Services.TokenService;
using OctoBridge.Infrastructure.Services.GitHubService;
using OctoBridge.Infrastructure.Services.CookieService;
using OctoBridge.Infrastructure.Repository.UserRepository;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(config.GetConnectionString(ConfigurationKeys.DefaultConnection)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.Configure<GitHubSettings>(config.GetSection(GitHubSettings.SectionName));
        services.Configure<EncryptionSettings>(config.GetSection(EncryptionSettings.SectionName));

        services.AddHttpClient<IGitHubClient, GitHubClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<GitHubSettings>>().Value;

            client.BaseAddress = new Uri(options.ApiBaseUrl);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(options.UserAgent);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        })
        .AddPolicyHandler(HttpClientPolicies.RetryPolicy())
        .AddPolicyHandler(HttpClientPolicies.RateLimitPolicy());

        services.AddHttpClient<IGitHubOAuthClient, GitHubOAuthClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<GitHubSettings>>().Value;

            client.BaseAddress = new Uri(options.OAuth.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        });

        services.AddHttpContextAccessor();
        services.AddMemoryCache(options =>
        {
            options.SizeLimit = SecurityConstants.CacheSizeLimit;
        });

        services.AddScoped<IUserContext, UserContext>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IGitHubService, GitHubService>();
        services.AddScoped<IEncryptionService, EncryptionService>();

        services.AddScoped<IOAuthService, GitHubOAuthService>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
