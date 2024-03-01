using StackOverflow.Infrastructure.Data;
using StackOverflow.Infrastructure.Entities.Membership;

namespace StackOverflow.Infrastructure.Entities
{
    public class Comment : IEntity<int>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsAcceptedAsAnswer { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<Vote>? Votes { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid UserId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}