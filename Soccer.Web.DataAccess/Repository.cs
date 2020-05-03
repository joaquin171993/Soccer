using Microsoft.EntityFrameworkCore;
using Soccer.Web.DataAccess.Data.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        /*Agrega una entidad a la base de datos dependiendo del tipo*/
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /*buscar un registro en la tabla de base de datos dependiento del tipo de dato, y devuelve un objeto*/
        public T Get(int? id)
        {
            return dbSet.Find(id);
        }

        public void Remove(int? id)
        {
            T entityToRemove = dbSet.Find(id);

            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return this.Context.Set<T>().AsNoTracking();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this.Context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.Context.Set<T>().Update(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            this.Context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.Context.SaveChangesAsync() > 0;
        }

    }
}
