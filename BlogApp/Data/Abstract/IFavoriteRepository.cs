using BlogApp.Data.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IFavoriteRepository
    {   
        Task<Favorite> GetPostById(int id);
        void AddFavorite(Favorite favorite);
        Task<Favorite?> GetFavoriteAsync(string userId, int postId);

        void DeleteFavorite(int id);
    }
}