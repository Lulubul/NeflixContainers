using Microsoft.AspNetCore.Mvc;

namespace Profiles.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
