namespace Infrastructure.Constants;

public static class AppSettingConstants
{
    public const string ProjectName = "SoftCorpTestTask";
    public const string ProjectDescription = "Test task";
    public const string SwaggerEndpointUrl = "/swagger/{0}/swagger.json";
    public const string HealthCheckMap = "/Health";
    public const string IsDataGenerationEnabled = nameof(IsDataGenerationEnabled);
}