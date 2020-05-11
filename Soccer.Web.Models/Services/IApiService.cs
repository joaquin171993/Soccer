using Soccer.Web.Models.Models;
using System.Threading.Tasks;

namespace Soccer.Web.Models.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
    }
}
