using BlogApp.Data.Abstract;
using BlogApp.Data.Entity;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BlogApp.Data.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BlogApp.Controllers;

public class PostController : Controller
{
    private readonly IPostRepository _postRepository;

    private readonly UserManager<BlogAppUser> _userManager;

    private readonly ICategoryRepository _categoryRepository;

    public PostController(IPostRepository postRepository, UserManager<BlogAppUser> userManager, ICategoryRepository categoryRepository)
    {
        _postRepository = postRepository;
        _userManager = userManager;
        _categoryRepository = categoryRepository;
    }


    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> List()
    {
        var posts = await _postRepository.GetAllPosts().Where(p => p.IsPublished)
            .Include(p => p.Writer) // Yazar bilgilerini dahil et
            .Include(p => p.Category) // Kategori bilgilerini dahil et
            .ToListAsync();
        return View(posts);
    }


    public async Task<IActionResult> Details(int id)
    {
        var post = await _postRepository.GetAllPosts()
            .Include(p => p.Writer).Include(p => p.Comments).ThenInclude(c => c.Writer).Include(p => p.Favorites)
            .FirstOrDefaultAsync(p => p.PostId == id);

        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }
    public async Task<IActionResult> Index(string? category, string? search)
    {
        var posts = await _postRepository.GetAllPosts()
            .Where(p => p.IsPublished)
            .Include(p => p.Writer)
            .Include(p => p.Category)
            .ToListAsync();

        if (!string.IsNullOrEmpty(category))
        {
            posts = posts.Where(p => p.Category.Name == category).ToList();
        }

        if (!string.IsNullOrEmpty(search))
        {
            posts = posts.Where(p => p.Title.Contains(search) || p.Content.Contains(search)).ToList();
        }

        var model = new PostViewModel
        {
            Posts = posts,
            Search = search
        };

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "Name");
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(PostViewModel model)
    {
        if (ModelState.IsValid)
        {
            string? imageUrl = null;

            if (model.Image != null)
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(model.Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                imageUrl = "/images/" + fileName;
            }
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsPublished = true,
                ImageUrl = imageUrl,
                WriterId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CategoryId = model.CategoryId
            };
            _postRepository.AddPost(post);

            return RedirectToAction("Index");
        }
        return View(model);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _postRepository.GetPostById(id);
        if (post == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || (post.WriterId != userId && !await _userManager.IsInRoleAsync(user, "Admin")))
        {
            return Forbid();
        }

        ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "Name", post.CategoryId);
        var model = new PostViewModel
        {
            Id = post.PostId,
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
            ImageUrl = post.ImageUrl,
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Edit(PostViewModel model)
    {
        if (ModelState.IsValid)
        {
            var post = await _postRepository.GetPostById(model.Id);
            if (post == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || (post.WriterId != userId && !await _userManager.IsInRoleAsync(user, "Admin")))
            {
                return Forbid();
            }

            post.Title = model.Title;
            post.Description = model.Description;
            post.Content = model.Content;
            post.UpdatedAt = DateTime.Now;
            post.CategoryId = model.CategoryId;

            if (model.Image != null)
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(model.Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                post.ImageUrl = "/images/" + fileName;
            }

            _postRepository.UpdatePost(post);

            return RedirectToAction("Details", new { id = post.PostId });
        }

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _postRepository.GetPostById(id);
        if (post == null)
        {
            return NotFound();
        }

        _postRepository.DeletePost(id);
        return Redirect(Request.Headers["Referer"].ToString());
    }


}
