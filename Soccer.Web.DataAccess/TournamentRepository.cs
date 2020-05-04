using Microsoft.EntityFrameworkCore;
using Soccer.Web.DataAccess.Data;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess
{
    public class TournamentRepository : Repository<TournamentEntity>, ITournamentRepository
    {
        private readonly ApplicationDbContext dbContext;
        
        public TournamentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TournamentEntity>> GetTournaments()
        {
            return await dbContext.Tournaments.ToListAsync();
        }

        public void Update(TournamentEntity teamEntity)
        {
           
        }
    }
}
