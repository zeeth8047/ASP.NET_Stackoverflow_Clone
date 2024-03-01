using StackOverflow.Infrastructure.Entities.Membership;

namespace StackOverflow.Infrastructure.BusinessObjects
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Vote>? Votes { get; set; }
        public List<Tag>? Tags { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid UserId { get; set; }
    }
}