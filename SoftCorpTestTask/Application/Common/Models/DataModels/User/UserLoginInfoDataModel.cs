namespace Application.Common.Models.DataModels.User;

public record UserLoginInfoDataModel
{
    public int Id { get; init; }
    public string Username { get; init; }
}