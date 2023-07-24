using Application.Common.Mappings;
using Application.Common.Models.DataModels.UsersCostStatistic;

namespace Application.Common.Models.UsersCostStatistic;

public record FamilyCostsStatisticModel : IMapFrom<FamilyCostsStatisticDataModel>
{
    public string FamilyTitle { get; init; }
    public IReadOnlyCollection<FamilyMemberCostsStatisticModel> FamilyMemberStatistic { get; init; }
}
