using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Services;
using StackOverflow.Membership.Services;

namespace StackOverflow.Web.Models
{
	public class AllPostModel : BaseModel
	{
        private IPostService _postService;
        public AllPostModel(IPostService postService,
            UserManager userManager,
            HttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(userManager, httpContextAccessor, mapper)
        {
            _postService = postService;
        }

        public AllPostModel()
        {

        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();

            base.ResolveDependency(scope);
        }

        public IList<Post> Posts { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        
        public async Task GetPosts()
        {
            Posts = (_postService.GetPosts(1, 100, SearchTerm, "CreatedAt DESC")).records;
        }
    }
}
