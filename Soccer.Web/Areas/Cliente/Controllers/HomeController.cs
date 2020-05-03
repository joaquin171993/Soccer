using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Soccer.Web.Models;
using System.Diagnostics;

namespace Soccer.Web.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
