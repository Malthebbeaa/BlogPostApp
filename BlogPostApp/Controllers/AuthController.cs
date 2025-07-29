using BlogPostApp.Models;
using BlogPostApp.Models.Auth;
using BlogPostApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostApp.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService _authService): Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO request)
    {
        var user = await _authService.RegisterUserAsync(request);

        if (user == null)
        {
            return BadRequest("User already exists");
        }
        
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        var token = await _authService.LoginUserAsync(request);
        if (token == String.Empty)
        {
            return Unauthorized();
        }
        return Ok(token);
    }
}