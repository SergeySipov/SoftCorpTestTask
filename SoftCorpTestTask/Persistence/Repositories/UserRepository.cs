using Application.Common.Models.DataModels.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SoftCorpTestTaskDbContext _dbContext;

    public UserRepository(SoftCorpTestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<UserLoginDataModel?> GetUserLoginDataAsync(string email)
    {
        return _dbContext.Users.Select(u => new UserLoginDataModel
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt
            })
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public Task<UserLoginInfoDataModel?> GetUserLoginInfoAsync(string email)
    {
        return _dbContext.Users
            .Where(u => u.Email.Equals(email))
            .Select(u => new UserLoginInfoDataModel
            {
                Id = u.Id,
                Username = u.Username
            }).FirstOrDefaultAsync();
    }
}