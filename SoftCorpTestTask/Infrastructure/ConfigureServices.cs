using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;
using Application.Interfaces.Services;
using Infrastructure.AppSettings;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Constants;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = services.AddAppSettingsModels(configuration);

        services.AddAuthenticationAndAuthorization(appSettings);

        services.AddSingleton<IJwtTokenGenerationService, JwtTokenGenerationService>();
        services.AddSingleton<IPasswordValidationService, PasswordValidationService>();
        
        return services;
    }

    private static AppSettingsCompositeModel AddAppSettingsModels(this IServiceCollection services,
    IConfiguration configuration)
    {
        (configuration as ConfigurationManager).AddUserSecrets(Assembly.GetExecutingAssembly(), true);

        var jwtSettingsSection = configuration.GetSection(SettingsSectionNameConstants.JwtSettings);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        services.Configure<JwtSettings>(jwtSettingsSection);

        var dataGeneratorSettingsSection = configuration.GetSection(SettingsSectionNameConstants.DataGeneratorSettings);
        var dataGeneratorSettings = dataGeneratorSettingsSection.Get<DataGeneratorSettings>();
        services.Configure<DataGeneratorSettings>(dataGeneratorSettingsSection);

        var compositeModel = new AppSettingsCompositeModel
        {
            JwtSettings = jwtSettings,
            DataGeneratorSettings = dataGeneratorSettings
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
