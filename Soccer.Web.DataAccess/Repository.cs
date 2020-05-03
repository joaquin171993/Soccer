using Microsoft.EntityFrameworkCore;
using Soccer.Web.DataAccess.Data;
using Soccer.Web.DataAccess.Data.Repository;

namespace Soccer.Web.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        internal DbSet<T> dbSet;
        
        public Repository(ApplicationDbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }
    }
}
