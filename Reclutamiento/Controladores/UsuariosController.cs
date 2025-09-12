using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reclutamiento.DTOs.UsuarioDTOs;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("admins")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAdmins()
        {
            var admins = await _usuarioService.GetAdminsAsync();
            return Ok(admins);
        }

        [HttpGet("admins/{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetAdmin(int id)
        {
            var admin = await _usuarioService.GetAdminByIdAsync(id);
            if (admin == null) return NotFound();
            return Ok(admin);
        }

        [HttpPost("admins")]
        public async Task<ActionResult<UsuarioDTO>> CreateAdmin([FromBody] UsuarioCrearDTO dto)
        {
            var newAdmin = await _usuarioService.CreateAdminAsync(dto);
            return CreatedAtAction(nameof(GetAdmin), new { id = newAdmin.Id }, newAdmin);
        }

        [HttpPut("admins/{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UsuarioActualizarDTO dto)
        {
            await _usuarioService.UpdateAdminAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("admins/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            await _usuarioService.DeleteAdminAsync(id);
            return NoContent();
        }
    }
}
