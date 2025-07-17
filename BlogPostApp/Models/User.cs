namespace BlogPostApp.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public string Role { get; set; } = "Reader";
    public List<Blog>  Blogs { get; set; } = new List<Blog>();
}