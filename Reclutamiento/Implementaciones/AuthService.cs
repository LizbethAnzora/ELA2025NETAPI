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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Reclutamiento.Implementaciones;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _config;

    public AuthService(IUsuarioRepository usuarioRepository, IConfiguration config)
    {
        _usuarioRepository = usuarioRepository;
        _config = config;
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
            Rol = user.Rol,
            Token = GenerarToken(user)

        };
    

    return login;
}

   private string GenerarToken(Usuario usuario)
{
    // Validar que el usuario y su rol no sean nulos
    if (usuario == null) throw new ArgumentNullException(nameof(usuario));
    if (usuario.Rol == null) throw new InvalidOperationException("El usuario no tiene rol asignado.");

    // Crear clave y credenciales para firmar el token
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    // Definir los claims del token (email y rol)
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.CorreoElectronico ?? ""),
        new Claim("rol", usuario.Rol.ToString())
    };

    // Crear token JWT con issuer, audience, claims y expiración
    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: creds
    );

    // Retornar token en formato string
    return new JwtSecurityTokenHandler().WriteToken(token);
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