using System;

namespace Reclutamiento.Interfaces;

public interface ISolicitudRepository
{
    Task<IEnumerable<Solicitud>> GetByVacanteIdAsync(int vacanteId);
    Task<IEnumerable<Solicitud>> GetByUserIdAsync(int userId);

}
