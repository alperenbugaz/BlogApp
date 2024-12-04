using BlogApp.Data.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IPostRepository
    {   
        IQueryable<Post> GetAllPosts();
        Task<Post> GetPostById(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);
    }
}