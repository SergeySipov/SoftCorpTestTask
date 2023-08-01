using Infrastructure.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebApi.Extensions;

public static class SwaggerStartupExtension
{
    private const string BearerSecurityDefinitionDescription =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n " +
        "Example: \"Bearer 1safsfsdfdfd\"";

    private const string SecuritySchemeName = "Authorization";

    public static void AddSwagger(this IServiceCollection services, string appVersion)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(appVersion,
                new OpenApiInfo
                {
                    Title = AppSettingConstants.ProjectName,
                    Version = appVersion,
                    Description = AppSettingConstants.ProjectDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Sergey Sipov",
                        Email = "Sergey.sipov.150699@gmail.com"
                    }
                });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = SecuritySchemeName,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = JwtConstants.TokenType,
                In = ParameterLocation.Header,
                Description = BearerSecurityDefinitionDescription
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}