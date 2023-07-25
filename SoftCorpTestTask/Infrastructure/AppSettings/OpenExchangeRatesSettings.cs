namespace Infrastructure.AppSettings;

public record OpenExchangeRatesSettings
{
    public string BaseUri { get; init; }
    public string AppId { get; init; }
}