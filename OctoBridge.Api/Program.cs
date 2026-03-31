using OctoBridge.Domain.Config;
using OctoBridge.Api.Extensions;
using OctoBridge.Api.Middleware;
using System.Text.Json.Serialization;
using OctoBridge.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddProjectServices(config);
builder.Services.AddCustomCors(config);
builder.Services.AddJwtAuthentication(config);

var appSettings = config.GetSection(AppConstants.AppSettings).Get<AppSettings>() ?? throw new InvalidOperationException(Messages.MissingAppSetting);

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
app.UseCors(AppConstants.DefaultCorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();