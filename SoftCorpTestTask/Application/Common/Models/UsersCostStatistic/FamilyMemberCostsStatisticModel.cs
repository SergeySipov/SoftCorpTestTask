using Application.Common.Mappings;
using Application.Common.Models.Common;
using Application.Common.Models.DataModels.UsersCostStatistic;

namespace Application.Common.Models.UsersCostStatistic;

public record FamilyMemberCostsStatisticModel : IMapFrom<FamilyMemberCostsStatisticDataModel>
{
    public decimal TotalCosts { get; init; }
    public PaginatedList<UsersCostsStatisticModel> FamilyMemberDetailedStatistic { get; init; }
}
