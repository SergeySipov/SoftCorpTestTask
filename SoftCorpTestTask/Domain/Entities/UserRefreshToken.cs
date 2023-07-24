using Domain.Common;

namespace Domain.Entities;

public class UserRefreshToken : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public Guid Token { get; set; }
    public DateTime ExpirationDateTime { get; set; }
}