using Domain.Common;

namespace Domain.Entities;

public class UserFamily : BaseEntity
{
    public int FamilyId { get; set; }
    public Family Family { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
