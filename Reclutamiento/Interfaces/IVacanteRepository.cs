using System;
using Reclutamiento.Entidades;

namespace Reclutamiento.Interfaces;

public interface IVacanteRepository : IRepository<Vacante>
{
    Task<IEnumerable<Vacante>> GetAllActiveAsync();

}
