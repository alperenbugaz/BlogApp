using BlogApp.Data.Entity;
using IdentityApp.Data.Concrete.EFCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Abstract
{
    public class EfPostRepository : IPostRepository
    {   

        private readonly BlogAppContext _context;
        public EfPostRepository(BlogAppContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                post.IsPublished = false;
                _context.Posts.Update(post);
                _context.SaveChanges();
            }
        }

        public IQueryable<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Writer);
        }

        public async Task<IEnumerable<Post>> GetRecentPostsAsync()
        {
            return await _context.Posts
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {   
            return await _context.Posts.FindAsync(id);
        }

        public void UpdatePost(Post post)
        {
            var updatedpost = _context.Posts.Find(post.PostId);
            if (post != null)
            {
                post.Title = post.Title;
                post.Content = post.Content;
                post.IsPublished = post.IsPublished;
                post.CreatedAt = post.CreatedAt;
                post.UpdatedAt = DateTime.Now;
                _context.Posts.Update(post);
                _context.SaveChanges();
            }     

        }
    }
}