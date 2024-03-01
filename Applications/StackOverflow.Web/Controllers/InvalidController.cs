using Microsoft.AspNetCore.Mvc;

namespace StackOverflow.Web.Controllers
{
    public class InvalidController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
