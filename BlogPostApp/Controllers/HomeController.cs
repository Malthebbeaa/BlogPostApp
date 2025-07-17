using System.Diagnostics;
using BlogPostApp.Data;
using Microsoft.AspNetCore.Mvc;
using BlogPostApp.Models;
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
        ViewData["Posts"] = await _context.BlogPosts.ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(BlogPostDto post)
    {
        var newPost = new BlogPost()
        {
            Content = post.Content,
            Title = post.Title,
            Created = DateTime.Now,
            AuthorId = post.Blog.AuthorId
        };
        await _context.BlogPosts.AddAsync(newPost);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}