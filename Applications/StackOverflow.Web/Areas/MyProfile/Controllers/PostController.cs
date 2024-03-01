using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Infrastructure.Entities.Membership;
using StackOverflow.Web.Areas.MyProfile.Models;
using System.Text.Json;

namespace StackOverflow.Web.Areas.MyProfile.Controllers
{
    [Authorize()]
    [Area("MyProfile")]
    public class PostController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger, 
            ILifetimeScope scope,
            UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _userManager = userManager;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = _scope.Resolve<ListPostModel>();
            model.ResolveDependency(_scope);
            await model.GetPosts();

            return View(model);
        }
        
        public async Task<IActionResult> Create()
        {
            var model = _scope.Resolve<CreatePostModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreatePost();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

            return View(model);
        }
        
        public async Task<IActionResult> Update(int id)
        {
            var model = _scope.Resolve<UpdatePostModel>();
            model.ResolveDependency(_scope);
            model.LoadData(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdatePostModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdatePost();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = _scope.Resolve<ListPostModel>();
                model.ResolveDependency(_scope);
                model.DeletePost(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}