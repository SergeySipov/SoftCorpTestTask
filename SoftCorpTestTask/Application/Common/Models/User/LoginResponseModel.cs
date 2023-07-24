namespace Application.Common.Models.User;

public record LoginResponseModel
{
    public string Username { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}