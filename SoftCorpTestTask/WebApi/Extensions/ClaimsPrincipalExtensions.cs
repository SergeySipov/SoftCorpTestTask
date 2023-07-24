using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using Infrastructure.Constants;

namespace WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdValue = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == AuthenticationConstants.UserIdClaimName)?.Value;

        return !string.IsNullOrEmpty(userIdValue) && int.TryParse(userIdValue, out var userId)
            ? userId
            : throw new AuthenticationException("Invalid user id.");
    }

    public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
    {
        var userEmail = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;

        return string.IsNullOrEmpty(userEmail)
            ? throw new AuthenticationException("Invalid user email.")
            : userEmail;
    }
}