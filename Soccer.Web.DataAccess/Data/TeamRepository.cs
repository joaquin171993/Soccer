using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;

namespace Soccer.Web.DataAccess.Data
{
    public class TeamRepository : Repository<TeamEntity>, ITeamRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TeamRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
