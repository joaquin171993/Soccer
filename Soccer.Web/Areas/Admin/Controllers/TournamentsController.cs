using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;
using System.Threading.Tasks;

namespace Soccer.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TournamentsController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public TournamentsController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region LLAMADAS A LA API

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await contenedorTrabajo.Tournament.GetTournaments() });
        }

        #endregion
    }
}
