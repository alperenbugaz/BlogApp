using BlogApp.Data.Entity;
using IdentityApp.Data.Concrete.EFCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Abstract
{
    public class EfFavoriteRepository : IFavoriteRepository
    {   

        private readonly BlogAppContext _context;
        public EfFavoriteRepository(BlogAppContext context)
        {
            _context = context;
        }

        public async Task<Favorite?> GetFavoriteAsync(string userId, int postId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PostId == postId);
        }
        public void AddFavorite(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            _context.SaveChanges();
        }

        public void DeleteFavorite(int id)
        {
            var favorite = _context.Favorites.Find(id);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
            }
        }

        public Post GetPostByFavoriteId(int id)
        {   
            var favorite = _context.Favorites.Include(f => f.Post).FirstOrDefault(f => f.FavoriteId == id);
            return favorite?.Post;
        }
    }
}