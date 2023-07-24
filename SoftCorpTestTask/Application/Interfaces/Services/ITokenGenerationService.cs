using Application.Common.Models.User;

namespace Application.Interfaces.Services;

public interface ITokenGenerationService
{
    UserTokensModel GenerateAccessAndRefreshTokens(int userId, string userEmail);
}