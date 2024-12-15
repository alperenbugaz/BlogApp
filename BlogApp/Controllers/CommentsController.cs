using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class CommentsController : Controller
{
    private readonly ICommentRepository _commentRepository;

    private readonly INotificationRepository _notificationRepository;

    public CommentsController(ICommentRepository commentRepository, INotificationRepository notificationRepository)
    {
        _commentRepository = commentRepository;
        _notificationRepository = notificationRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(int postId, string CommentText)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        if (string.IsNullOrEmpty(CommentText))
        {
            return RedirectToAction("Details", "Post", new { id = postId });
        }

        Comment comment = new Comment
        {
            PostId = postId,
            CommentText = CommentText,
            CreatedAt = DateTime.Now,
            WriterId = userId ?? "0"

        };
        _commentRepository.AddComment(comment);

        var post = _commentRepository.GetPostByCommentId(comment.CommentId);
        if (post != null)
        {
            // Bildirim oluştur
            await _notificationRepository.CreateNotificationAsync(post.WriterId, userId, "commented on your post", postId);
        }
        return RedirectToAction("Details", "Post", new { id = postId });

    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _commentRepository.DeleteComment(id);
        return Redirect(Request.Headers["Referer"].ToString());

    }

}