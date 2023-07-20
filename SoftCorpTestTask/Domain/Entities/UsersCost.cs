using Domain.Common;

namespace Domain.Entities;

public class UsersCost : BaseEntity
{
    public decimal Price { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int CostId { get; set; }
    public CostCategory CostCategory { get; set; }
}
