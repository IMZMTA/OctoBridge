using System.Text;
using OctoBridge.Domain.Config;
using OctoBridge.Domain.Constants;
using Microsoft.IdentityModel.Tokens;
using OctoBridge.Domain.Constants.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OctoBridge.Api.Extensions;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection(ConfigurationKeys.AppSettings).Get<AppSettings>()?.Jwt
                          ?? throw new InvalidOperationException(ErrorMessages.MissingAppSettings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies[SecurityConstants.JwtTokenName];
                        if (string.IsNullOrEmpty(token))
                        {
                            var authHeader = context.Request.Headers[SecurityConstants.AuthorizationHeader].FirstOrDefault();
                            if (authHeader?.StartsWith($"{SecurityConstants.Bearer} ") == true)
                            {
                                token = authHeader[$"{SecurityConstants.Bearer} ".Length..].Trim();
                            }
                        }
                        context.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAntiforgery(options => options.HeaderName = SecurityConstants.AntiforgeryHeader);

        return services;
    }
}
