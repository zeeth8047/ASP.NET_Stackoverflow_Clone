using Autofac;
using AutoMapper;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Services;
using StackOverflow.Membership.Services;

namespace StackOverflow.Web.Models
{
    public class VoteModel : BaseModel
    {
        private IVoteService _voteService;

        public VoteModel(IVoteService voteService,
            UserManager userManager,
            HttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(userManager, httpContextAccessor, mapper)
        {
            _voteService = voteService;
        }

        public VoteModel()
        {

        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _voteService = _scope.Resolve<IVoteService>();

            base.ResolveDependency(scope);
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Post? PostId { get; set; }
        public int? CommentId { get; set; }

        public async Task PostVoteUp(int id)
        {
            await GetUserInfoAsync();
            _voteService.PostVoteUp(id, UserInfo.Id);
        }

        public async Task PostVoteDown(int id)
        {
            await GetUserInfoAsync();
            _voteService.PostVoteDown(id, UserInfo.Id);
        }

        public async Task CommentVoteUp(int id)
        {
            await GetUserInfoAsync();
            _voteService.CommentVoteUp(id, UserInfo.Id);
        }

        public async Task CommentVoteDown(int id)
        {
            await GetUserInfoAsync();
            _voteService.CommentVoteDown(id, UserInfo.Id);
        }
    }
}
