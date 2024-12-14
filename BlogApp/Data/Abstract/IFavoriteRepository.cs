using BlogApp.Data.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IFavoriteRepository
    {   
        Post? GetPostByFavoriteId(int id);
        void AddFavorite(Favorite favorite);
        Task<Favorite?> GetFavoriteAsync(string userId, int postId);

        void DeleteFavorite(int id);
    }
}