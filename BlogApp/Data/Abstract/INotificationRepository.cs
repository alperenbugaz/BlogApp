using BlogApp.Data.Entity;

namespace BlogApp.Data.Abstract
{
    public interface INotificationRepository
    {
        Task CreateNotificationAsync(string userId, string actorId, string notificationType, int? postId = null);

        List<Notification> GetNotificationsByUserId(string id);
        List<Notification> GetUnreadNotificationsByUserId(string id);
        void MarkAsRead(int id);
    }
}