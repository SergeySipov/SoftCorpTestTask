using Application.Common.Models.User;

namespace Application.Common.Models;

public record FamilyMembersModel
{
    public string Title { get; init; }
    public IReadOnlyCollection<UserBriefModel> FamilyMembers { get; init; }
}