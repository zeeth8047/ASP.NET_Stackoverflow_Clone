using StackOverflow.Infrastructure.Entities;

namespace StackOverflow.Infrastructure.BusinessObjects
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsAcceptedAsAnswer { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<Vote>? Votes { get; set; }
        public ApplicationMember ApplicationUser { get; set; }
        public Guid UserId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
