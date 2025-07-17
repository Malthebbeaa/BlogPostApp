using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPostApp.Models;

public class Blog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string? Title { get; set; }

    public Guid? AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public User? Author { get; set; }

    public List<BlogPost> BlogPosts { get; set; } = new();
}