namespace Application.Common.Models.User;

public record UserTokensModel
{
    public string AccessJwtToken { get; init; }
    public UserRefreshTokenModel RefreshToken { get; init; }
}
