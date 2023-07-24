namespace Application.Common.Models.User;

public record UserRefreshTokenModel
{
    public int UserId { get; init; }
    public Guid Token { get; init; }
    public DateTime ExpirationDateTime { get; init; }
}