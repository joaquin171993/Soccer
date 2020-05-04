using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Soccer.Web.DataAccess.Data.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
