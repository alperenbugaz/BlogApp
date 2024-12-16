using BlogApp.Data.Concrete;

namespace BlogApp.Data.Entity
{
    public class Post
    {
        public int PostId { get; set; }

        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }

        public string? WriterId { get; set; }  // Foreign Key
        public BlogAppUser Writer { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Comment>? Comments { get; set; }

        public bool IsPublished { get; set; }

        public List<Favorite>? Favorites { get; set; }

        public int? CategoryId { get; set; }  // Foreign Key
        public Category? Category { get; set; }

        
    }
}