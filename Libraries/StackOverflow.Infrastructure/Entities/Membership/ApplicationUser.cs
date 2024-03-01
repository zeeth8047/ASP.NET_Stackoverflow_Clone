using Microsoft.AspNetCore.Identity;

namespace StackOverflow.Infrastructure.Entities.Membership
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? DisplayName { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}