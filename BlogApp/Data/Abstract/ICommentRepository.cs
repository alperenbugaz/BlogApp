using BlogApp.Data.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);

        Post GetPostByCommentId(int commentId); // Yeni metod

    }
}