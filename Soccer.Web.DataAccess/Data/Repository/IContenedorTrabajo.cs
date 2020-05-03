using System;

namespace Soccer.Web.DataAccess.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        void Save();
    }
}
