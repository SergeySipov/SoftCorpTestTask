using Application.Common.Mappings;
using Application.Common.Models.DataModels;

namespace Application.Common.Models;

public record UsersCostModel : IMapFrom<UsersCostDataModel>
{
    public decimal Price { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public string CostCategoryName { get; set; }
}
