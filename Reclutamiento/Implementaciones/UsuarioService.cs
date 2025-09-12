using System;
using Reclutamiento.DTOs.UsuarioDTOs;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAdminsAsync()
        {
            var admins = await _usuarioRepository.GetAllAsync();
            return admins.Where(u => u.Rol == Rol.Admin)
                         .Select(u => new UsuarioDTO { Id = u.Id, CorreoElectronico = u.CorreoElectronico, Rol = u.Rol.ToString() });
        }

        public async Task<UsuarioDTO> GetAdminByIdAsync(int id)
        {
            var user = await _usuarioRepository.GetByIdAsync(id);
            if (user == null || user.Rol != Rol.Admin) return null;
            return new UsuarioDTO { Id = user.Id, CorreoElectronico = user.CorreoElectronico, Rol = user.Rol.ToString() };
        }

        public async Task<UsuarioDTO> CreateAdminAsync(UsuarioCrearDTO dto)
        {
            var user = new Usuario
            {
                CorreoElectronico = dto.CorreoElectronico,
                HashContraseña = dto.Contrasena,
                Rol = Rol.Admin,
                FechaCreacion = DateTime.UtcNow
            };
            await _usuarioRepository.AddAsync(user);
            await _usuarioRepository.SaveAsync();
            return new UsuarioDTO { Id = user.Id, CorreoElectronico = user.CorreoElectronico, Rol = user.Rol.ToString() };
        }

        public async Task UpdateAdminAsync(int id, UsuarioActualizarDTO dto)
        {
            var user = await _usuarioRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("Admin no encontrado.");
            user.CorreoElectronico = dto.CorreoElectronico ?? user.CorreoElectronico;
            user.HashContraseña = dto.Contrasena ?? user.HashContraseña;
            _usuarioRepository.Update(user);
            await _usuarioRepository.SaveAsync();
        }

        public async Task DeleteAdminAsync(int id)
        {
            var user = await _usuarioRepository.GetByIdAsync(id);
            if (user != null)
            {
                _usuarioRepository.Delete(user);
                await _usuarioRepository.SaveAsync();
            }
        }
    }


