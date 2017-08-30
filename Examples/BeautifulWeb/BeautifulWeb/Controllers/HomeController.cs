using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeautifulColors;

namespace BeautifulWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var colorFactory = new ColorFactory();
            ViewData["Colors"] = colorFactory.RandomBeautiful(256).Select(c => c.ToString("HEX", null));
            return View();
        }
    }
}