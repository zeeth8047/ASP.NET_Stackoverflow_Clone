using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Areas.MyProfile.Models;

namespace StackOverflow.Web.Areas.MyProfile.Controllers
{
	[Area("MyProfile")]
	public class CommentController : Controller
	{
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger,
            ILifetimeScope scope)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
		{
			return View();
		}

        public async Task<IActionResult> CreateComment()
        {
            var model = _scope.Resolve<CreateCommentModel>();
            model.ResolveDependency(_scope);
            return View(model);
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(CreateCommentModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreateComment();
                    return RedirectToAction("PostDetails", "Home", new { area = "", id = model.PostId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

            return View(model);
        }
    }
}
