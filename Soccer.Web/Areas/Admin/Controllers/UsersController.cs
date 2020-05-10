using Microsoft.AspNetCore.Mvc;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
         
        public UsersController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.contenedorTrabajo.Usuario.LoginAsync(model);

                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction(nameof(Index));
                }
            }

            this.ModelState.AddModelError(string.Empty, "Contraseña o email inválidos");

            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await contenedorTrabajo.Usuario.LogoutAsync();
            return RedirectToAction("Index", "Home", new { area = "Cliente" });

            //Fuente:https://www.iteramos.com/pregunta/22138/redirecttoaction-entre-las-areas
        }

    }
}
