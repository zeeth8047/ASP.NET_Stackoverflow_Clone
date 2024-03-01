using Microsoft.EntityFrameworkCore;
using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.DbContexts;
using StackOverflow.Infrastructure.Entities;

namespace StackOverflow.Infrastructure.Repositories
{
    public class VoteRepository : Repository<Vote, int>, IVoteRepository
    {
        public VoteRepository(IApplicationDbContext applicationDbContext)
            : base((DbContext)applicationDbContext)
        {

        }
    }
}
