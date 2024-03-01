using Autofac;
using AutoMapper;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Services;
using StackOverflow.Membership.Services;
using StackOverflow.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace StackOverflow.Web.Areas.MyProfile.Models
{
	public class CreatePostModel : BaseModel
	{
        private IPostService _postService;
        public CreatePostModel(IPostService postService,
            UserManager userManager,
            HttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(userManager, httpContextAccessor, mapper)
        {
            _postService = postService;
        }

        public CreatePostModel()
        {
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();

            base.ResolveDependency(scope);
        }

        public async Task CreatePost()
        {
            if (IsValidPostTitle(Title))
            {
                throw new InvalidOperationException("Post title is invalid.");
            }
            
            var data = JsonSerializer.Deserialize<List<Value>>(Tag);
            await GetUserInfoAsync();
            var post = new Post
            {
                Title = Title,
                Description = Description,
                CreatedAt = CreatedAt,
                UserId = UserInfo!.Id
            };

            var tags = new List<Tag>();
            foreach (var tag in data)
            {
                tags.Add(new Tag { Name = tag.value });
            }
            post.Tags = tags;
            _postService.CreatePost(post);
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Title can't be more than 150 characters.")]
        [DataType(DataType.Text)]
        public string? Title { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Description can't be more than 2000 characters.")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Tag { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public Guid? UserId { get; set; }

        private bool IsValidPostTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;
            else if (title.Length > 150)
                return false;
            else
                return true;
        }
    }
}

public class Value
{
    public string value { get; set; }
}