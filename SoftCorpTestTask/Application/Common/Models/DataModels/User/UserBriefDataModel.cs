namespace Application.Common.Models.DataModels.User;

public record UserBriefDataModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
}