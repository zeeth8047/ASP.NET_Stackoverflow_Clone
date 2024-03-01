using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.Repositories;

namespace StackOverflow.Infrastructure.UnitOfWorks
{
    public interface IStackOverflowUnitOfWork : IUnitOfWork
    {
        public IPostRepository PostRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IVoteRepository VoteRepository { get; set; }
    }
}