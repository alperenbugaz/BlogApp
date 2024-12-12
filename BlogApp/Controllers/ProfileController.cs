using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Entity;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    [Route("profile/{userName}")]
    public class ProfileController : Controller
    {
        private readonly UserManager<BlogAppUser> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly SignInManager<BlogAppUser> _signInManager;

        public ProfileController(UserManager<BlogAppUser> userManager, IPostRepository postRepository, SignInManager<BlogAppUser> signInManager)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string userName)
        {
            var user = await _userManager.Users
                .Include(u => u.Posts)
                    .ThenInclude(p => p.Comments)
                .Include(u => u.Posts)
                    .ThenInclude(p => p.Favorites)
                .Include(u => u.Comments)
                    .ThenInclude(c => c.Post)
                .Include(u => u.Favorites)
                    .ThenInclude(f => f.Post)
                    .ThenInclude(p => p.Writer)
                 .Include(u => u.Subscribers)
                .Include(u => u.Subscriptions)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return NotFound();
            }
            var currentUserId = _userManager.GetUserId(User);
            var isSubscribed = user.Subscribers.Any(s => s.SubscriberId == currentUserId);

            var publishedPosts = user.Posts?.Where(p => p.IsPublished).ToList() ?? new List<Post>();
            var model = new UserProfileViewModel
            {
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                ProfileImageUrl = user.ProfileImageUrl,
                Posts = publishedPosts,
                Comments = user.Comments?.ToList() ?? new List<Comment>(),
                Favorites = user.Favorites?.ToList() ?? new List<Favorite>(),
                PostWriter = user.Posts?.Select(p => p.Writer).ToList() ?? new List<BlogAppUser>(),
                SubscribersCount = user.Subscribers.Count,
                IsSubscribed = isSubscribed


            };

            return View(model);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }
            if (user != null)
            {
                var model = new EditProfileViewModel
                {
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    ProfileImageUrl = user.ProfileImageUrl
                };

                return View(model);
            }
            
            return View();

        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Name = model.Name;
                user.Surname = model.Surname;

                if (model.ProfileImage != null)
                {
                    var fileName = Path.GetFileName(model.ProfileImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfileImage.CopyToAsync(stream);
                    }

                    user.ProfileImageUrl = $"/images/{fileName}";
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Kullanıcıyı yeniden oturum açmaya zorla
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", new { userName = user.UserName });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}