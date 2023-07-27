namespace Application.Common.Models.User;
public record UserTokenClaimsModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
}
