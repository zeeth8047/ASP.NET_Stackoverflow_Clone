using Autofac;
using AutoMapper;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Services;
using StackOverflow.Membership.Services;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models
{
	public class PostDetailsModel : BaseModel
	{
        private IPostService _postService;
        public PostDetailsModel(IPostService postService,
            UserManager userManager,
            HttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(userManager, httpContextAccessor, mapper)
        {
            _postService = postService;
        }

        public PostDetailsModel()
        {

        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();

            base.ResolveDependency(scope);
        }

        public Post Post { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Description can't be more than 2000 characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public async Task GetPostDetails(int id)
        {
            var post = new Post()
            {
                Id = id
            };
            Post = _postService.GetPostById(post.Id);
        }
    }
}
