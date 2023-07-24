using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserFamily Family { get; set; }
    public ICollection<UsersCost> UsersCosts { get; set; }
    public UserRefreshToken UserRefreshToken { get; set; }
}
