﻿using Microsoft.AspNetCore.Mvc;

namespace Marketing.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
