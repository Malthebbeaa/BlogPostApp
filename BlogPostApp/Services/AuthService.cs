using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogPostApp.Data;
using BlogPostApp.Models;
using BlogPostApp.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogPostApp.Services;

public class AuthService(BlogContext _context, IConfiguration _configuration) : IAuthService
{
    public async Task<User?> RegisterUserAsync(RegisterDTO request)
    {
        if (await _context.Users.AnyAsync(u => u.Name == request.Name))
        {
            return null;
        }

        User user = new User();
        var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
        
        user.Name = request.Name;
        user.PasswordHash = hashedPassword;
        user.Role = request.Role;
        user.Blogs = new List<Blog>();
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<string> LoginUserAsync(LoginDTO request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Name);

        if (user == null)
        {
            return null;
        }

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            return null;
        }

        string token = CreateToken(user);
        return token;
    }
    
    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Key")!));
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
            audience: _configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}