using Soccer.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess.Data.Repository
{
    public interface ITournamentRepository : IRepository<TournamentEntity>
    {
        Task<IEnumerable<TournamentEntity>> GetTournaments();
        Task<TournamentEntity> GetTournamentDetails(int id);
        void Update(TournamentEntity teamEntity);
        void Add(GroupEntity groupEntity);
        Task<GroupEntity> GetGroup(int id);
        void UpdateGroup(GroupEntity groupEntity);
        Task<GroupEntity> DeleteGroup(int id);
        Task<GroupEntity> GetGroupDetails(int id);
        void UpdateDetailGroupEntity(GroupDetailEntity groupDetailEntity);
        void AddDetailGroupEntity(GroupDetailEntity groupDetailEntity);
        void AddMatchEntity(MatchEntity matchEntity);
        Task<GroupDetailEntity> GetGroupDetailEntity(int id);
        Task<MatchEntity> GetMatch(int id);
        void UpdateMatch(MatchEntity matchEntity);
        Task<GroupDetailEntity> DeleteGroupDetail(int id);
        Task<MatchEntity> DeleteMatch(int id);
    }
}
