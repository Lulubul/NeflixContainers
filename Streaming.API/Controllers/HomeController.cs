using Microsoft.AspNetCore.Mvc;

namespace Streaming.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
