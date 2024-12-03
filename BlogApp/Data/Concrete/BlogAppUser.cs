using BlogApp.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Data.Concrete 
{
    public class BlogAppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string? ProfileImageUrl { get; set; }

        public List<Post>? Posts { get; set; } 


        public List<Comment>? Comments { get; set; }

        
    }

}