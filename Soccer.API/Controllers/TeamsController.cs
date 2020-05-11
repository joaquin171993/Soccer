using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using System;
using System.Collections.Generic;

namespace Soccer.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public TeamsController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IEnumerable<TeamEntity> GetTeams()
        {
            return contenedorTrabajo.Team.GetTeams();
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TeamEntity teamEntity = contenedorTrabajo.Team.Get(id);

            if (teamEntity == null)
            {
                return NotFound();
            }

            return Ok(teamEntity);
        }

        [HttpPut("{id}")]
        public IActionResult PutEntityAsync([FromRoute] int id, [FromBody] TeamEntity teamEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamEntity.Id) /*valida que el id que se manda por url sea el mismo que trae el body del cuerpo del json*/
            {
                return BadRequest();
            }

            try
            {
                contenedorTrabajo.Team.Update(teamEntity, id);
                contenedorTrabajo.Save();
            }
            catch (Exception)
            {
                if (!contenedorTrabajo.Team.TeamEntityExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();   /*devuelve un status 200 sin respuesta*/

        }

        [HttpPost]
        public IActionResult PostTeamEntity([FromBody] TeamEntity teamEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                contenedorTrabajo.Team.Add(teamEntity);
                contenedorTrabajo.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetTeamEntity", new { id = teamEntity.Id }, teamEntity);

        }

        

    }
}
