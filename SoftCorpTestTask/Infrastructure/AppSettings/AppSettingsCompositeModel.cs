namespace Infrastructure.AppSettings;

public record AppSettingsCompositeModel
{
    public JwtSettings JwtSettings { get; init; }
    public DataGeneratorSettings DataGeneratorSettings { get; init; }
    public OpenExchangeRatesSettings OpenExchangeRatesSettings { get; init; }
}
