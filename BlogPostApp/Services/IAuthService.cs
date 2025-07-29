using BlogPostApp.Models;
using BlogPostApp.Models.Auth;

namespace BlogPostApp.Services;

public interface IAuthService
{
    Task<User?> RegisterUserAsync(RegisterDTO request);
    Task<string> LoginUserAsync(LoginDTO request);
}