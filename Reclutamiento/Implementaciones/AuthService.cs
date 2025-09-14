using System;
using Reclutamiento.DTOs;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<string> AdminLogin(AdminLoginDTO dto)
    {
        var user = await _usuarioRepository.GetByEmailAsync(dto.CorreoElectronico);
        if (user == null || user.Rol != Rol.Admin || user.HashContraseña != dto.Contrasena)
        {
            return null;
        }

        return "FAKE_JWT_TOKEN";
    }

    public async Task<string> GithubLogin(string githubId)
    {
        long githubIdParsed = long.Parse(githubId);
        var user = await _usuarioRepository.GetByGithubIdAsync(githubIdParsed);

        if (user == null)
        {
            user = new Usuario
            {
                IdGithub = githubIdParsed,
                Rol = Rol.Solicitante,
                // Se establece un valor por defecto para el nombre
                NombreCompleto = "Usuario GitHub"
            };
            await _usuarioRepository.AddAsync(user);
            await _usuarioRepository.SaveAsync();
        }

        return "FAKE_GITHUB_JWT_TOKEN";
    }
}