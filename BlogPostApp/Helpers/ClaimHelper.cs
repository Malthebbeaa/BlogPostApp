using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlogPostApp.Models;

namespace BlogPostApp.Helpers;

public class ClaimHelper
{
    public static SecretClaimsDTO? ExtractSecretClaims(HttpContext context)
    {
        var tokenstr = context.Session.GetString("JWToken");

        if (tokenstr != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(tokenstr) as JwtSecurityToken;

            if (token == null)
            {
                return null;
            }
            
            var username = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == "name")?.Value;
            var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

            var secretClaims = new SecretClaimsDTO()
            {
                Username = username,
                Role = role
            };
            
            return secretClaims;
        }

        return null;
    }
}