namespace Application.Interfaces.Services;

public interface IJwtTokenGenerationService
{
    string GenerateJwt(int userId, string userEmail);
}