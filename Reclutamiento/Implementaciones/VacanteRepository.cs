using System;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class VacanteRepository : Repository<Vacante>, IVacanteRepository
{
    public VacanteRepository(ReclutamientoContext context) : base(context) { }

        public async Task<IEnumerable<Vacante>> GetAllActiveAsync()
        {
            return await _context.Vacantes.Where(v => v.EstaActiva).ToListAsync();
        }

}
