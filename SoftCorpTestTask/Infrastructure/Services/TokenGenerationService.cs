using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Models.User;
using Application.Interfaces.Services;
using Infrastructure.AppSettings;
using Infrastructure.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class TokenGenerationService : ITokenGenerationService
{
    private readonly JwtSettings _jwtSettings;

    public TokenGenerationService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public UserTokensModel GenerateAccessAndRefreshTokens(int userId, string userEmail)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaimsIdentity(userId, userEmail),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenDurationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var accessJwtToken = tokenHandler.WriteToken(token);
        var refreshTokenModel = GenerateRefreshToken(userId);

        return new UserTokensModel
        {
            AccessJwtToken = accessJwtToken,
            RefreshToken = refreshTokenModel
        };
    }
    
    private UserRefreshTokenModel GenerateRefreshToken(int userId)
    {
        var refreshToken = new UserRefreshTokenModel
        {
            UserId = userId,
            Token = Guid.NewGuid(),
            ExpirationDateTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays)
        };
        
        return refreshToken;
    }

    private ClaimsIdentity CreateClaimsIdentity(int userId, string userEmail)
    {
        var claims = new List<Claim>
        {
            new(AuthenticationConstants.UserIdClaimName, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, userEmail),
            new(JwtRegisteredClaimNames.Aud, _jwtSettings.ValidAudience),
            new(JwtRegisteredClaimNames.Iss, _jwtSettings.ValidIssuer),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        return new ClaimsIdentity(claims);
    }
}