using Application.Common.Models.DataModels.User;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserLoginDataModel?> GetUserLoginDataAsync(string email);
    Task<UserLoginInfoDataModel?> GetUserLoginInfoAsync(string email);
}