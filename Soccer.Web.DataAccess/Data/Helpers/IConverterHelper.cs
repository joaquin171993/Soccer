using Soccer.Web.Models.Entities;
using Soccer.Web.Models.ViewModels;

namespace Soccer.Web.DataAccess.Data.Helpers
{
    public interface IConverterHelper
    {
        TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity teamEntity);
    }
}
