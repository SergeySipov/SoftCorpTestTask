using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Application.Interfaces.Services;
using Infrastructure.AppSettings;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Constants;
using Infrastructure.MediatR;
using MediatR;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = services.AddAppSettingsModels(configuration);

        services.AddAuthenticationAndAuthorization(appSettings);

        services.AddSingleton<ITokenGenerationService, TokenGenerationService>();
        services.AddSingleton<IPasswordValidationService, PasswordValidationService>();

        services.AddOpenExchangeRatesHttpClientAndService(appSettings.OpenExchangeRatesSettings);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    private static void AddOpenExchangeRatesHttpClientAndService(this IServiceCollection services, OpenExchangeRatesSettings settings)
    {
        services.AddHttpClient(HttpClientConstants.OpenExchangeRatesHttpClientName, client =>
        {
            client.BaseAddress = new Uri(settings.BaseUri);
            client.DefaultRequestHeaders.Add("apikey", settings.AppId);
        });
        services.AddScoped<ICurrencyExchangeService, CurrencyExchangeService>();
    }

    private static AppSettingsCompositeModel AddAppSettingsModels(this IServiceCollection services,
    IConfiguration configuration)
    {
        var jwtSettingsSection = configuration.GetSection(SettingsSectionNameConstants.JwtSettings);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        services.Configure<JwtSettings>(jwtSettingsSection);

        var dataGeneratorSettingsSection = configuration.GetSection(SettingsSectionNameConstants.DataGeneratorSettings);
        var dataGeneratorSettings = dataGeneratorSettingsSection.Get<DataGeneratorSettings>();
        services.Configure<DataGeneratorSettings>(dataGeneratorSettingsSection);

        var openExchangeRatesSettingsSection = configuration.GetSection(SettingsSectionNameConstants.OpenExchangeRatesSettings);
        var openExchangeRatesSettings = openExchangeRatesSettingsSection.Get<OpenExchangeRatesSettings>();
        services.Configure<OpenExchangeRatesSettings>(openExchangeRatesSettingsSection);
        

        var compositeModel = new AppSettingsCompositeModel
        {
            JwtSettings = jwtSettings,
            DataGeneratorSettings = dataGeneratorSettings,
            OpenExchangeRatesSettings = openExchangeRatesSettings
        };

        return compositeModel;
    }
    
    private static void AddAuthenticationAndAuthorization(this IServiceCollection services,
        AppSettingsCompositeModel appSettings)
    {
        var jwtSettings = appSettings.JwtSettings;

        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = jwtSettings.SaveToken;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime
                };
            });

        services.AddAuthorization();
    }
}