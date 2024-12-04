
using BlogApp.Data.Concrete;

namespace BlogApp.Data.Entity;

public class Favorite
{
    public int FavoriteId { get; set; }
    public int PostId { get; set; }
    public string UserId { get; set; } = null!;
    public BlogAppUser User { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Post Post { get; set; } = null!;
}