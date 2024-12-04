using BlogApp.Data.Concrete;
using BlogApp.Data.Entity;

namespace BlogApp.ViewModels
{
    public class UserProfileViewModel
    {   
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public string UserName { get; set; } = null!;
        public List<Post>? Posts { get; set; }

        public IFormFile? Image { get; set; }

        public string? ProfileImageUrl { get; set; }

        public List<Comment>?  Comments { get; set; }

        public List<Favorite>? Favorites { get; set; }

        public List<BlogAppUser>? PostWriter { get; set; }
    }
}