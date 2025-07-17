namespace BlogPostApp.Models;

public class BlogPostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Blog Blog { get; set; }
}