using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents;

public class NotificationsViewComponent : ViewComponent
{
    private readonly INotificationRepository _notificationRepository;
    private readonly UserManager<BlogAppUser> _userManager;

    public NotificationsViewComponent(INotificationRepository notificationRepository, UserManager<BlogAppUser> userManager)
    {
        _notificationRepository = notificationRepository;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {   
        
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
        {
            return Content("User not found");
        }
        var notifications = _notificationRepository.GetUnreadNotificationsByUserId(user.Id);
        var unreadCount = notifications.Count(n => !n.IsRead);
        
        ViewBag.UnreadCount = unreadCount;

        return View(notifications);
    }
}