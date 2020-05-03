using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;

namespace Soccer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public WeatherForecastController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var list = contenedorTrabajo.Team.GetTeams();

            return Ok(list);
        }
    }
}
