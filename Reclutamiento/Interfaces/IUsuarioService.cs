using System;
using Reclutamiento.DTOs.UsuarioDTOs;

namespace Reclutamiento.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>> GetAdminsAsync();
        Task<UsuarioDTO> GetAdminByIdAsync(int id);
        Task<UsuarioDTO> CreateAdminAsync(UsuarioCrearDTO dto);
        Task UpdateAdminAsync(int id, UsuarioActualizarDTO dto);
        Task DeleteAdminAsync(int id);

}
