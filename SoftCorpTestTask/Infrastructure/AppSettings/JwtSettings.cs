namespace Infrastructure.AppSettings;

public record JwtSettings
{
    public bool ValidateIssuerSigningKey { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateAudience { get; init; }
    public bool SaveToken { get; init; }
    public string SecretKey { get; init; }
    public bool ValidateLifetime { get; init; }
    public string ValidIssuer { get; init; }
    public string ValidAudience { get; init; }
    public int TokenDurationInMinutes { get; init; }
}
