namespace BlogPostApp.Models.Auth;

public class RegisterDTO
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Reader";
}