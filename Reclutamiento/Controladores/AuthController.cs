using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reclutamiento.DTOs;
using Reclutamiento.DTOs.AuthDTOs;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login/admin")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDTO dto)
        {
            var token = await _authService.AdminLogin(dto);
            if (token == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }
            return Ok(new TokenDTO { Token = token });
        }

        [HttpPost("login/github")]
        public async Task<IActionResult> GithubLogin([FromBody] GithubLoginDTO dto)
        {
            var token = await _authService.GithubLogin(dto.GithubId);
            if (token == null)
            {
                return StatusCode(500, "Error en el inicio de sesión con GitHub.");
            }
            return Ok(new TokenDTO { Token = token });
        }
    }

    public class GithubLoginDTO
    {
        public string GithubId { get; set; }
    }
}
