using Application.Common.Models.Common;

namespace Application.Common.Models.DataModels.UsersCostStatistic;

public record FamilyMemberCostsStatisticDataModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
    public decimal TotalCosts { get; init; }
    public PaginatedList<UsersCostsStatisticDataModel> FamilyMemberDetailedStatistic { get; init; }
}
