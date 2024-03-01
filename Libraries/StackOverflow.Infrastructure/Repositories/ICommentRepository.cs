using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.Entities;

namespace StackOverflow.Infrastructure.Repositories
{
	public interface ICommentRepository : IRepository<Comment, int>
    {
	}
}