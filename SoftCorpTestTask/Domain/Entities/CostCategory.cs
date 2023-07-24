using Domain.Common;

namespace Domain.Entities;

public class CostCategory : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
}
