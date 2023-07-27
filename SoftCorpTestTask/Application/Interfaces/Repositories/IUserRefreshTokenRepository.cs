using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRefreshTokenRepository
{
    Task<UserRefreshToken?> GetUserRefreshTokenAsync(Guid token);
    Task<UserRefreshToken?> GetUserRefreshTokenAsync(int userId);
}