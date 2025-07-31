namespace BlogPostApp.Models.BlogDTO;

public class CreateBlogDTO
{
    public string Title { get; set; } = "Blog Title";
    public Guid AuthorId { get; set; } =  Guid.NewGuid();
}