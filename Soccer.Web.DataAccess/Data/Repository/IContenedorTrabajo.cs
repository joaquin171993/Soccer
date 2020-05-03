﻿using Soccer.Web.DataAccess.Data.Helpers;
using System;

namespace Soccer.Web.DataAccess.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IUserHelper Usuario { get; }
        ITeamRepository Team { get; }

        void Save();
    }
}