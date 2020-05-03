using Microsoft.AspNetCore.Identity;

namespace Soccer.Web.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellidos  { get; set; }
    }
}
