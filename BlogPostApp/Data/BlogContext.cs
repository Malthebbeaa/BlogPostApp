using BlogPostApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPostApp.Data;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<User> Users { get; set; }
}