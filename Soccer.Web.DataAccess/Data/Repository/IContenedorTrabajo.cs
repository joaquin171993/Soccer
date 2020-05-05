using Soccer.Web.DataAccess.Data.Helpers;
using System;

namespace Soccer.Web.DataAccess.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IUserHelper Usuario { get; }
        ITeamRepository Team { get; }
        ITournamentRepository Tournament { get; }
        IImageHelper Image { get; }
        IConverterHelper Converter { get; }
        ICombosHelper Combos { get; }
        void Save();
    }
}
