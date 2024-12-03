using BlogApp.Data.Entity;

namespace BlogApp.ViewModels
{
    public class PostViewModel
    {   
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Post>? Posts { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }
    }
}