using System;
using Reclutamiento.DTOs.VacanteDTOs;

namespace Reclutamiento.Interfaces;

public interface IVacanteService
{
    Task<IEnumerable<VacantePublicaDTO>> GetPublicVacantesAsync();
        Task<IEnumerable<VacanteAdminDTO>> GetAllVacantesAsync();
        Task<VacanteAdminDTO> GetVacanteByIdAsync(int id);
        Task<VacanteAdminDTO> CreateVacanteAsync(VacanteCrearDTO dto, int creadorId);
        Task UpdateVacanteAsync(int id, VacanteCrearDTO dto);
        Task DisableVacanteAsync(int id);

}
