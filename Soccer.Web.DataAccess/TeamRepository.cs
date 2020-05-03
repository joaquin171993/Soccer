﻿using Soccer.Web.DataAccess.Data;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Web.DataAccess
{
    public class TeamRepository : Repository<TeamEntity>, ITeamRepository
    {
        private readonly ApplicationDbContext dbContext;
        
        public TeamRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TeamEntity> GetTeams()
        {
            return dbContext.Teams.ToList();
        }
    }
}