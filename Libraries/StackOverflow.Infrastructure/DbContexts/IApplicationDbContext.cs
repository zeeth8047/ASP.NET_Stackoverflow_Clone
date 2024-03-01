using Microsoft.EntityFrameworkCore;
using StackOverflow.Infrastructure.Entities;
using StackOverflow.Infrastructure.Entities.Membership;

namespace StackOverflow.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}