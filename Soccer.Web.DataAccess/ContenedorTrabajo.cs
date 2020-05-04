using Microsoft.AspNetCore.Identity;
using Soccer.Web.DataAccess.Data;
using Soccer.Web.DataAccess.Data.Helpers;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;

namespace Soccer.Web.DataAccess
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {

        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;  /*cada cambio que se debe hacer a un usuario, sea cualquier accion, se hace a traves de este objeto*/
        private readonly SignInManager<ApplicationUser> signInManager; /*este permite realizar todo lo relacionado a inicios y cierres de sesion. Tiene inyeccion nativa igual que el UserManager*/
        private readonly RoleManager<IdentityRole> roleManager; /*esta trabaja directamente con la IdentityRole, y no con User directamente, NO OCUPA INYECTARSE TAMPOCO EN EL START UP*/

        public IUserHelper Usuario { get; private set; }
        public ITeamRepository Team { get; private set; }
        public ITournamentRepository Tournament { get; private set; }
        public IImageHelper Image { get; private set; }
        public IConverterHelper Converter { get; private set; }

        public ContenedorTrabajo(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

            Usuario = new UserHelper(this.userManager, this.signInManager, this.roleManager);
            Team = new TeamRepository(dbContext);
            Tournament = new TournamentRepository(dbContext);
            Image = new ImageHelper();
            Converter = new ConverterHelper();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
