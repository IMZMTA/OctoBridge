using OctoBridge.Domain.Config;
using OctoBridge.Api.Extensions;
using OctoBridge.Api.Middleware;
using System.Text.Json.Serialization;
using OctoBridge.Domain.Constants.Messages;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddProjectServices(config);
builder.Services.AddCustomCors(config);
builder.Services.AddJwtAuthentication(config);

var appSettings = config.GetSection(ConfigurationKeys.AppSettings).Get<AppSettings>() ?? throw new InvalidOperationException(ErrorMessages.MissingAppSettings);

if (appSettings.SwaggerEnabled)
{
    builder.Services.AddSwagger();
}

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (appSettings.SwaggerEnabled)
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();
app.UseCors(ConfigurationKeys.DefaultCorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();