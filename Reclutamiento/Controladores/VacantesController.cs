using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reclutamiento.DTOs.SolicitudDTOs;
using Reclutamiento.DTOs.VacanteDTOs;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacantesController : ControllerBase
    {
        private readonly IVacanteService _vacanteService;
        private readonly ISolicitudService _solicitudService;

        public VacantesController(IVacanteService vacanteService, ISolicitudService solicitudService)
        {
            _vacanteService = vacanteService;
            _solicitudService = solicitudService;
        }

        [HttpGet("publico")]
        public async Task<ActionResult<IEnumerable<VacantePublicaDTO>>> GetVacantesPublicas()
        {
            var vacantes = await _vacanteService.GetPublicVacantesAsync();
            return Ok(vacantes);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacanteAdminDTO>>> GetAllVacantes()
        {
            var vacantes = await _vacanteService.GetAllVacantesAsync();
            return Ok(vacantes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VacanteAdminDTO>> GetVacante(int id)
        {
            var vacante = await _vacanteService.GetVacanteByIdAsync(id);
            if (vacante == null) return NotFound();
            return Ok(vacante);
        }

        [HttpPost]
        public async Task<ActionResult<VacanteAdminDTO>> CreateVacante([FromBody] VacanteCrearDTO dto)
        {
            // ID de usuario fijo para el ejemplo.
            int creadorId = 1;
            var newVacante = await _vacanteService.CreateVacanteAsync(dto, creadorId);
            return CreatedAtAction(nameof(GetVacante), new { id = newVacante.Id }, newVacante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacante(int id, [FromBody] VacanteCrearDTO dto)
        {
            await _vacanteService.UpdateVacanteAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableVacante(int id)
        {
            await _vacanteService.DisableVacanteAsync(id);
            return NoContent();
        }

        [HttpGet("{vacanteId}/solicitudes")]
        public async Task<ActionResult<IEnumerable<SolicitudDTO>>> GetSolicitudesPorVacante(int vacanteId)
        {
            var solicitudes = await _solicitudService.GetSolicitudesByVacanteAsync(vacanteId);
            return Ok(solicitudes);
        }
    }
}
