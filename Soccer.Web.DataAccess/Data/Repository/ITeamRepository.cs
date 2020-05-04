using Soccer.Web.Models.Entities;
using System.Collections.Generic;

namespace Soccer.Web.DataAccess.Data.Repository
{
    public interface ITeamRepository : IRepository<TeamEntity>
    {
        IEnumerable<TeamEntity> GetTeams();

        void Update(TeamEntity teamEntity);
    }
}
