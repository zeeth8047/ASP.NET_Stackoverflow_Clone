using Autofac;
using AutoMapper;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Services;
using StackOverflow.Membership.Services;
using StackOverflow.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Areas.MyProfile.Models
{
	public class UpdatePostModel : BaseModel
	{
        private IPostService _postService;
        public UpdatePostModel(IPostService postService,
            UserManager userManager,
            HttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(userManager, httpContextAccessor, mapper)
        {
            _postService = postService;
        }

        public UpdatePostModel()
        {
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();

            base.ResolveDependency(scope);
        }

        public async Task UpdatePost()
        {
            
            await GetUserInfoAsync();
            var post = new Post
            {
                Id=Id,
                Title = Title,
                Description = Description,
                UserId = UserInfo!.Id
            };
            _postService.UpdatePost(post);
        }

        public void LoadData(int id)
        {
            var data = _postService.GetPostById(id);
            
                Title = data.Title;
                Description = data.Description;
                UserId = data.UserId;            
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Title can't be more than 150 characters.")]
        [DataType(DataType.Text)]
        public string? Title { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Description can't be more than 150 characters.")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }
        //public string? Tag { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public Guid? UserId { get; set; }
    }
}