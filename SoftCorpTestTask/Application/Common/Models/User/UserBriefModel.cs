using Application.Common.Mappings;
using Application.Common.Models.DataModels.User;

namespace Application.Common.Models.User;

public record UserBriefModel : IMapFrom<UserBriefDataModel>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
}