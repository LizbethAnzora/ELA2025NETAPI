using System;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class RespuestaSolicitudRepository : Repository<RespuestaSolicitud>, IRespuestaSolicitudRepository
{
    public RespuestaSolicitudRepository(ReclutamientoContext context) : base(context) { }

    public async Task<IEnumerable<RespuestaSolicitud>> GetBySolicitudIdAsync(int solicitudId)
    {
        return await _context.RespuestasSolicitudes.Where(r => r.IdSolicitud == solicitudId).ToListAsync();
    }

}