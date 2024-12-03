using System.ComponentModel.DataAnnotations;
using BlogApp.Data.Concrete;

namespace BlogApp.Data.Entity
{
    public class Comment
    {   
        [Key]
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public int PostId { get; set; }
        public string WriterId { get; set; } = null!;
        public BlogAppUser Writer { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

       public Post Post { get; set; } = null!;


    }
}