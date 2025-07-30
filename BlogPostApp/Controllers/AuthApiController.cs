using BlogPostApp.Data;
using BlogPostApp.Models.Auth;
using BlogPostApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostApp.Controllers;

// API-controller til JSON (kan være i AuthApiController.cs)
[ApiController]
[Route("api/auth")]
public class AuthApiController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> ApiLogin([FromBody] LoginDTO request)
    {
        var token = await authService.LoginUserAsync(request);
        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> ApiRegister([FromBody] RegisterDTO request)
    {
        var user = await authService.RegisterUserAsync(request);
        if (user == null)
            return BadRequest("User already exists");

        return Ok(user);
    }
}