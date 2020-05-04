using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(int? id);

        void Add(T entity);

        void Remove(int? id);

        void Remove(T entity);

        IQueryable<T> GetAll();

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
