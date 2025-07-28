using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogPostApp.Models;

public class BlogPageViewModel
{
    public BlogPostDto NewBlogPost { get; set; } = new BlogPostDto();
    public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    public SelectList Blogs { get; set; } = null!;
}