using StackOverflow.Infrastructure.Data;

namespace StackOverflow.Infrastructure.Entities
{
    public class Tag : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}