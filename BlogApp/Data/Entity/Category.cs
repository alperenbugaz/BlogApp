namespace BlogApp.Data.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public List<Post>? Posts { get; set; }

    }
}