using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class FavoritesController : Controller
{

    readonly IFavoriteRepository _favoriteRepository;
    readonly INotificationRepository _notificationRepository;

    public FavoritesController(IFavoriteRepository favoriteRepository, INotificationRepository notificationRepository)
    {
        _favoriteRepository = favoriteRepository;
        _notificationRepository = notificationRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite(int postId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var favorite = new Favorite
        {
            PostId = postId,
            UserId = userId,
            CreatedAt = DateTime.Now,
        };

        _favoriteRepository.AddFavorite(favorite);

        var post = _favoriteRepository.GetPostByFavoriteId(favorite.FavoriteId);
        if (post != null)
        {
            // Bildirim oluştur
            await _notificationRepository.CreateNotificationAsync(post.WriterId, userId, "favorited your post", postId);
        }

        return RedirectToAction("Details", "Post", new { id = postId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFavorite(int postId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var favorite = await _favoriteRepository.GetFavoriteAsync(userId, postId);
        if (favorite != null)
        {
            _favoriteRepository.DeleteFavorite(favorite.FavoriteId);
            Console.WriteLine("Favorite deleted");
        }

        return RedirectToAction("Details", "Post", new { id = postId });
    }
}