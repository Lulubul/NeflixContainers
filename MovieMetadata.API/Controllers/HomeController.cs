using Microsoft.AspNetCore.Mvc;

namespace MovieMetadata.API.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
