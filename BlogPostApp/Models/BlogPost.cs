using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPostApp.Models;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Content { get; set; }
    
    public int OneToTen { get; set; }

    public Guid? AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public User? Author { get; set; }
    
    public Guid? BlogId { get; set; }
    [ForeignKey("BlogId")]
    public Blog? Blog { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;
}