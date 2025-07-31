using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlogPostApp.Data;
using BlogPostApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using BlogPostApp.Models;
using BlogPostApp.Models.BlogDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogPostApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;

    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _context.Blogs.ToListAsync();
        var posts = await _context.BlogPosts.Include(p => p.Author).ToListAsync();

        posts.Reverse();
        var model = new BlogPageViewModel()
        {
            Blogs = new SelectList(blogs, "Id", "Title"),
            BlogPosts = posts
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(BlogPageViewModel model)
    {
        var post = model.NewBlogPost;
        var blog = await _context.Blogs.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == post.BlogId);

        if (blog == null)
        {
            Console.WriteLine("Blog not found with id " + post.BlogId);
            return RedirectToAction("Index");
        }
        var newPost = new BlogPost()
        {
            Content = post.Content,
            Title = post.Title,
            Created = DateTime.Now,
            AuthorId = blog.AuthorId,
            BlogId = blog.Id,
            OneToTen = post.OneToTen,
        };
        await _context.BlogPosts.AddAsync(newPost);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> CreatePost()
    {
        var blogs = await _context.Blogs.ToListAsync();
        var posts = await _context.BlogPosts.Include(p => p.Author).ToListAsync();

        posts.Reverse();
        var model = new BlogPageViewModel()
        {
            Blogs = new SelectList(blogs, "Id", "Title"),
            BlogPosts = posts
        };
        return View(model);
    }
    
    
    public IActionResult Privacy()
    {
        var secretClaims = ClaimHelper.ExtractSecretClaims(HttpContext);

        if (secretClaims == null)
        {
            return RedirectToAction("Index");
        }
        return View(secretClaims);
    }

    [HttpGet]
    public async Task<IActionResult> Blogs()
    {
        var blogs = await _context.Blogs.Include(b => b.Author).ToListAsync();
        var model = new BlogOverviewModel()
        {
            Blogs = blogs
        };
        
        return View(model);
    }

    [HttpGet]
    public IActionResult CreateBlog()
    {
        var secretClaims = ClaimHelper.ExtractSecretClaims(HttpContext);
        if (secretClaims == null || secretClaims.Role == "Reader")
        {
            return RedirectToAction("Index");
        }
        
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromForm] CreateBlogDTO request)
    {
        var secretClaims = ClaimHelper.ExtractSecretClaims(HttpContext);
        var username = secretClaims!.Username;
        
        var author = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);

        if (author == null)
        {
            return RedirectToAction("Index");
        }

        var blog = new Blog()
        {
            Title = request.Title,
            AuthorId = author.Id,
        };
        
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Blogs");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}