using Microsoft.EntityFrameworkCore;
using Soccer.Web.DataAccess.Data;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<TournamentEntity> GetTournamentDetails(int id)
        {

            return await dbContext.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Local)
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Visitor)
                .Include(t => t.Groups)
                .ThenInclude(t => t.GroupDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<IEnumerable<TournamentEntity>> GetTournaments()
        {
            return await dbContext.Tournaments.ToListAsync();
        }

        public async Task<IEnumerable<TournamentEntity>> GetTournamentsAPI()
        {
            return await dbContext.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(g => g.GroupDetails)
                .ThenInclude(t => t.Team)
                .Include(g => g.Groups)
                .ThenInclude(m => m.Matches)
                .ThenInclude(l => l.Local)
                .Include(g => g.Groups)
                .ThenInclude(m => m.Matches)
                .ThenInclude(l => l.Visitor)
                .ToListAsync();
        }

        public void Update(TournamentEntity tournamentEntity)
        {
            if (tournamentEntity != null)
            {
                var objDesdeBd = dbContext.Tournaments.FirstOrDefault(a => a.Id == tournamentEntity.Id);

                objDesdeBd.Name = tournamentEntity.Name;
                objDesdeBd.LogoPath = tournamentEntity.LogoPath;
                objDesdeBd.IsActive = tournamentEntity.IsActive;
                objDesdeBd.StartDate = tournamentEntity.StartDate;
                objDesdeBd.EndDate = tournamentEntity.EndDate;

            }
        }

        public void Add(GroupEntity groupEntity)
        {
            dbContext.Groups.Add(groupEntity);
        }

        public async Task<GroupEntity> Edit(int id)
        {
            return await dbContext.Groups
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<GroupEntity> GetGroup(int id)
        {
            return await dbContext.Groups
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public void UpdateGroup(GroupEntity groupEntity)
        {
            if (groupEntity != null)
            {
                var objDesdeBd = dbContext.Groups.FirstOrDefault(a => a.Id == groupEntity.Id);

                objDesdeBd.Name = groupEntity.Name;

            }
        }

        public async Task<GroupEntity> DeleteGroup(int id)
        {
            var groupEntity = await dbContext.Groups
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (groupEntity != null)
            {
                dbContext.Groups.Remove(groupEntity);

                return groupEntity;
            }

            return null;
          
        }

        public async Task<GroupEntity> GetGroupDetails(int id)
        {
            return await dbContext.Groups
                .Include(g => g.Matches)
                .ThenInclude(g => g.Local)
                .Include(g => g.Matches)
                .ThenInclude(g => g.Visitor)
                .Include(g => g.Tournament)
                .Include(g => g.GroupDetails)
                .ThenInclude(gd => gd.Team)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public void UpdateDetailGroupEntity(GroupDetailEntity groupDetailEntity)
        {
            if (groupDetailEntity != null)
            {
                var objDesdeBd = dbContext.GroupDetails.FirstOrDefault(a => a.Id == groupDetailEntity.Id);

                objDesdeBd.MatchesPlayed = groupDetailEntity.MatchesPlayed;
                objDesdeBd.MatchesWon = groupDetailEntity.MatchesWon;
                objDesdeBd.MatchesTied = groupDetailEntity.MatchesTied;
                objDesdeBd.MatchesLost = groupDetailEntity.MatchesLost;
                objDesdeBd.GoalsFor = groupDetailEntity.GoalsFor;
                objDesdeBd.GoalsAgainst = groupDetailEntity.GoalsAgainst;
            }
        }

        public void AddMatchEntity(MatchEntity matchEntity)
        {
            dbContext.Matches.Add(matchEntity);
        }

        public void AddDetailGroupEntity(GroupDetailEntity groupDetailEntity) 
        {
            dbContext.GroupDetails.Add(groupDetailEntity);
        }

        public async Task<GroupDetailEntity> GetGroupDetailEntity(int id)
        {
            return await dbContext.GroupDetails
                .Include(gd => gd.Group)
                .Include(gd => gd.Team)
                .FirstOrDefaultAsync(gd => gd.Id == id);
        }

        public async Task<MatchEntity> GetMatch(int id)
        {
            return await dbContext.Matches
                .Include(m => m.Group)
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public void UpdateMatch(MatchEntity matchEntity)
        {
            if (matchEntity != null)
            {
                var objDesdeBd = dbContext.Matches.FirstOrDefault(a => a.Id == matchEntity.Id);

                objDesdeBd.Date = matchEntity.Date;
                objDesdeBd.GoalsLocal = matchEntity.GoalsLocal;
                objDesdeBd.GoalsVisitor = matchEntity.GoalsVisitor;
                objDesdeBd.IsClosed = matchEntity.IsClosed;

            }
        }

        public async Task<GroupDetailEntity> DeleteGroupDetail(int id)
        {
            var groupDetailEntity = await dbContext.GroupDetails
                .FirstOrDefaultAsync(m => m.Id == id);

            if (groupDetailEntity != null)
            {
                dbContext.GroupDetails.Remove(groupDetailEntity);

                return groupDetailEntity;
            }

            return null;

        }

        public async Task<MatchEntity> DeleteMatch(int id)
        {
            var matchEntity = await dbContext.Matches
                .FirstOrDefaultAsync(m => m.Id == id);

            if (matchEntity != null)
            {
                dbContext.Matches.Remove(matchEntity);

                return matchEntity;
            }

            return null;

        }

    }
}
