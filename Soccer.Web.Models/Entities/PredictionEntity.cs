using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Models.Entities
{
    public class PredictionEntity
    {
        public int Id { get; set; }

        public MatchEntity Match { get; set; }

        public ApplicationUser User { get; set; }

        [Display(Name = "Goals Local")]
        public int? GoalsLocal { get; set; }

        [Display(Name = "Goals Visitor")]
        public int? GoalsVisitor { get; set; }

        public int Points { get; set; }
    }
}
