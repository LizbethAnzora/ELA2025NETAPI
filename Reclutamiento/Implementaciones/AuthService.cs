using System;
using Reclutamiento.DTOs;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;
using BCrypt.Net;
using ReclutamientoFrontend.WebApp.Models.Dtos;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Reclutamiento.Implementaciones;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<LoginResponseDto> AdminLogin(AdminLoginDTO dto)
{
    var user = await _usuarioRepository.GetByEmailAsync(dto.CorreoElectronico, dto.NombreCompleto);
    if (user == null || user.Rol != Rol.Admin || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, user.HashContraseña))
    {
        return null;
    }
        var login = new LoginResponseDto
        {
            NombreCompleto = user.NombreCompleto,
            CorreoElectronico = user.CorreoElectronico,
            Rol = user.Rol
        };
    

    return login;
}

private string GenerarToken(Usuario usuario)
if (usuario == null) throw new ArgumentNullException(nameof(usuario))
if (usuario.Rol == null) throw new InvalidOperationException("El usuario no tiene rol asignado")

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]:));
    var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
    new Claim(JwtRegisteredClaimNames.Sub, usuario.CorreoElectronico ?? ""),
    new Claim("rol", usuario.Rol.Nombre)
};

    var token = new JtwSecurityToken(
        issuer: _config["Jtw:Issuer"],
        audience: _config["Jtw:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        singinCredentials: creds
    );

return new JtwSecurityHandler().WriteToken



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