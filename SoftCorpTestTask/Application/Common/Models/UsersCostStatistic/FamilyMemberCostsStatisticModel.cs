using Application.Common.Mappings;
using Application.Common.Models.Common;
using Application.Common.Models.DataModels.UsersCostStatistic;

namespace Application.Common.Models.UsersCostStatistic;

public record FamilyMemberCostsStatisticModel : IMapFrom<FamilyMemberCostsStatisticDataModel>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
    public decimal TotalCosts { get; init; }
    public PaginatedList<UsersCostsStatisticModel> FamilyMemberDetailedStatistic { get; init; }
}
