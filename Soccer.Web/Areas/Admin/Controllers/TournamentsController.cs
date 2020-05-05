using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using Soccer.Web.Models.ViewModels;
using System;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.LogoFile != null)
                {
                    path = await contenedorTrabajo.Image.UploadImageAsync(model.LogoFile, "Tournaments");
                }

                TournamentEntity tournament = contenedorTrabajo.Converter.ToTournamentEntity(model, path, true);
                contenedorTrabajo.Tournament.Add(tournament);
                contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TournamentEntity tournamentEntity = contenedorTrabajo.Tournament.Get(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            TournamentViewModel model = contenedorTrabajo.Converter.ToTournamentViewModel(tournamentEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.LogoPath;

                if (model.LogoFile != null)
                {
                    path = await contenedorTrabajo.Image.UploadImageAsync(model.LogoFile, "Tournaments");
                }

                TournamentEntity tournamentEntity = contenedorTrabajo.Converter.ToTournamentEntity(model, path, false);
                contenedorTrabajo.Tournament.Update(tournamentEntity);
                contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await contenedorTrabajo.Tournament.GetTournamentDetails(id.Value);

            if (tournamentEntity == null)
            {
                return NotFound();
            }

            return View(tournamentEntity);
        }

        public IActionResult AddGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TournamentEntity tournamentEntity = contenedorTrabajo.Tournament.Get(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            GroupViewModel model = new GroupViewModel
            {
                Tournament = tournamentEntity,
                TournamentId = tournamentEntity.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroup(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                GroupEntity groupEntity = await contenedorTrabajo.Converter.ToGroupEntityAsync(model, true);
                contenedorTrabajo.Tournament.Add(groupEntity);
                contenedorTrabajo.Save();
                return RedirectToAction("Details", "Tournaments", new { id = model.TournamentId });
            }

            return View(model);
        }

        public async Task<IActionResult> EditGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupEntity groupEntity = await contenedorTrabajo.Tournament.GetGroup(id.Value);

            if (groupEntity == null)
            {
                return NotFound();
            }

            GroupViewModel model = contenedorTrabajo.Converter.ToGroupViewModel(groupEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                GroupEntity groupEntity = await contenedorTrabajo.Converter.ToGroupEntityAsync(model, false);
                contenedorTrabajo.Tournament.UpdateGroup(groupEntity);
                contenedorTrabajo.Save();
                return RedirectToAction("Details", "Tournaments", new { id = model.TournamentId });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupEntity groupEntity = await contenedorTrabajo.Tournament.DeleteGroup(id.Value);

            if (groupEntity != null)
            {
                contenedorTrabajo.Save();
                return RedirectToAction("Details", "Tournaments", new { id = groupEntity.Tournament.Id });
            }

            return NotFound();
        }

        public async Task<IActionResult> DetailsGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupEntity groupEntity = await contenedorTrabajo.Tournament.GetGroupDetails(id.Value);
            
            if (groupEntity == null)
            {
                return NotFound();
            }

            return View(groupEntity);
        }

        public async Task<IActionResult> AddGroupDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupEntity groupEntity = await contenedorTrabajo.Tournament.GetGroup(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }

            GroupDetailViewModel model = new GroupDetailViewModel
            {
                Group = groupEntity,
                GroupId = groupEntity.Id,
                Teams = contenedorTrabajo.Combos.GetComboTeams()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroupDetail(GroupDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                GroupDetailEntity groupDetailEntity = await contenedorTrabajo.Converter.ToGroupDetailEntityAsync(model, true);
                contenedorTrabajo.Tournament.AddDetailGroupEntity(groupDetailEntity);
                contenedorTrabajo.Save();
                return RedirectToAction("DetailsGroup", "Tournaments", new { id = model.GroupId });
            }

            model.Group = await contenedorTrabajo.Tournament.GetGroup(model.GroupId);
            model.Teams = contenedorTrabajo.Combos.GetComboTeams();
            return View(model);
        }

        public async Task<IActionResult> AddMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupEntity groupEntity = await contenedorTrabajo.Tournament.GetGroup(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }

            MatchViewModel model = new MatchViewModel
            {
                Date = DateTime.Today,
                Group = groupEntity,
                GroupId = groupEntity.Id,
                Teams = contenedorTrabajo.Combos.GetComboTeams(groupEntity.Id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMatch(MatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.LocalId != model.VisitorId)
                {
                    MatchEntity matchEntity = await contenedorTrabajo.Converter.ToMatchEntityAsync(model, true);
                    contenedorTrabajo.Tournament.AddMatchEntity(matchEntity);
                    contenedorTrabajo.Save();
                    return RedirectToAction("DetailsGroup", "Tournaments", new { id = model.GroupId });
                }

                ModelState.AddModelError(string.Empty, "The local and visitor must be differents teams.");
            }

            model.Group = await contenedorTrabajo.Tournament.GetGroup(model.GroupId);
            model.Teams = contenedorTrabajo.Combos.GetComboTeams(model.GroupId);
            return View(model);
        }

        public async Task<IActionResult> EditGroupDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupDetailEntity groupDetailEntity = await contenedorTrabajo.Tournament.GetGroupDetailEntity(id.Value);
            if (groupDetailEntity == null)
            {
                return NotFound();
            }

            GroupDetailViewModel model = contenedorTrabajo.Converter.ToGroupDetailViewModel(groupDetailEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroupDetail(GroupDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                GroupDetailEntity groupDetailEntity = await contenedorTrabajo.Converter.ToGroupDetailEntityAsync(model, false);
                contenedorTrabajo.Tournament.UpdateDetailGroupEntity(groupDetailEntity);
                contenedorTrabajo.Save();
                return RedirectToAction("DetailsGroup", "Tournaments", new { id = model.GroupId });
            }

            return View(model);
        }

        public async Task<IActionResult> EditMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchEntity matchEntity = await contenedorTrabajo.Tournament.GetMatch(id.Value);
            if (matchEntity == null)
            {
                return NotFound();
            }

            MatchViewModel model = contenedorTrabajo.Converter.ToMatchViewModel(matchEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMatch(MatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                MatchEntity matchEntity = await contenedorTrabajo.Converter.ToMatchEntityAsync(model, false);
                contenedorTrabajo.Tournament.UpdateMatch(matchEntity);
                contenedorTrabajo.Save();
                return RedirectToAction("DetailsGroup", "Tournaments", new { id = model.GroupId });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteGroupDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupDetailEntity groupDetailEntity = await contenedorTrabajo.Tournament.DeleteGroupDetail(id.Value);
            if (groupDetailEntity == null)
            {
                return NotFound();
            }

            contenedorTrabajo.Save();
            return RedirectToAction("DetailsGroup", "Tournaments", new { id = groupDetailEntity.Group.Id });
        }

        public async Task<IActionResult> DeleteMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchEntity matchEntity = await contenedorTrabajo.Tournament.DeleteMatch(id.Value);
            if (matchEntity == null)
            {
                return NotFound();
            }

            contenedorTrabajo.Save();
            return RedirectToAction($"{nameof(DetailsGroup)}/{matchEntity.Group.Id}");
        }
    }

}

