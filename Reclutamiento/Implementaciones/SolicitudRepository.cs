using System;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class SolicitudRepository : Repository<Solicitud>, ISolicitudRepository
{
    public SolicitudRepository(ReclutamientoContext context) : base(context) { }

    public async Task<IEnumerable<Solicitud>> GetByVacanteIdAsync(int vacanteId)
    {
        return await _context.Solicitudes.Where(s => s.IdVacante == vacanteId).ToListAsync();
    }

    public async Task<IEnumerable<Solicitud>> GetByUserIdAsync(int userId)
    {
        return await _context.Solicitudes.Where(s => s.IdUsuario == userId).ToListAsync();
    }
}



