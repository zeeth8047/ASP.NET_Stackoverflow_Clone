using Autofac;
using AutoMapper;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Services;
using StackOverflow.Membership.Services;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.MyProfile.Models
{
	public class ListPostModel : BaseModel
	{
        private IPostService _postService;
        public ListPostModel(IPostService postService,
            UserManager userManager,
            HttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(userManager, httpContextAccessor, mapper)
        {
            _postService = postService;
        }

        public ListPostModel()
        {

        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();

            base.ResolveDependency(scope);
        }

        public IList<Post> Posts { get; set; }
        public async Task GetPosts()
		{
            await GetUserInfoAsync();
            Posts = (_postService.GetAllPosts(1, 100, null, "CreatedAt DESC", UserInfo!.Id)).records;
		}

        public void DeletePost(int id)
        {
            _postService.DeletePost(id);
        }
    }
}