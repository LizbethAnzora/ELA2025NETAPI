using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reclutamiento.DTOs.RespuestaSolicitudDTOs;
using Reclutamiento.DTOs.SolicitudDTOs;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly ISolicitudService _solicitudService;
        private readonly IRespuestaSolicitudService _respuestaService;

        public SolicitudesController(ISolicitudService solicitudService, IRespuestaSolicitudService respuestaService)
        {
            _solicitudService = solicitudService;
            _respuestaService = respuestaService;
        }

        [HttpPost]
        public async Task<ActionResult<SolicitudDTO>> CreateSolicitud([FromBody] SolicitudCrearDTO dto)
        {
            // ID de usuario fijo para el ejemplo.
            int usuarioId = 2;
            var newSolicitud = await _solicitudService.CreateSolicitudAsync(dto, usuarioId);
            return CreatedAtAction(nameof(GetSolicitud), new { id = newSolicitud.Id }, newSolicitud);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudDTO>> GetSolicitud(int id)
        {
            var solicitud = await _solicitudService.GetSolicitudByIdAsync(id);
            if (solicitud == null) return NotFound();
            return Ok(solicitud);
        }

        [HttpGet("usuario/{userId}")]
        public async Task<ActionResult<IEnumerable<SolicitudDTO>>> GetSolicitudesPorUsuario(int userId)
        {
            var solicitudes = await _solicitudService.GetSolicitudesByUserAsync(userId);
            return Ok(solicitudes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSolicitud(int id, [FromBody] SolicitudActualizarDTO dto)
        {
            await _solicitudService.UpdateSolicitudAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}/rechazar")]
        public async Task<IActionResult> RejectSolicitud(int id)
        {
            await _solicitudService.RejectSolicitudAsync(id);
            return NoContent();
        }

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var pdfBytes = await _solicitudService.GenerateSolicitudPdfAsync(id);
            return File(pdfBytes, "application/pdf", $"Solicitud-{id}.pdf");
        }

        [HttpPost("{solicitudId}/respuesta")]
        public async Task<ActionResult<RespuestaSolicitudDTO>> SendRespuesta(int solicitudId, [FromBody] RespuestaSolicitudCrearDTO dto)
        {
            // ID de usuario fijo para el ejemplo.
            int enviadaPorId = 1;
            var newRespuesta = await _respuestaService.CreateRespuestaAsync(solicitudId, dto.ContenidoMensaje, enviadaPorId);
            return CreatedAtAction(nameof(GetRespuestas), new { solicitudId = newRespuesta.IdSolicitud }, newRespuesta);
        }

        [HttpGet("{solicitudId}/respuestas")]
        public async Task<ActionResult<IEnumerable<RespuestaSolicitudDTO>>> GetRespuestas(int solicitudId)
        {
            var respuestas = await _respuestaService.GetRespuestasBySolicitudAsync(solicitudId);
            return Ok(respuestas);
        }
    }
}
