using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace StackOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger,
            ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public async Task<IActionResult> Index()
        {
            var model = _scope.Resolve<AllPostModel>();
            model.ResolveDependency(_scope);
            await model.GetPosts();
            
            return View(model);
        }

        public async Task<IActionResult> PostDetails(int id)
        {
            try
            {
                var model = _scope.Resolve<PostDetailsModel>();
                model.ResolveDependency(_scope);
                await model.GetPostDetails(id);
                if (model.Post is null)
                {
                    return RedirectToAction("Error404", "Invalid", new { area = "" });
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
        public async Task<IActionResult> UpdateComment(int id)
        {
            var model = _scope.Resolve<UpdateCommentModel>();
            model.ResolveDependency(_scope);
            model.LoadData(id);
            return View(model);
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateComment(UpdateCommentModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateComment();                    
                    return RedirectToAction("PostDetails", "Home", new { area = "", id = model.PostId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> PostVoteUp(int id)
        {
            var model = _scope.Resolve<VoteModel>();
            model.ResolveDependency(_scope);
            await model.PostVoteUp(id);
            return RedirectToAction("Index", "Home", new { area = "", id = id });
        }

        [Authorize]
        public async Task<IActionResult> PostVoteDown(int id)
        {
            var model = _scope.Resolve<VoteModel>();
            model.ResolveDependency(_scope);
            await model.PostVoteDown(id);
            return RedirectToAction("Index", "Home", new { area = "", id = id });
        }

        [Authorize]
		public async Task<IActionResult> CommentVoteUp(int id)
        {
            var model = _scope.Resolve<VoteModel>();
            model.ResolveDependency(_scope);
            await model.CommentVoteUp(id);
            return RedirectToAction("Index", "Home", new { area = "", id = id });
        }

        
        [Authorize]
		public async Task<IActionResult> CommentVoteDown(int id)
        {
            var model = _scope.Resolve<VoteModel>();
            model.ResolveDependency(_scope);
            await model.CommentVoteDown(id);
            return RedirectToAction("Index", "Home", new { area = "", id = id });
        }

        public async Task<IActionResult> CommentApprove(int id)
        {
            var model = _scope.Resolve<UpdateCommentModel>();
            model.ResolveDependency(_scope);
            var approve = model.CommentApprove(id);
            return RedirectToAction("PostDetails", "Home", new { area = "", id = approve });
        }

        public async Task<IActionResult> CommentDisapprove(int id)
        {
            var model = _scope.Resolve<UpdateCommentModel>();
            model.ResolveDependency(_scope);
            var disapprove = model.CommentDisapprove(id);
            return RedirectToAction("PostDetails", "Home", new { area = "", id = disapprove });
        }
    }
}