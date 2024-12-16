using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class NotificationsController : Controller
{
    private readonly INotificationRepository _notificationRepository;
    private readonly UserManager<BlogAppUser> _userManager;

    public NotificationsController(INotificationRepository notificationRepository, UserManager<BlogAppUser> userManager)
    {
        _notificationRepository = notificationRepository;
        _userManager = userManager;
    }


    public async Task<IActionResult> MarkAsRead(int id)
    {
        _notificationRepository.MarkAsRead(id);
        return Redirect(Request.Headers["Referer"].ToString());
    }
}