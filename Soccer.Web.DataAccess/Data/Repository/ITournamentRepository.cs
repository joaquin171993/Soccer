using Soccer.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess.Data.Repository
{
    public interface ITournamentRepository : IRepository<TournamentEntity>
    {
        Task<IEnumerable<TournamentEntity>> GetTournaments();

        void Update(TournamentEntity teamEntity);
    }
}
