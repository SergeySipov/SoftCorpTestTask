namespace Application.Common.Models.DataModels.User;

public record UserLoginDataModel
{
    public int Id { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }
    public byte[] PasswordHash { get; init; }
    public string PasswordSalt { get; init; }
}