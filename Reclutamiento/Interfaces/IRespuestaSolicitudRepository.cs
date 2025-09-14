using System;
using Reclutamiento.Entidades;

namespace Reclutamiento.Interfaces;

public interface IRespuestaSolicitudRepository : IRepository<RespuestaSolicitud>
{
    Task<IEnumerable<RespuestaSolicitud>> GetBySolicitudIdAsync(int solicitudId);

}