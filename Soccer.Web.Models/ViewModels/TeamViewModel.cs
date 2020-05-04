using Microsoft.AspNetCore.Http;
using Soccer.Web.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Models.ViewModels
{
    public class TeamViewModel : TeamEntity
    {
        [Display(Name = "Logo")]
        public IFormFile LogoFile { get; set; }
    }
}
