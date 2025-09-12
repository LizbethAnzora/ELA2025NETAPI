using System;
using Reclutamiento.DTOs.SolicitudDTOs;

namespace Reclutamiento.Interfaces;

public interface ISolicitudService
{
    Task<SolicitudDTO> GetSolicitudByIdAsync(int id);
    Task<SolicitudDTO> CreateSolicitudAsync(SolicitudCrearDTO dto, int usuarioId);
    Task<IEnumerable<SolicitudDTO>> GetSolicitudesByVacanteAsync(int vacanteId);
    Task<IEnumerable<SolicitudDTO>> GetSolicitudesByUserAsync(int userId);
    Task UpdateSolicitudAsync(int id, SolicitudActualizarDTO dto);
    Task<byte[]> GenerateSolicitudPdfAsync(int id);
    Task RejectSolicitudAsync(int id);

}
