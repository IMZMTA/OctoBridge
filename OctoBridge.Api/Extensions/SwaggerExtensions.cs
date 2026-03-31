using NSwag;
using OctoBridge.Api.Descriptor;
using OctoBridge.Domain.Constants;
using NSwag.Generation.Processors.Security;

namespace OctoBridge.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddOpenApiDocument(config =>
        {
            config.Title = AppConstants.AppName;
            config.Version = AppConstants.Version;

            config.Description = @"### OctoBridge - GitHub Connector API

                                        **Authentication Flow**
                                        1. Call `/api/v1/auth/login` on Browsers or Get Redirect Url From Swagger (Copy and Paste) on Browser.
                                        2. On Redirect to GitHub OAuth, Login or Connect GitHub Account
                                        3. After successful Login, GitHub will callback the OctoBridge API URL.
                                        4. Token will be exchange(User Info will be saved) and a JWT will be generated and attach in HttpOnly cookie.`

                                        5. **Swagger Testing**
                                            - Cookie works automatically after login
                                            - Or use Bearer(JWT in Cookie) token manually

                                        **To test:** 
                                        1. Login via api/v1/auth/login
                                        2. Return here and use 'Try it out'.";

            config.SchemaSettings.SchemaProcessors.Add(new EnumDescriptionSchemaProcessor());

            config.AddSecurity(AppConstants.JWT, new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.Http,
                Scheme = AppConstants.Bearer.ToLower(),
                Name = AppConstants.Authorization,
                BearerFormat = AppConstants.JWT,
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = Messages.JWTDescription
            });

            config.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor(AppConstants.JWT));
        });

        return services;
    }
}