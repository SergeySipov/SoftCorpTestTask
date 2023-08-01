using Application;
using Infrastructure;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Persistence;
using System.Net.Mime;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence.Constants;
using WebApi.Extensions;
using Application.Interfaces.DbContextSeed;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
var webHostEnvironment = builder.Environment;
var appVersion = GetApplicationVersion();


// Add services to the container.
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwagger(appVersion);
services.AddApplicationServices();
services.AddInfrastructureServices(configuration);
services.AddPersistentServices(configuration);

var connectionString = configuration.GetConnectionString(DbContextConstants.SoftCorpTestTaskDbConnectionStringName);
services.AddHealthChecks()
    .AddSqlServer(connectionString!);

// Configure the HTTP request pipeline.
var app = builder.Build();

if (!webHostEnvironment.IsProduction())
{
    app.UseMigrationsEndPoint();

    var currentAppVersion = appVersion;
    var swaggerEndpointWithCurrentVersion =
        string.Format(AppSettingConstants.SwaggerEndpointUrl, currentAppVersion);

    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint(swaggerEndpointWithCurrentVersion, AppSettingConstants.ProjectName);
    });
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks(AppSettingConstants.HealthCheckMap,
    new HealthCheckOptions { ResponseWriter = WriteHealthCheckResponse });

var isDataGenerationEnabled = bool.Parse(configuration[AppSettingConstants.IsDataGenerationEnabled] ?? bool.FalseString);
if (isDataGenerationEnabled)
{
    using var dbContextSeed = app.Services.GetService<IDbContextSeed>();
    dbContextSeed?.InitDbWithDefaultValues();
}


app.Run();

static Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport result)
{
    httpContext.Response.ContentType = MediaTypeNames.Application.Json;
    var json = new JObject(
        new JProperty("status", result.Status.ToString()),
        new JProperty("results", new JObject(result.Entries.Select(pair =>
            new JProperty(pair.Key, new JObject(
                new JProperty("status", pair.Value.Status.ToString()),
                new JProperty("description", pair.Value.Description),
                new JProperty("data", new JObject(pair.Value.Data.Select(
                    p => new JProperty(p.Key, p.Value))))))))));
    return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
}

static string GetApplicationVersion()
{
    var currentAppVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
    return currentAppVersion;
}