using System;
using Reclutamiento.DTOs.SolicitudDTOs;
using Reclutamiento.Interfaces;
using Reclutamiento.Entidades;


namespace Reclutamiento.Implementaciones;

public class SolicitudService : ISolicitudService
{
    private readonly ISolicitudRepository _solicitudRepository;

    public SolicitudService(ISolicitudRepository solicitudRepository)
    {
        _solicitudRepository = solicitudRepository;
    }

    public async Task<SolicitudDTO> GetSolicitudByIdAsync(int id)
    {
        var solicitud = await _solicitudRepository.GetByIdAsync(id);
        if (solicitud == null) return null;
        return new SolicitudDTO { Id = solicitud.Id, IdUsuario = solicitud.IdUsuario, IdVacante = solicitud.IdVacante, NombreCompleto = solicitud.NombreCompleto, CorreoElectronico = solicitud.CorreoElectronico, NumeroTelefono = solicitud.NumeroTelefono, Foto = solicitud.Foto, CamposPersonalizados = solicitud.CamposPersonalizados, Estado = solicitud.Estado, RespuestaEnviada = solicitud.RespuestaEnviada, FechaEnvio = solicitud.FechaEnvio };
    }

    public async Task<SolicitudDTO> CreateSolicitudAsync(SolicitudCrearDTO dto, int usuarioId)
    {
        var solicitud = new Solicitud
        {
            IdUsuario = usuarioId,
            IdVacante = dto.IdVacante,
            NombreCompleto = dto.NombreCompleto,
            CorreoElectronico = dto.CorreoElectronico,
            NumeroTelefono = dto.NumeroTelefono,
            Foto = dto.Foto,
            CamposPersonalizados = dto.CamposPersonalizados,
            Estado = EstadoSolicitud.Pendiente,
            RespuestaEnviada = false,
            FechaEnvio = DateTime.UtcNow
        };
        await _solicitudRepository.AddAsync(solicitud);
        await _solicitudRepository.SaveAsync();
        return new SolicitudDTO { Id = solicitud.Id, IdUsuario = solicitud.IdUsuario, IdVacante = solicitud.IdVacante, NombreCompleto = solicitud.NombreCompleto, CorreoElectronico = solicitud.CorreoElectronico, NumeroTelefono = solicitud.NumeroTelefono, Foto = solicitud.Foto, CamposPersonalizados = solicitud.CamposPersonalizados, Estado = solicitud.Estado, RespuestaEnviada = solicitud.RespuestaEnviada, FechaEnvio = solicitud.FechaEnvio };
    }

    public async Task<IEnumerable<SolicitudDTO>> GetSolicitudesByVacanteAsync(int vacanteId)
    {
        var solicitudes = await _solicitudRepository.GetByVacanteIdAsync(vacanteId);
        return solicitudes.Select(s => new SolicitudDTO { Id = s.Id, IdUsuario = s.IdUsuario, IdVacante = s.IdVacante, NombreCompleto = s.NombreCompleto, CorreoElectronico = s.CorreoElectronico, NumeroTelefono = s.NumeroTelefono, Foto = s.Foto, CamposPersonalizados = s.CamposPersonalizados, Estado = s.Estado, RespuestaEnviada = s.RespuestaEnviada, FechaEnvio = s.FechaEnvio });
    }
    public async Task<IEnumerable<SolicitudDTO>> GetSolicitudesByUserAsync(int userId)
    {
        var solicitudes = await _solicitudRepository.GetByUserIdAsync(userId);
        return solicitudes.Select(s => new SolicitudDTO { Id = s.Id, IdUsuario = s.IdUsuario, IdVacante = s.IdVacante, NombreCompleto = s.NombreCompleto, CorreoElectronico = s.CorreoElectronico, NumeroTelefono = s.NumeroTelefono, Foto = s.Foto, CamposPersonalizados = s.CamposPersonalizados, Estado = s.Estado, RespuestaEnviada = s.RespuestaEnviada, FechaEnvio = s.FechaEnvio });
    }

    public async Task UpdateSolicitudAsync(int id, SolicitudActualizarDTO dto)

    {
        var solicitud = await _solicitudRepository.GetByIdAsync(id);
        if (solicitud == null) throw new Exception("Solicitud no encontrada.");
        solicitud.NombreCompleto = dto.NombreCompleto ?? solicitud.NombreCompleto;
        solicitud.CorreoElectronico = dto.CorreoElectronico ?? solicitud.CorreoElectronico;
        solicitud.NumeroTelefono = dto.NumeroTelefono ?? solicitud.NumeroTelefono;
        solicitud.Foto = dto.Foto ?? solicitud.Foto;
        solicitud.CamposPersonalizados = dto.CamposPersonalizados ?? solicitud.CamposPersonalizados;
        _solicitudRepository.Update(solicitud);
        await _solicitudRepository.SaveAsync();
    }

    public async Task<byte[]> GenerateSolicitudPdfAsync(int id)
    {
        // Lógica para generar PDF.
        return new byte[] { /* bytes del PDF */ };
    }

    public async Task RejectSolicitudAsync(int id)
    {
        var solicitud = await _solicitudRepository.GetByIdAsync(id);
        if (solicitud == null) throw new Exception("Solicitud no encontrada.");
        solicitud.Estado = EstadoSolicitud.Rechazada;
        _solicitudRepository.Update(solicitud);
        await _solicitudRepository.SaveAsync();
    }
}