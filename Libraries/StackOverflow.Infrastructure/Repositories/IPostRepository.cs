using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.Entities;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IPostRepository : IRepository<Post, int>
    {
    }
}