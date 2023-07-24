using Domain.Common;

namespace Domain.Entities;

public class Family : BaseEntity
{
    public string Title { get; set; }

    public ICollection<UserFamily> UsersFamilies { get; set; }
}
