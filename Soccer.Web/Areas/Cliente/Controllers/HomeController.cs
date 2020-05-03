using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Soccer.Web.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        public ActionResult Index()
        {

            var list = contenedorTrabajo.Team.GetTeams();

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
