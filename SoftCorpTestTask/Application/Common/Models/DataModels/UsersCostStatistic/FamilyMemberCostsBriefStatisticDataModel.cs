using Application.Common.Models.Common;

namespace Application.Common.Models.DataModels.UsersCostStatistic;

public record FamilyMemberCostsBriefStatisticDataModel
{
    public decimal TotalCosts { get; init; }
    public PaginatedList<UsersCostsStatisticDataModel> FamilyMemberDetailedStatistic { get; init; }
}