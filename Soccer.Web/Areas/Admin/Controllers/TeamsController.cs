using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using Soccer.Web.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace Soccer.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamsController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        
        public TeamsController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {

                string path = string.Empty;

                if (teamViewModel.LogoFile != null)
                {
                    path = await contenedorTrabajo.Image.UploadImageAsync(teamViewModel.LogoFile, "Teams");
                }

                TeamEntity teamEntity = contenedorTrabajo.Converter.ToTeamEntity(teamViewModel, path, true);
                contenedorTrabajo.Team.Add(teamEntity);

                try
                {
                    contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already exists a team with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(teamViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity teamEntity = contenedorTrabajo.Team.Get(id);
            if (teamEntity == null)
            {
                return NotFound();
            }

            TeamViewModel teamViewModel = contenedorTrabajo.Converter.ToTeamViewModel(teamEntity);
            return View(teamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamViewModel teamViewModel)
        {
            if (id != teamViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string path = teamViewModel.LogoPath;

                if (teamViewModel.LogoFile != null)
                {
                    path = await contenedorTrabajo.Image.UploadImageAsync(teamViewModel.LogoFile, "Teams");
                }

                TeamEntity teamEntity = contenedorTrabajo.Converter.ToTeamEntity(teamViewModel, path, false);
                contenedorTrabajo.Team.Update(teamEntity);

                try
                {
                    contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, $"Already exists the team: {teamEntity.Name}.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(teamViewModel);
        }

        #region LLAMADAS A LA API

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = contenedorTrabajo.Team.GetTeams() });
        }

        #endregion

    }
}
