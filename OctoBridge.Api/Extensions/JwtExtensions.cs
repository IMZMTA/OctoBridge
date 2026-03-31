using System.Text;
using OctoBridge.Domain.Config;
using OctoBridge.Domain.Constants;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OctoBridge.Api.Extensions;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection(AppConstants.AppSettings).Get<AppSettings>()?.Jwt
                          ?? throw new InvalidOperationException(Messages.MissingAppSetting);

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
                        var token = context.Request.Cookies[AppConstants.JWTTokenName];
                        if (string.IsNullOrEmpty(token))
                        {
                            var authHeader = context.Request.Headers[AppConstants.Authorization].FirstOrDefault();
                            if (authHeader?.StartsWith($"{AppConstants.Bearer} ") == true)
                            {
                                token = authHeader[$"{AppConstants.Bearer} ".Length..].Trim();
                            }
                        }
                        context.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAntiforgery(options => options.HeaderName = AppConstants.AntiforgeryHeader);

        return services;
    }
}
