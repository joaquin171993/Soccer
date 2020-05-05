using Soccer.Web.Models.Entities;

namespace Soccer.Web.Models.ViewModels
{
    public class GroupViewModel : GroupEntity
    {
        /*para poder guardarla con el id del torneo*/
        public int TournamentId { get; set; }
    }
}
