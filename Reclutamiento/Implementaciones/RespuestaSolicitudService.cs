using System;
using Reclutamiento.DTOs.RespuestaSolicitudDTOs;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class RespuestaSolicitudService : IRespuestaSolicitudService
{
    private readonly IRespuestaSolicitudRepository _respuestaRepository;
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IEmailService _emailService;

    public RespuestaSolicitudService(IRespuestaSolicitudRepository respuestaRepository, ISolicitudRepository solicitudRepository, IEmailService emailService)
    {
        _respuestaRepository = respuestaRepository;
        _solicitudRepository = solicitudRepository;
        _emailService = emailService;
    }

    public async Task<RespuestaSolicitudDTO> CreateRespuestaAsync(int solicitudId, string mensaje, int enviadaPorId)
    {
        var solicitud = await _solicitudRepository.GetByIdAsync(solicitudId);
        if (solicitud == null) throw new Exception("Solicitud no encontrada.");

        var respuesta = new RespuestaSolicitud
        {
            IdSolicitud = solicitudId,
            ContenidoMensaje = mensaje,
            EnviadaPor = enviadaPorId,
            FechaEnvio = DateTime.UtcNow
        };
        await _respuestaRepository.AddAsync(respuesta);
        await _respuestaRepository.SaveAsync();

        solicitud.RespuestaEnviada = true;
        _solicitudRepository.Update(solicitud);
        await _solicitudRepository.SaveAsync();

        await _emailService.SendEmailAsync(solicitud.CorreoElectronico, "Actualización sobre tu solicitud", mensaje);

        return new RespuestaSolicitudDTO { Id = respuesta.Id, IdSolicitud = respuesta.IdSolicitud, ContenidoMensaje = respuesta.ContenidoMensaje, FechaEnvio = respuesta.FechaEnvio };
    }

    public async Task<IEnumerable<RespuestaSolicitudDTO>> GetRespuestasBySolicitudAsync(int solicitudId)
    {
        var respuestas = await _respuestaRepository.GetBySolicitudIdAsync(solicitudId);
        return respuestas.Select(r => new RespuestaSolicitudDTO { Id = r.Id, IdSolicitud = r.IdSolicitud, ContenidoMensaje = r.ContenidoMensaje, FechaEnvio = r.FechaEnvio });
    }

}