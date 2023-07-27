namespace Infrastructure.AppSettings;

public record DataGeneratorSettings
{
    public int NumbersOfUsersToGenerate { get; init; }
    public string DefaultUserPassword { get; init; }
    public int NumbersOfFamiliesToGenerate { get; init; }
}
