using BlogApp.Data.Concrete;

namespace BlogApp.Data.Entity
{
    public class Notification
    {
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? ActorId { get; set; }
    public int? PostId { get; set; }
    public string? NotificationType { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public bool IsRead { get; set; } = false;

    public BlogAppUser User { get; set; } = null!;
    public BlogAppUser Actor { get; set; }  = null!;
    public Post? Post { get; set; } 


    }
}