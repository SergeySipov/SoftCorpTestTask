using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.Services;
using Infrastructure.AppSettings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtTokenGenerationService : IJwtTokenGenerationService
{
    private readonly IOptions<JwtSettings> _jwtSettings;

    public JwtTokenGenerationService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateJwt(int userId, string userEmail)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaimsIdentity(userEmail),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.TokenDurationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity CreateClaimsIdentity(string userEmail)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, userEmail),
            new(JwtRegisteredClaimNames.Aud, _jwtSettings.Value.ValidAudience),
            new(JwtRegisteredClaimNames.Iss, _jwtSettings.Value.ValidIssuer),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        return new ClaimsIdentity(claims);
    }
}
