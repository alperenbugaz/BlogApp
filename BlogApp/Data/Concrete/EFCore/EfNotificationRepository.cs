using BlogApp.Data.Abstract;
using BlogApp.Data.Entity;
using IdentityApp.Data.Concrete.EFCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete;

public class EfNotificationRepository : INotificationRepository
{
    private readonly BlogAppContext _context;

    public EfNotificationRepository(BlogAppContext context)
    {
        _context = context;
    }


    public async Task CreateNotificationAsync(string userId, string actorId, string notificationType, int? postId = null)
    {
        var notification = new Notification
        {
            UserId = userId,
            ActorId = actorId,
            NotificationType = notificationType,
            PostId = postId
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }

    public List<Notification> GetNotificationsByUserId(string id)
    {
        return _context.Notifications.Where(n => n.UserId == id).ToList();
    }

    public async Task<List<Notification>>GetUnreadNotificationsByUserIdAsync(string id)
    {
        return await _context.Notifications
        .Include(n => n.Actor)
        .Where(n => n.UserId == id && !n.IsRead).ToListAsync();
    }

    public void MarkAsRead(int id)
    {
        var notification = _context.Notifications.Find(id);
        if (notification != null)
        {
            notification.IsRead = true;
            _context.SaveChanges();
        }
    }


}