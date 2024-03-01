using Microsoft.EntityFrameworkCore;
using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.DbContexts;
using StackOverflow.Infrastructure.Entities;

namespace StackOverflow.Infrastructure.Repositories
{
    public class PostRepository : Repository<Post, int>, IPostRepository
    {
        public PostRepository(IApplicationDbContext applicationDbContext) 
            : base((DbContext)applicationDbContext)
        {

        }
    }
}
