using StackOverflow.Infrastructure.Data;

namespace StackOverflow.Infrastructure.Entities
{
    public class Vote : IEntity<int>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
    }
}