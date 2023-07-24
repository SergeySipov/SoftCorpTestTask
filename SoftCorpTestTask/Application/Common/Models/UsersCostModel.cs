using Application.Common.Mappings;
using Application.Common.Models.DataModels;

namespace Application.Common.Models;

public record UsersCostModel : IMapFrom<UsersCostDataModel>
{
    public decimal Price { get; init; }
    public string Comment { get; init; }
    public DateTime Date { get; init; }
    public string CostCategoryName { get; init; }
}
