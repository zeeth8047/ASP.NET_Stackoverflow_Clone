using Microsoft.EntityFrameworkCore;
using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.DbContexts;
using StackOverflow.Infrastructure.Entities;

namespace StackOverflow.Infrastructure.Repositories
{
	public class CommentRepository : Repository<Comment, int>, ICommentRepository
    {
        public CommentRepository(IApplicationDbContext applicationDbContext)
            : base((DbContext)applicationDbContext)
        {
        }
    }
}