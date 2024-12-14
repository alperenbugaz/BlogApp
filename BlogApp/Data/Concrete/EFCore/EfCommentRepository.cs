using BlogApp.Data.Entity;
using IdentityApp.Data.Concrete.EFCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Abstract
{
    public class EfCommentRepository : ICommentRepository
    {   

        private readonly BlogAppContext _context;
        public EfCommentRepository(BlogAppContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }


        public void DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public IQueryable<Post> GetAllComments()
        {
            throw new NotImplementedException();

        }

        public Task<Post> GetCommentsById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            
            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public Post GetPostByCommentId(int commentId)
        {
            var comment = _context.Comments.Include(c => c.Post).FirstOrDefault(c => c.CommentId == commentId);
            return comment?.Post;
        }

    }
}