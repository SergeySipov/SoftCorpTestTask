namespace Application.Interfaces.Services;

public interface IPasswordValidationService
{
    bool ValidatePassword(string inputPassword, byte[] userPassword, string salt);
    byte[] GeneratePasswordHash(string inputPassword, string salt);
}