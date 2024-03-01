using StackOverflow.Infrastructure.BusinessObjects;

namespace StackOverflow.Infrastructure.Services
{
	public interface ICommentService
	{
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
        Comment GetCommentById(int id);
        int CommentApprove(int id);
        int CommentDisapprove(int id);
    }
}
