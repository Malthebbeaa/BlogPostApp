namespace BlogPostApp.Models;

public class BlogPostDto
{
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public int OneToTen { get; set; } = 5;
    public Guid BlogId { get; set; }
}