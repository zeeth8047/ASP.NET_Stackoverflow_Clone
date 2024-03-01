using Autofac;
using AutoMapper;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Services;
using StackOverflow.Membership.Services;
using StackOverflow.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Areas.MyProfile.Models
{
	public class CreateCommentModel : BaseModel
	{
        private ICommentService _commentService;
        public CreateCommentModel(ICommentService commentService,
            UserManager userManager,
            HttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(userManager, httpContextAccessor, mapper)
        {
            _commentService = commentService;
        }

        public CreateCommentModel()
        {

        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _commentService = _scope.Resolve<ICommentService>();

            base.ResolveDependency(scope);
        }

        public async Task CreateComment()
        {
            await GetUserInfoAsync();
            var comment = new Comment
            {
                Description = Description,
                CreatedAt = DateTimeOffset.Now,
                UserId = UserInfo!.Id,
                PostId = PostId
            };
            _commentService.CreateComment(comment);
        }

        public int Id { get; set; }

        [Required]
        [StringLength(1500, ErrorMessage = "Answer can't be more than 1500 characters.")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public int PostId { get; set; }
    }
}