using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Soccer.Web.DataAccess.Data.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboTeams();
        IEnumerable<SelectListItem> GetComboTeams(int id);
    }
}
