using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;
using System.Threading.Tasks;

namespace Soccer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public TournamentsController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTournaments()
        {
            return Ok(await contenedorTrabajo.Tournament.GetTournamentsAPI());
        }

    }
}
