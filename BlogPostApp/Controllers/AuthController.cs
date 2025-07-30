using BlogPostApp.Models;
using BlogPostApp.Models.Auth;
using BlogPostApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostApp.Controllers;

[Route("auth")]
public class AuthController(IAuthService _authService): Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDTO request)
    {
        var user = await _authService.RegisterUserAsync(request);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User already exists");
            return View(request);
        }

        return RedirectToAction("Login");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm]LoginDTO request)
    {
        var token = await _authService.LoginUserAsync(request);
        if (token == String.Empty)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(request);
        }
        HttpContext.Session.SetString("JWToken", token);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Remove("JWToken");
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        return View();
    }
    
    [HttpGet("register")]
    public async Task<IActionResult> Register()
    {
        return View();
    }
}