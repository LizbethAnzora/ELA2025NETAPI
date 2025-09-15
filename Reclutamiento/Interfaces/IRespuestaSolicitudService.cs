using System;
using Reclutamiento.DTOs.RespuestaSolicitudDTOs;

namespace Reclutamiento.Interfaces;

public interface IRespuestaSolicitudService
{
    Task<RespuestaSolicitudDTO> CreateRespuestaAsync(int solicitudId, string mensaje, int enviadaPorId);
    
    Task<IEnumerable<RespuestaSolicitudDTO>> GetRespuestasBySolicitudAsync(int solicitudId);
}