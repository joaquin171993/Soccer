using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Models.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} can´t have more than {1} characters")]
        [Required(ErrorMessage = "the field {0} is mandatory")]
        public string Name { get; set; }

        [Display(Name="Logo")]
        public string LogoPath { get; set; }

        [Display(Name = "Logo")]
        public string LogoFullPath => string.IsNullOrEmpty(LogoPath)
            ? null
            : $"https://soccerapp.azurewebsites.net{LogoPath.Substring(1)}";

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
