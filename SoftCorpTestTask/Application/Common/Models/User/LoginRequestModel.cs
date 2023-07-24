namespace Application.Common.Models.User;

public record LoginRequestModel
{
    public string Username { get; init; }
    public string Password { get; init; }
}