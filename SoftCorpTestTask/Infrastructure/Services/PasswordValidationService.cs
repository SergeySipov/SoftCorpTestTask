using System.Security.Cryptography;
using System.Text;
using Application.Interfaces.Services;

namespace Infrastructure.Services;

public class PasswordValidationService : IPasswordValidationService
{
    public bool ValidatePassword(string inputPassword, byte[] userPassword, string salt)
    {
        var inputPasswordHash = GeneratePasswordHash(inputPassword, salt);
        return inputPasswordHash.SequenceEqual(userPassword);
    }

    public byte[] GeneratePasswordHash(string inputPassword, string salt)
    {
        // Convert the plain string pwd into bytes
        var plainTextBytes = Encoding.Unicode.GetBytes(inputPassword);
        var saltBytes = Encoding.Unicode.GetBytes(salt);

        // Append salt to pwd before hashing
        var combinedBytes = new byte[plainTextBytes.Length + salt.Length];
        Buffer.BlockCopy(plainTextBytes, 0, combinedBytes, 0, plainTextBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, combinedBytes, plainTextBytes.Length, salt.Length);

        HashAlgorithm hashAlgorithm = new SHA256Managed();
        var hash = hashAlgorithm.ComputeHash(combinedBytes);
        return hash;
    }
}